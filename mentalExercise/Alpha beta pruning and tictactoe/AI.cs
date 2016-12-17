using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alpha_beta_pruning_and_tictactoe
{
    public class AI: Player{
    public AI(String PlayerName): base(PlayerName){}
    public int NextMove(Board gBoard){
        Inteligence intel= new Inteligence(gBoard);
        return intel.Move();
    }

}
}
