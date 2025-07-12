using System.Text.RegularExpressions;

namespace CHRP_EXAM;

public class InputValidator
{
    public bool IsValidWord(string input) => Regex.IsMatch(input, @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ]+$");
    public bool IsValidTranslation(string input) => Regex.IsMatch(input, @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ]+$");
    public bool IsValidDict(string input) => Regex.IsMatch(input, @"^[a-zA-Z]+-[a-zA-Zа-яА-ЯіІїЇєЄґҐ]+$");
}