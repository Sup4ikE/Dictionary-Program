namespace CHRP_EXAM;

[Serializable]
public class Dictionary
{
    public string Name { get; set; }
    public List<Word> Words { get; set; }
    
    public Dictionary(){}
    public Dictionary(string name, List<Word> words)
    {
        Name = name;
        Words = words;
    }
}