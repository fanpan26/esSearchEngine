using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    public sealed class RangeOptions
    {
        public string Field { get; set; }
        public long SmallValue { get; set; }
        public long BigValue { get; set; }
        public bool GtWithEqual { get; set; } = false;
        public bool LtWithEqual { get; set; } = false;
    }
    public class RangeFilter : BaseFilter
    {
        private List<RangeOptions> _options;
        public RangeFilter() {
            _options = new List<RangeOptions>();
        }

        public RangeFilter Options(RangeOptions options)
        {
            _options.Add(options);
            return this;
        }

        private void Build()
        {
            StringBuilder str = new StringBuilder();
            int i = 0;
            foreach (var item in _options)
            {
                str.Append("{\"range\":{\"" + item.Field + "\":{" + (
                    item.GtWithEqual ? ConditionConst._gte : ConditionConst._gt) +
                    item.SmallValue + "," + (
                    item.LtWithEqual ? ConditionConst._lte : ConditionConst._lt) +
                    item.BigValue + "}}}");

                if (i < _options.Count - 1) {
                    str.Append(",");
                }
                i++;
            }
            _condition += str.ToString();
        }

        public override string ToString()
        {
            Build();
            return _condition;
        }
    }
}
