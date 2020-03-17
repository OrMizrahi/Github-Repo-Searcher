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
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SaveRepos(string repoName)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers["User-Agent"] = Request.Headers["User-Agent"];
                var json = webClient.DownloadString($"https://api.github.com/search/repositories?q={repoName}");
                var result = JsonConvert.DeserializeObject<RootObject>(json);
                
                foreach (var repo in result.items)
                {
                   await SaveToDatabase(repo.html_url);
                }

                return Json(json);
            }
        }

        private static async Task SaveToDatabase(string htmlUrl)
        {
            using (var db = new MainDbContext())
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