namespace CHRP_EXAM;

[Serializable]
public class Word
{ 
    public string Term { get; set; }
    public List<string> Translations { get; set; }

    public Word(){}
    public Word(string term, List<string> translations)
    {
        Term = term;
        Translations = translations;
    }
}
