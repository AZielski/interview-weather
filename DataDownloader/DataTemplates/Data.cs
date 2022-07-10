namespace DataDownloader.DataTemplates;

public class Data
{
    public List<Request> request { get; set; }
    public List<CurrentCondition> current_condition { get; set; }
    public List<Weather> weather { get; set; }
}

public class Root
{
    public Data data { get; set; }
}