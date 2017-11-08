using Ericore.Entities;
using Ericore.Services;
using Ericore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ericore.Controllers
{
    public class HomeController : Controller
    {
        private ICommentData _commentData;
        private IGreeter _greeter;

        public HomeController(ICommentData commentData, IGreeter greeter)
        {
            _commentData = commentData;
            _greeter = greeter;
        }
        public ViewResult Index()
        {
            var model = new HomePageViewModel(); //_commentData.GetAllComments();
            model.Comments = _commentData.GetAllComments();
            model.Greeting = _greeter.GetGreeting();
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
