/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Inteligence;

import TicTacToe.*;
import java.util.*;

/**
 *Alpha Beta
 * @author Taufiq

 */
public class Inteligence {

    public int nodeCount;
    public Node rootNode;

    public Inteligence() {
    }

    public Inteligence(Board b) {
        Board newB = new Board(b);
        rootNode = new Node(newB);

    }


    public int Move() {
        int max = -10;
        Node bestNode= new Node();
        for (int x = 0; x <= 8; x++) {
            Node n = new Node();
            n.copy(rootNode);
            if (n.board.SetMove(x) == true) {
                rootNode.AddChildren(n);
                n.moveBox = x;
                int val = minimaxAB(n, true, 100, -10, 10);
                if (val >= max) {
                    max = val;
                    bestNode = n;
                    
                }
            }
        }
        return bestNode.moveBox;
        
    }


    private int minimaxAB(Node node, boolean min, int depth, int alpha, int beta) {
        if (boardPoint(node) != -2){ //|| depth == 0) {
//            if (depth <= 0) {
//                System.out.print("Depth Reached");
//            }

           node.point = boardPoint(node);
           //node.Print();
           //System.out.println(nodeCount++ + " node.point: " + node.point + " Alpha: " + alpha + " Beta: " + beta);

            return boardPoint(node);
        } else {
            if (min == true) {
                for (int x = 0; x <= 8; x++) {
                    Node n = new Node();
                    n.copy(node);
                    if (n.board.SetMove(x) == true) {
                        node.AddChildren(n);
                        n.moveBox = x;
                        //System.out.println("In min:"+ min);
                        int val = minimaxAB(n, false, depth - 1, alpha, beta);

                        if (val < beta) {
                            beta = val;
                            n.parent.point = val;
                        }
                    }
                }
                //System.out.println("Out min:"+ min);
                return beta;
            }

            if (min == false) {
                for (int x = 0; x <= 8; x++) {
                    Node n = new Node();
                    n.copy(node);
                    if (n.board.SetMove(x) == true) {
                        node.AddChildren(n);
                        n.moveBox = x;
                        //System.out.println("In min:"+ min);
                        int val = minimaxAB(n, true, depth - 1, alpha, beta);

                        if (val > alpha) {
                            alpha = val;
                            n.parent.point = val;
                        }
                    }

                }
                //System.out.println("Out min:"+ min);
                return alpha;

            }

        }
        return -100;


    }


    private int boardPoint(Node n) {
        if (n.board.CheckCondition() == 1) {
            return -1;
        }
        if (n.board.CheckCondition() == 2) {
            return 1;
        }
        if (n.board.CheckCondition() == -1) {
            return 0;
        }
        return -2;

    }
}
