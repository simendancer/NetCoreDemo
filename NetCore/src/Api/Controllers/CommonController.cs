using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        [HttpPost("getresult")]
        [EnableCors("AllowDomain")]
        public object GetResult()
        {
            var obj = new DataModel.ReportModel();
            List<Source> list = new List<Source>() {
                new Source(){ id=1, province="浙江", discount=  new List<int>() {1,2,3,4,5,6 } },
                new Source(){ id=2, province="北京", discount=  new List<int>() {5,1,3,7,5,6 }},
                new Source(){ id=3, province="哈尔滨", discount=  new List<int>() {6,2,3,4,1,6 }},
                new Source(){ id=4, province="广州", discount= new List<int>() {0,5,3,4,6,8 }},
                new Source(){ id=5, province="上海", discount=  new List<int>() {3,2,1,8,5,6 }}
            };
            obj.xAxis = list.Select(o => o.province).ToList();
            obj.yAxis = null;//list.Select(o => o.discount.ToString()).ToList();
            obj.series = list.Select(o => new DataModel.Data() { name = o.province, data = o.discount }).ToList();

            return Json(obj);
        }
    }

    public class Source
    {
        public int id { get; set; }

        public string province { get; set; }

        public List<int> discount { get; set; }
    }
}
