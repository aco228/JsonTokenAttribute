namespace Aco228.JsonToken;

public class JsonTokenException : Exception
{
    public JsonTokenException(string message, Exception? ex)
        :base(message, ex)
    {
    }
}