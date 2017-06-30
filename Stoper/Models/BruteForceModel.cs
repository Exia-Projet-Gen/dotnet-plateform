using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIforJEE.Models
{
    public class BruteForceModel
    {
        public class StopMessageModel
        {
            public string key { set; get; }
            public string fileName { set; get; }
            public string decodedText { set; get; }
            public double matchPercent { set; get; }
            public string mailAddress { set; get; }
        }
    }
}