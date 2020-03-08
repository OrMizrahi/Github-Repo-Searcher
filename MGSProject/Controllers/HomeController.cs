using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MGSProject.Models;

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
        public bool SaveRepos(int[] ids,string[] htmlUrls)
        {
            try
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    var currentHtmlUrl = htmlUrls[i];
                    var repo = _dbContext.Repositories.SingleOrDefault(x => x.HtmlUrl == currentHtmlUrl);
                    if (repo == null)
                    {
                        repo = new Repository
                        {
                            HtmlUrl = htmlUrls[i],
                            NumberOfAppearances = 0
                        };
                         _dbContext.Repositories.Add(repo);
                    }

                    repo.NumberOfAppearances++;
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

    }
}