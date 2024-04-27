using System;
using System.Collections.Generic;

public class Edge
    {
        public long from;
        public long to;
        public long capacity;
        public long flow;
        public Edge(long _from, long _to, long _capacity, long _flow)
        {
            this.from = _from;
            this.to = _to;
            this.capacity = _capacity;
            this.flow = _flow;
        }
    }
    public class FlowGraph
    {
        /*List for storing forward and backward edges*/
        public List<Edge> edges = new List<Edge>();
        public List<List<long>> adj = new List<List<long>>();
        public FlowGraph(long nodeCount)
        {
            for (long i = 0; i < nodeCount; i++)
            {
                adj.Add(new List<long>());
            }
        }
        public long getSize()
        {
            return adj.Count;
        }

        public void addEdge(long from, long to, long capacity)
        {
            /*Actually we add two edges in this function,
                one for from -> to, and the other is to -> from*/
            Edge forward = new Edge(from, to, capacity, 0);
            /*residual edge*/
            Edge backward = new Edge(to, from, 0, 0);
            adj[(int)from].Add(edges.Count);
            edges.Add(forward);
            adj[(int)to].Add(edges.Count);
            edges.Add(backward);
            /* ?? */
        }
        public List<long> getAdj(long from)
        {
            return adj[(int)from];
        }
        public Edge getEdge(long id)
        {
            return edges[(int)id];
        }
        public void addFlow(long id, long flow)
        {
            edges[(int)id].flow += flow;
            /* ?? */
            edges[(int)id ^ 1].flow -= flow;
        }

    }

class AirlineCrewsToFligts
{
    public static FlowGraph buildNetWork(long flightCount, long crewCount, long[][] info)
        {
            /* 2 because source and sink*/
            FlowGraph graph = new FlowGraph(flightCount + crewCount + 2);

            /* edges from source to flights, all the capacities are initialized with 1*/
            for (long i = 0; i < flightCount; i++)
            {
                graph.addEdge(0, i + 1, 1);
            }
            /* edges between flights and crews*/
            for (long i = 0; i < flightCount; i++)
            {
                for (long j = 0; j < crewCount; j++)
                {
                    if (info[i][j] == 1)
                    {
                        /* the capacity must be still 1*/
                        graph.addEdge(i + 1, flightCount + j + 1, 1);
                    }
                }
            }
            /* edges from crews to sink */
            for (long i = 0; i <crewCount; i++)
            {
                graph.addEdge(flightCount + 1, crewCount + flightCount + 1, 1);
            }
            return graph;
        }
        public static void BFS(FlowGraph graph, long source, long sink, long[] parent)
        {
            Array.Fill(parent, -1);

            Queue<long> q = new Queue<long>();
            q.Enqueue(source);

            while (q.Count != 0)
            {
                long current = q.Dequeue();

                foreach (var adjacent in graph.getAdj(current))
                {
                    Edge edge = graph.getEdge(adjacent);
                    if (parent[edge.to] == -1 && edge.capacity > edge.flow && edge.to != source)
                    {
                        parent[edge.to] = adjacent;
                        q.Enqueue(edge.to);
                    }
                }
            }
        }
        public static long maxFlow(FlowGraph graph, long source, long sink)
        {
            long flow = 0;
            long[] parent = new long[graph.getSize()];
            do
            {
                BFS(graph, source, sink, parent);
                if (parent[sink] != -1)
                {
                    long minFlow = int.MaxValue;

                    for (long i = parent[sink]; i != -1; i = parent[graph.getEdge(i).from])
                    {
                        minFlow = Math.Min(minFlow, graph.getEdge(i).capacity - graph.getEdge(i).flow);
                    }

                    for (long i = parent[sink]; i != -1; i = parent[graph.getEdge(i).from])
                    {
                        graph.addFlow(i, minFlow);
                    }

                    flow += minFlow;
                }
            }while (parent[sink] != -1);

            return flow;
        }
        public static long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            FlowGraph g = buildNetWork(flightCount, crewCount, info);
            maxFlow(g, 0, g.getSize() - 1);
            long[] result = new long[flightCount];

            for (long i = 0; i < flightCount; i++)
            {
                long res = -1;
                foreach (var adj in g.getAdj(i + 1))
                {
                    Edge e = g.getEdge(adj);
                    if (e.flow == 1)
                    {
                        res = e.to - flightCount;
                        break;
                    }
                }
                result[i] = res;
            }

            return result;
        } 
}