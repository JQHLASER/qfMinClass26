using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;


namespace qfmain
{
    /// <summary>
    /// 安装 Speech.Synthesis
    /// </summary>
    public class 语音
    {


        SpeechSynthesizer voice = null; //创建语音实例

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="音量">0,100</param>
        /// <param name="语速">-10,10</param>
        /// <param name="异步朗读"></param>
        public virtual bool   播报(string value, out string msgErr,int 音量 = 100, int 语速 = 1, bool 异步朗读 = false)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                using (voice = new SpeechSynthesizer())
                {
                    voice.Rate = 语速; //设置语速,[-10,10]     
                    voice.Volume = 音量; //设置音量,[0,100]
                    if (异步朗读)
                    {
                        voice.SpeakAsync(value);
                    }
                    else
                    {
                        voice.Speak(value);
                    }
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr =ex.Message;
            }

            voice = null;

            return rt;

        }


        public void 取消朗读()
        {
            if (voice != null)
            {
                voice.SpeakAsyncCancelAll(); //取消朗读
            }
        }
        public void 继续朗读()
        {
            if (voice != null)
            {
                voice.Resume(); //继续朗读
            }
        }
        public void 暂停朗读()
        {
            if (voice != null)
            {
                voice.Pause(); //暂停朗读
            }
        }


    }
}
