using Newtonsoft.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    public string?[] lines;
    //public string? FilePath { get; set; }
    public List<Character>? CharactersList { get; set; }

    public JsonFileHandler()
    {
        CharactersList = new List<Character>();
    }

    public List<Character> ReadCharacters(string filePath)
    {
        //// TODO: Implement JSON reading logic
        //throw new NotImplementedException();
        var json = File.ReadAllText(filePath);
        CharactersList = JsonConvert.DeserializeObject<List<Character>>(json);

        return CharactersList;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        //// TODO: Implement JSON writing logic
        //throw new NotImplementedException();

        string json = JsonConvert.SerializeObject(characters);
        File.WriteAllText(filePath,json);
    }
}