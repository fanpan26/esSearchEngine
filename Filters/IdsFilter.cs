using Macrosage.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    public class IdsFilter : BaseFilter
    {
        public IdsFilter Values(object[] values)
        {
            var val = values.ToJson();
            Build(val);
            return this;
        }
        private void Build(string value)
        {
            bool hasValue = HasCondition();
            _condition += (hasValue ? "," : "") + "{\"ids\":{\"values\":" + value + "}}";
        }
    }
}
