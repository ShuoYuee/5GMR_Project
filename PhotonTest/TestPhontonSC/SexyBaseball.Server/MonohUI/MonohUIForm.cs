using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MR.Server
{
    public partial class MonohUIForm : Form
    {
        public enum ELogType
        {
            INFO,
            DEBUG,
            WARN,
            ERROR,
            FATAL,
        }

        public delegate void LogEventHandler(ELogType t, string format, params object[] args);

        public MonohUIForm()
        {
            InitializeComponent();


            MessageBox.DEBUG("MonohUIForm...");
        }

        private void MonohUIForm_Load(object sender, EventArgs e)
        {
            richTextBox_Log.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox_Log.Text = "";
        }

        public void f_Log(ELogType t, string format, params object[] args)
        {
            richTextBox_Log.Invoke((MethodInvoker)delegate
            {
                f_LogRaw(t, format, args);
            });
        }

        private void f_LogRaw(ELogType t, string format, params object[] args)
        {
            string strData = string.Format(format, args);
            string strAppend = string.Format("[{0}] {1}", t.ToString(), strData + Environment.NewLine);
            int oldLen = richTextBox_Log.Text.Length;
            richTextBox_Log.AppendText(strAppend);
            richTextBox_Log.Select(oldLen, strAppend.Length);
            switch (t)
            {
                case ELogType.INFO:
                case ELogType.DEBUG:
                    richTextBox_Log.SelectionColor = Color.Black;
                    break;
                case ELogType.ERROR:
                case ELogType.FATAL:
                    richTextBox_Log.SelectionColor = Color.Red;
                    break;
                case ELogType.WARN:
                    richTextBox_Log.SelectionColor = Color.Blue;
                    break;
                default:
                    break;
            }
            richTextBox_Log.Select(richTextBox_Log.Text.Length, 0);
            richTextBox_Log.SelectionColor = Color.Black;
        }

        private void winResultSingleField_Validated(object sender, EventArgs e)
        {
            int value;
            if(!int.TryParse(winResultSingleField.Text, out value))
            {
                winResultSingleField.Text = value.ToString();
            }
        }

        private void winResultDoubleField_Validated(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(winResultDoubleField.Text, out value))
            {
                winResultDoubleField.Text = value.ToString();
            }
        }

        private void winResultTripleField_Validated(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(winResultTripleField.Text, out value))
            {
                winResultTripleField.Text = value.ToString();
            }
        }

        private void winResultHomeRunField_Validated(object sender, EventArgs e)
        {
            
            int value;
            if (!int.TryParse(winResultHomeRunField.Text, out value))
            {
                winResultHomeRunField.Text = value.ToString();
            }
        }

        private void buttonSetWinResult_Click(object sender, EventArgs e)
        {
            int[] newResults = new int[4]
            {
                int.Parse(winResultSingleField.Text),
                int.Parse(winResultDoubleField.Text),
                int.Parse(winResultTripleField.Text),
                int.Parse(winResultHomeRunField.Text)
            };



            // Log.
            string logStr = "";
            for (int i = 0; i < newResults.Length; i++)
            {
                if (i == 0)
                {
                    logStr += newResults[i].ToString();
                    continue;
                }

                logStr += ", " + newResults[i].ToString();
            }
            winResultSingleField.DeselectAll();
            MessageBox.DEBUG("手動設置機率為：'" + logStr + "'");
        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            //StopServer
            //TODO：当服务器启动关服指定后，就要停止前端的游戏的登陆，同时发消息给前端服务器即将关的消息，这时前端就要跳出关服倒计时提示2021/12/24(OK)
            //傳訊息給前端說：幾分鐘後關服維修
            //幾分鐘到了之後傳訊息給前端：停止任何操作
           

            MessageBox.DEBUG("關服開始");
        }

        private void Start_Click(object sender, EventArgs e)
        {
           

            MessageBox.DEBUG("重新開服");
        }
    }
}
