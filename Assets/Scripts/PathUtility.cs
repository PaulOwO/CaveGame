using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using UnityEngine;

namespace path
{
    public struct Neighbor
    {
        public int nodeIndex;
        public float length;

        public Neighbor(int nodeIndex, float length)
        {
            this.nodeIndex = nodeIndex;
            this.length = length;
        }
    }
    /** 
     * <summary>Simple node struct used for pathfinding</summary>
     */
    public struct Node
    {
        public Vector2 position;
        public List<Neighbor> neighbors;

        public Node(Vector2 position)
        {
            this.position = position;
            neighbors = new List<Neighbor>();
        }

    }



    /**
     * <summary>Bidirectional graph</summary> 
     */
    public class Graph
    {
        public struct QueryInfo
        {
            public int nodeTraversedCount;
            public QueryInfo(int nodeTraversedCount)
            {
                this.nodeTraversedCount = nodeTraversedCount;
            }

        }
        private List<Node> nodes_ = new List<Node>();
        private QueryInfo lastQueryInfo_ = new QueryInfo(0);
        public QueryInfo LastQueryInfo => lastQueryInfo_;

        public List<Node> Nodes => nodes_;

        public int AddNode(Vector2 position)
        {
            int index = Nodes.Count;
            Nodes.Add(new Node(position));
            return index;
        }
        /**
         * <summary>Add neighbor node2 to node1 neighbors list.
         * Neighbors are represented as node indexes</summary>
         */
        public void AddNeighborEdge(int node1, int node2, bool bidirectional = false)
        {
            var distance = (Nodes[node2].position - Nodes[node1].position).magnitude;
            Nodes[node1].neighbors.Add(new Neighbor(node2, distance));
            if (bidirectional)
            {
                Nodes[node2].neighbors.Add(new Neighbor(node1, distance));
            }
        }

        public void Clear()
        {
            Nodes.Clear();
        }

        public List<int> DijkstraAlgorithm(int startNode, int destination)
        {
            var path = new List<int>();
            Dictionary<int, int> cameFrom = new Dictionary<int, int>();
            cameFrom[startNode] = -1;
            Dictionary<int, float> costSoFar = new Dictionary<int, float>();
            costSoFar[startNode] = 0;
            List<Tuple<int, float>> nextNodes = new List<Tuple<int, float>>();
            nextNodes.Add(new Tuple<int, float>(startNode, 0.0f));
            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.First().Item1;
                nextNodes.RemoveAt(0);
                if (currentNode == destination)
                    break;
                foreach (var neighbor in nodes_[currentNode].neighbors)
                {
                    if (cameFrom.ContainsKey(neighbor.nodeIndex))
                    {
                        var cost = costSoFar[currentNode] + neighbor.length;
                        if (cost < costSoFar[neighbor.nodeIndex])
                        {
                            cameFrom[neighbor.nodeIndex] = currentNode;
                            costSoFar[neighbor.nodeIndex] = cost;
                            int index = nextNodes.FindIndex(t => t.Item1 == neighbor.nodeIndex);
                            var heuristicCost = cost + (nodes_[destination].position - nodes_[neighbor.nodeIndex].position)
                                .magnitude;
                            var tuple = new Tuple<int, float>(neighbor.nodeIndex, heuristicCost);
                            nextNodes[index] = tuple;
                            nextNodes.Sort(Comparer<Tuple<int, float>>.Create(
                                (t1, t2) => t1.Item2 < t2.Item2 ? -1 : t1.Item2 > t2.Item2 ? 1 : 0));
                        }
                    }
                    else
                    {
                        cameFrom[neighbor.nodeIndex] = currentNode;
                        var cost = costSoFar[currentNode] + neighbor.length;
                        costSoFar[neighbor.nodeIndex] = cost;
                        var heuristicCost = cost + (nodes_[destination].position - nodes_[neighbor.nodeIndex].position)
                            .magnitude;
                        nextNodes.Add(new Tuple<int, float>(neighbor.nodeIndex, heuristicCost));
                        nextNodes.Sort(Comparer<Tuple<int, float>>.Create(
                            (t1, t2) => t1.Item2 < t2.Item2 ? -1 : t1.Item2 > t2.Item2 ? 1 : 0));
                    }
                }
            }

            if (!cameFrom.ContainsKey(destination))
                return path;
            int tmp = destination;
            while (tmp != startNode)
            {
                path.Add(tmp);
                tmp = cameFrom[tmp];
            }
            path.Add(startNode);
            path.Reverse();
            return path;
        }
        /**
         * <summary>Calculate the shortest path from the startNode to the destinationNode.
         * Using A* pathfinding algorithm.</summary>
         */
        public List<int> CalculatePath(int startNode, int destinationNode)
        {
            var path = new List<int>();
            lastQueryInfo_.nodeTraversedCount = 0;
            if (destinationNode < 0 || destinationNode >= Nodes.Count)
            {
                return path;
            }

            if (startNode < 0 || startNode >= Nodes.Count)
            {
                return path;
            }
            var cameFrom = new int[Nodes.Count];
            cameFrom.Fill(-1);
            var costSoFar = new float[Nodes.Count];
            costSoFar.Fill(-1.0f);
            costSoFar[startNode] = 0.0f;
            SimplePriorityQueue<int> frontier = new SimplePriorityQueue<int>();
            frontier.Enqueue(startNode, 0.0f);
            var destinationPosition = Nodes[destinationNode].position;
            int nodeTraversedCount = 0;
            while (frontier.Count > 0)
            {
                nodeTraversedCount++;
                var currentNode = frontier.Dequeue();
                if (currentNode == destinationNode)
                {
                    break;
                }
                foreach (var neighbor in Nodes[currentNode].neighbors)
                {
                    var newCost = costSoFar[currentNode] + neighbor.length;
                    if (!(costSoFar[neighbor.nodeIndex] < 0.0f) &&
                        !(newCost < costSoFar[neighbor.nodeIndex])) continue;
                    costSoFar[neighbor.nodeIndex] = newCost;
                    var priority = newCost + (destinationPosition - Nodes[neighbor.nodeIndex].position).magnitude;
                    if (frontier.Contains(neighbor.nodeIndex))
                    {
                        frontier.UpdatePriority(neighbor.nodeIndex, priority);
                    }
                    else
                    {
                        frontier.Enqueue(neighbor.nodeIndex, priority);
                    }

                    cameFrom[neighbor.nodeIndex] = currentNode;
                }
            }



            lastQueryInfo_.nodeTraversedCount = nodeTraversedCount;
            if (cameFrom[destinationNode] < 0)
            {
                return path;
            }
            path.Add(destinationNode);
            var tmpNode = destinationNode;
            while (tmpNode != startNode)
            {
                tmpNode = cameFrom[tmpNode];
                path.Add(tmpNode);
            }
            path.Reverse();
            return path;
        }
    }
}