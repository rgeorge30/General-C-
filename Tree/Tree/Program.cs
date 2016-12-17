using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//This is Binary search tree BST
namespace Tree
{
    class treeNode
    {
        public int value;
        public treeNode left;
        public treeNode right;
    }
    class Tree
    {
        treeNode root;
        public Tree()
        {
            root = null;
        }
        
        // left is small, right is large - this is IMP
        public void InsertIntoTree(int pVal, treeNode node)
        {
            treeNode tmp;
            if (node == null) return;//error
            if (pVal > node.value)
            {
                if (node.right == null)
                {
                    tmp = new treeNode();
                    tmp.value = pVal;
                    tmp.left = null;
                    tmp.right = null;
                    node.right = tmp;

                }
                else InsertIntoTree(pVal, node.right);
            }
            else if (pVal < node.value)
            {
                if (node.left == null)
                {
                    tmp = new treeNode();
                    tmp.value = pVal;
                    tmp.left = null;
                    tmp.right = null;
                    node.left = tmp;

                }
                else   InsertIntoTree(pVal, node.left);
            }
            else
                return; //value already exist; assume no duplicates

        }
        public void BuildTree(int[] pVal)
        {
            //build binary search tree
            if (pVal == null || pVal.Length == 0) root = null;
            foreach (int i in pVal)
            {
                if (root == null)
                {
                    root = new treeNode();
                    root.value = i;
                    root.left = null;
                    root.right = null;
                }
                else
                {
                    InsertIntoTree(i, root);
                }

            }
        }
        public treeNode GetRoot()
        {
            return root;
        }
        public void InOrderSearch(treeNode pNode)
        {
            if (pNode == null) return;
            InOrderSearch(pNode.left);
            Console.WriteLine(pNode.value);
            InOrderSearch(pNode.right);

        }
        public void PreOrderSearch(treeNode pNode)
        {
            if (pNode == null) return;
            Console.WriteLine(pNode.value);
            PreOrderSearch(pNode.left);
            PreOrderSearch(pNode.right);

        }

        public void PostOrderSearch(treeNode pNode)
        {
            if (pNode == null) return;
            PostOrderSearch(pNode.left);
            PostOrderSearch(pNode.right);
            Console.WriteLine(pNode.value);

        }
        //Hw3Given sorted binary tree and 2 INT return total number of nodes in graph between teh 2 INTs
        public void SearchForINTRange(treeNode pNode, int start, int end)
        {
            //Inorder search finds elements in order, starting with max element; so modify Inorder
            if (pNode == null) return;
            SearchForINTRange(pNode.left, start, end);
            if (pNode.value >= start && pNode.value <= end) Console.WriteLine(pNode.value);
            SearchForINTRange(pNode.right, start, end);

        }
        //Assuming updates to the child's (being promoted) left or right  is done after calling this function
        public void UpdateParent(treeNode parentNode, treeNode node, bool isLeftParent, bool isLeftChild)
        {
            
            if (parentNode == null) return;
            if(node==null) 
            {
                Console.WriteLine(" ERROR!! Null node in Update Parent. No Delete Operation done! ");
                return;
            }
            if (isLeftParent)
                if (isLeftChild)
                {
                    if (node.left !=null) node.left.right = node.right;//TODO do the same for all cases
                    parentNode.left = node.left;
                    
                }
                else
                {
                    if (node.right != null) node.right.left = node.left;
                    parentNode.left = node.right;
                }
            else //isLeftParent==false
                if (isLeftChild)
                {
                    if (node.left != null) node.left.right = node.right;
                    parentNode.right = node.left;
                }
                else
                {
                    if (node.right != null) node.right.left = node.left;
                    parentNode.right = node.right;
                }
            //free node
            node = null;
         }
        public treeNode FindParentOfInorderSucc(treeNode node)
        {
            //Find parent of Inorder successor instead of Inorder succ since we want to deal with cases where inorder succ has a right child
            //Also we know that inorder succ is left child of parent.
            if (node == null || node.left == null) return node;// this should never happen
            treeNode inOrderSuccParent = node;
            while (inOrderSuccParent.left != null && inOrderSuccParent.left.left != null)
            {
                inOrderSuccParent = inOrderSuccParent.left;
            }
            return inOrderSuccParent;
        }
        public void DeleteFromTree(int pVal, treeNode node, treeNode parentNode, bool isLeft)
        {
            //Find the node
            //case 1: no right child: replace the pointer leading to p by p's left child, if it has one, or by a null pointer, if not.
            //case 2: p's right child has no left child: Replace p with right child r
            //case 3: p's right child has a left child: Replace p with inorder successor (node with next biggest number=smallest value in p's right sub tree).
            //We can easily detach inorder successor s from its position:Since s has the smallest value in p's right subtree, s cannot have a left child. Because s doesn't have a left child, we can simply replace it by its right child, if any. This is the mirror image of case 1
             if (node == null) return;//error
             if (pVal < node.value)
             {
                 if (node.left != null)
                 DeleteFromTree(pVal, node.left, node, true);
             }
             else if (pVal > node.value)
             {
                 if (node.right != null)
                 DeleteFromTree(pVal, node.right, node, false);
             }
             else
             {
                 //found node
                 if (node.right == null)
                    //case1 replace the pointer leading to p by p's left child
                    UpdateParent(parentNode, node, isLeft, true);
                 else if (node.right.left == null)
                    //case2 replace the pointer leading to p with right child r
                    UpdateParent(parentNode, node, isLeft, false);
                 else
                 {
                     //case3 Replace the pointer leading to p with inorder successor (node with next biggest number=smallest value in p's right sub tree).
                     treeNode inOrderSuccParent = FindParentOfInorderSucc(node.right);
                     if (inOrderSuccParent == null || inOrderSuccParent.left == null)
                     {
                         //this should never happen!
                         Console.WriteLine("Error! No inorder successor found. Delete Operation failed");
                         return;
                     }
                     //save inordersuccessor Right
                     treeNode inOrderSuccRight = inOrderSuccParent.left.right;
                     if (isLeft)
                     {
                         parentNode.left = inOrderSuccParent.left;
                         parentNode.left.right = node.right;
                     }
                     else
                     {
                         parentNode.right = inOrderSuccParent.left;
                         parentNode.right.right = node.right; 
                     }
                     //inOrderSucc cannot have a left child; so update left child
                    inOrderSuccParent.left.left = node.left;

                      //IMP: put the right child as the left child of InorderSuc's parent for cases where there is a right child for inorder succ
                     inOrderSuccParent.left = inOrderSuccRight;
                     //free node
                    node = null;
                 }
             }
        }
        //find the common ancestor of x and y: Do tree find;go down the tree until you find a node where you have to choose different path- this node is common ancestor
        public int commonAncestor(treeNode node, int x, int y)
        {
            if (node == null) return -1;//error
            treeNode ancestor = node;
            while (node !=null)
            {
                if (node.value == x || node.value == y) return node.value;
                if (x > node.value && y > node.value) node = node.right;
                else if (x < node.value && y < node.value) node = node.left;
                else return node.value;
            }
            return -1;
        }
    
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            tree.BuildTree(new int[] { 7, 4, 9, 10, 55, 77, 44, 33, 1, 8 });
            tree.InOrderSearch(tree.GetRoot());
            Console.WriteLine("Common ancestor: {0}", tree.commonAncestor(tree.GetRoot(), 1, 55));
            //tree.DeleteFromTree(55, tree.GetRoot(), null, true);
            //tree.InOrderSearch(tree.GetRoot());
            /*tree.SearchForINTRange(tree.GetRoot(), 10, 60);
            tree.InOrderSearch(tree.GetRoot());
            Console.WriteLine("PreOrder");
            tree.PreOrderSearch(tree.GetRoot());
            Console.WriteLine("PostOrder");
            tree.PostOrderSearch(tree.GetRoot());
             * */
            Console.ReadKey();
        }
    }
}
