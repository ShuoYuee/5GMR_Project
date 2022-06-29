using System;
using System.Collections.Generic;
using ccU3DEngine;
using ExitGames.Client.Photon;
using SexyBaseball.Server;

namespace ccPhotonSocket
{
    /// <summary>
    /// 创建客户端连接Photon服务器 客户端框架控制类
    /// </summary>
    public sealed class ccServerSocketPeer : IPhotonPeerListener
    {
        private ccSocketMessagePool m_GMSocketMessagePool = new ccSocketMessagePool();

        private PhotonPeer _Socket;

        public float connectTimeout = 5.0f;
        public bool m_bIsConnected { get; private set; }


        public ccServerSocketPeer()
        {
            InitMessage();
        }

        public void f_Update()
        {
            if (_Socket == null)
            {
                return;
            }
            _Socket.Service();
        }

        public void f_Connect(string strIP, int iPort)
        {
            if (m_bIsConnected)
            {
                return;
            }
            string ipAddrPort = string.Format("{0}:{1}", strIP, iPort);         // 127.0.0.1:4530";
            _Socket = new PhotonPeer(this, ConnectionProtocol.Tcp);
            _Socket.Connect(ipAddrPort, "SexyBaseballServer");
        }

        public void f_Disconnect()
        {
            if (_Socket != null)
            {
                _Socket.Disconnect();
                _Socket = null;
            }
        }

        private void OnConnected()
        {
            DebugReturn(DebugLevel.INFO, "Connected");
            m_bIsConnected = true;
        }

        private void OnDisconnected(StatusCode statusCode)
        {
            DebugReturn(DebugLevel.ERROR, statusCode.ToString());
            m_bIsConnected = false;
            _Socket = null;
        }

        #region IPhotonPeerListener

        public void DebugReturn(DebugLevel level, string message)
        {
            MessageBox.DEBUG(string.Format("Log[{0}]:{1}", level, message));
        }

        public void OnEvent(EventData eventData)
        {
            Dictionary<byte, object> requestData = eventData.Parameters;
            DoRouter(eventData.Code, (byte[])requestData[1]);
        }

        public void OnMessage(object messages)
        {
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            Dictionary<byte, object> requestData = operationResponse.Parameters;
            DoRouter(operationResponse.OperationCode, (byte[])requestData[1]);
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:
                    OnConnected();
                    break;
                default:
                    OnDisconnected(statusCode);
                    break;
            }
        }

        #endregion

        #region 消息处理

        /// <summary>
        /// 定义游戏消息，并按定义的消息对相应的数据进行拆分和转发
        /// </summary>
        /// <param name="tSocketCommand"></param>
        /// <param name="tSockBaseDT"></param>
        /// <param name="handler"></param>
        public void f_AddListener(int tSocketCommand, SockBaseDT tSockBaseDT, ccCallback handler)
        {
            m_GMSocketMessagePool.f_AddListener(tSocketCommand.ToString(), tSockBaseDT, handler, null, 0, true);
        }

        public void f_RemoveListener(int tSocketCommand)
        {
            m_GMSocketMessagePool.f_RemoveListener(tSocketCommand.ToString());
        }

        aaa12334 _stHead = new aaa12334();
        private int DoRouter(int tSocketCommand, byte[] bytes)
        {
            _stHead.iPackType = tSocketCommand;
            MessageBox.DEBUG("DoRouter:" + _stHead.iPackType);

            try
            {
                return m_GMSocketMessagePool.f_Router(_stHead, bytes, 0);
            }
            catch(Exception e)
            {
                MessageBox.DEBUG("111111111111");
            }
            return -2;
        }

        CreateSocketBuf _CreateSocketBuf = new CreateSocketBuf();
        public void f_SendBuf(int wAssistantCmd, SockBaseDT tSockBaseDT)
        {
            _CreateSocketBuf.f_Reset();
            _CreateSocketBuf.f_Add(tSockBaseDT);

            Dictionary<byte, object> aBuf = new Dictionary<byte, object>();
            aBuf.Add(1, _CreateSocketBuf.f_GetBuf());

            _Socket.OpCustom((byte)wAssistantCmd, aBuf, true);
        }

        void InitMessage()
        {
            //stGameCommandReturn tGameCommandRet = new stGameCommandReturn();
            //f_AddListener((int)SocketCommand.CONTROL_CTG_OperateResult, tGameCommandRet, On_CMsg_GameCommandReturn);
        }

        #endregion



        #region 操作返回

       

        class sCommandReturn
        {
            public SocketCallbackDT m_SocketCallbackDT = new SocketCallbackDT();
            public bool m_bAlive;
        }

        private Dictionary<int, List<sCommandReturn>> _aGameCommandReturn = new Dictionary<int, List<sCommandReturn>>();
        public void f_RegCommandReturn(int tEroeMsgOperateType, SocketCallbackDT tSocketCallbackDT, bool bAlive = false)
        {
            List<sCommandReturn> ttList = null;
            sCommandReturn tsCommandReturn = new sCommandReturn();
            tsCommandReturn.m_SocketCallbackDT.m_ccCallbackSuc = tSocketCallbackDT.m_ccCallbackSuc;
            tsCommandReturn.m_SocketCallbackDT.m_ccCallbackFail = tSocketCallbackDT.m_ccCallbackFail;
            tsCommandReturn.m_bAlive = bAlive;
            if (_aGameCommandReturn.TryGetValue(tEroeMsgOperateType, out ttList))
            {
                ttList.Add(tsCommandReturn);
            }
            else
            {
                ttList = new List<sCommandReturn>();
                ttList.Add(tsCommandReturn);
                _aGameCommandReturn.Add((int)tEroeMsgOperateType, ttList);
            }
        }

        public void f_UnRegCommandReturnAll(int tEroeMsgOperateType)
        {
            List<sCommandReturn> ttList = null;
            if (_aGameCommandReturn.TryGetValue(tEroeMsgOperateType, out ttList))
            {
                _aGameCommandReturn.Remove((int)tEroeMsgOperateType);
            }
        }

        public void f_UnRegCommandReturn(int tEroeMsgOperateType, ccCallback tccCallbackSuc, ccCallback tccCallbackFail)
        {
            List<sCommandReturn> ttList = null;
            if (_aGameCommandReturn.TryGetValue(tEroeMsgOperateType, out ttList))
            {
                foreach (sCommandReturn tsCommandReturn in ttList)
                {
                    if (tsCommandReturn.m_SocketCallbackDT.m_ccCallbackFail == tccCallbackFail && tsCommandReturn.m_SocketCallbackDT.m_ccCallbackSuc == tccCallbackSuc)
                    {
                        ttList.Remove(tsCommandReturn);
                        return;
                    }
                }
            }
        }

        List<sCommandReturn> _aWaitDelList = new List<sCommandReturn>();
        protected void On_CMsg_GameCommandReturn(object Obj)
        {
            if (Obj == null)
            {
                return;
            }
            stGameCommandReturn tstGameCommandReturn = (stGameCommandReturn)Obj;

            //if (GloData.m_bDebug)
            //{
            //    eMsgOperateType teMsgOperateType = (eMsgOperateType)tstGameCommandReturn.iCommand;
            //    if (tstGameCommandReturn.iResult != 0)
            //    {
            //        MessageBox.DEBUG("操作失败：" + tstGameCommandReturn.iCommand + ">>" + teMsgOperateType.ToString() + " >> " + tstGameCommandReturn.iResult);
            //    }
            //}

            List<sCommandReturn> ttList = null;
            if (_aGameCommandReturn.TryGetValue(tstGameCommandReturn.iCommand, out ttList))
            {
                _aWaitDelList.Clear();
                //foreach (sCommandReturn tsCommandReturn in ttList)
                for (int i = 0; i < ttList.Count; i++)
                {
                    sCommandReturn tsCommandReturn = ttList[i];
                    if (tstGameCommandReturn.iResult == (int)eMsgOperateResult.OR_Succeed)
                    {
                        if (tsCommandReturn.m_SocketCallbackDT.m_ccCallbackSuc != null)
                        {
                            tsCommandReturn.m_SocketCallbackDT.m_ccCallbackSuc(tstGameCommandReturn.iResult);//執行你所寫好的callbackSuc
                        }
                    }
                    else
                    {
                        if (tsCommandReturn.m_SocketCallbackDT.m_ccCallbackFail != null)
                        {
                            tsCommandReturn.m_SocketCallbackDT.m_ccCallbackFail(tstGameCommandReturn.iResult);//執行你所寫好的callbackFail
                        }
                    }
                    if (!tsCommandReturn.m_bAlive)
                    {
                        _aWaitDelList.Add(tsCommandReturn);
                    }
                }
                foreach (sCommandReturn tsCommandReturn in _aWaitDelList)
                {
                    ttList.Remove(tsCommandReturn);
                }
            }

        }
        #endregion



    }
}