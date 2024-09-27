using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;

namespace W4_assignment_template;

class Program
{
    static IFileHandler fileHandler;
    static List<Character> characters;
    
    // Selected File to read and Write to
    private static string filePath = "Files/input.csv"; // Default to CSV file
    //private static string filePath = "Files/input.json";  // JSON File


    static void Main()
    {
        //  Method to select FileHandler according to filePath 
        FileHandlerSelector(filePath);

        // Method to read file and input into characters list
        characters = fileHandler.ReadCharacters(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    // TODO  add Write methods for CSV and JSON File Handlers
                    fileHandler.WriteCharacters(filePath, characters);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters()
    {
        // Display each character in the characters list
        foreach (var character in characters)
        {
            Console.WriteLine();
            Console.WriteLine($"Name: {character.Name}, Class: {character.Class}, Level: {character.Level}, HP: {character.HP}");
            Console.WriteLine($"Equipment: {string.Join(", ", character.Equipment)}");
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();
    }

    static void AddCharacter()
    {
        // Prompt for character details (name, class, level, hit points, equipment)
        // Add the new character to the characters list
        Console.WriteLine("");
        Console.WriteLine("Enter Your Character's Name and Press Enter: ");
        var nameInput = Console.ReadLine();
        Console.WriteLine("");
        Console.WriteLine("Enter Your Character's Class and Press Enter: ");
        var classTypeInput = Console.ReadLine();
        Console.WriteLine("");
        Console.WriteLine("Enter Your Character's Level and Press Enter: ");
        var levelInputValue = Console.ReadLine();
        int levelInput = Convert.ToInt32(levelInputValue);
        Console.WriteLine("");
        Console.WriteLine("Enter Your Character's Hit Power and Press Enter: ");
        var hitPointsInputValue = Console.ReadLine();
        int hitPointsInput = Convert.ToInt32(hitPointsInputValue);
        Console.WriteLine("");
        Console.WriteLine("Enter Your Character's First Piece of Equipment and Press Enter: ");
        var equipmentInput = Console.ReadLine();

        //  Use Do While loop to continue asking for additional equipment until complete
        // Add to equipment string with Pipe delineated format
        //TODO Add input validation to Do While Inputs  - No Null for adding equipment and Y/N Questions
        bool addEquipment = true;

        do
        {
            Console.WriteLine("");
            Console.WriteLine("Would You Like to Add Another Piece of Equipment? Enter Y for Yes or N for No. ");
            var addEquipmentResponse = Console.ReadLine();
            if (addEquipmentResponse == "Y" || addEquipmentResponse == "y")
            {
                Console.WriteLine("");
                Console.WriteLine("Enter Your Character's Next Piece of Equipment and Press Enter: ");
                var nextEquipment = Console.ReadLine();
                equipmentInput = (equipmentInput + "|" + nextEquipment);
            }
            else addEquipment = false;
        }
        while (addEquipment == true);

        // Display character to user
        Console.WriteLine("");
        Console.WriteLine($"Your Character Has Been Added to the Game!");
        Console.WriteLine($"Name = {nameInput}");
        Console.WriteLine($"Class = {classTypeInput}");
        Console.WriteLine($"Level = {levelInput}");
        Console.WriteLine($"HP = {hitPointsInput}");
        Console.WriteLine($"Equipment List = {equipmentInput}");
        Console.WriteLine("");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("");

        //  Convert inputed character attributes to Character Class Objects and Add to characters list
        var character = new Character();
        List<string> tempEquipmentList = new List<string>();
        character.Name = nameInput;
        character.Class = classTypeInput;
        character.Level = levelInput;
        character.HP = hitPointsInput;
        string[] Equipment = equipmentInput.Split('|');
        foreach (string equipment in Equipment)
        {
            tempEquipmentList.Add(equipment);
        }
        character.Equipment = tempEquipmentList;
        characters?.Add(character);

    }

    static void LevelUpCharacter()
    {
        // method to find character and increase class level by one 
        Console.WriteLine("");
        Console.WriteLine("Enter the Name of the Character You Want to Find");
        //Find Character using linq
        var findCharacter = Console.ReadLine();
        findCharacter = findCharacter.ToLower();
        var foundCharacter = characters?.FirstOrDefault(foundC => foundC.Name.ToLower() == findCharacter);

        //  Display Found Character  or If found Character is Null display could not find Message
        if (foundCharacter == null)
        {
            Console.WriteLine("");
            Console.WriteLine($"Could Not Find {findCharacter} in the Character List");
        }
        else
        {
            // Update FoundCharacter in CharacterList and Display Updated Character Level
            foreach (Character character in characters)
            {
                if (character.Name == foundCharacter.Name)
                {
                    var characterOldLevel = character.Level;
                    character.Level++;
                    Console.WriteLine();
                    Console.WriteLine($"Character {character.Name} Has Been Upgraded From a Level {characterOldLevel} {character.Class}, To a Level {character.Level} {character.Class}!");
                }
            }
        }
        Console.WriteLine("");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("");

    }

    public static void FileHandlerSelector(string filePath)
    {   //  Method to select which fileHandler to use based on file type of filePath
        string[] fileType = filePath.Split('.');
        if (fileType[1] == "csv")
        {
            fileHandler = new CsvFileHandler();
        }
        else if (fileType[1] == "json")
        {
            fileHandler = new JsonFileHandler();
        }
        else
        {
            //  if filetype is not supported display error message and end program
            Console.WriteLine($"PROGRAM ERROR: SELECTED FILE TYPE NOT SUPPORTED BY EXISTING PROGRAM");
            Console.WriteLine("Press ENTER to Exit Program");
            var repeatError = Console.ReadLine();
            System.Environment.Exit(0);
        }
        
    }

}