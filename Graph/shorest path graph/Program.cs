using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graph;

//Important ideas: just keep track of the roots of graphs or trees in an array if you are workign with a forest of graph or trees

namespace shorest_path_graph
{
    //Implementation of priority queue; PQueueItem & Pqueue stores the undirected graph
    public class PQueueItem
    {
        public int[] vertices;//contains fromVertex and toVertex
        public int Priority { get; set; }
        public PQueueItem()
        {
            vertices = new int[2];
            Priority = 2;
        }
        public PQueueItem(int pPriority, int[] pVertices)
        {
            Priority = pPriority;
            vertices = new int[2];
            vertices[0] = pVertices[0];
            vertices[1] = pVertices[1];
        }
    }
    public class PQueue
    {
        public PQueueItem[] pQ; //contains initial graph
        public int numElem;
        public PQueue()
        {
            pQ = new PQueueItem[40];
            numElem = 1;
        }
        public void Enqueue(PQueueItem item)
        {
            int index = numElem;
            if (item == null || index >= 39) return;// first element stored at 1

            while (index > 1 && item.Priority < pQ[index / 2].Priority)
            {
                pQ[index] = pQ[index / 2];
                index = index / 2;
            }
            pQ[index] = item;
            numElem++;
        }
        public PQueueItem Dequeue()
        {
            if (pQ[1] == null || numElem <= 1) return null;
            PQueueItem itemtoBeReturned = pQ[1];
            PQueueItem lastElement = pQ[--numElem];
            int childIndex;
            int i;
            //IMP: this for loop is critical! see i being init to childIndex! (i tried while loop and failed miserably)
            for (i = 1; i * 2 <= numElem; i = childIndex)
            {
                //find smaller child
                childIndex = i * 2;
                if (childIndex != numElem)
                    if (pQ[childIndex + 1].Priority < pQ[childIndex].Priority) childIndex++;
                //percolate one level
                if (lastElement.Priority > pQ[childIndex].Priority)
                    pQ[i] = pQ[childIndex];
                else break;
            }
            pQ[i] = lastElement;
            pQ[numElem] = null;
            return itemtoBeReturned;
        }
        public PQueueItem Peek()
        {
            return pQ[1];
        }
    }
    /*
* ALGORITHM: TREE_DIAMETER (T)

maxlength ← 0
for s ← 0 to s < |V[T]|
  do temp ← BSF(T, S)
        if maxlength < temp
               maxlength ← temp
               increment s by 1
return maxlength
        
    //Cannot Use Path to find the max distance - does not work in cases where the from and to node have a common parent;
    public int FindGraphDiameter(List<Node> pNodeList)
    {
        int maxLength = 0;
        int distance = 0;
        foreach (Node fromNode in pNodeList)
            foreach (Node toNode in pNodeList)
            {
                distance = PrintPath(fromNode, toNode, 0);
                if (distance > maxLength) maxLength = distance;
            }
        return maxLength;
    }
    */
    /*II    Kruskal's Algorithm Implemented with Priority Queue Data Structure
MST_KRUSKAL(G)
for each vertex v in V[G] 
 do define set S(v) ← {v}
Initialize priority queue Q that contains all edges of G, using the weights as keys
A ← { }                               ▷ A will ultimately contains the edges of the MST
while A has less than n − 1 edges 
do Let set S(v) contains v and S(u) contain u  // this is the hard part - making sure there is no cycle
     if S(v) ≠ S(u)
         then Add edge (u, v) to A
                 Merge S(v) and S(u) into one set i.e., union
return A
*/
    class MinSpanningTree
    {
        const int maxItem = 100;
        PQueueItem[] MST; //contains min spanning tree
        int numMstElem;
        PQueue GraphPQ; // contains priority queue of original undirected graph
        
        public MinSpanningTree()
        {
            numMstElem =0;
            GraphPQ = new PQueue();
            MST = new PQueueItem[maxItem];
        }
        private void KruskalGraphInit()
        {
            //populate graph - TODO
            PQueueItem a = new PQueueItem(1, new int[2] {1,2});
            PQueueItem b = new PQueueItem(1, new int[2] {2,3});
            PQueueItem c = new PQueueItem(1, new int[2] {3,4});
            PQueueItem d = new PQueueItem(1, new int[2] {4,5});
            PQueueItem e = new PQueueItem(4, new int[2] {2,5});
            PQueueItem f = new PQueueItem(3, new int[2] {3,5});
            PQueueItem g = new PQueueItem(6, new int[2] {1,4 });
            PQueueItem h = new PQueueItem(5, new int[2] {1,5 });
            
            GraphPQ.Enqueue(a);
            GraphPQ.Enqueue(b);
            GraphPQ.Enqueue(c);
            GraphPQ.Enqueue(d);
            GraphPQ.Enqueue(e);
            GraphPQ.Enqueue(f);
            GraphPQ.Enqueue(g);
            GraphPQ.Enqueue(h);
            

        }

        private int findset(int x, int[] parentArray) 
        { 
         if(x != parentArray[x]) 
         parentArray[x] = findset(parentArray[x], parentArray); 
         return parentArray[x]; 
        } 
        //Get Minimum Spanning tree of the graph
        
        public void Kruskal()
        {
            int i, total, N, Ei, pu, pv;
            int[] parentArray = new int[maxItem];
            KruskalGraphInit();
            for(i=total=0; i<GraphPQ.numElem-1; i++) 
            { 
                //pQ index starts at 1
                pu = findset(GraphPQ.pQ[i+1].vertices[0], parentArray); 
                pv = findset(GraphPQ.pQ[i+1].vertices[1], parentArray); 
                if(pu != pv) 
                { 
                    MST[numMstElem++] = GraphPQ.pQ[i+1]; // add to tree 
                    total += GraphPQ.pQ[i+1].vertices[0]; // add edge cost 
                    parentArray[pu] = parentArray[pv]; // link 
                 } 
            }
            for (i = 0; i < numMstElem; i++)
                Console.WriteLine("Vertex: {0} {1} Weight {2}", MST[numMstElem].vertices[0], MST[numMstElem].vertices[1], MST[numMstElem].Priority); 
        }
        /*
         * Now we describe the Prim's algorithm when the graph G = (V, E) is represented as an adjacency matrix. Instead of heap structure, we'll use an array to store the key of each node.
 
1. A ← V[G]                ▷ Array
 2. for each vertex u in Q
 3.     do key [u] ← ∞
 4. key [r] ← 0
 5. π[r] ← NIL
 6. while array A is empty
 7.     do scan over A find the node u with smallest key, and remove it from array A
 8.     for each vertex v in Adj[u]
 9.         if v is in A and w[u, v] < key[v]
 10.           then  π[v] ← u
 11.                   key[v] ← w[u, v]
 
 
*/
        public void Prim(List<Node> pNodeList, Dictionary<int, List<int>> pAdjacencyList)
        {

        }

        /*
         * DIJKSTRA (G, w, s)

INITIALIZE SINGLE-SOURCE (G, s) 
S ← { }     // S will ultimately contains vertices of final shortest-path weights from s 
Initialize priority queue Q i.e., Q  ←  V[G] 
while priority queue Q  is not empty do 
    u  ←  EXTRACT_MIN(Q)    // Pull out new vertex 
    S  ←  S È {u}
    // Perform relaxation for each vertex v adjacent to u 
    for each vertex v in Adj[u] do 
        Relax (u, v, w) 

         The following code performs a relaxation step on edge(u,v).
    Relax (u, v, w)
    If d[u] + w(u, v) < d[v]
        then d[v] d[u] + w[u, v]

*/
        public void Dijkstra()
        {
        }

        /*
         * Bellman-Ford algorithm solves the single-source shortest-path problem in the general case in which edges of a given digraph can have negative weight as long as G contains no negative cycles.

BELLMAN-FORD (G, w, s)
INITIALIZE-SINGLE-SOURCE (G, s) 
for each vertex i = 1 to V[G] - 1 do 
    for each edge (u, v) in E[G] do 
        RELAX (u, v, w) 
For each edge (u, v) in E[G] do 
    if d[u] + w(u, v) < d[v] then 
        return FALSE 
return TRUE 
*/
        public void BellmanFord()
        {
        }

        static void Main(string[] args)
        {
            /*    
            //int[] temp = new int[2];    
            PQueueItem a = new PQueueItem();
            PQueueItem b = new PQueueItem(3,temp);
            PQueueItem c = new PQueueItem(1, temp);
            PQueueItem d = new PQueueItem(0, temp);
            PQueueItem e = new PQueueItem();
            PQueueItem f = new PQueueItem(1, temp);
            PQueue Q = new PQueue();
            Q.Enqueue(a);
            Q.Enqueue(b);
            Q.Enqueue(c);
            Q.Enqueue(d);
            Q.Enqueue(e);
            Q.Enqueue(f);
            Console.WriteLine("{0}", Q.Dequeue().Priority);
            Console.WriteLine("{0}", Q.Dequeue().Priority);
            Console.WriteLine("{0}", Q.Dequeue().Priority);
            Console.WriteLine("{0}", Q.Dequeue().Priority);
            Console.WriteLine("{0}", Q.Dequeue().Priority);
            Console.WriteLine("{0}", Q.Dequeue().Priority);
        
                List<Node> NodeList = new List<Node>();
                NodeList.Add(new Node(1));
                NodeList.Add(new Node(2));
                NodeList.Add(new Node(3));
                NodeList.Add(new Node(4));
                NodeList.Add(new Node(5));
                Console.WriteLine("{0}", NodeList[0].id);

                NodeList.Add(new Node(6));
                NodeList.Add(new Node(7));
                NodeList.Add(new Node(8));
                NodeList.Add(new Node(9));
                NodeList.Add(new Node(10));
                NodeList.Add(new Node(11));
             
                //adjacency list has the cost associated with the edge
       
                Dictionary<int, List<int[]>> adjacencyList = new Dictionary<int,List<int[]>>();
                //This is an undirected graph, hence if 1 is connected to 2 then 2 is connected to 1 as well
                adjacencyList.Add(1, new List<int[]> { new int[2] { 2, 1 }, new int[2] { 3, 2 }, new int[2] { 4, 1 } });
                adjacencyList.Add(2, new List<int[]> { new int[2] {3,2},new int[2] { 5,1},new int[2] { 1,2} });
                adjacencyList.Add(3, new List<int[]> { new int[2] { 2, 3 }, new int[2] { 5, 1 }, new int[2] { 1, 1 } });
                adjacencyList.Add(4, new List<int[]> { new int[2] {1,2} });
                adjacencyList.Add(5, new List<int[]> { new int[2]{3,1}, new int[2]{2,2} });
                adjacencyList.Add(6, new List<int[]> { new int[2] {4,2} });
                adjacencyList.Add(7, new List<int> { 8 });
                adjacencyList.Add(8, new List<int> { 9 });
                adjacencyList.Add(9, new List<int> { 10 });
                adjacencyList.Add(10, new List<int> { 11 });
                adjacencyList.Add(11, new List<int> { 6 });
                */
            MinSpanningTree minSpanT = new MinSpanningTree();
            minSpanT.Kruskal();
            //p.DFS(NodeList, adjacencyList);

            Console.ReadLine();
        }
    }
}

/*
Sample C++ for Kruskal
 
07 #define MAX 1001 

08   

09 // ( w (u, v) ) format 
 * * define edge pair< int, int > 


10 vector< pair< int, edge > > GRAPH, MST; 

11 int parent[MAX], total, N, E; 

12   

13 int findset(int x, int *parent) 

14 { 

15     if(x != parent[x]) 

16         parent[x] = findset(parent[x], parent); 

17     return parent[x]; 

18 } 

19   

20 void kruskal() 

21 { 

22     int i, pu, pv; 

23     sort(GRAPH.begin(), GRAPH.end()); // increasing weight 

24     for(i=total=0; i<E; i++) 

25     { 

26         pu = findset(GRAPH[i].second.first, parent); 

27         pv = findset(GRAPH[i].second.second, parent); 

28         if(pu != pv) 

29         { 

30             MST.push_back(GRAPH[i]); // add to tree 

31             total += GRAPH[i].first; // add edge cost 

32             parent[pu] = parent[pv]; // link 

33         } 

34     } 

35 } 

36   

37 void reset() 

38 { 

39     // reset appropriate variables here 

40     // like MST.clear(), GRAPH.clear(); etc etc. 

41     for(int i=1; i<=N; i++) parent[i] = i; 

42 } 

43   

44 void print() 

45 { 

46     int i, sz; 

47     // this is just style... 

48     sz = MST.size(); 

49     for(i=0; i<sz; i++) 

50     { 

51         printf("( %d", MST[i].second.first); 

52         printf(", %d )", MST[i].second.second); 

53         printf(": %d\n", MST[i].first); 

54     } 

55     printf("Minimum cost: %d\n", total); 

56 } 

57   

58 int main() 

59 { 

60     int i, u, v, w; 

61   

62     scanf("%d %d", &N, &E); 

63     reset(); 

64     for(i=0; i<E; i++) 

65     { 

66         scanf("%d %d %d", &u, &v, &w); 

67         GRAPH.push_back(pair< int, edge >(w, edge(u, v))); 

68     } 

69     kruskal(); // runs kruskal and construct MST vector 

70     print(); // prints MST edges and weights 

71   

72     return 0; 

73 } 

*/