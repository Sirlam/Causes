using Causes.UI.Web.Data;
using Causes.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.DAC
{
    public class SignatureDAC
    {

        public int CountSignatures(int cause_id)
        {
            using (var db = new AppDbContext())
            {
                IQueryable<TB_SIGNATURES> query = db.TB_SIGNATURES.Where(x => x.CAUSE_ID == cause_id);

                return query.Count();
            }
        }

        public bool ISigned(int user_id, int cause_id)
        {
            using (var db = new AppDbContext())
            {
                var query = db.TB_SIGNATURES.Where(x => x.CAUSE_ID == cause_id).Where(x => x.USER_ID == user_id).FirstOrDefault();

                if (query != null)
                    return true;

                return false;
            }
        }
    }
}