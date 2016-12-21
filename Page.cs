using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core
{
   public class Page
    {
        public static string CreatePageAndSort(int pageIndex = 0, int pageSize = 0, Dictionary<string, string> order = null)
        {
            if (pageSize > 0) {
                if (pageIndex <= 0) { pageIndex = 1; }
            }
            //,"from":0,"size":50,"sort":{"grade":{"order":"desc"}}
            #region 不分页，并且没排序
            if (pageSize == 0 && (order == null || order.Count == 0))
            {
                return "";
            }
            #endregion

            StringBuilder str = new StringBuilder();

            #region 仅仅排序
            if (pageSize == 0 && order != null && order.Count > 0)
            {
                return createSort(order);
            }
            #endregion

            #region 仅仅分页
            if (pageSize > 0 && (order == null || order.Count == 0))
            {
                return createPage(pageIndex, pageSize);
            }
            #endregion

            #region 既排序又分页
            return createPage(pageIndex, pageSize) + createSort(order);
            #endregion
        }


        #region 私有方法，提取出
        private static string createPage(int pageindex, int pagesize)
        {
            StringBuilder str = new StringBuilder();
            var from = (pageindex - 1) * pagesize;
            var size = pagesize;
            str.AppendFormat(",\"from\":{0},\"size\":{1}", from, size);
            return str.ToString();
        }

        private static string createSort(Dictionary<string, string> order)
        {
            if (order == null) { return ""; }
            StringBuilder str = new StringBuilder();
            if (order.Count == 1)
            {
                str.Append(",\"sort\":{\"" + order.First().Key + "\":{\"order\":\"" + order.First().Value + "\"}}");
            }
            else
            {
                str.Append(",\"sort\":[");
                var i = 0;
                foreach (var key in order.Keys)
                {
                    str.Append("{\"" + key + "\":{\"order\":\"" + order[key] + "\"}}");
                    if (i < order.Count - 1)
                    {
                        str.Append(",");
                    }
                    i++;
                }
                str.Append("]");
            }
            return str.ToString();
        }
        #endregion
    }
}
