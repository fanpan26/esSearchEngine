/*
 * 类名：QueryCreator
 * 创建时间：2016-12-20
 * 作者：小小小丶盘子
 * 
 * V1.0 第一次创建，增加排序方法，分页方法支持
 * 
 * 
 * 
 * 
 * 
 */

using Macrosage.ElasticSearch.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core
{

    public class QueryCreator
    {
        private const string _defaultCondition = "";

        private string _condition = string.Empty;
        private bool _start = false;
        private Dictionary<string, string> _sort;

        private int _index = 0;
        private int _size = 10;
        public string Condition
        {
            get
            {
                return _condition;
            }
        }

        public QueryCreator()
        {
            _condition = _defaultCondition;
            _sort = new Dictionary<string, string>();
        }

        private void Prapare()
        {
            if (!_start)
            {
                _start = true;
                _condition = "";
            }
        }

        public QueryCreator Filter(Func<Filter, Filter> filter)
        {
            _condition += filter(new Filter());
            return this;
        }

        #region 分页
        public QueryCreator Page(int index)
        {
            _index = index;
            return this;
        }
        public QueryCreator Size(int size)
        {
            _size = size;
            return this;
        }

        #endregion

        #region 排序
        public QueryCreator OrderBy(params string[] value)
        {
            string orderby = string.Empty;
            foreach (string v in value)
            {
                //去掉其他空格
                orderby = Regex.Replace(v, @"\b\s+\b", " ").ToLowerInvariant();
                string[] sortArr = orderby.Split(' ');
                if (sortArr.Length == 2)
                {
                    _sort.Add(sortArr[0], sortArr[1]);
                }
            }
            return this;
        }

        public QueryCreator OrderByAsc(string key)
        {
            _sort.Add(key, ConditionConst._sortAsc);
            return this;
        }
        public QueryCreator OrderByDesc(string key)
        {
            _sort.Add(key, ConditionConst._sortDesc);
            return this;
        }
        #endregion

        #region 高亮

        public QueryCreator HighLightAll(string[] fields, string pretags, string posttags)
        {
            return this;
        }

        public QueryCreator HighLightFields(string[] fileds, string[] pretags, string[] posttags)
        {
            return this;
        }


        #endregion

        #region 构建查询语句
        public string Build()
        {
            //先根据参数创建分页信息
            Build(false);
            return this.ToString();
        }

        public string BuildBeautiful()
        {
            Build(true);
            return this.ToString();
        }

        private void Build(bool beauty)
        {
            if (string.IsNullOrEmpty(_condition)) {
                _condition = Filter(x => x.Query(q => q.MatchAll())).ToString();
            }
            if (_sort.Count == 0) {
                _sort.Add("_score", "desc");//默认排序
            }
            var pager = Core.Page.CreatePageAndSort(_index, _size, _sort);

            StringBuilder str = new StringBuilder();
            str.Append(_condition);
            str.Append(pager);
            str.Append(ConditionConst._rightBracket);
            _condition = str.ToString();

            if (beauty)
            {
                _condition = JsonBeautifier.Beautify(_condition);
            }
        }
        #endregion

        public override string ToString()
        {
            return this._condition;
        }
    }
}
   


