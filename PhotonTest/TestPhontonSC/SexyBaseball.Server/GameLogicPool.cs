using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using PHPServiceServer;
using PHPServiceServer.MySQL;
using ccU3DEngine;


namespace GameLogic
{
    public class stClient
    {
        public stClient(string strAcount, string strPwd, int iId, string strName, int iItem)
        {
            m_ccClientSocketPeer = null;
            m_strAcount = strAcount;
            m_strPwd = strPwd;
            m_iPlayerId = iId;
            m_strName = strName;
            m_iTeam = iItem;
        }
        public bool f_CheckIsOnline()
        {//該用戶是否在線
            return m_ccClientSocketPeer == null ? false : true;
        }
        public bool f_CheckIsAcount(string strAcount, long lId)
        {//用戶確認
            return (strAcount == m_strAcount ? true : false) && (lId == m_iPlayerId ? true : false);
        }
        public bool f_CheckIsAcount(string strAcount)
        {//用戶確認
            return strAcount == m_strAcount ? true : false;
        }

        public ccClientSocketPeer m_ccClientSocketPeer = null;//用戶節點
        public string m_strAcount;
        public string m_strPwd;
        public long m_iPlayerId;
        public string m_strName;//用戶名稱
        public int m_iTeam;//用戶隊伍
    }
    
    public class GameLogicPool
    {
        private List<stClient> m_aTeam = new List<stClient>();

        ccMySql _ccMySql = new ccMySql();

        #region 初始化帳戶
        /// <summary>
        /// 初始化玩家帳戶(寫死)
        /// </summary>
        private void f_InitPlayer()
        {
            for(int i = 0; i < 10; i++)
            {
                stClient tstClient = new stClient("A" + i, "aaa" + i, i, "APlayer" + i, 1);
                m_aTeam.Add(tstClient);
            }
            for (int i = 0; i < 10; i++)
            {
                stClient tstClient = new stClient("B" + i, "bbb" + i, i + 10, "BPlayer" + i, 2);
                m_aTeam.Add(tstClient);
            }
        }

        /// <summary>
        /// 初始化玩家帳戶(讀取文字檔)
        /// </summary>
        private void f_InitPlayerForFile()
        {
            string _strPath = "C:\\Users\\judy2\\OneDrive\\Desktop\\practice\\5GMR\\5GMR_Project\\TestPhontonSC\\TestPlayerData.txt";
            if (File.Exists(_strPath))
            {
                string[] strFileLine = File.ReadAllLines(_strPath);
                for (int i = 0; i < strFileLine.Length; i++)
                {
                    string[] strData = ccMath.f_String2ArrayString(strFileLine[i], ",");
                    if (strData.Length == 5)
                    {
                        stClient stClient = new stClient(strData[1], strData[2], ccMath.atoi(strData[0]), strData[3], ccMath.atoi(strData[4]));
                        m_aTeam.Add(stClient);
                    }
                    else
                    {
                        stClient stClient = new stClient("Acount" + i, i.ToString() + i.ToString() + i.ToString(), i, "用戶" + i, 1);
                        m_aTeam.Add(stClient);
                    }
                }
            }
            else
            {
                f_InitPlayer();
            }
        }
        #endregion

        public GameLogicPool()
        {
            
        }

        public void f_Init(string strBinaryPath)
        {
            string strPath = Path.Combine(strBinaryPath, "config.xml");
            Config tConfig = new Config(strPath);
            string game_db_host = tConfig.f_Read("Config", "Server", "game_db_host");
            int game_db_port = int.Parse(tConfig.f_Read("Config", "Server", "game_db_port"));
            string game_db_user = tConfig.f_Read("Config", "Server", "game_db_user");
            string game_db_password = tConfig.f_Read("Config", "Server", "game_db_password");
            string game_db_name = tConfig.f_Read("Config", "Server", "game_db_name");

            f_InitPlayerForFile();
            //_ccMySql.f_Connect(game_db_host, game_db_name, game_db_user, game_db_password);
        }

        #region ClientList
        public void f_BoardCast(int iTeam, int iSend, SockBaseDT tSockBaseDT)
        {
            if (iTeam == 0)
            {
                for(int i = 0; i < m_aTeam.Count; i++)
                {
                    if (m_aTeam[i].f_CheckIsOnline())
                    {
                        m_aTeam[i].m_ccClientSocketPeer.f_SendBuf(iSend, tSockBaseDT);
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_aTeam.Count; i++)
                {
                    if (m_aTeam[i].f_CheckIsOnline() && m_aTeam[i].m_iTeam == iTeam)
                    {
                        m_aTeam[i].m_ccClientSocketPeer.f_SendBuf(iSend, tSockBaseDT);
                    }
                }
            }
        }

        public ccClientSocketPeer f_Login_CheckPeer(ccClientSocketPeer tccClientSocketPeer, string strAccount)
        {
            for (int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].f_CheckIsAcount(strAccount) && m_aTeam[i].m_ccClientSocketPeer != null)
                {
                    return m_aTeam[i].m_ccClientSocketPeer;
                }
            }
            return tccClientSocketPeer;
        }

        #region Find
        public stClient f_FindClient(string strAcount, long lPlayerID)
        {
            for(int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].f_CheckIsAcount(strAcount, lPlayerID))
                {
                    return m_aTeam[i];
                }
            }
            return null;
        }

        public stClient f_FindClient(string strAcount)
        {
            for(int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].f_CheckIsAcount(strAcount))
                {
                    return m_aTeam[i];
                }
            }
            return null;
        }

        public stClient f_FindClient(ccClientSocketPeer tccClientSocketPeer)
        {
            if (tccClientSocketPeer == null) { return null; }
            for (int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].m_ccClientSocketPeer == tccClientSocketPeer)
                {
                    return m_aTeam[i];
                }
            }
            return null;
        }

        public stClient f_FindOnlineClient(string strAcount, long lPlayerID)
        {
            stClient stClient = f_FindClient(strAcount, lPlayerID);
            if (stClient != null)
            {
                if (!stClient.f_CheckIsOnline())
                {
                    return null;
                }
                return stClient;
            }
            return null;
        }

        public stClient f_FindOnlineClient(ccClientSocketPeer tccClientSocketPeer)
        {
            stClient stClient = f_FindClient(tccClientSocketPeer);
            if (stClient != null)
            {
                if (!stClient.f_CheckIsOnline())
                {
                    return null;
                }
                return stClient;
            }
            return null;
        }
        #endregion

        public bool f_CheckIsOnline(ccClientSocketPeer tccClientSocketPeer)
        {
            stClient stClient = f_FindClient(tccClientSocketPeer);
            return stClient == null ? false : true;
        }
        #endregion

        public CMsg_GTC_LoginRelt f_UserLogin(ccClientSocketPeer tccClientSocketPeer, string strUserName, string strPwd)
        {//登入
            /*string ppSQL = string.Format("SELECT iUserId From x_role Where acc_name = {0} AND acc_password = {1}", strUserName, strPwd);
            DataTable tDataTable = _ccMySql.ExecuteDataTable(ppSQL);
            if (tDataTable == null)
            {
                return -2;
            }
            else
            {
                if (tDataTable.Rows.Count == 0)
                {//无此帐号信息
                    return -1;
                }
                else if (tDataTable.Rows.Count == 1)
                {
                    int iUserId = (int)tDataTable.Rows[0][0];
                    return iUserId;
                }                
            }
            return -1;*/

            MessageBox.DEBUG("f_UserLogin " + strUserName);

            CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = new CMsg_GTC_LoginRelt();
            tCMsg_GTC_LoginRelt.m_result = 0;
            for(int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].m_strAcount == strUserName)
                {
                    tCMsg_GTC_LoginRelt.m_PlayerId = m_aTeam[i].m_iPlayerId;
                    m_aTeam[i].m_ccClientSocketPeer = tccClientSocketPeer;
                    if (m_aTeam[i].m_strPwd == strPwd)
                    {
                        tCMsg_GTC_LoginRelt.m_userName = m_aTeam[i].m_strName;
                        tCMsg_GTC_LoginRelt.m_iTeam = m_aTeam[i].m_iTeam;
                        return tCMsg_GTC_LoginRelt;
                    }
                    else
                    {
                        tCMsg_GTC_LoginRelt.m_result = (int)eMsgOperateResult.OR_Error_Password;
                        return tCMsg_GTC_LoginRelt;
                    }
                }
            }

            tCMsg_GTC_LoginRelt.m_result = (int)eMsgOperateResult.OR_Error_NoAccount;
            return tCMsg_GTC_LoginRelt;
        }

        public void f_UserLogout(ccClientSocketPeer tccClientSocketPeer)
        {//登出

            MessageBox.DEBUG("f_UserLogout " + tccClientSocketPeer.RemotePort);

            for (int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].m_ccClientSocketPeer == tccClientSocketPeer)
                {                   
                    m_aTeam[i].m_ccClientSocketPeer = null;
                    break;
                }
            }
        }

        public void f_UserLogout(string strUserName, long iPlayerID)
        {//登出

            MessageBox.DEBUG("f_UserLogout " + strUserName);

            for (int i = 0; i < m_aTeam.Count; i++)
            {
                if (m_aTeam[i].f_CheckIsAcount(strUserName, iPlayerID))
                {
                    /*CMsg_CTG_AccountExitRelt tCMsg_CTG_AccountExitRelt = new CMsg_CTG_AccountExitRelt();
                    tCMsg_CTG_AccountExitRelt.m_iRelt = 0;
                    m_aTeam[i].m_ccClientSocketPeer.f_SendBuf((int)SocketCommand.UserLogout_Reps, tCMsg_CTG_AccountExitRelt);
                    */
                    m_aTeam[i].m_ccClientSocketPeer = null;
                    break;
                }
            }
        }

        public int f_UserCreate(CMsg_CTG_AccountCreate tCMsg_CTG_AccountCreate)
        {
            return -1;
        }
    }
}
