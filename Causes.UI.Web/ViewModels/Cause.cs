using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.ViewModels
{
    public class Cause
    {
        public int ID { get; set; }

        [DisplayName("Cause")]
        public string TOPIC { get; set; }

        [DisplayName("Description")]
        public string DESCRIPTION { get; set; }

        public string IMG_URL { get; set; }

        [DisplayName("Picture")]
        public HttpPostedFileBase IMG_FILE { get; set; }

        [DisplayName("By")]
        public int CREATED_BY { get; set; }

        [DisplayName("Date")]
        public DateTime? CREATED_DATE { get; set; }
    }
}