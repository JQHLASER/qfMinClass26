
using QRCoder;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 安装 QRCoder
    /// </summary>
    public class QRcode
    {

         
        /// <summary>
        /// 生成二维码
        /// </summary> 
        /// <param name="imag"></param>
        public bool 生成(string text, out Bitmap imag, _QRcode_Cfg_ info, out string msgErr)
        {
            imag = null;
            msgErr = string.Empty;
            bool rt = true;
            try
            {

                QRCodeGenerator qrGenerator = new QRCodeGenerator();

                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(info.像素大小, info.darkColor, info.darkClightColorolor, info.水印图标, info.水印大小比例, info.水印边框宽度, info.是否绘制空白边框, info.水印背景色);
                imag = qrCodeImage;


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }






    }


}
