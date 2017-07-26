using System;
using System.Collections.Generic;

namespace DataModel
{
    public class ReportModel
    {
        public List<string> xAxis { get; set; }

        public List<string> yAxis { get; set; }

        public List<Data> series { get; set; }
    }

    public class Data
    {
        public string name { get; set; }

        public List<int> data { get; set; }
    }
}
