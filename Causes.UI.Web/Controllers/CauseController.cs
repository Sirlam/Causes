using Causes.UI.Web.Data;
using Causes.UI.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Causes.UI.Web.DAC;
using System.IO;
using Causes.UI.Web.Models;
using Causes.UI.Web.Security;

namespace Causes.UI.Web.Controllers
{
    public class CauseController : Controller
    {
        AppDbContext db;
        CausesDAC _cDAC;
        SignatureDAC _signatureDAC;

        public CauseController()
        {
            db = new AppDbContext();
            _cDAC = new CausesDAC();
            _signatureDAC = new SignatureDAC();
        }

        // GET: Cause
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyCauses(int id)
        {
            List<Cause> model = new List<Cause>();

            id = 1;
            var items = _cDAC.SelectCausesByUser(id);

            foreach(var i in items)
            {
                Cause cs = new Cause();
                cs.ID = i.ID;
                cs.TOPIC = i.TOPIC;
                cs.DESCRIPTION = i.DESCRIPTION;
                cs.IMG_URL = i.IMG_URL;
                cs.CREATED_BY = i.CREATED_BY;
                cs.CREATED_DATE = i.CREATED_DATE;
                cs.SignatureCount = _signatureDAC.CountSignatures(i.ID);
                model.Add(cs);
            }
            return View(model);
        }

        //GET:Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cause cause)
        {
            //var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(cause.IMG_FILE.FileName);
                string extension = Path.GetExtension(cause.IMG_FILE.FileName);
                filename = filename = DateTime.Now.ToString("yymmssfff")+extension;
                cause.IMG_URL = "~/images/uploads/" + filename;
                filename = Path.Combine(Server.MapPath("~/images/uploads/"), filename);
                cause.IMG_FILE.SaveAs(filename);

                TB_CAUSES tB_CAUSES = new TB_CAUSES
                {
                    TOPIC = cause.TOPIC,
                    DESCRIPTION = cause.DESCRIPTION,
                    IMG_URL = cause.IMG_URL,
                    CREATED_BY = 1,
                    CREATED_DATE = DateTime.Now,                
                };
                _cDAC.InsertCause(tB_CAUSES);
                ViewBag.Message = "Cause added successfully";
            }
            return View();
        }

        public ActionResult Views(string id)
        {
            var model = new Cause();
            int cause_id = Convert.ToInt32(id);

            model.ID = cause_id;
            return View(model);
        }
    }
}