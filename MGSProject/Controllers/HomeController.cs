using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MGSProject.Models;
using Newtonsoft.Json;

namespace MGSProject.Controllers
{
    public class HomeController : Controller
    {
        private string _json;
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendJsonToClient(string repoName)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers["User-Agent"] = Request.Headers["User-Agent"];
                _json = webClient.DownloadString($"https://api.github.com/search/repositories?q={repoName}");
                
                return Json(_json);
            }
        }

        [HttpGet]
        public async Task SaveToDatabase()
        {
            var result = JsonConvert.DeserializeObject<RootObject>(_json);

            using (var db = new MainDbContext())
            {
                foreach (var htmlUrl in result.items.Select(repo => repo.html_url))
                {
                    var repository = await db.Repositories.FirstOrDefaultAsync(el => el.HtmlUrl == htmlUrl);
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
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}