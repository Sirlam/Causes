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

        public List<String> getSignatures(int cause_id)
        {
            using (var db = new AppDbContext())
            {
                List<String> theirnames = new List<String>();

                var query = db.TB_SIGNATURES.Where(x => x.CAUSE_ID == cause_id).ToList();

                if (query != null)
                {
                    foreach(var item in query)
                    {
                        string name = db.TB_USERS.Where(x => x.ID == item.USER_ID).Select(q => q.DISPLAY_NAME).FirstOrDefault();
                        theirnames.Add(name);
                    }
                    return theirnames;
                }

            }
            return null;
        }

        public bool SignCause(int cause_id, int user_id)
        {
            using (var db = new AppDbContext())
            {
                TB_SIGNATURES tB_SIGNATURES = new TB_SIGNATURES
                {
                    CAUSE_ID = cause_id,
                    USER_ID = user_id,
                    SIGNED_DATE = DateTime.Now,
                };

                db.Set<TB_SIGNATURES>().Add(tB_SIGNATURES);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}