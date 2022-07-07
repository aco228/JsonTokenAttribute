namespace Aco228.JsonToken;

public class JsonTokenAttribute : Attribute
{
    public string Path { get; set; }
    public bool Required { get; set; }

    public JsonTokenAttribute() { }
    public JsonTokenAttribute(string path)
    {
        Path = path;
    }
}