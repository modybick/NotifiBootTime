using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifiReboot
{
    [System.Xml.Serialization.XmlRoot("settings")]
    public class Settings
    {
        /// <summary>
        /// タイマー間隔
        /// </summary>
        [System.Xml.Serialization.XmlElement("timerInterval")]
        public int TimerInterval { get; set; }
        /// <summary>
        /// 通知時間（時間）
        /// </summary>
        [System.Xml.Serialization.XmlElement("notifiSpanH")]
        public int NotifiSpanH { get; set; }
        /// <summary>
        /// 通知時間（分）
        /// </summary>
        [System.Xml.Serialization.XmlElement("notifiSpanM")]
        public int NotifiSpanM { get; set; }
        /// <summary>
        /// 通知時間（秒）
        /// </summary>
        [System.Xml.Serialization.XmlElement("notifiSpanS")]
        public int NotifiSpanS { get; set; }
        /// <summary>
        /// 再通知回数
        /// </summary>
        [System.Xml.Serialization.XmlElement("notifiTimes")]
        public int NotifiTimes { get; set; }
        /// <summary>
        /// 通知メッセージ
        /// </summary>
        [System.Xml.Serialization.XmlElement("notifiMsg")]
        public String NotifiMsg { get; set; }
    }
}