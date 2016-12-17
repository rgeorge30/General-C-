using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Important ideas: just keep track of the roots of graphs or trees in an array if you are workign with a forest of graph or trees
//Since the root contains the total frequency, it is very easy to pick the min from the priority queue
namespace HuffmanCode
{
    //Priority Queue for storing trees or leaves

    public class PQueue
    {
        public BinTreeNode[] pQ;//contains roots of the forest of trees
        int numElem;
        public PQueue()
        {
            pQ = new BinTreeNode[40];
            numElem = 1;//first element stored at 1
        }
        public void Enqueue(BinTreeNode item)
        {
            int index = numElem;
            if (item == null || index >= 39) return;// first element stored at 1

            while (index > 1 && item.kvp.Value < pQ[index / 2].kvp.Value)
            {
                pQ[index] = pQ[index / 2];
                index = index / 2;
            }
            pQ[index] = item;
            numElem++;
        }
        public BinTreeNode Dequeue()
        {
            if (pQ[1] == null || numElem <= 1) return null;
            BinTreeNode itemtoBeReturned = pQ[1];
            BinTreeNode lastElement = pQ[--numElem];
            int childIndex;
            int i;
            for(i =1; i*2<=numElem; i =childIndex)
            {
            //find smaller child - important step
            childIndex =i*2;
            if(childIndex!=numElem)
                if(pQ[childIndex+1].kvp.Value < pQ[childIndex].kvp.Value) childIndex++;
            //percolate one level
            if(lastElement.kvp.Value>pQ[childIndex].kvp.Value)
                pQ[i] = pQ[childIndex];
            else break;
            }
            pQ[i] = lastElement;
            pQ[numElem] = null;
            return itemtoBeReturned;
        }
        public BinTreeNode Peek()
        {
            return pQ[1];
        }
    }
    public class BinTreeNode
    {
        public KeyValuePair<char, int> kvp {get;set;}//stores char and its frequency
        public BinTreeNode left;
        public BinTreeNode right;
        public string huffCode;//stores huffmanCode
        public BinTreeNode(KeyValuePair<char, int> pKvp)
        {
            kvp = pKvp;
            left = null;
            right=null;
            huffCode = String.Empty;
        }

    }
    class HuffmanTree
    {
        public BinTreeNode root;
        public BinTreeNode subtreeRoot; //Stores the root of the sub tree - used only in inefficient
        Dictionary<char, string> EncodeDict;
        Dictionary<string,char> DecodeDict;
        public HuffmanTree()
        {
            root = null;
            subtreeRoot = null;
            EncodeDict = new Dictionary<char, string>();
            DecodeDict = new Dictionary<string, char>();
        }
        private BinTreeNode MergeNodes(BinTreeNode node1, BinTreeNode node2)
        {
            BinTreeNode parentNode = new BinTreeNode(new KeyValuePair<char, int>(':', node1.kvp.Value + node2.kvp.Value));//use ':' for denoting internal parent
            //Ensure left node has higher value
            if (node1.kvp.Value > node2.kvp.Value)
            {
                parentNode.left = node1;
                parentNode.right = node2;
            }
            else
            {
                parentNode.left = node2;
                parentNode.right = node1;
            }
            return parentNode;

        }

        //Encode a string using the Encoding dictionary
        public string EncodeString(string input)
        {
            string output = string.Empty;
            foreach (char c in input)
                output += EncodeDict[c];
            return output;

        }

        //decode a string using the encoding dictionary
        public string DecodeString(string input)
        {
            string output = string.Empty;
            string matchString = string.Empty; ;
            foreach (char c in input)
            {
                matchString += c;
                if (DecodeDict.ContainsKey(matchString))
                {
                    output += DecodeDict[matchString];
                    matchString = string.Empty;
                }
            }
            if (matchString != string.Empty) Console.WriteLine("cannot match string {0}", matchString);
            return output;
        }
        //traverse the tree and generate code for each key
        public void TraverseTreeBFSAndGenCode(BinTreeNode node)
        {
            if (node == null) return;
            Queue<BinTreeNode> BFSQueue = new Queue<BinTreeNode>();
            //special case - single node
            if (node.left == null && node.right == null)
            {
                node.huffCode = "1";
                if (node.kvp.Key != ':')
                {
                    if (!EncodeDict.ContainsKey(node.kvp.Key))
                        EncodeDict.Add(node.kvp.Key, node.huffCode);
                    Console.WriteLine("Key {0} Code {1}", node.kvp.Key, node.huffCode);
                }
                return; //added later not verified
            }
            BFSQueue.Enqueue(node);
            BinTreeNode currentNode;
            while (BFSQueue.Count != 0)
            {
                try
                {
                    currentNode = BFSQueue.Dequeue();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("{0}", e);
                    break;
                }
                if (currentNode.kvp.Key != ':')
                {
                    if (!EncodeDict.ContainsKey(currentNode.kvp.Key))
                        EncodeDict.Add(currentNode.kvp.Key, currentNode.huffCode);
                    Console.WriteLine("Key {0} Code {1}", currentNode.kvp.Key, currentNode.huffCode);
                }

                //IMP: CHECK FOR NULL BEFORE ENQUEUEING
                if (currentNode.left != null)
                {
                    //if (currentNode.left.left == null && currentNode.left.right == null)
                    currentNode.left.huffCode = currentNode.huffCode + "0";
                    BFSQueue.Enqueue(currentNode.left);
                }
                if (currentNode.right != null)
                {
                    //  if (currentNode.right.left == null && currentNode.right.right == null)
                    currentNode.right.huffCode = currentNode.huffCode + "1";
                    BFSQueue.Enqueue(currentNode.right);
                }
            }
            Console.WriteLine("Entries in the Encode Dictionary are:");
            foreach (KeyValuePair<char, string> kvp in EncodeDict)
            {
                Console.WriteLine("Key {0} Code {1}", kvp.Key, kvp.Value);
                //Populate Decode dictionary
                DecodeDict.Add(kvp.Value, kvp.Key);
            }

        }

        public void GenerateHuffmanTreev2(WordFreqGenerator wordFreq)
        {
            /*Huffman (c)
            n = |c|
            Q = c
            for  i =1  to   n-1
                do   z = Allocate-Node ()
                         x = left[z] = EXTRACT_MIN(Q)
                         y = right[z] = EXTRACT_MIN(Q)
                        f[z] = f[x] + f[y]
                        INSERT (Q, z)
            return EXTRACT_MIN(Q)
            */
            int n = wordFreq.WordStatsDict.Count();
            PQueue Q = new PQueue();
            BinTreeNode node1;
            BinTreeNode node2;
            BinTreeNode node3;

            //Add each leaf to priority Q
            foreach(KeyValuePair<char, int> kvp in wordFreq.WordStatsDict)
            {
                node1 = new BinTreeNode(kvp);
                Q.Enqueue(node1);
            }
            for (int i = 1; i < n; i++)
            {
                node1 = Q.Dequeue();
                node2 = Q.Dequeue();
                node3 = MergeNodes(node1, node2);
                Q.Enqueue(node3);
            }
            root = Q.Dequeue();
        }
    }
    
     public class WordFreqGenerator
    {
         public Dictionary<char, int> WordStatsDict { get; set; }

        public WordFreqGenerator()
        {
            WordStatsDict = new Dictionary<char, int>();
        }

         //Populate the SortedDictionary using the input string
        public void PopulateDictionary(string pString)
        {
            foreach (char word in pString)
            {
                if (!WordStatsDict.ContainsKey(word))
                {
                    WordStatsDict.Add(word,1);
                }
                else
                {
                    WordStatsDict[word]++;
                }
            }
        }
        public void PrintDictionary()
        {
            Console.WriteLine("List of words with word frequencies :\n");
            foreach (KeyValuePair<char, int> kvp in WordStatsDict)
            {
                Console.WriteLine(" {0}\t {1}",
                    kvp.Key, kvp.Value);
            }
        }
     }
    
    class Program
    {
        static void Main(string[] args)
        {
            WordFreqGenerator freqgen = new WordFreqGenerator();
            //freqgen.PopulateDictionary("aabbccaabacdefabce");
            //freqgen.PrintDictionary();
            
            freqgen.WordStatsDict.Add('a', 45000);
            freqgen.WordStatsDict.Add('b', 13000);
            freqgen.WordStatsDict.Add('c', 12000);
            freqgen.WordStatsDict.Add('d', 16000);
            freqgen.WordStatsDict.Add('e', 9000);
            freqgen.WordStatsDict.Add('f', 5000);
            
            HuffmanTree tree = new HuffmanTree();
            tree.GenerateHuffmanTreev2(freqgen);
            tree.TraverseTreeBFSAndGenCode(tree.root);
           // Console.WriteLine("Encoded string for {0} is {1}", "123456", tree.EncodeString("123456"));
           // Console.WriteLine("Decoded string for {0} is {1}", "011101100010100001", tree.DecodeString("011101100010100001"));
            Console.ReadLine();
        }
    }
}

/*        
        private void InsertHuffman(KeyValuePair<char, int> kvp)
        {
            BinTreeNode newNode = new BinTreeNode(kvp);
            if (root == null) root = newNode;
            else if (newNode.kvp.Value >= root.kvp.Value)
            {
                //if new node value >= current root value, add to root using parent
                root = MergeNodes(root, newNode);
            }
            else//create or add to subtrees as appropriate
            {
                if (subtreeRoot == null) subtreeRoot = newNode;
                else
                {
                    //if new node value >= current root value, add to root using parent
                    subtreeRoot = MergeNodes(subtreeRoot, newNode);
                }
            }
            // if new root of subtree >=root then merge to root and init subtree to null
            if (subtreeRoot!=null && subtreeRoot.kvp.Value >= root.kvp.Value)
            {
                    root = MergeNodes(subtreeRoot, root);
                    subtreeRoot = null;
            }
         }


        //Generate Huffman tree based on <key, frequency>
        public void GenerateHuffmanTree(WordFreqGenerator wordFreq)
        {
            List<char> keyList = new List<char> (); 
            //get min value from dictionary and add to tree
            while(wordFreq.WordStatsDict.Count!=0)
            {
                
                int minFreqCount = (from KeyValuePair<char,int> kvp in wordFreq.WordStatsDict
                 select kvp.Value).Min();
                //Get all kvps with value = minFreqCount; add them to tree and remove them from dict
                var query = (from KeyValuePair<char, int> kvp in wordFreq.WordStatsDict
                             where kvp.Value == minFreqCount
                             select kvp);
                foreach (KeyValuePair<char, int> kvp in query)
                {
                    InsertHuffman(kvp);
                    keyList.Add(kvp.Key);
                }
                foreach(char key in keyList)
                    wordFreq.WordStatsDict.Remove(key);
                //empty the KeyList - IMP: how to remove all elements from a list
                keyList.RemoveAll(c => true);
                
            }
            //Ensure at end there is only one tree under root; set subtree to null
            if (subtreeRoot != null)
            {
                root = MergeNodes(subtreeRoot, root);
                subtreeRoot = null;
            }
        }
         

 * public void PrintTree(BinTreeNode node)
        {
            if (node == null) return;
            if (node.kvp.Key !=':') Console.WriteLine("Key {0} Value {1}", node.kvp.Key, node.kvp.Value);
            PrintTree(node.left);
            PrintTree(node.right);
        }
*/
/*
A quick tutorial on generating a huffman tree
Lets say you have a set of numbers and their frequency of use and want to create a huffman encoding for them: 
	FREQUENCY	VALUE
        ---------       -----
       	     5            1
             7            2
            10            3
            15            4
            20            5
            45            6

Creating a huffman tree is simple. Sort this list by frequency and make the two-lowest elements into leaves, creating a parent node with a frequency 
 * that is the sum of the two lower element's frequencies: 
    	12:*
    	/  \
      5:1   7:2

The two elements are removed from the list and the new parent node, with frequency 12, is inserted into the list by frequency. So now the list, sorted 
 * by frequency, is: 
        10:3
        12:*
        15:4
        20:5
        45:6

You then repeat the loop, combining the two lowest elements. This results in: 
    	22:*
    	/   \
     10:3   12:*
     	    /   \
	  5:1   7:2

and the list is now: 
	15:4
	20:5
	22:*
	45:6

You repeat until there is only one element left in the list. 
    	35:*
    	/   \
      15:4  20:5

      	22:*
      	35:*
      	45:6

      	    57:*
      	___/    \___
       /            \
     22:*          35:*
    /   \          /   \
 10:3   12:*     15:4   20:5
        /   \
      5:1   7:2

	45:6
	57:*

                                   102:*
                __________________/    \__
               /                          \
      	    57:*                         45:6
      	___/    \___
       /            \
     22:*          35:*
    /   \          /   \
 10:3   12:*     15:4   20:5
        /   \
      5:1   7:2

Now the list is just one element containing 102:*, you are done. 
This element becomes the root of your binary huffman tree. To generate a huffman code you traverse the tree to the value you want, outputing a 0 every time you take a lefthand branch, and a 1 every time you take a righthand branch. (normally you traverse the tree backwards from the code you want and build the binary huffman encoding string backwards as well, since the first bit must start from the top). 

Example: The encoding for the value 4 (15:4) is 010. The encoding for the value 6 (45:6) is 1 

Decoding a huffman encoding is just as easy : as you read bits in from your input stream you traverse the tree beginning at the root, taking the left hand path if you read a 0 and the right hand path if you read a 1. When you hit a leaf, you have found the code. 

Generally, any huffman compression scheme also requires the huffman tree to be written out as part of the file, otherwise the reader cannot decode the data. For a static tree, you don't have to do this since the tree is known and fixed. 

The easiest way to output the huffman tree itself is to, starting at the root, dump first the left hand side then the right hand side. For each node you output a 0, for each leaf you output a 1 followed by N bits representing the value. For example, the partial tree in my last example above using 4 bits per value can be represented as follows: 

  000100 fixed 6 bit byte indicates how many bits the value
         for each leaf is stored in.  In this case, 4.
  0      root is a node
         left hand side is
  10011  a leaf with value 3
         right hand side is
  0      another node
         recurse down, left hand side is
  10001  a leaf with value 1
         right hand side is
  10010  a leaf with value 2
         recursion return

So the partial tree can be represented with 00010001001101000110010, or 23 bits. Not bad!
*/