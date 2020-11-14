using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.PL;

namespace DTB.ProgDec.BL
{
    public class AdvisorManager
    {
        public static List<Advisor> Load()
        {
            using (ProgDecEntities dc = new ProgDecEntities())
            {
                List<Advisor> advisors = new List<Advisor>();

                
                dc.tblAdvisors.ToList().ForEach(r => advisors.Add(new Advisor { Id = r.Id, Name = r.Name }));
                return advisors;


            }
        }
        public static List<Advisor> Load(int progDecId)
        {
            try
            {
                using(ProgDecEntities dc = new ProgDecEntities())
                {
                    List<Advisor> advisors = new List<Advisor>();

                    var results = (from a in dc.tblAdvisors
                                    join pda in dc.tblProgDecAdvisors on a.Id equals pda.AdvisorId
                                    where pda.ProgDecId == progDecId
                                    select new
                                    {
                                        a.Id,
                                        a.Name
                                    }).ToList();

                    results.ForEach(r => advisors.Add(new Advisor { Id = r.Id, Name = r.Name }));

                    return advisors;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

