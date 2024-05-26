namespace CompetenceProfilingDomain.Domain;
public class Card
{
    public Card(string id,string outcomeId,string description, string longDescription)
    {
        Id = id;
        Description = description;
        OutcomeId = outcomeId;

        if (description.IndexOf('-') > 0)
        {
            Activitiet = description.Substring(0, description.IndexOf('-'));
            Architecktuur = description.Substring(description.IndexOf('-') + 1, 1);
            Niveaus = description.Substring(description.IndexOf('-') + 1,
                description.Length - description.IndexOf('-') - 1);
        }
        else
        {
            Activitiet = description.Substring(0, description.IndexOf(' '));
            Niveaus = description.Substring(description.IndexOf(' ')+1 ,
                description.Length - description.IndexOf(' ')-1);
            Architecktuur = "po";
        }
        LongDescription = longDescription
            .Split("</p> ")
            .FirstOrDefault()?
            .Split("</strong>")
            .LastOrDefault()?
            .Replace("<br>","")
            .Replace("<p>","")
            .Replace("<span>","")
            .Replace("</span>","")
            .Replace("</p>","")
            .Replace("&nbsp;","")
            .Replace("<p class=\"p1\">","")
            .Trim('•')
            .TrimStart();
    }
    public string Activitiet { get; } //Competence
    public string Architecktuur { get; }  //Architecture
    public string Niveaus { get; } // level
    public string Id { get; }
    public string OutcomeId { get; } // general id over al courses
    public string Description { get; }
    public string? LongDescription { get; }
    public int? Points { get; set; }  // per student

    public string DebugTxt
    {
        get
        {
            //return $"Id:{Id}-OutcomeId:{OutcomeId}";
            return "";
        }
    }
    
    // add course numbers of previous work for this outcome. 
    public List<int> CourseHistory { get; set; } = new();

}