using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance;

    public List<GraphNode> nodes = new List<GraphNode>();

    private void Awake()
    {
        Instance = this;

        nodes.Clear();

        nodes.AddRange(GetComponentsInChildren<GraphNode>());

        Debug.Log("Nodos encontrados: " + nodes.Count);
    }

    public GraphNode GetNode(string zoneName)
    {
        foreach (GraphNode node in nodes)
        {
            if (node.zoneName == zoneName)
                return node;
        }

        return null;
    }

    public GraphNode GetClosestNode(Vector3 position)
    {
        GraphNode closest = null;

        float minDistance = Mathf.Infinity;

        foreach (GraphNode node in nodes)
        {
            float distance = Vector3.Distance(position, node.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = node;
            }
        }

        return closest;
    }
}