using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CHRP_EXAM;

class Program
{
    static void Main(string[] args)
    {
        InputValidator val = new InputValidator();
        Dictionaries dict = new Dictionaries();
        
        Console.WriteLine("Welcome to Dictionaries");
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Show all dictionaries");
            Console.WriteLine("2. Create a new dictionary");
            Console.WriteLine("3. Add word or translation");
            Console.WriteLine("4. Change word or word translation");
            Console.WriteLine("5. Delete word or translation");
            Console.WriteLine("6. Search word by translation");
            Console.WriteLine("7. Sort dictionaries");
            Console.WriteLine("8. Show history");
            Console.WriteLine("9. Show last 3 words in history");
            Console.WriteLine("10. Serialize");
            Console.WriteLine("11. Deserialize");
            Console.WriteLine("12. Exit");

            Console.Write("Your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    dict.ShowAllDict();
                    break;
                case 2:
                    dict.AddDict(val);
                    break;
                case 3:
                    dict.AddWord(val);
                    break;
                case 4:
                    dict.ChangeWordOrTranslation(val);
                    break;
                case 5:
                    dict.DeleteWordOrTrs();
                    break;
                case 6:
                    dict.SearchWordsFromTrs();
                    break;
                case 7:
                    dict.SortDictionaries();
                    break;
                case 8:
                    dict.PrintHistoryOfSearching();
                    break;
                case 9:
                    dict.PrintHistoryOfLastWordsSearching();
                    break;
                case 10:
                    dict.Serialize();
                    break;
                case 11:
                    dict.dictionaries = dict.Deserialize();
                    break;
                case 12:
                    return;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}