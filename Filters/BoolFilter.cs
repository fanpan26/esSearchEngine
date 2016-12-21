using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macrosage.ElasticSearch.Core.Filters
{
    public class BoolFilter : BaseFilter
    {
        public BoolFilter() { }

        private bool _hasBool = false;

        #region Should Must MustNot
        public BoolFilter Should(Func<BoolFilter, BoolFilter> func)
        {
            return ShouldMustNot(func, 1);
        }
        public BoolFilter Must(Func<BoolFilter, BoolFilter> func)
        {
            return ShouldMustNot(func, 2);
        }
        public BoolFilter MustNot(Func<BoolFilter, BoolFilter> func)
        {
            return ShouldMustNot(func, 3);
        }

        private BoolFilter ShouldMustNot(Func<BoolFilter, BoolFilter> func, int t)
        {
            string shouldResult = func(new BoolFilter()).ToString();
            string smm = "";
            if (t == 1)
            {
                smm = ConditionConst._should;
            }
            else
            if (t == 2)
            {
                smm = ConditionConst._must;
            }
            else
            if (t == 3)
            {
                smm = ConditionConst._mustnot;
            }
            PrapareCondition();
            _condition = (_hasBool ? "" : ConditionConst._bool) +
                         ConditionConst._leftBracket + smm + shouldResult +
                         ConditionConst._rightBracket;
            return this;
        }

        #endregion

        #region And Or
        public BoolFilter And(Func<BoolFilter, BoolFilter> func)
        {
            return AndOr(func, isand: true);
        }
        public BoolFilter Or(Func<BoolFilter, BoolFilter> func)
        {
            return AndOr(func, isand: false);
        }

        private BoolFilter AndOr(Func<BoolFilter, BoolFilter> func, bool isand = true)
        {
            string funcResult = func(new BoolFilter()).ToString();
            PrapareCondition();
            _condition += (isand ? ConditionConst._leftBeginAnd : ConditionConst._leftBeginOr) +
                         ConditionConst._leftMiddleBracket
                         + funcResult +
                         ConditionConst._rightBigAndMiddleBracket;
            return this;
        }

        #endregion

        #region Term

        public BoolFilter Term(Func<TermFilter, TermFilter> termFunc)
        {
            PrapareCondition();
            _condition += termFunc(new TermFilter());
            return this;
        }
        #endregion

        #region Ids

        public BoolFilter Ids(Func<IdsFilter, IdsFilter> idsFunc)
        {
            PrapareCondition();
            _condition += idsFunc(new IdsFilter());
            return this;
        }
        #endregion

        #region match_all
        public BoolFilter MatchAll()
        {
            _condition += ConditionConst._matchall;
            return this;
        }
        #endregion
    }
}
