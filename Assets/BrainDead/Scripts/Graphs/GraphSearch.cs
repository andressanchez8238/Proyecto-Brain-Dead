using System.Collections.Generic;

public static class GraphSearch
{
    public static List<GraphNode> FindPath(GraphNode start, GraphNode goal)
    {
        Queue<GraphNode> queue = new Queue<GraphNode>();

        Dictionary<GraphNode, GraphNode> parent = new Dictionary<GraphNode, GraphNode>();

        queue.Enqueue(start);

        parent[start] = null;

        while (queue.Count > 0)
        {
            GraphNode current = queue.Dequeue();

            if (current == goal)
                break;

            foreach (GraphNode neighbor in current.neighbors)
            {
                if (parent.ContainsKey(neighbor))
                    continue;

                parent[neighbor] = current;

                queue.Enqueue(neighbor);
            }
        }

        List<GraphNode> path = new List<GraphNode>();

        GraphNode node = goal;

        while (node != null)
        {
            path.Insert(0, node);

            parent.TryGetValue(node, out node);
        }

        return path;
    }
}