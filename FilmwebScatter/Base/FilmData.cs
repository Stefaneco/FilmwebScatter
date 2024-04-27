namespace FilmwebScatter.Base;
internal struct FilmData
{
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string PremiereYear { get; set; }
    public string RateDate { get; set; }
    public string Rate { get; set; }
    public string AvgRate { get; set; }
    public string RateCount { get; set; }
    public string CriticsRate { get; set; }
    public string CriticsRateCount { get; set; }
    public string Director { get; set; }
    public string Duration { get; set; }
    public string Genre { get; set; }
    public string Country { get; set; }
    public string BoxOffice { get; set; }
    public string BoxOfficeUsa { get; set; }
    public string BoxOfficeWorld { get; set; }
    public string Budget { get; set; }
    public string IsFavorite { get; set; }
    public List<string> Actors { get; set; }

    public string ToCsv()
    {
        return $"{Title};{OriginalTitle};{PremiereYear};{RateDate};{Rate};{AvgRate};{RateCount};{CriticsRate};{CriticsRateCount};{Director};{Duration};{Genre};{Country};{BoxOffice};{BoxOfficeUsa};{BoxOfficeWorld};{Budget};{IsFavorite}";
    }

    public static string GetCsvHeader()
    {
        return "Title;OriginalTitle;PremiereYear;RateDate;Rate;AvgRate;RateCount;CriticsRate;CriticsRateCount;Director;Duration;Genre;Country;BoxOffice;BoxOfficeUsa;BoxOfficeWorld;Budget;IsFavorite";
    }

    public List<string> ActorsToCsv()
    {
        List<string> actorsCsv = new();
        foreach (var actor in Actors)
        {
            actorsCsv.Add($"{Title};{actor}");
        }
        return actorsCsv;
    }

    public static string GetActorsCsvHeader()
    {
        return "Title;Actor";
    }
}
