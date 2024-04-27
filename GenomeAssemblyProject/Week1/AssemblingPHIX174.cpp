#include <bits/stdc++.h>

using namespace std;

// time limit in c#, so I have converted into c++

int Overlap(string v1, string v2) {
    for (int i = 0; i < v1.size(); ++i) {
        if (v1.substr(i) == v2.substr(0, v1.size() - i)) {
            return v1.size() - i;
        }
    }
    return 0;
}

int main ()
{
    std::ios::sync_with_stdio(false);

    vector<string> read;
    string s;
    while ((cin >> s)) 
    {
        read.push_back(s);
    }

    int last = 0;
    bool visited[read.size()];
    visited[last]=true;
    string final = "" + read[0];
    for (int i = 0; i < read.size(); ++i) 
	{
        visited[last] = true;
        int index = -1;
        int best = 0;
        for (int j = 0; j < read.size(); ++j) 
		{
            if (!visited[j]) 
			{
                int overlap = Overlap(read[last], read[j]);
                if (overlap > best) 
				{
                    best = overlap;
                    index = j;
                }
            }
        }
        if (index == -1) 
            break;
        final += read[index].substr(best);
        last = index;
    }
    cout << final.substr(Overlap(read[last], read[0]));
    return 0;
}