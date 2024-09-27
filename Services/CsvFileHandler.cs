using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class CsvFileHandler : IFileHandler
{
    public string?[] lines;
    //public string? FilePath { get; set; }
    public List<Character>? CharactersList { get; set; }

    public CsvFileHandler()
    {
        CharactersList = new List<Character>();
    }

    public List<Character> ReadCharacters(string filePath)
    {
        //// TODO: Implement CSV reading logic
        //throw new NotImplementedException();
        lines = File.ReadAllLines(filePath);

        // Skip the header row and parse individual lines
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            String[] CharacterFields = line.Split(',');
            List<string> tempEquipmentList = new List<string>();
            var character = new Character();

            // Check if the name is quoted
            var quotedNameTest = CharacterFields[0];
            if (quotedNameTest.StartsWith('"'))
            {
                // Parse each line into individual character attributes
                // Remove quotes from the name if present
                // TODO put Equipment into an array and add to EquipmentList

                var characterLastName = CharacterFields[0].Trim('"');
                var characterFirstName = CharacterFields[1].Trim('"');

                //  Add character to List using one of many optional Syntax

                character.Name = ($"{characterFirstName} {characterLastName}");
                character.Class = CharacterFields[2];
                character.Level = Convert.ToInt32(CharacterFields[3]);
                character.HP = Convert.ToInt32(CharacterFields[4]);
                //character.EquipmentList = CharacterFields[5];   //.Replace("|", ", ");
                string[] Equipment = CharacterFields[5].Split('|');
                foreach (string equipment in Equipment)
                {
                    tempEquipmentList.Add(equipment);
                }
                character.Equipment = tempEquipmentList;
            }
            else
            {
                // Parse each line into individual character attributes
                // Remove quotes from the name if present
                // Replace | in Equipment list with , and space for better readability

                character.Name = CharacterFields[0];    //.Trim('"');
                character.Class = CharacterFields[1];
                character.Level = Convert.ToInt32(CharacterFields[2]);
                character.HP = Convert.ToInt32(CharacterFields[3]);
                //character.EquipmentList = CharacterFields[5]; 
                string[] Equipment = CharacterFields[4].Split('|');
                foreach (string equipment in Equipment)
                {
                    tempEquipmentList.Add(equipment);
                }
                character.Equipment = tempEquipmentList;
            }
            //  Add Character to the CharacterList
            CharactersList.Add(character);
        }
        return CharactersList;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        // Create list for CSV formated character objects
        List<string> CsvFormattedCharacters = new List<string>();
        //  Add header to CsvFormattedCharacters List in CSV format
        CsvFormattedCharacters.Add("Name,Class,Level,HP,Equipment");

        //  Convert characters objects into CSV format line by line and add to list
        foreach (var character in characters)
        {
            string chLevelString = Convert.ToString(character.Level);
            string chHitPointsString = Convert.ToString(character.HP);
            string chEquipmentListString = null;
            foreach (string equipment in character.Equipment)
            {
                if (chEquipmentListString == null)
                {
                    chEquipmentListString = equipment;
                }
                else
                {
                    chEquipmentListString = chEquipmentListString + "|" + equipment;
                }

            }
            string csvLine = ($"{character.Name},{character.Class},{chLevelString},{chHitPointsString},{chEquipmentListString}");

            CsvFormattedCharacters.Add(csvLine);
        }
        //  Overwrite CSV File with CsvFormattedCharacters List
        //File.Delete(FileName);
        File.WriteAllLines(filePath, CsvFormattedCharacters);
    }


}