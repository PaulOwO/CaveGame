using System;
using System.Collections;
using System.Collections.Generic;
using path;
using UnityEditor;
using UnityEngine;


public class WaypointGraph : MonoBehaviour
{

    private Graph graph_ = new Graph();

    [SerializeField] private float resolution = 0.5f;

    private List<int> path = new List<int>();

    void Start()
    {
        var mainCamera = Camera.main;
        var cameraSize = 2.0f * mainCamera.orthographicSize * new Vector2(mainCamera.aspect, 1.0f);
        var cameraRect = new Rect() { min = -cameraSize / 1.0f, max = cameraSize / 1.0f };

        var width = cameraRect.width / resolution;
        var height = cameraRect.height / resolution;
        Dictionary<Vector2Int, int> nodeMap = new Dictionary<Vector2Int, int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var worldPos = new Vector2(x, y) * resolution - cameraRect.size / 2.0f;
                var raycast = Physics2D.Raycast(worldPos, Vector2.up, resolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.down, resolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.left, resolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.right, resolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                nodeMap[new Vector2Int(x, y)] = graph_.AddNode(worldPos);
            }
        }

        foreach (var nodePair in nodeMap)
        {
            var pos = nodePair.Key;
            var nodeIndex = nodePair.Value;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    var neighborPos = pos + new Vector2Int(dx, dy);
                    if (neighborPos != pos && nodeMap.ContainsKey(neighborPos))
                    {
                        graph_.AddNeighborEdge(nodeIndex, nodeMap[neighborPos]);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < graph_.Nodes.Count; i++)
        {
            var node = graph_.Nodes[i];
            //Gizmos.DrawSphere(node.position, resolution/2.0f);

            foreach (var neighbor in node.neighbors)
            {

                Gizmos.color = path.Contains(i) && path.Contains(neighbor.nodeIndex) ? Color.red : Color.blue;
                Gizmos.DrawLine(node.position, graph_.Nodes[neighbor.nodeIndex].position);
            }
        }

    }
}
