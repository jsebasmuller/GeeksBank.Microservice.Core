namespace GeeksBank.Core.Api.SeedWork
{
    public class ConfigServerData
    {
        public string Data { get; set; }
        public Info Info { get; set; }
    }

    public class Info
    {
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
