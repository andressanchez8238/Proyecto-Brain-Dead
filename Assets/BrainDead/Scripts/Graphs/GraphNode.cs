using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public string zoneName;

    public List<GraphNode> neighbors = new List<GraphNode>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.4f);

        Gizmos.color = Color.white;

        foreach (GraphNode node in neighbors)
        {
            if (node != null)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position + Vector3.up, zoneName);
        #endif
    }
}