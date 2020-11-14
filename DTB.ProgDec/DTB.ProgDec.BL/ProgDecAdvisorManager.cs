using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.PL;

namespace DTB.ProgDec.BL
{
    public static class ProgDecAdvisorManager
    {
        public static void Delete(int progdecid, int advisorid)
        {
            using (ProgDecEntities dc = new ProgDecEntities())
            {
                tblProgDecAdvisor pda = dc.tblProgDecAdvisors.FirstOrDefault(p => p.ProgDecId == progdecid
                                        && p.AdvisorId == advisorid);
                if(pda != null)
                {
                    dc.tblProgDecAdvisors.Remove(pda);
                    dc.SaveChanges();
                }
            }
        }

        public static void Add(int progdecid, int advisorid)
        {
            using (ProgDecEntities dc = new ProgDecEntities())
            {
                tblProgDecAdvisor pda = new tblProgDecAdvisor();
                pda.Id = dc.tblProgDecAdvisors.Any() ? dc.tblProgDecAdvisors.Max(p => p.Id) + 1 : 1;
                pda.ProgDecId = progdecid;
                pda.AdvisorId = advisorid;

                dc.tblProgDecAdvisors.Add(pda);
                dc.SaveChanges();
            }
        }
    }
}
