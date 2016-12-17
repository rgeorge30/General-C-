using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8_queeens_problem
    //update 8 tiles problem to solve 8 queens
{
    class Board : IComparable<Board>
    {
        //For simplicity start at index 1 and denote empty using -1
        public int[,] tiles { get; set; }
        public int distFromBegin { get; set; }
        public int ManhattanDistance { get; set; }
        public Board()
        {
            tiles = new int[8,8];
            ManhattanDistance = 0;
            distFromBegin = 0;
        }
        public Board(int[,] ptiles)
        {
            //size =8+ empty+ first location that is not used = 10
            tiles = ptiles;
            ManhattanDistance = 0;
            distFromBegin = 0;
        }

        public override int GetHashCode()
        {
            int hashVal = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
            {
                if (tiles[i,j] >0 )
                    hashVal += (i * j * i * tiles[i,j]) % Int32.MaxValue;
            }
            return hashVal;
        }
        public Board[] GetChildBoards()
        {
            Board[] ChildBoards = new Board[8];
            //TODO
            /*
            Board[] ChildBoards = new Board[8];
            int numChild = 0;
            int blankIndex = 0;
            int i;
            //find where the blank value is
            for (i = 1; i < 10; i++)
                if (tiles[i] == -1) { blankIndex = i; break; }
            if (blankIndex % 3 == 1 || blankIndex % 3 == 2)// modulo 1 or 2
            {
                //can move right
                ChildBoards[numChild] = new Board();
                for (i = 0; i < 10; i++)
                    ChildBoards[numChild].tiles[i] = tiles[i];
                ChildBoards[numChild].tiles[blankIndex] = ChildBoards[numChild].tiles[blankIndex + 1];
                ChildBoards[numChild].tiles[blankIndex + 1] = -1;
                numChild++;
            }
            if (blankIndex % 3 > 1)//modulo 2 or 3
            {
                //can move left
                ChildBoards[numChild] = new Board();
                for (i = 0; i < 10; i++)
                    ChildBoards[numChild].tiles[i] = tiles[i];
                ChildBoards[numChild].tiles[blankIndex] = ChildBoards[numChild].tiles[blankIndex - 1];
                ChildBoards[numChild].tiles[blankIndex - 1] = -1;
                numChild++;
            }
            if (blankIndex / 3 > 0 && blankIndex != 3)
            {
                //can move up
                ChildBoards[numChild] = new Board();
                for (i = 0; i < 10; i++)
                    ChildBoards[numChild].tiles[i] = tiles[i];
                ChildBoards[numChild].tiles[blankIndex] = ChildBoards[numChild].tiles[blankIndex - 3];
                ChildBoards[numChild].tiles[blankIndex - 3] = -1;

                numChild++;
            }
            if (blankIndex / 3 < 2 || blankIndex == 6)
            {
                //can move down
                ChildBoards[numChild] = new Board();
                for (i = 0; i < 10; i++)
                    ChildBoards[numChild].tiles[i] = tiles[i];
                ChildBoards[numChild].tiles[blankIndex] = ChildBoards[numChild].tiles[blankIndex + 3];
                ChildBoards[numChild].tiles[blankIndex + 3] = -1;
                numChild++;
            }
            return ChildBoards;
             * */
            return ChildBoards;
        }
        public int CompareTo(Board other)
        {
            //compare based on total cost defined as sum of manhattan distance and distFromBegin
            // If other is not a valid object reference, this instance is greater. 
            if (other == null) return 1;

            return (ManhattanDistance + distFromBegin).CompareTo(other.ManhattanDistance + other.distFromBegin);
        }

        public void Display()
        {
            Console.WriteLine("Tiles");
            for (int i = 1; i < 8; i++)
                for (int j = 1; j < 8; j++)
            {
                Console.Write("  {0}", tiles[i,j]);
                if (j == 8) Console.WriteLine();
            }
            Console.WriteLine(" Manhattan distance is {0}", ManhattanDistance);
        }
    }
    class EightTileGame
    {
        /** What if tiles can move diagonally?
 * If your map allows diagonal movement you need a different heuristic (sometimes called the Chebyshev distance). The Manhattan distance for (4 east, 4 north) will be 8*D. However, you could simply move (4 northeast) instead, so the heuristic should be 4*D. This function handles diagonals, assuming that both straight and diagonal movement costs D:
h(n) = D * max(abs(n.x-goal.x), abs(n.y-goal.y))
*/
        public int CalculateManhattanDistance(Board b)
        {
            return 1;
            /*
            if (b.tiles == null || b.tiles.Length != 10) return -1;
            int i;
            b.ManhattanDistance = 0;
            int[] manDistColumns = new int[10];//right positive; left negative
            int[] manDistRows = new int[10];//positive down; negative up

            //Calculate Manhattan distance for each tile      Assumes goal is {-1,1,2,3,4,5,6,7,8,-1}
            for (i = 1; i < b.tiles.Length; i++)
            {
                if (b.tiles[i] != -1)
                {
                    //calculate the number of rows and columns that need to be moved
                    int columnCurrent = i % 3;
                    if (columnCurrent == 0) columnCurrent = 3;
                    int rowCurrent = i / 3;
                    if (i % 3 != 0) rowCurrent++;// accomodate for starting with 0

                    int columnTarget = b.tiles[i] % 3;
                    if (columnTarget == 0) columnTarget = 3;
                    int rowTarget = b.tiles[i] / 3;
                    if (b.tiles[i] % 3 != 0) rowTarget++;// accomodate for starting with 0
                    manDistColumns[i] = columnTarget - columnCurrent;
                    manDistRows[i] = rowTarget - rowCurrent;
                }
                else
                {
                    //need not calculate manhattan distance for blank
                    manDistColumns[i] = 0;
                    manDistRows[i] = 0;
                }
            }
            for (i = 0; i < b.tiles.Length; i++)
                b.ManhattanDistance += Math.Abs(manDistColumns[i]) + Math.Abs(manDistRows[i]);
            return b.ManhattanDistance;
             */ 
        }
        /* A* algo
         *maintains 2 lists open and closed ;2 functions: g value computes distance of state from start state, h value computes
         *heuristic estimate of distance of the state from goal state; fs is the sum of gs and hs and this give us the estimated cost of solution
         * 1.	Init: Set OPEN = {S}, closed = {}, g(s) =0, f(s) =h(s) 
a.	g(n) is path cost from s (start) to state n; f(n) = g(n)+h(n)
2.	Fail: If OPEN={} terminate and fail;
3.	Select: Select min cost state, n, from OPEN. Save n in CLOSED
4.	Terminate: if n belongs to G terminate with success and return f(n)
5.	Expand: for each successor m of n  //Expand all adjacent nodes of n by adding to OPEN and updating cost by computing g value and h value of the node
//If it is already in open or closed we update it only if the cost is decreased
a.	if m does not belongs to [OPEN U CLOSED]//means we are visiting for first time
i.	set g(m) = g(n)+C(n,m) //updated cost is cost to n + cost from n to m
ii.	Set f(m) =g(m)+h(m) //update f(m)
iii.	Insert m in OPEN
b.	if m belongs to [OPEN U CLOSED]
i.	set g(m) = min{g(m), g(n)+C(n,m)}//check if we have better path to that state
ii.	Set f(m) =g(m)+h(m)
//if the node is closed and its cost has decreased then we must bring it back to open
c.	if f(m) has decreased and m belongs to CLOSED move m to OPEN
6.	Loop: go to step 2
*/
        public int AStar(Board initialBoard)
        {
            //use priority Q for open OR use sorted list; store hash of board as key 
            SortedList<int, Board> Open = new SortedList<int, Board>();
            SortedList<int, Board> Closed = new SortedList<int, Board>();
            //The final goal state         //Assumes goal is {-1,1,2,3,4,5,6,7,8,-1}
            int[] goaltiles = new int[10] { -1, 1, 2, 3, 4, 5, 6, 7, 8, -1 };
            Board goalboard = new Board(goaltiles);
            CalculateManhattanDistance(initialBoard);
            initialBoard.distFromBegin = 0;
            Open.Add(initialBoard.GetHashCode(), initialBoard);
            while (true)
            {
                if (Open.Count() == 0) break;//2
                //get min total cost from Open //3
                var query = from kvp in Open
                            select kvp.Value;
                Board board = query.Min();
                Open.Remove(board.GetHashCode());
                Closed.Add(board.GetHashCode(), board);//3
                if (board.GetHashCode() == goalboard.GetHashCode())
                {
                    Console.WriteLine("Result from AStar");
                    foreach (KeyValuePair<int, Board> kvp in Closed)
                        kvp.Value.Display();
                    Console.WriteLine("Final Goal");
                    board.Display();
                    return board.distFromBegin;//4
                }
                //5 : Find all child board positions  from "board", find manhattan distance and add to OPEN
                Board[] childBoards = board.GetChildBoards();
                foreach (Board childBoard in childBoards)
                {
                    if (childBoard == null) break;
                    //Check if board in Open or Closed //b.	if m belongs to [OPEN U CLOSED]
                    if (Open.ContainsKey(childBoard.GetHashCode()) || Closed.ContainsKey(childBoard.GetHashCode()))
                    {

                        if (childBoard.distFromBegin > board.distFromBegin + 1)
                        {
                            childBoard.distFromBegin = board.distFromBegin + 1;
                            CalculateManhattanDistance(childBoard);
                            //h(n) which is the manhattan distance remains unchanged; bring back to open if child board is in closed
                            if (Closed.ContainsKey(childBoard.GetHashCode()))
                            {
                                Closed.Remove(childBoard.GetHashCode());
                                Open.Add(childBoard.GetHashCode(), childBoard);
                            }

                        }
                        //Console.WriteLine(" Node already in Open or Closed");
                    }
                    else
                    {
                        //a.	if m does not belongs to [OPEN U CLOSED]//means we are visiting for first time. Calculate distances and insert in open 
                        CalculateManhattanDistance(childBoard);
                        childBoard.distFromBegin = board.distFromBegin + 1;
                        Open.Add(childBoard.GetHashCode(), childBoard);
                    }
                }
            }
            Console.WriteLine("No solution is found by AStar");
            return 0;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            EightTileGame ET = new EightTileGame();
            //ET.CalculateManhattanDistance(board);
            //board.Display();
            int cost = ET.AStar(board);
            Console.WriteLine("AStar cost : {0}", cost);
            /*
            Board[] childB = board.GetChildBoards();
            Console.WriteLine("Child Nodes");
            foreach (Board b in childB)
            {
                b.Display();
                b.GetChildBoards();
            }
             */
            Console.ReadLine();
        }
    }
}
