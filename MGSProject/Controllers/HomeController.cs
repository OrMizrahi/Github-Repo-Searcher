using System.Linq;
using System.Net;
using System.Web.Mvc;
using MGSProject.Models;
using Newtonsoft.Json;

namespace MGSProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendJsonToClient(string repoName)
        {
            var json = GetJsonFromUrl($"https://api.github.com/search/repositories?q={repoName}");
            return Json(json);
        }

        [HttpGet]
        public void SaveToDatabase(string repoName)
        {
                var json = GetJsonFromUrl($"https://api.github.com/search/repositories?q={repoName}");
                var result = JsonConvert.DeserializeObject<RootObject>(json);
                using (var db = new MainDbContext())
                {
                    foreach (var htmlUrl in result.items.Select(repo => repo.html_url))
                    {
                        var repository = db.Repositories.FirstOrDefault(el => el.HtmlUrl == htmlUrl);
                        if (repository == null)
                        {
                            repository = new Repository
                            {
                                HtmlUrl = htmlUrl,
                                NumberOfAppearances = 0
                            };
                            db.Repositories.Add(repository);
                        }

                        repository.NumberOfAppearances++;
                        db.SaveChanges();
                    }
                }
        }

        private string GetJsonFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers["User-Agent"] = Request.Headers["User-Agent"];
                var json = webClient.DownloadString(url);
                return json;
            }
        }
    }
}