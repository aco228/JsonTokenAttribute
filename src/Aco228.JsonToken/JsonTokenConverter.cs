using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aco228.JsonToken;

public class JsonTokenConverter
{
    public static T? Convert<T>(string input)
        where T : new()
    {
        if (string.IsNullOrEmpty(input))
            return default;

        var data = JsonConvert.DeserializeObject<JObject>(input);
        var model = (T) Activator.CreateInstance(typeof(T))!;

        foreach (var prop in model.GetType().GetProperties())
        {
            var classProp = prop.GetCustomAttributes(typeof(JsonTokenAttribute), true)?.FirstOrDefault() ?? null;
            if (classProp == null)
                continue;

            var attribute = classProp as JsonTokenAttribute;

            try
            {
                var value = data.SelectToken(attribute!.Path)?.Value<object?>();
                if (value == null && (attribute.Required && Nullable.GetUnderlyingType(prop.PropertyType) == null))
                {
                    // propValue is not nullable, error
                    throw new JsonTokenException(
                        $"For path '{attribute.Path}' we got null, but prop '{prop.Name}' is not nullable", null);
                }

                if (value != null)
                {
                    if (IsSimple(prop.PropertyType))
                        prop.SetValue(model, CastObject(value, prop.PropertyType));
                    else
                        prop.SetValue(model,
                            JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), prop.PropertyType));
                }
            }
            catch (Exception ex)
            {
                throw new JsonTokenException($"Exception for prop '{prop.Name}' with path '{attribute!.Path}'", ex);
            }
        }

        return model;
    }

    private static bool IsSimple(Type type)
        => TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));

    private static object CastObject(object input, Type to)
    {
        try
        {
            return TypeDescriptor.GetConverter(to).ConvertFrom(input.ToString());
        }
        catch (Exception ex)
        {
            try
            {
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(input), to);
            }
            catch (Exception jsonEx)
            {
                return null;
            }
        }
    }
}