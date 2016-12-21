using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    public class TermFilter : BaseFilter
    {
        private Dictionary<string, object> _terms;
        public TermFilter()
        {
            _terms = new Dictionary<string, object>();
        }
        public TermFilter KeyValue(string key, object value)
        {
            _terms.Add(key, value);
            return this;
        }

        private void Build()
        {
            StringBuilder str = new StringBuilder();
            int i = 0;
            foreach (KeyValuePair<string, object> kv in _terms)
            {

                str.Append("{\"term\":{\"" + kv.Key + "\":" + kv.Value + "}}");
                if (i >= 0 && i < _terms.Count - 1)
                {
                    str.Append(",");
                }
                i++;
            }
            _condition = str.ToString();
        }
        public override string ToString()
        {
            Build();

            return base.ToString();
        }
    }
}
