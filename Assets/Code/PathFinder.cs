using System.Collections.Generic;
using System.Linq;

public static class PathFinder
{
    public class PathNode
    {
        public readonly float Prob;
        public readonly int Length;
        public readonly List<string> Path;
        public PathNode(float prob, int length, List<string> path)
        {
            Prob = prob;
            Length = length;
            Path = new List<string>(path);
        }
    }

    private static readonly Dictionary<string, MapData.Node> Nodes = MapData.Nodes; // static 데이터 사용

    public static List<PathNode> FindPath(string start, string end)
    {
        Dictionary<string, List<PathNode>> pareto = new Dictionary<string, List<PathNode>>();
        Queue<PathNode> queue = new Queue<PathNode>();
        queue.Enqueue(new PathNode(1f, 0, new List<string> { start }));

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            string current = node.Path[node.Path.Count - 1];

            if (!Nodes.ContainsKey(current)) continue;

            foreach (var edge in Nodes[current].Edges)
            {
                if (node.Path.Contains(edge.Neighbor)) continue;

                float newProb = node.Prob * edge.Probability;
                int newLength = node.Length + 1;
                List<string> newPath = new List<string>(node.Path) { edge.Neighbor };

                bool skip = false;
                List<PathNode> newList = new List<PathNode>();
                if (pareto.ContainsKey(edge.Neighbor))
                {
                    foreach (var p in pareto[edge.Neighbor])
                    {
                        if (p.Prob > newProb && p.Length <= newLength) { skip = true; break; }
                        if (newProb > p.Prob && newLength <= p.Length) continue;
                        newList.Add(p);
                    }
                }

                if (skip) continue;

                newList.Add(new PathNode(newProb, newLength, newPath));
                pareto[edge.Neighbor] = newList;
                queue.Enqueue(new PathNode(newProb, newLength, newPath));
            }
        }

        return !pareto.ContainsKey(end) ? new List<PathNode>() : pareto[end].OrderBy(p => p.Length).ToList();
    }
}
