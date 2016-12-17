using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace topological_sort
{
    class Program
    {
        /*
         * TOPOLOGICAL-SORT(V, E)
 
1. Call DFS(V, E) to compute finishing times f[v] for all v in V
2.  Output vertices in order of decreasing finish times
One way to get a topological sort of a DAG is to run a depth-first search and then order the vertices so their f time-stamps are in descending order. This requires Θ(|V| lg |V|) time if a comparison sort algorithm is used.
 
One can modify the depth-first search of a DAG to produce a topological sort. When a vertex changes to black push it on a stack or put it on the front of a linked list. In which case, the running time is Θ(|V| + |E|).

        
        public void TopologicalSort(List<Node> pNodeList)
        {
            //Assu
        }
         * */
        /* STRONGLY-CONNECTED-COMPONENTS (G)
 
 1. Call DFS(G) to compute finishing times f[u] for all u.
  2. Compute GT
  3. Call DFS(GT), but in the main loop, consider vertices in order of decreasing f[u] (as computed in first DFS)
  4. Output the vertices in each tree of the depth-first forest formed in second DFS as a separate SCC.

the transpose of G, is defined as:
 •GT = (V, ET), where ET = {(u, v): (v, u) in E}.
•GT is G with all edges reversed.
        From the given graph G, one can create GT in linear time (i.e., Θ(V + E)) if using adjacency lists.
Idea:    By considering vertices in second DFS in decreasing order of finishing times from first DFS, we are visiting vertices of the component graph in topological sort order.
         */
        public void StronglyConnectedComp()
        {
        }


        /*
         *Part 2 Find an Euler tour of given graph G if one exists.
ALGORITHM
Given a starting vertex , the v0 algorithm will first find a cycle C starting and ending at  v0 such that C contains all edges going into and out of  v0. 
         * This can be performed by a walk in the graph. As we discover vertices in cycle C, we will create a linked list which contains vertices in order and such that the list begins and ends in vertex  v0. 
         * We set the current painter to the head of the list. We now traverse the list by moving our pointer "current" to successive vertices until we and a vertex which has an outgoing edge which has not been discovered. (If we reach the end of the list, then we have already found the Euler tour). Suppose we find the vertex,  vi, that has an undiscovered outgoing edge. We then take a walk beginning and ending at  vi such that all undiscovered edges containing vi are contained in the walk. We insert our new linked list into old linked list in place of vi and more "current" to the new neighbor pointed to the first node containing vi. We continue this process until we search the final node of the linked list, and the list will then contains an Euler tour.
Running Time of Euler Tour
The algorithm traverse each edge at most twice, first in a walk and second while traversing the list to find vertices with outgoing edges. Therefore, the total running time of the algorithm is O(|E|).
         */
        static void Main(string[] args)
        {
        }
    }
}
