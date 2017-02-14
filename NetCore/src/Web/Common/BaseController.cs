using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Tools.Usual;

namespace Web.Common
{
    public class BaceController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            base.OnActionExecuting(context);
        }

        #region Request方法
        /// <summary>
        /// Request一个数字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int RequestInt(string name)
        {
            string v = Request.Query[name];
            if (v == null)
            {
                return 0;
            }
            else
            {
                return v.ToInt32();
            }
        }
        /// <summary>
        /// Request一个Byte
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte RequestByte(string name)
        {
            string v = Request.Query[name];
            if (v == null)
            {
                return 0;
            }
            else
            {
                return v.ToByte();
            }
        }

        /// <summary>
        /// Request一个Decimal
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public decimal RequestDecimal(string name)
        {
            string v = RequestString(name);
            decimal result = 0;
            if (decimal.TryParse(v, out result))
            {
                return result;
            }
            return 0;
        }
        /// <summary>
        /// Request一个布尔值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RequestBool(string name)
        {
            string v = Request.Query[name];
            if (v == null)
            {
                return false;
            }
            else
            {
                try
                {
                    return bool.Parse(v);
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 请求一个字符串，返回null时返回空字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string RequestString(string name)
        {
            string v = Request.Query[name];
            if (v == null)
            {
                v = string.Empty;
            }
            return v.Trim();
        }

        /// <summary>
        /// 请求字符串类型
        /// </summary>
        /// <param name="requestStr">请求的Request值</param>
        /// <returns>返回字符串，null则返回""</returns>
        public string ConvertString(string requestStr)
        {
            return requestStr.ToString2();
        }

        /// <summary>
        ///   数字类型值
        /// </summary>
        /// <param name="requestStr">获取request值</param>
        /// <returns>返回数字类型,空则返回0</returns>
        public int ConvertInt(string requestStr)
        {
            return requestStr.ToInt32();
        }

        /// <summary>
        /// 获得邮费
        /// </summary>
        /// <param name="Postage">邮费</param>
        /// <returns>（包邮或具体邮费）</returns>
        public string GetPostage(string Postage)
        {
            return Postage == "0" ? "包邮" : Postage;
        }

        /// <summary>
        ///   浮点类型值
        /// </summary>
        /// <param name="requestStr">获取request值</param>
        /// <returns>返回数字类型,空则返回0</returns>
        public float ConvertFloat(string requestStr)
        {
            return requestStr.ToFloat();
        }

        /// <summary>
        ///   双精度类型值
        /// </summary>
        /// <param name="requestStr">获取request值</param>
        /// <returns>返回数字类型,空则返回0</returns>
        public decimal ConvertDecimal(string requestStr)
        {
            return requestStr.ToDecimal();
        }

        public Int32 ConvertObjectToInt(object requestStr)
        {
            try
            {
                return Convert.ToInt32(requestStr);
            }
            catch
            {
                return 0;
            }
        }

        public string ConvertObjectToStr(object requestStr)
        {
            try
            {
                return Convert.ToString(requestStr);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 转换成布尔型
        /// </summary>
        /// <param name="requestStr"></param>
        /// <returns></returns>
        public bool ConvertBoolean(string requestStr)
        {
            try
            {
                if (string.IsNullOrEmpty(requestStr))
                {
                    return false;
                }

                requestStr = requestStr.ToLower();
                if (requestStr == "1" || requestStr == "true")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 分页方法

        /// <summary>
        /// 获取分页HTML
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="records">总记录数</param>
        /// <param name="displayCount">控件显示页数</param>
        /// <param name="urlFormat">分页路由，如 Home/Index?page={0}</param>
        /// <param name="showJump">是否显示跳转按钮</param>
        /// <returns></returns>
        public string GetPageHtml(int currentPage, int pageSize, long records, int displayCount, string urlFormat, bool showJump = false)
        {
            int allPage = (int)Math.Ceiling((float)records / pageSize); //总页数
            currentPage = currentPage > allPage ? allPage : currentPage; //限制最大页数
            int allPagePart = (int)Math.Ceiling((float)allPage / displayCount); //总页段
            int currentPart = currentPage % displayCount == 0 ? currentPage / displayCount : (currentPage / displayCount) + 1; //当前页段
            int prePage = (currentPart - 1) * displayCount; //当前页段的上一页
            int nextPage = currentPart * displayCount + 1; //当前页段的下一页

            StringBuilder sb = new StringBuilder();
            sb.Append("<nav aria-label=\"Page navigation\">");
            sb.Append("<ul class=\"pagination\">");
            sb.AppendFormat("<li {1}><a href=\"{0}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;&laquo;</span></a></li>", currentPage > 1 ? GetPage(urlFormat, 1) : "javascript:void(0)", currentPage > 1 ? "" : "class=\"disabled\"");
            sb.AppendFormat("<li {1}><a href=\"{0}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>", currentPage > 1 ? GetPage(urlFormat, currentPage - 1) : "javascript:void(0)", currentPage > 1 ? "" : "class=\"disabled\"");
            if (records > displayCount)
            {
                for (int i = prePage + 1; i <= prePage + displayCount; i++)
                {
                    sb.AppendFormat("<li {2}><a href=\"{0}\">{1}</a></li>", GetPage(urlFormat, i), i, i == currentPage ? "class=\"active\"" : "");
                }
            }
            else
            {
                for (int i = 1; i <= allPage; i++)
                {
                    sb.AppendFormat("<li {2}><a href=\"{0}\">{1}</a></li>", GetPage(urlFormat, i), i, i == currentPage ? "class=\"active\"" : "");
                }
            }
            sb.AppendFormat("<li {1}><a href=\"{0}\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>", allPagePart > currentPart ? GetPage(urlFormat, currentPage + 1) : "javascript:void(0)", allPagePart > currentPart ? "" : "class=\"disabled\"");
            sb.AppendFormat("<li {1}><a href=\"{0}\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;&raquo;</span></a></li>", allPagePart > currentPart ? GetPage(urlFormat, allPage) : "javascript:void(0)", allPagePart > currentPart ? "" : "class=\"disabled\"");

            if (showJump)
            {
                sb.Append("<li><form class=\"navbar-form\">");
                sb.Append("<div class=\"form-group\">");
                sb.Append("<input type=\"text\" name=\"page\" class=\"form-control\" placeholder=\"page\">");
                sb.Append("</div>");
                sb.Append("<button type=\"submit\" class=\"btn btn-default\">Go</button>");
                sb.Append("</form></li>");
            }

            sb.Append("</ul>");
            sb.Append("</nav>");

            return sb.ToString();
        }

        private string GetPage(string format, int page)
        {
            return string.Format(format, page);
        }

        public string GetUrl(string postStr)
        {
            string Addressurl = Request.Headers["SCRIPT_NAME"].ToString();
            Addressurl += "?";
            string ItemUrls = "";
            foreach (string queryStr in Request.Query.Keys)
            {
                if (postStr == "")
                {
                    if (queryStr != "page")
                    {
                        ItemUrls += queryStr + "=" + System.Net.WebUtility.UrlEncode(Request.Query[queryStr]) + "&";
                    }
                }
                else
                {
                    if (queryStr != "page" && queryStr != "records")
                    {
                        ItemUrls += queryStr + "=" + System.Net.WebUtility.UrlEncode(Request.Query[queryStr]) + "&";
                    }
                }
            }
            if (postStr != "")
            {
                Addressurl += postStr + "&";
            }
            return Addressurl + ItemUrls + "page=";
        }
        #endregion
    }
}