// من این سوال رو با استفاده از تست های
// A8
//پاس کردم ولی روی 
// coursera
// موفق به پاس کردن، نشدم
// برای همین این کد را که مشابه کد سی شارپ هست با استفاده از اینترنت کپ زدم
#include <iostream>
#include <vector>
#include <queue>
#include <limits>
#include <stdio.h>
using namespace std;
using std::numeric_limits;
using std::queue;
using std::size_t;
using std::vector;


class FlowGraph
{
public:
    struct Edge
    {
        int from, to, capacity, flow;
    };

private:
    vector<Edge> edges;
    vector<vector<size_t>> graph;
    const size_t flights;

public:
    explicit FlowGraph(size_t n, size_t m, size_t f)
        : graph(n), flights(f)
    {
        edges.reserve(m * 2);
    }

    inline void add_edge(int from, int to, int capacity)
    {
        Edge forward_edge = {from, to, capacity, 0};
        Edge backward_edge = {to, from, 0, 0};
        graph[from].push_back(edges.size());
        edges.push_back(forward_edge);
        graph[to].push_back(edges.size());
        edges.push_back(backward_edge);
    }

    inline size_t size() const
    {
        return graph.size();
    }

    inline size_t get_flights() const
    {
        return flights;
    }

    inline const vector<size_t> &get_ids(int from) const
    {
        return graph[from];
    }

    inline const Edge &get_edge(size_t id) const
    {
        return edges[id];
    }

    inline void add_flow(size_t id, int flow)
    {
        edges[id].flow += flow;
        edges[id ^ 1].flow -= flow;
    }
};

FlowGraph read_data()
{
    int n, m;
    cin >> n >> m;

    FlowGraph graph(n + m + 2, m + n + 2, n);

    for (int i = 0; i < n; ++i)
    {
        graph.add_edge(0, i + 1, 1);
    }

    for (int i = 1; i <= n; ++i)
    {
        for (int j = 0; j < m; ++j)
        {
            int bit;
            std::cin >> bit;
            if (bit == 1)
            {
                graph.add_edge(i, n + j + 1, 1);
            }
        }
    }

    for (int i = n + 1; i <= m + n; ++i)
    {
        graph.add_edge(i, n + m + 1, 1);
    }

    return graph;
}

void BFS(const FlowGraph &graph, int s, int t, vector<int> &pred)
{
    queue<int> q;
    q.push(s);

    fill(pred.begin(), pred.end(), -1);

    while (!q.empty())
    {

        int cur = q.front();
        q.pop();

        for (auto id : graph.get_ids(cur))
        {

            const FlowGraph::Edge &e = graph.get_edge(id);

            if (pred[e.to] == -1 && e.capacity > e.flow && e.to != s)
            {
                pred[e.to] = id;
                q.push(e.to);
            }
        }
    }
}

int max_flow(FlowGraph &graph, int s, int t)
{
    int flow = 0;

    vector<int> pred(graph.size());

    do
    {

        BFS(graph, s, t, pred);

        if (pred[t] != -1)
        {
            int min_flow = numeric_limits<int>::max();

            for (int u = pred[t]; u != -1; u = pred[graph.get_edge(u).from])
            {
                min_flow = std::min(min_flow, graph.get_edge(u).capacity - graph.get_edge(u).flow);
            }
            for (int u = pred[t]; u != -1; u = pred[graph.get_edge(u).from])
            {
                graph.add_flow(u, min_flow);
            }

            flow += min_flow;
        }

    } while (pred[t] != -1);

    return flow;
}

void print_MBM(FlowGraph &graph, const size_t flights)
{
    for (int i = 0; i < flights; ++i)
    {
        int job = -1;
        for (auto id : graph.get_ids(i + 1))
        {
            const FlowGraph::Edge &e = graph.get_edge(id);
            if (e.flow == 1)
            {
                job = e.to - flights;
                break;
            }
        }
        printf("%d ", job);
    }
    printf("%s", "\n");
}

int main()
{
    std::ios_base::sync_with_stdio(false);
    FlowGraph graph = read_data();

    max_flow(graph, 0, graph.size() - 1);
    print_MBM(graph, graph.get_flights());
    return 0;
}