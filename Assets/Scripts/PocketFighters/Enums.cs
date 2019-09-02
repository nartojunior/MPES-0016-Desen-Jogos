using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PocketFighters
{
    public enum EstiloLuta
    {
        Karate,
        KungFu,
        MuayThai,
        JiuJitsu
    }

    public enum JokenPo
    {
        Rock, 
        Paper, 
        Scissors,
        None
    }

    public enum Player
    {
        Left,
        Right,
        Nobody
    }

    public enum BattleMode
    {
        History,
        PlayerVersusPlayer,
        PlayerVersusIA
    }
}
