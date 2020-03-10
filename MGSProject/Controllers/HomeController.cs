using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        private static readonly HttpClient Client = new HttpClient();


        public ActionResult Index()
        {
            return View();
        }


        public async Task<string> SaveRepos(string repoName)
        {
            Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MGSProject", "1"));

            var response = await Client.GetAsync($"https://api.github.com/search/repositories?q={repoName}");
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsAsync<RootObject>();
            var json = await response.Content.ReadAsStringAsync();

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

            return json;
        }
    }
}