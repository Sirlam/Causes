using Causes.UI.Web.Data;
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
        public Cause()
        {
            Signatures = new List< String > ();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "A topic is required", AllowEmptyStrings = false)]
        [DisplayName("Cause")]
        public string TOPIC { get; set; }

        [Required(ErrorMessage = "A description is required", AllowEmptyStrings = false)]
        [DisplayName("Description")]
        public string DESCRIPTION { get; set; }

        [DisplayName("Picture")]
        public string IMG_URL { get; set; }

        [Required(ErrorMessage = "Please select a file")]
        //[FileExtensions(ErrorMessage = "Please select an image", Extensions = "jpg,png,jpeg,gif,svg")]
        [DisplayName("Picture")]
        [ValidateFile]
        public HttpPostedFileBase IMG_FILE { get; set; }

        [DisplayName("By")]
        public int CREATED_BY { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CREATED_DATE { get; set; }

        public int SignatureCount { get; set; } // Count of signatures
        public string Creator { get; set; } //Name of the person that created the cause
        public IList<String> Signatures { get; set; } // List of the people that signed the cause
        public bool ISigned { get; set; } //Check if logged in user has signed
    }
}