/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package TicTacToe;
import Inteligence.*;
/**
 *
 * @author Taufiq
 */
public class AI extends Player{
    public AI(String PlayerName){
        super(PlayerName);
        
    }
    public int NextMove(Board gBoard){
        Inteligence intel= new Inteligence(gBoard);
        return intel.Move();
    }

}
