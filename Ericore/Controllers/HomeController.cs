using Ericore.Entities;
using Ericore.Services;
using Ericore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ericore.Controllers
{
    public class HomeController : Controller
    {
        private ICommentData _commentData;
        private IGreeter _greeter;
        private IConfiguration _configuration;

        public HomeController(ICommentData commentData, IGreeter greeter, IConfiguration configuration)
        {
            _commentData = commentData;
            _greeter = greeter;
            _configuration = configuration;
        }
        public ViewResult Index()
        {
            var model = new HomePageViewModel(); //_commentData.GetAllComments();
            model.Comments = _commentData.GetAllComments();
            model.ConnectionString = _configuration["database:connectionString"];
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _commentData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Text = model.Text
                };

                _commentData.Add(comment);

                return RedirectToAction("Details", new { id = comment.Id });
            }
            return View();
        }
    }
}
