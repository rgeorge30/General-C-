/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package TicTacToe;

/**
 *
 * @author taufiq
 */

public class Board {
    private int intBoard[]= new int[9];
    private int intMoveSymbol=2; //0=empty;1=Player1;2=Player2
    public int moveCount=0;
    public Board(){
        for (int x =0;x<=8;x++){
            intBoard[x]=0;
        }
    }
    public Board(int[] intBoard){
        for (int x =0;x<=8;x++){
            this.intBoard[x]= intBoard[x];
        }      
    }
   public Board(Board board){
        this.moveCount=board.moveCount;
        this.intMoveSymbol=board.intMoveSymbol;
        System.arraycopy(board.intBoard, 0,this.intBoard  , 0, 9);
    }
    public void copy(Board board){
        this.moveCount=board.moveCount;
        this.intMoveSymbol=board.intMoveSymbol;
        System.arraycopy(board.intBoard, 0,  this.intBoard, 0, 9);
    }
    public boolean SetMove(int boxNo){
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
            System.out.println();
            for (int y=0;y<3;y++){
                System.out.print("|");
                if (intBoard[y+(3*x)]!=0)
                    if (intBoard[y+(3*x)]==1 ){
                        System.out.print("[X]");
                    }else
                        System.out.print("[0]");
                else
                    System.out.print( " "+ (y+(3*x)) + " ");
                System.out.print("|");
                    
            }
        }

    }

    

}
