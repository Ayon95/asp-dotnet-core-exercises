using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaLinksConfiguration.Options;

namespace SocialMediaLinksConfiguration.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialMediaLinksOptions _socialMediaLinksOptions;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IOptions<SocialMediaLinksOptions> options, IWebHostEnvironment webHostEnvironment)
        {
            _socialMediaLinksOptions = options.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.FacebookLink = _socialMediaLinksOptions.Facebook;
            ViewBag.TwitterLink = _socialMediaLinksOptions.Twitter;
            ViewBag.YoutubeLink = _socialMediaLinksOptions.Youtube;
            // Instagram link will not be available in Development environment 
            if (!_webHostEnvironment.IsDevelopment())
            {
                ViewBag.InstagramLink = _socialMediaLinksOptions.Instagram;
            }

            return View();
        }
    }
}
