/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package TicTacToe;

/**
 *
 * @author Taufiq
 */
public class Player {
    public String playerName;
    public String symbol;
    public int playerWin;
    public int playerLoss;
    public Player(String playerName){
        this.playerName=playerName;
    }
    public void PlayerWins(){
        playerWin++;
    }
    public void PlayerLoses(){
        playerLoss++;
    }
    public int PlayerWinCount(){
        return playerWin;
    }
    public int PlayerLossCount(){
        return playerLoss;
    }

}
