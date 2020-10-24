using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DTB.Bands.UI.Models
{
    public class BandModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        [DisplayName("Year Founded")]
        public int YearFounded { get; set; }
    }
}