using Causes.UI.Web.DAC;
using Causes.UI.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Causes.UI.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        CausesDAC _causesDAC;

        public HomeController()
        {
            _causesDAC = new CausesDAC();
        }

        public ActionResult Index()
        {
            List<Cause> model = new List<Cause>();
            var items = _causesDAC.SelectAllCauses();

            foreach(var item in items)
            {
                Cause cause = new Cause();
                cause.ID = item.ID;
                cause.IMG_URL = item.IMG_URL;
                cause.TOPIC = item.TOPIC;
                cause.DESCRIPTION = item.DESCRIPTION;
                cause.CREATED_BY = item.CREATED_BY;
                cause.CREATED_DATE = item.CREATED_DATE;
                model.Add(cause);
            }
            return View(model);
        }
    }
}