using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    public class Filter : BaseFilter
    {
        public Filter() { }
        //
        public Filter Bool(Func<BoolFilter, BoolFilter> boolFunc)
        {
            string boolFuncResult = boolFunc(new BoolFilter()).ToString();
            _condition = "{\"query\":{\"filtered\":{\"filter\":{" + boolFuncResult + "}}}";
            return this;
        }
        public Filter Query(Func<BoolFilter, BoolFilter> boolFunc)
        {
            string boolFuncResult = boolFunc(new BoolFilter()).ToString();
            _condition = "{\"query\":" + boolFuncResult;
            return this;
        }
    }
}
