using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Home.GetUpcommingGigs();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) ||
                    g.Venue.Contains(query));
            }

            var attendances = _unitOfWork.Home.GetGigsUserGoing(User.Identity.GetUserId());


            var viewModel = new HomeViewModel
            {
                UpcommingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcomming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}