using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    public class BaseFilter
    {
        protected string _condition = string.Empty;
        protected bool HasCondition()
        {
            return !string.IsNullOrEmpty(_condition);
        }

        protected void PrapareCondition()
        {
            bool hasValue = HasCondition();
            _condition += (hasValue ? "," : "");
        }
        public override string ToString()
        {
            return _condition;
        }
    }
}
