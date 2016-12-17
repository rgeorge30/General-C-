using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*http://www.cs.utah.edu/~hal/courses/2009S_AI/Walkthrough/AlphaBeta/
 * function ALPHA-BETA-SEARCH(state) returns an action 
 * inputs: state, current state in game
 * v← MAX-VALUE(state, -inf, +inf) 
 * return the action in SUCCESSORS(state) with value v
 * 
 * function MAX-VALUE(state, α, β) returns a utility value
 * inputs: state, current state in game
 *       α, the value of the best alternative for MAX along the path to state
 *       β, the value of the best alternative for MIN along the path to state
 * if TERMINAL-TEST(state) then return UTILITY(state)
 * v ← -inf
 * for a, s in SUCCESSORS(state) do
 *          v ← MAX(v, MIN-VALUE(s, α, β))
 *          if v ≥ β then return v
 *          α ← MAX(α, v)
 * return v
 * 
 * function MIN-VALUE(state, α, β) returns a utility value
 * inputs: state, current state in game
 *      α, the value of the best alternative for MAX along the path to state
 *      β, the value of the best alternative for MIN along the path to state
 * if TERMINAL-TEST(state) then return UTILITY(state)
 * v ← +inf
 * for a, s in SUCCESSORS(state) do
 *      v ← MIN(v, MAX-VALUE(s, α, β))
 *      if v ≤ α then return v
 *      β ← MIN(β, v)
 * return v

With optimal ordering, Alpha-Beta Pruning can take the Minimax Algorithm runs in O(b^m) time to O(b^(m/2)) time
 * 
 * */

namespace Alpha_beta_pruning_and_tictactoe
{
    class Program
    {
        
     /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        Game game;
        Console.WriteLine("||||||Unbeatable TicTacToe|||||||");
        Console.Write("Enter Player 1 Name:");

        String playerOneName = Console.ReadLine();
        Console.WriteLine("1) One PLayer Game");
        Console.WriteLine("2) Two PLayer Game");
        Console.Write("Select 1 or 2:");



        if (in.nextLine().equals("2")) {
            Console.Write("Enter Player 2 Name:");

            game = new Game(playerOneName, in.nextLine());
        } else {
            game = new Game(playerOneName);
        }
        do {
            Console.Write("Number of games you want to play:");
            game.outOff = in.nextInt();

            do {
                game.NewBoard();
                int gCon = 0;
                int pNo = 0;
                do {
                    if (pNo == 1) {
                        pNo = 2;
                    } else {
                        pNo = 1;
                    }
                    game.gBoard.Print();
                    Console.WriteLine();
                    Console.Write(game.PlayerName(pNo) + "'s Move:");
                    if (game.twoPlayer == false && pNo == 2) {
                        AI a = (AI) game.player2;
                        int aiMove = a.NextMove(game.gBoard);
                        Console.Write(aiMove);
                        gCon = game.Move(aiMove);
                    } else {
                        gCon = game.Move(in.nextInt());

                    }

                    Console.WriteLine();
                    if (gCon == 3) {
                        Console.WriteLine(":::Wrong Move Try Again:::");
                        if (pNo == 1) {
                            pNo = 2;
                        } else {
                            pNo = 1;
                        }

                    } else if (gCon != 0) {
                        game.gBoard.Print();
                        Console.WriteLine();
                        if (gCon == 1 || gCon == 2) {
                            Console.WriteLine(game.PlayerName(pNo) + " Wins");
                            game.Win(pNo);
                        }
                        if (gCon == -1) {
                            Console.WriteLine(" Draw ");
                        }
                        break;
                    }



                } while (true);
            } while (game.endGame());
            Console.WriteLine(game.PlayerName(1) + " Wins: " + game.Score(1));
            Console.WriteLine(game.PlayerName(2) + " Wins: " + game.Score(2));
            Console.WriteLine("Out off : " + game.outOff + " games");
            game.Reset();
            Console.WriteLine();
            Console.WriteLine("Want lose more?");
            Console.Write("1 to continue or any other key to end:");
            in.nextLine();
        } while (in.nextLine().equals("1"));

    
        }
    }
}
