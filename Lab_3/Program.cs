int verticesCount = 4;
int edgesCount = 5;
Graph graph = CreateGraph(verticesCount, edgesCount);

// Edge 0-1
graph.edge[0].Source = 0;
graph.edge[0].Destination = 1;
graph.edge[0].Weight = 10;

// Edge 0-2
graph.edge[1].Source = 0;
graph.edge[1].Destination = 2;
graph.edge[1].Weight = 6;

// Edge 0-3
graph.edge[2].Source = 0;
graph.edge[2].Destination = 3;
graph.edge[2].Weight = 5;

// Edge 1-3
graph.edge[3].Source = 1;
graph.edge[3].Destination = 3;
graph.edge[3].Weight = 15;

// Edge 2-3
graph.edge[4].Source = 2;
graph.edge[4].Destination = 3;
graph.edge[4].Weight = 4;

Kruskal(graph);

static Graph CreateGraph(int verticesCount, int edgesCoun)
{
    Graph graph = new Graph();
    graph.VerticesCount = verticesCount;
    graph.EdgesCount = edgesCoun;
    graph.edge = new Edge[graph.EdgesCount];

    return graph;
}

static int Find(Subset[] subsets, int i)
{
    if (subsets[i].Parent != i)
        subsets[i].Parent = Find(subsets, subsets[i].Parent);

    return subsets[i].Parent;
}

static void Union(Subset[] subsets, int x, int y)
{
    int xroot = Find(subsets, x);
    int yroot = Find(subsets, y);

    if (subsets[xroot].Rank < subsets[yroot].Rank)
        subsets[xroot].Parent = yroot;
    else if (subsets[xroot].Rank > subsets[yroot].Rank)
        subsets[yroot].Parent = xroot;
    else
    {
        subsets[yroot].Parent = xroot;
        ++subsets[xroot].Rank;
    }
}

static void Print(Edge[] result, int e)
{
    for (int i = 0; i < e; ++i)
        Console.WriteLine("{0} -- {1} == {2}", result[i].Source, result[i].Destination, result[i].Weight);
}

static void Kruskal(Graph graph)
{
    int verticesCount = graph.VerticesCount;
    Edge[] result = new Edge[verticesCount];
    int i = 0;
    int e = 0;

    Array.Sort(graph.edge, delegate (Edge a, Edge b)
    {
        return a.Weight.CompareTo(b.Weight);
    });

    Subset[] subsets = new Subset[verticesCount];

    for (int v = 0; v < verticesCount; ++v)
    {
        subsets[v].Parent = v;
        subsets[v].Rank = 0;
    }

    while (e < verticesCount - 1)
    {
        Edge nextEdge = graph.edge[i++];
        int x = Find(subsets, nextEdge.Source);
        int y = Find(subsets, nextEdge.Destination);

        if (x != y)
        {
            result[e++] = nextEdge;
            Union(subsets, x, y);
        }
    }

    Print(result, e);
}
public struct Edge
{
    public int Source;
    public int Destination;
    public int Weight;
}

public struct Graph
{
    public int VerticesCount;
    public int EdgesCount;
    public Edge[] edge;
}

public struct Subset
{
    public int Parent;
    public int Rank;
}

class MST
{

    // Number of vertices in the graph
    static int V = 5;

    // A utility function to find
    // the vertex with minimum key
    // value, from the set of vertices
    // not yet included in MST
    static int minKey(int[] key, bool[] mstSet)
    {

        // Initialize min value
        int min = int.MaxValue, min_index = -1;

        for (int v = 0; v < V; v++)
            if (mstSet[v] == false && key[v] < min)
            {
                min = key[v];
                min_index = v;
            }

        return min_index;
    }

    // A utility function to print
    // the constructed MST stored in
    // parent[]
    static void printMST(int[] parent, int[,] graph)
    {
        Console.WriteLine("Edge \tWeight");
        for (int i = 1; i < V; i++)
            Console.WriteLine(parent[i] + " - " + i + "\t"
                              + graph[i, parent[i]]);
    }

    // Function to construct and
    // print MST for a graph represented
    // using adjacency matrix representation
    static void primMST(int[,] graph)
    {

        // Array to store constructed MST
        int[] parent = new int[V];

        // Key values used to pick
        // minimum weight edge in cut
        int[] key = new int[V];

        // To represent set of vertices
        // included in MST
        bool[] mstSet = new bool[V];

        // Initialize all keys
        // as INFINITE
        for (int i = 0; i < V; i++)
        {
            key[i] = int.MaxValue;
            mstSet[i] = false;
        }

        // Always include first 1st vertex in MST.
        // Make key 0 so that this vertex is
        // picked as first vertex
        // First node is always root of MST
        key[0] = 0;
        parent[0] = -1;

        // The MST will have V vertices
        for (int count = 0; count < V - 1; count++)
        {

            // Pick thd minimum key vertex
            // from the set of vertices
            // not yet included in MST
            int u = minKey(key, mstSet);

            // Add the picked vertex
            // to the MST Set
            mstSet[u] = true;

            // Update key value and parent
            // index of the adjacent vertices
            // of the picked vertex. Consider
            // only those vertices which are
            // not yet included in MST
            for (int v = 0; v < V; v++)

                // graph[u][v] is non zero only
                // for adjacent vertices of m
                // mstSet[v] is false for vertices
                // not yet included in MST Update
                // the key only if graph[u][v] is
                // smaller than key[v]
                if (graph[u, v] != 0 && mstSet[v] == false
                    && graph[u, v] < key[v])
                {
                    parent[v] = u;
                    key[v] = graph[u, v];
                }
        }

        // print the constructed MST
        printMST(parent, graph);
    }