using Macrosage.ElasticSearch.Core;
using Macrosage.ElasticSearch.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esSearchEngine
{
    class Program
    {
        static void Main(string[] args)
        {


            QueryCreator creator = new QueryCreator();
            //查询
            //var result = creator.Filter(f =>
            //                                 f.Bool(b => //bool查询
            //                                 b.Must(m => //must，必须符合条件
            //                                 m.And(a => //and查询
            //                                 a.Term(t => //构造查询条件
            //                                 t.KeyValue("type", 3).KeyValue("area", "北京")).
            //                                 Or(o => o.Term(t1 => t1.KeyValue("age", 20).KeyValue("experience", 1))))))).//or查询，构造查询条件
            //                                 Page(3).//页码
            //                                 Size(20).//每页大小
            //                                 OrderByDesc("name").//姓名倒叙排序
            //                                 BuildBeautiful();//根据之前的条件创建查询语句

            //Console.WriteLine(result);

            //简单查询
            var result = creator.Filter(f => f.Query(q => q.Term(t => t.KeyValue("bookid", 123456)))).BuildBeautiful();//.Build();
            Console.WriteLine(result);
            Console.Read();
        }
    }
}
