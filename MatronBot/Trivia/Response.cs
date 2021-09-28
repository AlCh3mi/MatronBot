using System.Text.Json.Serialization;

namespace MatronBot.Trivia
{
    public class Response
    {
        [JsonInclude] public int response_code;
        [JsonInclude] public Question[] results;
    }
}