#include <bits/stdc++.h>


using namespace std;

vector<string> inputs;
int k, t;
long res = 0;
map<string, int> nameToIndex;
vector<set<int>> adj;

set<int> inEdges;
set<int> outEdges;

set<int> visited;
map<int, map<int, vector<set<int>>>> paths;

void constructDeBruijnGraph()
{
    int counter = 0;
    for (auto str : inputs)
    {
        for (int i = 0; i + k - 1 < str.size(); i++)
        {
            if (nameToIndex.find(str.substr(i, k - 1)) == nameToIndex.end())
            {
                nameToIndex.insert({str.substr(i, k - 1), counter++});
                set<int> newVec;
                adj.push_back(newVec);
            }
            if (nameToIndex.find(str.substr(i + 1, k - 1)) == nameToIndex.end())
            {
                nameToIndex.insert({str.substr(i + 1, k - 1), counter++});
                set<int> newVec;
                adj.push_back(newVec);
            }
            adj[nameToIndex[str.substr(i, k - 1)]].insert(nameToIndex[str.substr(i + 1, k - 1)]);
        }
    }
}


void checkEdges()
{
    vector<int> in(adj.size());
    vector<int> out(adj.size());
    for (int i = 0; i < adj.size(); i++)
    {
        for (auto v : adj[i])
        {
            in[v]++;
            out[i]++;
        }
    }
    for (int i = 0; i < adj.size(); i++)
    {
        if (in[i] > 1)
        {
            inEdges.insert(i);
        }
        if (out[i] > 1)
        {
            outEdges.insert(i);
        }
    }
}

void DFS(int root, int currentNode, set<int>& visited)
{
    if (currentNode != root && inEdges.find(currentNode) != inEdges.end())
    {
        set<int> path = visited;
        path.erase(currentNode);
        path.erase(root);
        paths[root][currentNode].push_back(path);
    }

    if (visited.size() > t)
    {
        return;
    }

    for (auto v : adj[currentNode])
    {
        if (visited.find(v) == visited.end())
        {
            set<int> to_be_visited = visited;
            to_be_visited.insert(v);
            DFS(root, v, to_be_visited);
        }
    }
}

int main()
{
    ios::sync_with_stdio(false);
    /*
        Given a list of error-prone reads and two integers, 𝑘 and 𝑡, construct a de Bruijn graph from the
        𝑘-mers created from the reads and perform the task of bubble detection on this de Bruijn graph with
        a path length threshold of 𝑡.
    */
    cin >> k >> t;
    string s = "a";
    while (s.length() != 0 && !s.empty() && (s != "TTGC"))
    {
        cin >> s;
        if (s.length() != 0 && !s.empty())
            inputs.push_back(s);
    }
    constructDeBruijnGraph();
    checkEdges();
    for (auto node : outEdges)
    {
        visited.clear();
        visited.insert(node);
        DFS(node, node, visited);
    }

    for (auto Upath : paths)
    {
        for (auto UVpath : Upath.second)
        {
            vector<set<int>> newPath = UVpath.second;

            for (int i = 0; i < newPath.size(); i++)
            {
                for (int j = i + 1; j < newPath.size(); j++)
                {
                    bool flag = false;
                    for (auto p : newPath[i])
                    {
                        if (newPath[j].find(p) != newPath[j].end())
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        res++;
                    }
                }
            }
        }
    }

    cout << res << '\n';
}