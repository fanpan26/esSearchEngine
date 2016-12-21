# 模仿PlainElastic.NET做的小组件练习

查询语句：
```
{
    "query": {
        "filtered": {
            "filter": {
                "bool": {
                    "must": {
                        "and": [
                            {
                                "term": {
                                    "type": 3
                                }
                            },
                            {
                                "term": {
                                    "area": 北京
                                }
                            },
                            {
                                "or": [
                                    {
                                        "term": {
                                            "age": 20
                                        }
                                    },
                                    {
                                        "term": {
                                            "experience": 1
                                        }
                                    }
                                ]
                            }
                        ]
                    }
                }
            }
        }
    },
    "from": 40,
    "size": 20,
    "sort": {
        "name": {
            "order": "desc"
        }
    }
}
```
转换成程序：
```c#
            Macrosage.ElasticSearch.Core.QueryCreator creator = new Macrosage.ElasticSearch.Core.QueryCreator();
            var result = creator.Filter(f =>
                                             f.Bool(b => //bool查询
                                             b.Must(m => //must，必须符合条件
                                             m.And(a => //and查询
                                             a.Term(t => //构造查询条件
                                             t.KeyValue("type", 3).KeyValue("area", "北京")).
                                             Or(o => o.Term(t1 => t1.KeyValue("age", 20).KeyValue("experience", 1))))))).//or查询，构造查询条件
                                             Page(3).//页码
                                             Size(20).//每页大小
                                             OrderByDesc("name").//姓名倒叙排序
                                             BuildBeautiful();//根据之前的条件创建查询语句
```
