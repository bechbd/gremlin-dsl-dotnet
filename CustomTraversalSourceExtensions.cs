using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using System;
using System.Collections.Generic;

public static class KillrVideoGraphTraversalExtensions
{
    public static GraphTraversal<Vertex, Vertex> Gods(this GraphTraversal<Vertex, Vertex> t)
    {
        return t.HasLabel("god");
    }

    public static GraphTraversal<Vertex, T> SelectBy<T>(this GraphTraversal<Vertex, Vertex> t, string selectKey)
    {
        var arrStr = selectKey.Split(".");
        if (string.IsNullOrEmpty(selectKey) || arrStr.Length % 2 != 0)
        {
            throw new ArgumentException("The selectKey must be non-null and in the format <alias name>.<property name>");
        }
        return t.Select<T>(arrStr[0]).By(arrStr[1]);
    }

    public static GraphTraversal<Vertex, IDictionary<string, T>> SelectBy<T>(this GraphTraversal<Vertex, Vertex> t, string[] selectKey)
    {
        var trav = new GraphTraversal<Vertex, IDictionary<string, T>>();
        var aliases = new List<string>();
        var properties = new List<string>();
        foreach (var key in selectKey)
        {
            var arrStr = key.Split(".");
            if (string.IsNullOrEmpty(key) || arrStr.Length % 2 != 0)
            {
                throw new ArgumentException("The selectKey must be non-null and in the format <alias name>.<property name>");
            }
            aliases.Add(arrStr[0]);
            properties.Add(arrStr[1]);
        }

        if (aliases.Count == 2)
        {
            trav = t.Select<T>(aliases[0], aliases[1]);
        }
        else if (aliases.Count > 2)
        {
            trav = t.Select<T>(aliases[0], aliases[1], aliases.GetRange(2, aliases.Count - 2).ToArray());
        }

        foreach (var prop in properties)
        {
            trav = trav.By(prop);
        }


        return trav;

    }

    public static GraphTraversal<Vertex, T> SelectValues<T>(this GraphTraversal<Vertex, Vertex> t, string selectKey)
    {
        return t.Select<T>(selectKey).By(__.Values<T>());
    }
    public static GraphTraversal<Vertex, T> SelectValueMap<T>(this GraphTraversal<Vertex, Vertex> t, string selectKey)
    {
        GraphTraversal<Vertex, T> trav = t.Select<T>(selectKey).By(__.ValueMap<T>());

        return trav;
    }
}