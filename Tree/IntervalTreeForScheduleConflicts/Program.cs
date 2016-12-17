using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Modify Binary search tree BST to work as interval tree so it is easy to find overlaps (schedule conflict). Each node stores the interval (low val, high val)as well as the maximum value in the sub tree
//This interval tree stores free times, so you can simply search for an available slot and get it.
//TODO only insert and search modified - delete has not yet been modified
namespace Tree
    {
    class treeNode
        {
        public int lowValue;
        public int highValue;
        public int maxValue;//m = max{high point of interval(x) or m [left[ x]] or m[right[ x]]}
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
        //fix m on the way down (while node is finding the right location) 
        public void InsertIntoTree(int pVal, int pHighVal,treeNode node)
            {
            treeNode tmp;
            if (node == null)
                return;//error
            if (pVal > node.lowValue)
                {
                //update node's mValue
                if (node.maxValue < pHighVal)
                    node.maxValue = pHighVal;

                if (node.right == null)
                    {
                    tmp = new treeNode();
                    tmp.lowValue = pVal;
                    tmp.highValue = pHighVal;
                    tmp.maxValue = pHighVal;
                    tmp.left = null;
                    tmp.right = null;
                    node.right = tmp;

                    }
                else InsertIntoTree(pVal, pHighVal, node.right);
                }
            else if (pVal <= node.lowValue)
                {
                //update node's mValue
                if (node.maxValue < pHighVal)
                    node.maxValue = pHighVal;

                if (node.left == null)
                    {
                    tmp = new treeNode();
                    tmp.lowValue = pVal;
                    tmp.highValue = pHighVal;
                    tmp.maxValue = pHighVal;
                    tmp.left = null;
                    tmp.right = null;
                    node.left = tmp;

                    }
                else InsertIntoTree(pVal, pHighVal, node.left);
                }
            else
                {
                Console.WriteLine("Issue with insert {0} {1}", pVal, pHighVal);
                return; //error does not take care of situation of duplicate lowValues
                }

            }
        public void BuildTree(int[] pVal, int[] pHighVal)
            {
            //build binary search tree
            if (pVal == null || pVal.Length == 0)
                root = null;
            for (int i = 0; i < pVal.Count(); i++ )
                {
                if (root == null)
                    {
                    root = new treeNode();
                    root.lowValue = pVal[i];
                    root.highValue = pHighVal[i];
                    root.left = null;
                    root.right = null;
                    }
                else
                    {
                    InsertIntoTree(pVal[i],pHighVal[i], root);
                    }

                }
            }
        public treeNode GetRoot()
            {
            return root;
            }
        public void InOrderSearch(treeNode pNode)
            {
            if (pNode == null)
                return;
            InOrderSearch(pNode.left);
            Console.WriteLine("Low Value {0} High Value {1}" , pNode.lowValue, pNode.highValue);
            InOrderSearch(pNode.right);

            }
       
        //This function prints out all intervals that overlaps pLowVal and pHighVal
        public void IntervalSearch(treeNode node, int pLowVal, int pHighVal)
            {
            if (node==null) return;
            CheckOverlap(node,pLowVal,pHighVal); //check overlap with current node)
            //go left if plowVal<node.lowVal || node.left.m>plowVal - the second condition exist because the high value could exist in left; added= afterwards for duplicate support
            if(node.left!=null && (pLowVal<=node.lowValue ||node.left.maxValue >= pLowVal))
                    IntervalSearch(node.left, pLowVal, pHighVal);
            //go right if plowVal>node.lowVal;added= afterwards for duplicate support
            if (node.right != null && pLowVal >= node.lowValue)
                    IntervalSearch(node.right, pLowVal, pHighVal);
            }
        //check whether current node overlaps and add to output
        public void CheckOverlap(treeNode node, int pLowVal, int pHighVal)
            {
            if(node.lowValue>pLowVal && node.lowValue<pHighVal || node.highValue>pLowVal && node.highValue <pHighVal) 
            Console.WriteLine("Overlap found {0} {1}", node.lowValue, node.highValue);
            //TODO take care of = ; if they start or end at the same time there need not be an overlap, so need to take care of this.
            }
        }
    class Program
        {
        static void Main(string[] args)
            {
            Tree tree = new Tree();
            tree.BuildTree(new int[] { 7, 4, 9, 10, 55, 77, 44, 33, 1, 8 ,9}, new int[] { 9, 6, 11, 13, 58, 79, 49, 39, 12, 81,16 });
            tree.InOrderSearch(tree.GetRoot());
            tree.IntervalSearch(tree.GetRoot(), 8, 15);
            Console.ReadKey();
            }
        }
    }
