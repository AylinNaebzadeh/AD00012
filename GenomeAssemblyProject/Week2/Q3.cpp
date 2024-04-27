/*بدلیل وجود متغیر های استاتیک در کد سی شارپ و ایراد گرفتن کورسرا از آن، آن را به سی پلاس پلاس تغییر دادم*/
#include <bits/stdc++.h>
using namespace std;

unordered_set<string> seen;
vector<int> edges;


void dfs(string node, int& k)
{
	for (int i = 0; i < k; ++i) 
    {
		string str = node + to_string(i % 2);
		if (seen.find(str) == seen.end()) 
        {
			seen.insert(str);
			dfs(str.substr(1), k);
			edges.push_back(i);
		}
	}
}


string deBruijn(int n)
{
	seen.clear();
	edges.clear();
	string startingNode = string(n - 1, '0');
	dfs(startingNode, n);
	string S;
	int l = pow(2, n);
	for (int i = 0; i < l; ++i)
		S += to_string(edges[i] % 2);
	S += startingNode;
	return S;
}

int main()
{
	int n;
    cin >> n;
	cout << deBruijn(n);

	return 0;
}
