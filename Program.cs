using System;
using Dse;
using Dse.Graph;
using System.Collections.Generic;

namespace gremlin_dsl_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IDseCluster cluster = DseCluster.Builder().
                                             AddContactPoint("127.0.0.1").
                                             WithGraphOptions(new GraphOptions().SetName("STUDIO_TUTORIAL_GRAPH")).
                                             Build();
            IDseSession session = cluster.Connect();
            var g = DseGraph.Traversal(session);

            System.Console.WriteLine(g.V().Gods().As("g").Select<string>("g").By("name").Next());
            System.Console.WriteLine(g.V().Gods().As("g").As("t").SelectBy<string>("g.name").Next());
            System.Console.WriteLine(g.V().Gods().As("g").SelectValues<string>("g").Next());
            var items = g.V().Gods().As("g").SelectValueMap<Dictionary<string, object>>("g").ToList();

            foreach (var item in items) Console.WriteLine(item);

            var arr = new string[] { "g.name", "t.name" };
            var t = g.V().Gods().As("g").As("t").SelectBy<Object>(arr).Next();

            foreach (var r in t) Console.WriteLine(r);

            arr = new string[] { "g.name", "t.name", "p.name" };
            var l = g.V().Gods().As("g").As("t").As("p").SelectBy<Object>(arr).Next();

            foreach (var r in l) Console.WriteLine(r);
        }
    }
}
