using System;
using System.Collections.Generic;
using ccU3DEngine;

namespace GameLogic
{
    class GameMainPool
    {
        public bool _bGameStart = false;
        public ccClientSocketPeer _RoomMaster;

        private int _iCurGuess = 0;
        private int _iTeamAScore, _iTeamBScore;

        public void f_Init()
        {//遊戲初始化
            _iTeamAScore = 0;
            _iTeamBScore = 0;
        }

        public void f_GuessIng()
        {//遊戲進行
            Random random = new Random();
            _iCurGuess = random.Next(1, 3);
        }

        public int f_CheckGuessIsWin(int iGuess)
        {//是否猜測正確
            if (iGuess == 0) { return (int)EM_GuessResult.None; }
            return iGuess == _iCurGuess ? (int)EM_GuessResult.Win : (int)EM_GuessResult.Lost;
        }

        public void f_AddTeamScore(int iTeamID, int iScore)
        {//增加陣營分數
            if (iTeamID == (int)EM_TeamID.TeamA)
            {
                _iTeamAScore += iScore;
            }
            else if (iTeamID == (int)EM_TeamID.TeamB)
            {
                _iTeamBScore += iScore;
            }
        }

        public int f_GetTeamScore(int iTeamID)
        {//獲得陣營分數
            if (iTeamID == (int)EM_TeamID.TeamA)
            {
                return _iTeamAScore;
            }
            else if (iTeamID == (int)EM_TeamID.TeamB)
            {
                return _iTeamBScore;
            }
            else
            {
                return 0;
            }
        }
    }
}
