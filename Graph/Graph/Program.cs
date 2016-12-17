using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Program to check if a grap is bipartite 
namespace Graph
{
    public enum Color
        {
            White,
            Gray,
            Black
        };
    public class Node
    {
        
        public int id {get;set;}
        public Color color { get; set; }//used in dfs and bfs
        //When vertex v is changed from white to gray the time is recorded in arrT; when vertex v is changed from gray to black the time is recorded in depT. 
        public int arrivalT{ get; set; }//used in dfs v2
        public int depT { get; set; }//used in dfs v2
        public static int timeCounter=0;//static counter to capture arrival and dep T. Used in dfsv2
        public Node parentNode;
        public int BFSLevel;
        public int BFSLabel;//used only for connected components - all connected components have same label.
        public static int BFSLabelCounter = 0;//static counter to capture Label. used for connected components
        
        public Node(int iId )
        {
            id = iId;
            color = Color.White;
            arrivalT = int.MaxValue;
            depT = int.MaxValue;
            parentNode = null;
            BFSLevel = 0;//level starts at 1; 0 means the node is not visited in bfs yet
            BFSLabel =0;// label starts at 1; 0 means node is not visited yet
        }
    }
    class Program
    {
        /*
         * DFS (V, E)
 
                1.     for each vertex u in V[G]
                 2.        do color[u] ← WHITE
                 3.                π[u] ← NIL//calculates parent is null if root
                 4.     time ← 0
                 5.     for each vertex u in V[G]
                 6.        do if color[u] ← WHITE
                 7.                then DFS-Visit(u)              ▷ build a new DFS-tree from u
 
 
 
                DFS-Visit(u)
 
                1.     color[u] ← GRAY                         ▷ discover u
                 2.     time ← time + 1
                 3.     d[u] ← time
                 4.     for each vertex v adjacent to u     ▷ explore (u, v)
                 5.        do if color[v] ← WHITE
                 6.                then π[v] ← u
                 7.                        DFS-Visit(v)
                 8.     color[u] ← BLACK
                 9.     time ← time + 1
                 10.   f[u] ← time                                 ▷ we are done with u

         */ 
          
        public void DFSVisit(List<Node> pNodeList, Dictionary<int, List<int>> pAdjacencyList, Node pCurNode)
        {
            if (pNodeList == null || pAdjacencyList == null || pCurNode == null) return;
            pCurNode.color = Color.Gray;
            pCurNode.arrivalT = ++Node.timeCounter;
            Console.WriteLine("Node visisted {0}", pCurNode.id);
            //Get adjacency list for Node
            List<int>NodeList = pAdjacencyList[pCurNode.id];
            foreach (int id in NodeList)
            {
                //Get Node
            /*    Node node = pNodeList.First( 
                    delegate(Node n)
                    {
                        return n.id == id;
                    }
                ); 
             * */
                Node adjNode = (from n in pNodeList
                            where n.id ==id
                            select n).FirstOrDefault();
                //TODO why are we checking for parent Node (called predecessor in algorithm text) http://www.personal.kent.edu/~rmuhamma/Algorithms/MyAlgorithms/GraphAlgor/depthSearch.htm
                if (adjNode.color == Color.Gray && adjNode.parentNode != pCurNode)
                    Console.WriteLine("Cycle detected with nodes {0} and {1}", adjNode.id, pCurNode.id);
                if (adjNode.color == Color.White)
                {
                    adjNode.parentNode = pCurNode;
                    DFSVisit(pNodeList, pAdjacencyList, adjNode);
                }
                
            }
            pCurNode.color = Color.Black;
            pCurNode.depT = ++Node.timeCounter;

        }
          
        public void DFS(List<Node> pNodeList, Dictionary<int, List<int>> pAdjacencyList)
        {
            if (pNodeList == null || pAdjacencyList == null) return;
            foreach(Node curNode in pNodeList)
            {
                if (curNode.color == Color.White)
                {
                    DFSVisit(pNodeList, pAdjacencyList, curNode);
                    //DFSVisitAndLabelEdges(pNodeList, pAdjacencyList, curNode);
                }
            }
        }
        /*
         * Parenthesis Theorem    For all u, v, exactly one of the following holds:
 
1. d[u] < f[u] < d[v] < f[v] or d[v] < f[v] < d[u] < f[u] and neither of u and v is a descendant of the other.
 2. d[u] < d[v] < f[v] < f[u] and v is a descendant of u.
 3. d[v] < d[u] < f[u] < f[v] and u is a descendant of v.
  
So, d[u] < d[v] < f[u] < f[v] cannot happen. Like parentheses: ( ) [], ( [ ] ), and  [ ( ) ] are OK but ( [ ) ] and [ ( ] ) are not OK.
*/
        public void CheckDescendent()
        {
            //TODO
        }
        /*
         * DFS-Visit can be modified to classify the edges of a directed graph during the depth first search:
 
        DFS-Visit(u)         ▷ with edge classification. G must be a directed graph
 
        1.        color[u] ← GRAY
         2.        time ← time + 1
         3.        d[u] ← time
         4.        for each vertex v adjacent to u
         5.            do if color[v] ← BLACK
         6.                then if d[u] < d[v]   //D[] denotes arrival time
         7.                            then Classify (u, v) as a forward edge
         8.                            else Classify (u, v) as a cross edge
         9.                        if color[v] ← GRAY
         10.                            then Classify (u, v) as a back edge
         11.                      if color[v] ← WHITE
         12.                            then π[v] ← u
         13.                                    Classify (u, v) as a tree edge
         14.                                    DFS-Visit(v)
         15.        color[u] ← BLACK
         16.        time ← time + 1
         17.        f[u] ← time
        In a depth-first search of an undirected graph G, every edge in E[G] is either a tree edge or a back edge. No forward or cross edges.
         */
        public void DFSVisitAndLabelEdges(List<Node> pNodeList, Dictionary<int, List<int>> pAdjacencyList, Node pCurNode)
        {
            if (pNodeList == null || pAdjacencyList == null || pCurNode == null) return;
            //Assume Directed graph; if undirected there is no forward or cross edge
            pCurNode.color = Color.Gray;
            pCurNode.arrivalT = ++Node.timeCounter;
            //Console.WriteLine("Node visisted {0}", pCurNode.id);
            //Get adjacency list for Node
            List<int> NodeList = pAdjacencyList[pCurNode.id];
            foreach (int id in NodeList)
            {
                //Get Node
                Node adjNode = (from n in pNodeList
                                where n.id == id
                                select n).FirstOrDefault();
                if (adjNode.color == Color.Black)
                {
                    if(pCurNode.arrivalT< adjNode.arrivalT)
                        Console.WriteLine(" ({0}, {1}) is a forward edge", pCurNode.id, adjNode.id);
                    else Console.WriteLine(" ({0}, {1}) is a cross edge", pCurNode.id, adjNode.id);
                }
                if (adjNode.color == Color.Gray)
                    Console.WriteLine("({0}, {1}) is a back edge", pCurNode.id,adjNode.id);
                if (adjNode.color == Color.White)
                {
                    adjNode.parentNode = pCurNode;
                    Console.WriteLine("({0}, {1}) is a tree edge", pCurNode.id, adjNode.id);
                    DFSVisitAndLabelEdges(pNodeList, pAdjacencyList, adjNode);
                }

            }
            pCurNode.color = Color.Black;
            pCurNode.depT = ++Node.timeCounter;

        }

        

        /*BFS(G)
         * mark each vertex in V as 0
         * count=0
         * for each vertex in v do
         * if v is marked with 0
         * bfsV(v)
         * 
         * bfsV(v)
         * count =count+1
         * mark v with count iniialize q with v
         * while queue not empty
         * for each vertex w in V adj to front vertex
         * if w marked with 0
         * count = count+1
         * add w to queue
         * remove the front vertex from front queue
         * 
         *  O(num edges+num vertices)
         *  
         * Finding total number of forests or find connected components in linear time lecture 26
         *Initialize all labels to 0. pick a vertex with label=0 and label is ++CurCount. do bfs - all the vertex are labelled with a number= curCount. Continue doing untill labels of all vertex are >0         * 
         * Bi partitite and connected components are applications of bfs
         * G is bipartite if there exist a partition of V into two sets U,W such that every edge has one end point in U and another in V.** Do a BFS starting at any vertex. BFS will divide the graph into levels. All edges of original graph go between adjacent levels or within the level. There cannot be any edges that jump levels.
         *In BFS if no edge has both end point in same level => graph is bi partite assuming connected graph
         * * */


        public void BFS(List<Node> pNodeList, Dictionary<int, List<int>> pAdjacencyList)
        {
            if (pNodeList == null || pAdjacencyList == null) return;
            Queue<Node> qBFS = new Queue<Node>();
            foreach (Node curNode in pNodeList)
            {
                if (curNode.BFSLevel == 0)
                    BFSVisit(pNodeList, pAdjacencyList, curNode,qBFS);
            }


        }
        //level starts at 1 (distinguish from 0 which is visited
        public void BFSVisit(List<Node> pNodeList, Dictionary<int, List<int>> pAdjacencyList, Node pCurNode, Queue<Node> pQBFS)
        {
            if (pNodeList == null || pAdjacencyList == null || pCurNode == null) return;
            pCurNode.BFSLevel = 1;
            pCurNode.color = Color.Gray;
            Node.BFSLabelCounter++;
            pCurNode.BFSLabel = Node.BFSLabelCounter;
            pCurNode.parentNode = null;
            pQBFS.Enqueue(pCurNode);
            while (pQBFS.Count != 0)
            {
                Node currentNode = pQBFS.Dequeue();
                Console.WriteLine("Node visited {0} with level {1} with label {2} with color {3}", currentNode.id, currentNode.BFSLevel, currentNode.BFSLabel, currentNode.color);
                //Get adjacency list for Node
                List<int> NodeList = pAdjacencyList[currentNode.id];
                foreach (int id in NodeList)
                {
                    //returns null if adjacency list empty
                    Node adjNode = (from n in pNodeList
                                 where n.id == id
                                 select n).FirstOrDefault();
                    /*
                     * In other to determine if a graph G = (V, E) is bipartite, we perform a BFS on it with a little modification such that whenever the BFS is at a vertex u and encounters a vertex v that is already 'gray' our modified BSF should check to see if the depth of both u and v are even, or if they are both odd. If either of these conditions holds which implies d[u] and d[v] have the same parity, then the graph is not bipartite. Note that this modification does not change the running time of BFS and remains O(V + E).
                     * * */
                    //Check if adjNode is already gray and both level is even or both level is odd 
                    if (adjNode!=null && adjNode.color ==Color.Gray && ((adjNode.BFSLevel%2==0 && currentNode.BFSLevel%2==0) || (adjNode.BFSLevel%2!=0 && currentNode.BFSLevel%2!=0)))
                    {
                        //this mean an odd cycle; hence graph not bipartite
                        Console.WriteLine("Odd Cycle detected. Graph not bipartite");
                        Console.WriteLine("Cycle ids that form the odd cycle {0} {1}. Both have level {2} ", adjNode.id, currentNode.id, adjNode.BFSLevel);

                    }
                    if (adjNode != null && adjNode.BFSLevel == 0)
                    {
                        adjNode.BFSLevel = currentNode.BFSLevel + 1;
                        adjNode.BFSLabel = currentNode.BFSLabel;
                        adjNode.parentNode = currentNode;
                        adjNode.color = Color.Gray;
                        pQBFS.Enqueue(adjNode);
                    }
                }
                currentNode.color = Color.Black;
            }
        }

        /*
         * BFS builds a tree called a breadth-first-tree containing all vertices reachable from s. The set of edges in the tree (called tree edges) contain (π[v], v) for all v where π[v] ≠ NIL.
 
If v is reachable from s then there is a unique path of tree edges from s to v. Print-Path(G, s, v) prints the vertices along that path in O(|V|) time.
 
Print-Path(G, s, v)
 
if v = s
         then print s
         else   if π[v] ← NIL
                 then  print "no path exists from " s "to" v"
                 else   Print-Path(G, s, π[v])
                         print v
*/
        public int PrintPath(Node fromNode, Node toNode, int distance)
        {
            if (fromNode == null || toNode == null) return 0;
            
            //Assume BFS already done so parent is already there
            //This has problem that the parent depends on which node we started BFS from. A better way to do this is to do bfs from fromNode to toNode
            if (fromNode.id == toNode.id)
            {
                Console.WriteLine("{0}", toNode.id);
                ++distance;
            }
            else if (toNode.parentNode == null)
                Console.WriteLine("No path exists");
            else
            {
                Console.WriteLine("{0}", toNode.id);
                ++distance;
                distance = PrintPath(fromNode, toNode.parentNode, distance);
            }
            return distance;
        }



        static void Main(string[] args)
        {
            List<Node> NodeList = new List<Node>();
            NodeList.Add(new Node(1));
            NodeList.Add(new Node(2));
            NodeList.Add(new Node(3));
            NodeList.Add(new Node(4));
            NodeList.Add(new Node(5));
            NodeList.Add(new Node(6));
            NodeList.Add(new Node(7));
            NodeList.Add(new Node(8));
            NodeList.Add(new Node(9));
            NodeList.Add(new Node(10));
            NodeList.Add(new Node(11));
            Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();
            //This is an undirected graph, hence if 1 is connected to 2 then 2 is connected to 1 as well
            adjacencyList.Add(1, new List<int> {2,3,4 });
            adjacencyList.Add(2, new List<int> { 3,5,1 });
            adjacencyList.Add(3, new List<int> { 2,5,1 });
            adjacencyList.Add(4, new List<int> { 1 });
            adjacencyList.Add(5, new List<int> { 3,2 });
            adjacencyList.Add(6, new List<int> { 7,11 });
            adjacencyList.Add(7, new List<int> { 8 });
            adjacencyList.Add(8, new List<int> { 9 });
            adjacencyList.Add(9, new List<int> { 10 });
            adjacencyList.Add(10, new List<int> { 11 });
            adjacencyList.Add(11, new List<int> {6 });

            Program p = new Program();
            //p.DFS(NodeList, adjacencyList);
            p.BFS(NodeList, adjacencyList);


            Node fromNode = (from n in NodeList
                            where n.id == 6
                            select n).FirstOrDefault();
            Node toNode = (from n in NodeList
                            where n.id == 10
                            select n).FirstOrDefault();
            int distance  = p.PrintPath(fromNode, toNode,0);// You should do bfs before calling this fn. if you use DFS, then result is not the shortest path - path from 1 to 5 goes thru 2 and 3
            Console.WriteLine("distance between {0} and {1} is {2}", fromNode.id, toNode.id, distance);
            //Console.WriteLine("Diameter of graph is {0}", p.FindGraphDiameter(NodeList));
            Console.ReadLine();
        }
    }
}
