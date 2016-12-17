using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Alpha_beta_pruning_and_tictactoe
{
public class Node {

    public int moveBox;
    public int point;
    public Board board;
    public Node parent;
    public List<Node> childrenNode = new List<Node>();

    public Node() {
        this.board = new Board();
    }

    public Node(Board board) {
        this.board = board;
    }

    public void AddChildren(Node node) {
        childrenNode.Add(node);
        node.Parent(this);
    }

    private void Parent(Node node) {
        this.parent = node;
    }

    public void copy(Node n) {
        this.point=n.point;

        this.board.copy(n.board);

    }

    public void Print() {
        Console.WriteLine();
        Console.WriteLine("Point" + this.point);
        Console.WriteLine("moveBox" + this.moveBox);
        this.board.Print();
        Console.WriteLine();
    }

}

}
