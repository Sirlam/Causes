using Causes.UI.Web.DAC;
using Causes.UI.Web.Security;
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
        private CausesDAC _causesDAC;
        private SignatureDAC _signatureDAC;
        private CustomIdentity identity;

        public HomeController()
        {
            _signatureDAC = new SignatureDAC();
            _causesDAC = new CausesDAC();
        }

        public ActionResult Index()
        {
            //identity = ((CustomPrincipal)User).CustomIdentity;
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
                cause.SignatureCount = _signatureDAC.CountSignatures(item.ID);
                cause.Creator = _causesDAC.getCauseCreator(item.CREATED_BY);
                cause.ISigned = _signatureDAC.ISigned(1, item.ID);
                model.Add(cause);
            }
            return View(model);
        }


    }
}