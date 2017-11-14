using Ericore.Entities;
using Ericore.Services;
using Ericore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ericore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ICommentData _commentData;
        private IDBTag _dbTag;
        private IConfiguration _configuration;

        public HomeController(ICommentData commentData, IDBTag dBTag, IConfiguration configuration)
        {
            _commentData = commentData;
            _dbTag = dBTag;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = new HomePageViewModel(); //_commentData.GetAllComments();
            model.Comments = _commentData.GetAllComments();
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
                _commentData.Commit();

                return RedirectToAction("Details", new { id = comment.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var comment = _commentData.Get(id);
            if (comment == null)
            {
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        [HttpPost]
        public IActionResult Edit(int id, CommentEditViewModel input)
        {
            var comment = _commentData.Get(id);
            if (comment != null && ModelState.IsValid)
            {
                comment.Text = input.Text;
                _commentData.Commit();
                return RedirectToAction("Details", new { id = comment.Id });
            }
            return View(comment);
        }
    }
}
