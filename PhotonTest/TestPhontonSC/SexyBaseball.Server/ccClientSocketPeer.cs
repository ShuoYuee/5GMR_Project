
    using System.Collections.Generic;
    using ccU3DEngine;
using GameLogic;
using Photon.SocketServer;
    using PhotonHostRuntimeInterfaces;

/// <summary>
/// 创建连接Photon服务器的客户端 服务器端框架控制类
/// </summary>
public sealed class ccClientSocketPeer : ClientPeer
{
    private ccSocketMessagePool m_GMSocketMessagePool = new ccSocketMessagePool();
    private GameLogicPool _GameLogicPool;

    public ccClientSocketPeer(InitRequest request, GameLogicPool tGameLogicPool)
        : base(request)
    {
        _GameLogicPool = tGameLogicPool;
    }


    /// <summary>
    ///  //处理客户端断开连接 的处理
    /// </summary>
    /// <param name="disconnectCode"></param>
    /// <param name="reasonDetail"></param>
    protected override void OnDisconnect(DisconnectReason disconnectCode, string reasonDetail)
    {
        _GameLogicPool.f_UserLogout(this);
    }

    /// <summary>
    /// //处理客户端请求
    /// </summary>
    /// <param name="operationRequest"></param>
    /// <param name="sendParameters"></param>
    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        Dictionary<byte, object> requestData = operationRequest.Parameters;
        DoRouter(operationRequest.OperationCode, (byte[])requestData[1]);
    }


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
        MessageBox.DEBUG("DoRouter..." + tSocketCommand);
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

        OperationResponse response = new OperationResponse((byte)wAssistantCmd);
        response.SetParameters(aBuf);

        SendOperationResponse(response, new SendParameters());
    }


    #endregion


}