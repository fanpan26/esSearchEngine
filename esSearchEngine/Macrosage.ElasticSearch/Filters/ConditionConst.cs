using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    internal sealed class ConditionConst
    {
        public const string _leftBracket = "{";
        public const string _rightBracket = "}";

        public const string _leftMiddleBracket = "[";
        public const string _rightMiddleBracket = "]";
        public const string _leftBigAndMiddleBracket = "{[";
        public const string _rightBigAndMiddleBracket = "]}";

        public const string _sortAsc = "asc";
        public const string _sortDesc = "desc";

        public const string _bool = "\"bool\":";
        public const string _and = "\"and\":";
        public const string _or = "\"or\":";

        public const string _leftBeginBool = "{\"bool\":";
        public const string _leftBeginAnd = "{\"and\":";
        public const string _leftBeginOr = "{\"or\":";

        public const string _should = "\"should\":";
        public const string _must = "\"must\":";
        public const string _mustnot = "\"must_not\":";

        public const string _matchall = "{\"match_all\":{}}";



    }
}
