using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string notifiMsg;
        DateTime startTime = DateTime.Now;  //起動時刻


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   //アプリケーション開始時

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

            if (runningSpan >= notifiSpan)
            {   //起動から特定の時間が経過していたら
                MessageBox.Show(notifiMsg,
                    "起動時間超過",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
    }



}
