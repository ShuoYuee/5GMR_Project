using System.Collections.Generic;

public enum EM_RPS
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public static class EM_RPSExtend
{
    public static int Challenge(this EM_RPS player, EM_RPS opponent)
    {
        var table = new Dictionary<EM_RPS, Dictionary<EM_RPS, int>>{
            {EM_RPS.Rock, new Dictionary<EM_RPS, int>{
                {EM_RPS.Rock, 0},
                {EM_RPS.Paper, -1},
                {EM_RPS.Scissors, 1}
            }},
            {EM_RPS.Paper, new Dictionary<EM_RPS, int>{
                {EM_RPS.Rock, 1},
                {EM_RPS.Paper, 0},
                {EM_RPS.Scissors, -1}
            }},
            {EM_RPS.Scissors, new Dictionary<EM_RPS, int>{
                {EM_RPS.Rock, -1},
                {EM_RPS.Paper, 1},
                {EM_RPS.Scissors, 0}
            }},
        };

        return table[player][opponent];
    }

    public static bool CanWin(this EM_RPS player, EM_RPS opponent)
    {
        return player.Challenge(opponent) > 0;
    }

    public static bool IsTiedWith(this EM_RPS player, EM_RPS opponent)
    {
        return player == opponent;
    }
}