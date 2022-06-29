using ccU3DEngine;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace ccPhotonSocket
{

    /// <summary>
    /// 创建客户端连接Photon服务器 客户端框架控制类
    /// </summary>

    public sealed class ccServerSocketPeer : IPhotonPeerListener
    {

        private ccSocketMessagePool m_GMSocketMessagePool = new ccSocketMessagePool();

        PhotonPeer _Socket;

        public float connectTimeout = 5.0f;
        public bool isConnected { get; private set; }

        public ccServerSocketPeer()
        {

        }

        //~ClientHandler()
        //{
        //    peerThread?.Abort();
        //}


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
            if (isConnected)
            {
                return;
            }
            string ipAddrPort = string.Format("{0}:{1}", strIP, iPort);         // 127.0.0.1:4530";
            _Socket = new PhotonPeer(this, ConnectionProtocol.Tcp);
            _Socket.Connect(ipAddrPort, "SexyBaseballServer");

        }

        public void Disconnect()
        {
            _Socket.Disconnect();
            _Socket = null;
        }

        private void OnConnected()
        {
            DebugReturn(DebugLevel.INFO, "Connected");
            isConnected = true;
        }

        private void OnDisconnected(StatusCode statusCode)
        {
            DebugReturn(DebugLevel.ERROR, statusCode.ToString());
            isConnected = false;
        }

        #region IPhotonPeerListener

        public void DebugReturn(DebugLevel level, string message)
        {
            //listener.LogMsg(level, message);
            Console.WriteLine(string.Format("Log[{0}]:{1}", level, message));
        }

        public void OnEvent(EventData eventData)
        {
            //listener.OnEvent(eventData);
        }

        public void OnMessage(object messages)
        {
            //throw new NotImplementedException();
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
            //listener.OnStatusChanged(statusCode);

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

        aaa12334 _stHead = new aaa12334();
        private int DoRouter(int tSocketCommand, byte[] bytes)
        {
            _stHead.iPackType = tSocketCommand;
            return m_GMSocketMessagePool.f_Router(_stHead, bytes, 0);
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



#endregion



    }


}