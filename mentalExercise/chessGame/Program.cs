using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//VIMP: read http://chessprogramming.wikispaces.com/ and http://chessprogramming.wikispaces.com/Bitboards

namespace chessGame
{
    //8x8 with [x,y] position Num assigned to each square, free/busy designation
    //This is the "back end" for the chess engine, which controls how it keeps track of the board and the rules of the game. The very first step to writing a chess engine is to write a complete, bug free board representation that knows every rule of chess. While this can sometimes be a pain, especially implementing the more complicated rules such as castling and repetition draws, it is the backbone of the chess engine, and your engine will not get far without it.
    class ChessBoard
    {

    }
    //base class containingType of chess piece, color of chess piece, current Chessboard position, potential next moves
    class ChessPiece
    {
    }
    class Soldier : ChessPiece { }
    class Bishop: ChessPiece { }
    class Castle : ChessPiece { }
    class Queen: ChessPiece { }
    class King : ChessPiece { }
    class Horse : ChessPiece { }
    //combines the various items for a game
    class Game
    { }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
