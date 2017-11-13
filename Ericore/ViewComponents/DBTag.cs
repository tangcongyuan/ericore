using Ericore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ericore.ViewComponents
{
    public class DBTag : ViewComponent
    {
        private IDBTag _dbTag;

        public DBTag(IDBTag dBTag)
        {
            _dbTag = dBTag;
        }

        public IViewComponentResult Invoke()
        {
            var model = _dbTag.GetDBTag();
            return View("Default", model);
        }
    }
}