using System.Xml.Serialization;
using NLog;

namespace CHRP_EXAM;

public class Dictionaries
{
    Logger logger = LogManager.GetCurrentClassLogger();
    
    public List<Dictionary> dictionaries = new List<Dictionary>
    {
        new Dictionary("English-Ukrainian", new List<Word>()),
        new Dictionary("Ukrainian-English", new List<Word>()),
        new Dictionary("Poland-Ukrainian", new List<Word>()),
        new Dictionary("Ukrainian-Poland", new List<Word>())
    };
    
    public void ShowAllDict()
    {
        Console.WriteLine("Dictionaries:");
        foreach (Dictionary dictionary in dictionaries)
        {
            Console.WriteLine(dictionary.Name);
            for (int i = 0;i < dictionary.Words.Count(); i++)
            {
                Console.WriteLine(dictionary.Words[i].Term);
                if (dictionary.Words[i].Translations.Count > 0)
                {
                    foreach (var translation in dictionary.Words[i].Translations)
                    {
                        Console.WriteLine($"Translation: {translation}");
                    }
                }
                else
                {
                    Console.WriteLine("No translations available.");
                }
            }
        }
        Console.WriteLine();
    }
    public void AddDict(InputValidator val)
    {
        Console.WriteLine("Write name of dictionary:");
        string name = Console.ReadLine();
        if (val.IsValidDict(name))
        {
            dictionaries.Add(new Dictionary(name, new List<Word>()));
            Console.WriteLine($"Added dictionary: {name}");
        }
        else
        {
            Console.WriteLine("Invalid dictionary name");
        }
    }
    public void AddWord(InputValidator val)
    {
        Console.WriteLine("1.Add word\n2.Add translation");
        int c = int.Parse(Console.ReadLine());

        Console.WriteLine("Write number of dictionary: ");
        int number = int.Parse(Console.ReadLine());
        
        if (c == 1)
        {
            Console.WriteLine("Write word: ");
            string word = Console.ReadLine();
            if (val.IsValidWord(word))
            {
                Console.WriteLine("1.one translation\n2.more than one translations");
                int a = int.Parse(Console.ReadLine());

                if(a == 1)
                {
                    Console.WriteLine("Write word translation: ");
                    string translation = Console.ReadLine();
                    if (val.IsValidTranslation(translation))
                    {
                        for (int i = 0; i < dictionaries.Count; i++)
                        {
                            if (number == i)
                            {
                                dictionaries[i].Words.Add(new Word(word, new List<string>(){translation}));
                            }
                        }
                        Console.WriteLine($"Added word: {word}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid translation");
                    }
                }
                else if(a == 2)
                {
                    List<string> translations = new List<string>();
                    Console.WriteLine("Write number of translations: ");
                    int num = int.Parse(Console.ReadLine());
                    for (int i = 0; i < num; i++)
                    {
                        Console.WriteLine("Write word translation: ");
                        string translation = Console.ReadLine();
                        if (val.IsValidTranslation(translation))
                        {
                            translations.Add(translation);
                        }
                        else
                        {
                            Console.WriteLine("Invalid translation");
                        }
                    }
                    for (int i = 0; i < dictionaries.Count; i++)
                    {
                        if (number == i)
                        {
                            dictionaries[i].Words.Add(new Word(word, translations));
                        }
                    }
                    Console.WriteLine($"Added word: {word}");
                }
        }
        else
        {
            Console.WriteLine("Invalid word");
        }
        }
        else if (c == 2)
        {
            Console.WriteLine("Write word to add translation: ");
            string wordd = Console.ReadLine();
            Console.WriteLine("Write translations: ");
            string translation = Console.ReadLine();
            
            var selectedDictionary = dictionaries[number];
            
            foreach (var word in selectedDictionary.Words)
            {
                if (word.Term == wordd)
                {
                    word.Translations.Add(translation);
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid choice");
        }
    }
    public void ChangeWordOrTranslation(InputValidator val)
    { 
        Console.WriteLine("Write number of dictionary: ");
        if (!int.TryParse(Console.ReadLine(), out int number) || number < 0 || number >= dictionaries.Count)
        {
            Console.WriteLine("Invalid dictionary number.");
            return;
        }
        Console.WriteLine("1. Change word\n2. Change translation");
        if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        var selectedDictionary = dictionaries[number];

        if (choice == 1)
        {
            Console.WriteLine("Write word to change: ");
            string word = Console.ReadLine();

            Console.WriteLine("Write new word: ");
            string newWord = Console.ReadLine();

            if (!val.IsValidWord(newWord))
            {
                Console.WriteLine("Invalid word format.");
                return;
            }

            foreach (var wor in selectedDictionary.Words)
            {
                if (wor.Term == word)
                {
                    wor.Term = newWord;
                    Console.WriteLine($"Word '{word}' changed to '{newWord}'.");
                    return;
                }
            }
            Console.WriteLine($"Word '{word}' not found in the dictionary.");
        }
        else if (choice == 2)
        {
            Console.WriteLine("Write word to change translation: ");
            string word = Console.ReadLine();

            Console.WriteLine("Write translation to change: ");
            string translation = Console.ReadLine();

            Console.WriteLine("Write new translation: ");
            string newTranslation = Console.ReadLine();

            if (!val.IsValidTranslation(newTranslation))
            {
                Console.WriteLine("Invalid translation format.");
                return;
            }

            foreach (var wor in selectedDictionary.Words)
            {
                if (wor.Term == word)
                {
                    if (wor.Translations.Contains(translation))
                    {
                        int index = wor.Translations.IndexOf(translation);
                        wor.Translations[index] = newTranslation;

                        Console.WriteLine($"Translation '{translation}' successfully changed to '{newTranslation}' for word '{word}'.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Translation '{translation}' not found for word '{word}'.");
                        return;
                    }
                }
            }
            Console.WriteLine($"Word '{word}' not found in the dictionary.");
        } 
    }
    public void DeleteWordOrTrs()
    {
        Console.WriteLine("Enter dictionary to delete word or translation: ");
        int input = int.Parse(Console.ReadLine());
        Console.WriteLine("1. Delete word\n2. Delete translation: ");
        int choice = int.Parse(Console.ReadLine());
        
        var selectedDictionary = dictionaries[input];
        if (choice == 1)
        {
            Console.WriteLine("Enter word to delete: ");
            string word = Console.ReadLine();
            
            var wordToRemove = selectedDictionary.Words.FirstOrDefault(wor => wor.Term == word);
            if (wordToRemove != null)
            {
                selectedDictionary.Words.Remove(wordToRemove);
                Console.WriteLine($"Word '{word}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Word '{word}' not found.");
            }
        }
        else if (choice == 2)
        {
            Console.WriteLine("Write word to delete translation: ");
            string word = Console.ReadLine();

            Console.WriteLine("Write translation to delete: ");
            string translation = Console.ReadLine();
            
            foreach (var wor in selectedDictionary.Words)
            {
                if (wor.Term == word)
                {
                    if (wor.Translations.Contains(translation) && wor.Translations.Count > 1)
                    { 
                        wor.Translations.Remove(translation);
                        Console.WriteLine($"Translation '{translation}' successfully deleted");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Translation '{translation}' not found or translations <= 1");
                        return;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
    public void SearchWordsFromTrs()
    {
        Console.WriteLine("Enter dictionary to search: ");
        int input = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter word to show translation: ");
        string word = Console.ReadLine();
        
        var selectedDictionary = dictionaries[input];

        foreach (var wor in selectedDictionary.Words)
        {
            if (wor.Term == word)
            {
                foreach (var tra in wor.Translations)
                {
                    Console.WriteLine($"Translation: {tra}");
                }
            }
        }
        logger.Log(LogLevel.Info, $"Searched word: {word}");
        
        Console.WriteLine("Do you want export this word to result file? (y/n)");
        string answer = Console.ReadLine();
        if (answer.ToLower() == "y")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Word>));

            using (FileStream fs = new FileStream("result.xml", FileMode.Create))
            {
                serializer.Serialize(fs, selectedDictionary.Words);
            }
            Console.WriteLine("Export successful");
        }
    }
    public void SortDictionaries()
    {
        Console.WriteLine("1.Sort by name\n2.Sort by count of words\n3.Sort by count of translations: ");
        int input = int.Parse(Console.ReadLine());

        if (input == 1)
        {
            var sortByName = dictionaries.OrderBy(name => name.Name);
            foreach (var dict in sortByName)
            {
               Console.WriteLine($"Name: {dict.Name}"); 
            }
        }
        else if (input == 2)
        {
            var sortByCofWords = dictionaries.OrderBy(d => d.Words.Count());
            
            foreach (Dictionary dictionary in sortByCofWords)
            {
                Console.WriteLine(dictionary.Name); 
                for (int i = 0;i < dictionary.Words.Count(); i++)
                {
                    Console.WriteLine(dictionary.Words[i].Term);
                    if (dictionary.Words[i].Translations.Count > 0)
                    {
                        foreach (var translation in dictionary.Words[i].Translations)
                        {
                            Console.WriteLine($"Translation: {translation}");
                        }
                    }
                    else
                    { 
                        Console.WriteLine("No translations available.");
                    }
                }
            }
        }
        else if(input == 3)
        {
            Console.WriteLine("Enter word to search: ");
            string word = Console.ReadLine();
            var sortedByTranslationCount = dictionaries.OrderBy(d => d.Words.FirstOrDefault(w => w.Term == word)?.Translations.Count ?? 0).ToList();
        }
        else
        {
            Console.WriteLine("Invalid input");
        }
    }
    public void PrintHistoryOfSearching()
    {
        string logFilePath =@"logs/2024-11-18.log";
        string logContent = File.ReadAllText(logFilePath);
        Console.WriteLine(logContent);
    }
    public void PrintHistoryOfLastWordsSearching()
    { 
        string logFilePath =@"logs/2024-11-18.log";
        
        string[] logLines = File.ReadAllLines(logFilePath);
        
        int messagesToShow = Math.Min(3, logLines.Length);

        var lastMessages = logLines.Skip(Math.Max(0, logLines.Length - messagesToShow)).ToArray();

        foreach (string message in lastMessages)
        {
            Console.WriteLine(message);
        }
    }
    public void Serialize()
    {
        var serializer = new XmlSerializer(typeof(List<Dictionary>));

        using (FileStream fs = new FileStream("dictionaries.xml", FileMode.Create))
        {
            serializer.Serialize(fs, dictionaries);
            Console.WriteLine("Dictionaries serialized.");
        }
    }
    public List<Dictionary> Deserialize()
    {
        var serializer = new XmlSerializer(typeof(List<Dictionary>));

        using (FileStream fs = new FileStream("dictionaries.xml", FileMode.Open))
        {
            Console.WriteLine("Dictionaries loaded.");
            return serializer.Deserialize(fs) as List<Dictionary>;
        }
    }
}

