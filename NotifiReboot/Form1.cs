using System;
using System.IO;
using System.Windows.Forms;

namespace NotifiReboot
{
    public partial class Form1 : Form
    {
        //変数の定義
        int timerInterval;      //タイマー間隔（ミリ秒）
        int notifiSpanH, notifiSpanM, notifiSpanS;
        TimeSpan notifiSpan;
        int notifiTimes;
        string notifiTitle;
        string notifiMsg;
        Boolean msgFrag = true;
        DateTime startTime = DateTime.Now;  //起動時刻


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   //アプリケーション開始時
            Hide();
            //設定読み込み
            FileStream fs = new FileStream(@".\Settings.xml", FileMode.Open);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
            Settings settings = (Settings)serializer.Deserialize(fs);
            //設定を変数に代入
            timerInterval = settings.TimerInterval;
            notifiSpanH = settings.NotifiSpanH;
            notifiSpanM = settings.NotifiSpanM;
            notifiSpanS = settings.NotifiSpanS;
            notifiSpan = new TimeSpan(notifiSpanH, notifiSpanM, notifiSpanS);      //メッセージ通知時間
            notifiTimes = settings.NotifiTimes;
            notifiTitle = settings.NotifiTitle;
            notifiMsg = settings.NotifiMsg;

            Timer timer = new Timer();
            timer.Tick += new EventHandler(CheckTime);
            timer.Interval = timerInterval;
            timer.Enabled = true;   //タイマー開始
        }

        private void CheckTime(object sender, EventArgs e)
        {   //10分ごとに作動する動作
            DateTime nowTime = DateTime.Now;
            TimeSpan runningSpan = nowTime - startTime;


            if (runningSpan >= notifiSpan && msgFrag == true && notifiTimes != 0)
            {   //起動から特定の時間が経過していたら
                msgFrag = false;    //メッセージ表示中は、新しいメッセージが出ないようにする。
                if (MessageBox.Show(notifiMsg,
                    notifiTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation) == DialogResult.OK)
                {   //メッセージボックスを閉じたら
                    msgFrag = true;
                    notifiTimes -= 1;
                    if (notifiTimes == 0)
                    {   //通知回数が０の時は、アプリを閉じる。
                        this.Close();
                        Application.Exit();
                    }
                }
            }
        }
    }
}
