

using Newtonsoft.Json;

namespace MGSProject.Models
{
    public class Repository
    {
        public int RepositoryId { get; set; }
        public string HtmlUrl { get; set; }
        public int NumberOfAppearances { get; set; }
    }
}