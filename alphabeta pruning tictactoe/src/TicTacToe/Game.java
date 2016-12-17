/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package TicTacToe;

/**
 *
 * @author taufiq
 */
public class Game {
    public Board gBoard= new Board();
    public Player player1;
    public Player player2;
    public int Player1Score;
    public int Player2Score;
    public boolean twoPlayer;
    public int gameCount;
    public int outOff;

    public Game(String player1Name){
        gBoard =new Board();
        this.player1= new Player(player1Name);
        this.player2= new AI("AI");
        this.twoPlayer=false;
   }
    public Game(String player1Name,String player2Name){
        gBoard =new Board();
        this.player1= new Player(player1Name);
        this.player2= new Player(player2Name);
        this.twoPlayer=true;
   }
    public void NewBoard(){
        gBoard=new Board();
        gameCount++;
    }
    public int Move(int box){
        if (gBoard.SetMove(box)==false)
            return 3;
        return gBoard.CheckCondition();       
    }
    public String PlayerName(int playerNo){
        if (playerNo==1)
           return player1.playerName;
        return player2.playerName;
    }
    public void Win(int playerNo){
        if (playerNo==1)
            this.Player1Score++;
        else
            this.Player2Score++;

    }
    public int Score(int playerNo){
        if (playerNo==1)
           return this.Player1Score;
        else
           return this.Player2Score;

    }
    public void Reset(){
        this.Player1Score=0;
        this.Player2Score=0;
        this.gameCount=0;


    }
    public boolean endGame(){
        if (outOff==gameCount){
            return false;
        }
        return true;

    }



}
