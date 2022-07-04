using ccPhotonSocket;
using ccU3DEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SexyBaseball.ConsoleClient
{
    public partial class Form1 : Form
    {
        ccServerSocketPeer _ccPhotonSocketClient = new ccServerSocketPeer();

        System.Timers.Timer _LoopCtrl = new System.Timers.Timer(1000 / 10);

        public Form1()
        {
            InitializeComponent();
            
            _LoopCtrl.Elapsed += new System.Timers.ElapsedEventHandler(Update);
            _LoopCtrl.AutoReset = true;

            this.BtnConnect.Enabled = true;
            this.BtnDisConnect.Enabled = false;

            InitMessage();
            Console.WriteLine("初始化完成");
        }

        void InitMessage()
        {
            CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = new CMsg_GTC_LoginRelt();
            _ccPhotonSocketClient.f_AddListener((int)SocketCommand.UserLogin_Reps, tCMsg_GTC_LoginRelt, On_CMsg_GTC_LoginRelt);

            CMsg_CTG_AccountCreateRelt tCMsg_CTG_AccountCreateRelt = new CMsg_CTG_AccountCreateRelt();
            _ccPhotonSocketClient.f_AddListener((int)SocketCommand.UserCreate_Reps, tCMsg_CTG_AccountCreateRelt, On_Cmsg_GTC_CreateRelt);
        }

        void On_CMsg_GTC_LoginRelt(object Obj)
        {
            /*CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt =(CMsg_GTC_LoginRelt)Obj;
            if (tCMsg_GTC_LoginRelt.m_PlayerId > 0)
            {
                Console.WriteLine("登陆成功，玩家Id:" + tCMsg_GTC_LoginRelt.m_PlayerId);
            }
            else
            {
                Console.WriteLine("登陆失败，帐号密码错误");
            }*/

            CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = (CMsg_GTC_LoginRelt)Obj;
            if (tCMsg_GTC_LoginRelt.m_PlayerId > 0)
            {
                Console.WriteLine("登陆成功，玩家Id:" + tCMsg_GTC_LoginRelt.m_PlayerId);
                //PlayerDataTools.f_InitServerDataToClient(tCMsg_GTC_LoginRelt);//初始化數值
            }
            else
            {
                Console.WriteLine("登陆失败，帐号密码错误");
            }
        }

        void On_Cmsg_GTC_CreateRelt(object Obj)
        {
            CMsg_CTG_AccountCreateRelt tCMsg_CTG_AccountCreateRelt = (CMsg_CTG_AccountCreateRelt)Obj;
            if (tCMsg_CTG_AccountCreateRelt.m_iResult == (int)eMsgOperateResult.OR_Succeed)
            {
                Console.WriteLine("註冊成功：" + tCMsg_CTG_AccountCreateRelt.m_strAccount);
            }
            else if(tCMsg_CTG_AccountCreateRelt.m_iResult==(int)eMsgOperateResult.OR_Error_AccountRepetition)
            {
                Console.WriteLine("註冊失敗；" + tCMsg_CTG_AccountCreateRelt.m_strAccount + "已被註冊");
            }
            else
            {
                Console.WriteLine("出現未知錯誤，請重新操作");
            }
        }


        private void BtnConnect_Click(object sender, EventArgs e)
        {            
            _ccPhotonSocketClient.f_Connect("127.0.0.1", 4530);

            _LoopCtrl.Enabled = true;
            _LoopCtrl.Start();

            this.BtnConnect.Enabled = false;
            this.BtnDisConnect.Enabled = true;
        }

        private void Update(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_ccPhotonSocketClient == null)
            {
                return;
            }
            _ccPhotonSocketClient.f_Update();
        }

        private void BtnDisConnect_Click(object sender, EventArgs e)
        {
            this.BtnConnect.Enabled = true;
            this.BtnDisConnect.Enabled = false;

            _ccPhotonSocketClient.Disconnect();
        }

        private void btnSendBuf1_Click(object sender, EventArgs e)
        {
            CMsg_CTG_AccountEnter tCMsg_CTG_AccountEnter = new CMsg_CTG_AccountEnter();
            tCMsg_CTG_AccountEnter.m_strAccount = textBox_Name.Text;
            tCMsg_CTG_AccountEnter.m_strPassword = textBox_Pwd.Text;

            _ccPhotonSocketClient.f_SendBuf((int)SocketCommand.CS_UserLogin, tCMsg_CTG_AccountEnter);
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            CMsg_CTG_AccountCreate tCMsg_CTG_AccountCreate = new CMsg_CTG_AccountCreate();
            tCMsg_CTG_AccountCreate.m_strAccount = textBox_Name.Text;
            tCMsg_CTG_AccountCreate.m_strPassword = textBox_Pwd.Text;
            tCMsg_CTG_AccountCreate.m_iTeam = boxListTeam.SelectedIndex;

            _ccPhotonSocketClient.f_SendBuf((int)SocketCommand.CS_UserCreate, tCMsg_CTG_AccountCreate);
        }
    }
}