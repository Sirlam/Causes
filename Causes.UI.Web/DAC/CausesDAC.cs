﻿using Causes.UI.Web.Data;
using Causes.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Causes.UI.Web.DAC
{
    public class CausesDAC
    {
        public TB_CAUSES InsertCause (TB_CAUSES tB_CAUSES)
        {
            using(var db = new AppDbContext())
            {
                db.Set<TB_CAUSES>().Add(tB_CAUSES);
                db.SaveChanges();

                return tB_CAUSES;
            }
        }

        public void UpdateCause(TB_CAUSES tB_CAUSES)
        {
            using(var db = new AppDbContext())
            {
                var entry = db.Entry<TB_CAUSES>(tB_CAUSES);

                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public TB_CAUSES SelectCauseById(int id)
        {
            using(var db = new AppDbContext())
            {
                return db.Set<TB_CAUSES>().Find(id);
            }
        }

        public IList<TB_CAUSES> SelectCausesByUser(int user_id)
        {
            using(var db = new AppDbContext())
            {
                var query = db.TB_CAUSES.Where(x => x.CREATED_BY == user_id).ToList();

                return query;
            }
        }

        public List<TB_CAUSES> SelectAllCauses()
        {
            using(var db = new AppDbContext())
            {
                var query = db.TB_CAUSES.Select(x => x);

                query.OrderByDescending(q => q.CREATED_DATE);

                return query.ToList();
            }
        }

        public void DeleteCause(TB_CAUSES tB_CAUSES)
        {
            using(var db = new AppDbContext())
            {
                var signatures = db.TB_SIGNATURES.Where(x => x.CAUSE_ID == tB_CAUSES.ID);
                //First Remove existing signature
                if(signatures != null)
                {
                    db.TB_SIGNATURES.RemoveRange(signatures);
                }
                var cause = db.TB_CAUSES.Find(tB_CAUSES.ID);
                if(cause != null)
                {
                    db.TB_CAUSES.Remove(cause);
                }
            }
        }

        public int CountCauses()
        {
            using (var db = new AppDbContext())
            {
                IQueryable<TB_CAUSES> query = db.Set<TB_CAUSES>();

                return query.Count();
            }
        }
    }
}