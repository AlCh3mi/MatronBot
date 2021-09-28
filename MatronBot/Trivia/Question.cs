using System.Text.Json.Serialization;

namespace MatronBot.Trivia
{
    public class Question
    {
        [JsonInclude] public string category;
        [JsonInclude] public string type;
        [JsonInclude] public string difficulty;
        [JsonInclude] public string question;
        [JsonInclude] public string correct_answer;
        [JsonInclude] public string[] incorrect_answers;
    }
}