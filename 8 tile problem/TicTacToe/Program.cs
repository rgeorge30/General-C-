using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Problem statement: Given [G,S,T] find min cost solution tree; where
•	G implicitly specified AND/OR graph
•	S: startnode of AND/OR graph
•	T: set of terminal nodes (set of subproblems directly solvable)
•	h(n) heuristic function estimating the cost of solving sub problem at n
Solution: AO*
1.	Initialize: G* = {S}, f(s) = h(s)//don’t have any other cost at this time
a.	if s belongs to T label as SOLVED
2.	Terminate: If s is SOLVED, then terminate
3.	Select: 	Select a non-terminal leaf node n from the marked sub tree
4.	Expand: Make explicit the successors of n 
a.	For each new successor m:
i.	set f(m) = h(m)
ii.	if m is terminal label m as SOLVED
5.	Cost revision: call cost-revise(n)//compute cost of parents from successors
6.	Loop: Go to step 2
Cost_Revise function(n)
1.	Create Z = {n}
2.	if Z = {} return //empty nothing more to solve
3.	select a node m from Z such that m has no descendants in Z (go bottom up from child to parent)
4.	If m is an AND node with successors r1,r2..rk then f(m) = sigma(f(ri)+ c(m,ri)) // sigma means sum of all edges
a.	Mark the edge to each successor of m
b.	If each successor is labeled SOLVED then label m as SOLVED
5.	If m is an OR node with successors r1,r2..rk then f(m) = min(f(ri)+ c(m,ri)) 
a.	Mark the edge to best successor of m
b.	If marked successor is labeled SOLVED then label m as SOLVED
6.	If the cost or label of m has changed, then insert those parents of m into Z for which m is a marked successor

 * 
 * MiniMax alg: Computer chooses MAX (score of 1) and human chooses MINI(score of -1) (or 0 which is a draw.). Any other non-leaf 
 * score captured using Minimax (use heuristic cost). (note:Choose default best move as -2 so atleast one move is picked even if it is a losing move.) 
 * If alpha>=beta, further investigation of current grid is useless. In both min and max nodes, we return when alpha>=beta.
Note: do DFS when doing alphabeta pruning with game trees.
Algorithm:
1.	If J is terminal V(j) = h(j)
2.	If J is max node, 
a.	for each successor Jk of J
i.	set alpha=max(alpha, v(jk)
ii.	if alpha>=beta, return beta else continue
iii.	return alpha
3.	If J is min node, 
a.	for each successor Jk of J
i.	set beta=min(beta, v(jk)
ii.	if alpha>=beta, return beta else continue
iii.	return beta

 */ 
namespace TicTacToe
{
   
    public class Board 
    {
        private int[] intBoard;
        private int intMoveSymbol=2; //0=empty;1=Player1;2=Player2
        public int moveCount=0;
        public Board()
        {
            intBoard = new int[9];
            for (int x =0;x<=8;x++)
                intBoard[x]=0;
        }
        public Board(int[] pBoard)
        {
            intBoard = new int[9];
            for (int x =0;x<=8;x++)
                intBoard[x]= pBoard[x];
        }
       public Board(Board board){
           intBoard = new int[9];
           copy(board);
         }
        public void copy(Board board){
            this.moveCount=board.moveCount;
            this.intMoveSymbol=board.intMoveSymbol;
            for (int x = 0; x <= 8; x++)
                intBoard[x] = board.intBoard[x];           
        }
        public bool CheckValid(int inp)
        {
            if (inp >= 0 && inp < 9 && intBoard[inp] == 0) return true;
            else return false;
        }
        public bool SetMove(int boxNo){
	    //exception if not free
            if (intBoard[boxNo]!=0){
                return false;
            }
            if (intMoveSymbol==1){
                intMoveSymbol=2;
            }else{
                intMoveSymbol=1;
            }
            intBoard[boxNo]=intMoveSymbol;
            moveCount++;
            return true;
        }
        public int CheckCondition()//[1=Player1],[2=Player2],[-1=draw],[0=no condition]
        {

            if (intBoard[0]==intBoard[1] & intBoard[1]==intBoard[2] & intBoard[2]!=0)
                return intBoard[0];
            else if (intBoard[3]==intBoard[4] & intBoard[4]==intBoard[5]& intBoard[5]!=0)
                return intBoard[3];
            else if (intBoard[6]==intBoard[7] & intBoard[7]==intBoard[8]& intBoard[8]!=0)
                return intBoard[6];
        
            else if (intBoard[0]==intBoard[3] & intBoard[3]==intBoard[6]& intBoard[6]!=0)
                return intBoard[0];
            else if (intBoard[1]==intBoard[4] & intBoard[4]==intBoard[7]& intBoard[7]!=0)
                return intBoard[1];
            else if (intBoard[2]==intBoard[5] & intBoard[5]==intBoard[8]& intBoard[8]!=0)
                return intBoard[2];

            else if (intBoard[0]==intBoard[4] & intBoard[4]==intBoard[8]& intBoard[8]!=0)
                return intBoard[0];
            else if (intBoard[1]==intBoard[4] & intBoard[4]==intBoard[7]& intBoard[7]!=0)
                return intBoard[1];
            else if (intBoard[2]==intBoard[4] & intBoard[4]==intBoard[6]& intBoard[6]!=0)
                return intBoard[2];
            else
                for(int x=0;x<9;x++)
                    if (intBoard[x]==0)
                        return 0;

                return -1;
        }

        public void Print(){
            for (int x=0;x<3;x++){
                Console.WriteLine();
                for (int y=0;y<3;y++){
                    Console.Write("|");
                    if (intBoard[y+(3*x)]!=0)
                        if (intBoard[y+(3*x)]==1 ){
                            Console.Write("[X]");
                        }else
                            Console.Write("[0]");
                    else
                        Console.Write( " "+ (y+(3*x)) + " ");
                    Console.Write("|");
                    
                }
            }

        }
}

    class Player
    {
        public string name { get; set; }
        public Player(string pName)
        {
            name = pName;
        }
    }
    public class AI
    {

       // public int nodeCount;
        public Node rootNode;

        public AI()
        {
            Board newB = new Board();
            rootNode = new Node(newB);

        }

        public AI(Board b)
        {
            Board newB = new Board(b);
            rootNode = new Node(newB);
        }

        public int Move()
        {
            int max = -10;
            Node bestNode = new Node();
            for (int x = 0; x <= 8; x++)
            {
                Node n = new Node();
                n.copy(rootNode);
                if (n.board.SetMove(x) == true)
                {
                    rootNode.AddChildren(n);
                    n.moveBox = x;
                    int val = minimaxAB(n, true, 100, -10, 10);
                    if (val >= max)
                    {
                        max = val;
                        bestNode = n;

                    }
                }
            }
            return bestNode.moveBox;

        }


        private int minimaxAB(Node node, bool min, int depth, int alpha, int beta)
        {
            if (boardPoint(node) != -2)
            { //|| depth == 0) {
                //            if (depth <= 0) {
                //                Console.Write("Depth Reached");
                //            }

                node.point = boardPoint(node);
                //node.Print();
                //Console.Writeln(nodeCount++ + " node.point: " + node.point + " Alpha: " + alpha + " Beta: " + beta);

                return boardPoint(node);
            }
            else
            {
                if (min == true)
                {
                    for (int x = 0; x <= 8; x++)
                    {
                        Node n = new Node();
                        n.copy(node);
                        if (n.board.SetMove(x) == true)
                        {
                            node.AddChildren(n);
                            n.moveBox = x;
                            //Console.Writeln("In min:"+ min);
                            int val = minimaxAB(n, false, depth - 1, alpha, beta);

                            if (val < beta)
                            {
                                beta = val;
                                n.parent.point = val;
                            }
                        }
                    }
                    //Console.Writeln("Out min:"+ min);
                    return beta;
                }

                if (min == false)
                {
                    for (int x = 0; x <= 8; x++)
                    {
                        Node n = new Node();
                        n.copy(node);
                        if (n.board.SetMove(x) == true)
                        {
                            node.AddChildren(n);
                            n.moveBox = x;
                            //Console.Writeln("In min:"+ min);
                            int val = minimaxAB(n, true, depth - 1, alpha, beta);

                            if (val > alpha)
                            {
                                alpha = val;
                                n.parent.point = val;
                            }
                        }

                    }
                    //Console.Writeln("Out min:"+ min);
                    return alpha;

                }

            }
            return -100;


        }


        private int boardPoint(Node n)
        {
            if (n.board.CheckCondition() == 1)
            {
                return -1;
            }
            if (n.board.CheckCondition() == 2)
            {
                return 1;
            }
            if (n.board.CheckCondition() == -1)
            {
                return 0;
            }
            return -2;

        }
    }
    public class Node
    {

        public int moveBox;
        public int point;
        public Board board;
        public Node parent;
        public List<Node> childrenNode = new List<Node>();

        public Node()
        {
            this.board = new Board();
        }

        public Node(Board board)
        {
            this.board = board;
        }

        public void AddChildren(Node node)
        {
            childrenNode.Add(node);
            node.Parent(this);
        }

        private void Parent(Node node)
        {
            this.parent = node;
        }

        public void copy(Node n)
        {
            this.point = n.point;

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
    /*
    class AI
    {
        int alphaScore;
        int betaScore;

        public int NextMove(Board board)
        {
            minimaxAB(Board board, bool min, int depth, int alphaScore, int betaScore)            return 
        }
        private int minimaxAB(Board board, bool min, int depth, int alphaScore, int betaScore) 
        {
            //TODO
            
        }

    }
    */
    class Program
    {
        static void Main(string[] args)
        {
            Player P1 = new Player("P1");
            //Board board = new Board();
            AI P2 = new AI();
            string line;
            int inp;
            while (true)
            {
                Console.Write("Enter the positin you want to play (0-8): ");
                line = Console.ReadLine(); // Read string from console
	
	            if (!int.TryParse(line, out inp)) // Try to parse the string as an integer
	            {
	                Console.WriteLine("Not an integer!");
	            }
                else if (P2.rootNode.board.CheckValid(inp))
                {
                    if (P2.rootNode.board.SetMove(inp))
                    {
                        int condition = P2.rootNode.board.CheckCondition();
                        if (condition != 0)
                        {
                            P2.rootNode.board.Print();
                            Console.WriteLine(" Game ended with condition {0}", condition);
                            break;
                        }
                    }
                    //TODO Why should the board not be given as input? 
                    inp = P2.Move();
                    if (P2.rootNode.board.SetMove(inp))
                    {
                        P2.rootNode.board.Print();
                        int condition = P2.rootNode.board.CheckCondition();
                        if (condition != 0)
                        {
                            //P2.rootNode.board.Print();
                            if (condition > 0)
                                Console.WriteLine(" Game ended with Player {0} win", condition);
                            else Console.WriteLine(" Game ended with draw");
                            break;
                        }
                    }
                     
                }
                    
            }
            Console.ReadLine();
        }
    }
}

/*
 
 * Alpha-Beta Pseudocode
Note: Here Black prefers high scores and White prefers low scores. 


minimax (in game board, in int depth, in int max_depth,
         in boolean alpha_beta, in score black_best, in score white_best, 
         out score chosen_score, out score chosen_move)
begin
        :	
        :
        minimax(new_board, depth+1, max_depth,
                alpha_beta, black_best, white_best, 
                the_score, the_move);
        if (alpha_beta) then
            if (to_move(black) and (the_score > black_best))
                if (the_score > white_best)
                    break;  //  alpha_beta cutoff  
                else
                    black_best = the_score;
                endif
            endif
            if (to_move(white) and (the_score < white_best))
                if (the_score < white_best)
                    break;  //  alpha_beta cutoff  
                else
                    white_best = the_score;
                endif
            endif
        endif
        :
        :
end.
Minimax and Alpha-Beta Types and Functions
Any implementation of minimax and alpha-beta must be supplied with the following types and routines. 


board type 
This type contains all information specific to the current state of the game, including layout of the board and current player. 

score type 
This data type indicates piece advantage, strategic advantage, and possible wins. In most games, strategic advantage includes the number of moves available to each player with the goal of minimizing the opponent's mobility. 

neg_infinity and pos_infinity 
The most extreme scores possible in the game, each most disadvantageous for one player in the game. 

generate_moves 
This function takes the current board and generates a list of possible moves for the current player. 

apply_move 
This function takes a board and a move, returning the board with all the updates required by the given move. 

null_move 
If the chosen game allows or requires a player to forfeit moves in the case where no moves are available, this function takes the current board and returns it, after switching the current player. 

static_evaluation 
This function takes the board as input and returns a score for the game. 

compare_scores 
This function takes 2 scores to compare and a player, returning the score that is more advantageous for the given player. If scores are stored as simple integers, this function can be the standard < and > operators. 
*/