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
        private readonly MainDbContext _dbContext = new MainDbContext();
        
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveRepos(string repoName)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers["User-Agent"] = Request.Headers["User-Agent"];
                var json = webClient.DownloadString($"https://api.github.com/search/repositories?q={repoName}");
                var result = JsonConvert.DeserializeObject<RootObject>(json);

                foreach (var repo in result.items)
                {
                    var currentHtmlUrl = repo.html_url;
                    var repository = _dbContext.Repositories.SingleOrDefault(el => el.HtmlUrl == currentHtmlUrl);
                    if (repository == null)
                    {
                        repository = new Repository
                        {
                            HtmlUrl = currentHtmlUrl,
                            NumberOfAppearances = 0
                        };
                        _dbContext.Repositories.Add(repository);
                    }

                    repository.NumberOfAppearances++;
                }

                _dbContext.SaveChanges();

                return Json(json);
            }
        }
    }
}