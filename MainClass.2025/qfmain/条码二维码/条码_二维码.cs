
using DataMatrix.net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Presentation;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace mainclassqf 
{

    /// <summary>
    /// 安装 ZXing 
    /// <para>安装 DataMatrix</para>
    /// </summary>
    public class 条码_二维码
    {



        /// <summary>
        /// DM码....DataMatrix
        /// </summary>
        /// <param name="content"></param>
        /// <param name="moduleSize"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public Bitmap 生成DM码(string content, int moduleSize = 5, int margin = 5)
        {
            DmtxImageEncoderOptions opt = new DmtxImageEncoderOptions();
            opt.ModuleSize = moduleSize;
            opt.MarginSize = margin;//如果有显示不全的二维码，可以适当增大margin值         
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            Bitmap bm = encoder.EncodeImage(content, opt);

            return bm;
        }

        /// <summary>
        /// 生成二维码... QRcode
        /// <para>编码格式: 如:"UTF-8"</para>
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public Bitmap 生成QR码(string content, int width, int height, string 编码格式 = "UTF-8", int margin = 5)
        {
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {

                DisableECI = true,//设置内容编码
                CharacterSet = 编码格式,  //设置二维码的编码格式           
                Margin = margin,//设置二维码的边距,单位不是固定像素
                Width = width,
                Height = height

            };
            writer.Options = options;
            Bitmap map = writer.Write(content);
            return map;
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public Bitmap 生成二维码(string text, System.Windows.Forms.PictureBox controls)
        {
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()

            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = controls.Width,
                Height = controls.Height,
                Margin = 2//设置二维码的边距,单位不是固定像素

            };

            writer.Options = options;
            Bitmap map = writer.Write(text);
            controls.Image = map;
            return map;
        }


        /// <summary>
        /// <para>barcodeFormat_:   QR_CODE,DATA_MATRIX,CODE_39,PDF_417,</para>
        ///<para>barcodeFormat_: CODE_93,CODE_128,EAN_8,EAN_13,itf,RSS_14,RSS_EXPANDED,UPC_A,UPC_E,UPC_EAN_EXTENSION,AZTEC,All_1D ,PLESSEY</para> 
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public Bitmap 生成条码(string text, int width, int height, int Margins, BarcodeFormat barcodeFormat_ = BarcodeFormat.CODE_128)
        {
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            //writer.Format = BarcodeFormat.CODE_39;

            // writer.Format = BarcodeFormat.DATA_MATRIX;
            writer.Format = barcodeFormat_;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = Margins,
                PureBarcode = true


            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }











        /// <summary>
        /// 生成带Logo的二维码...QRcode
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        private Bitmap 生成带Logo的QR码(string 内容, string 图片path, int width, int height)
        {
            //Logo 图片
            string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\img\logo.png";
            // string logoPath = 图片path;
            Bitmap logo = new Bitmap(logoPath);
            //构造二维码写码器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            hint.Add(EncodeHintType.MARGIN, 2);//旧版本不起作用，需要手动去除白边

            //生成二维码 
            BitMatrix bm = writer.encode(内容, BarcodeFormat.QR_CODE, width + 30, height + 30, hint);
            bm = deleteWhite(bm);
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);

            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, height);
                //白底将二维码插入图片
                g.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            return bmpimg;
        }

        public Bitmap 生成带Logo的二维码_自定义(string 内容, string 图片path, int width, int height, int 图片width, int 图片height, int 图片X坐标, int 图片Y坐标)
        {
            //Logo 图片
            // string logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\img\logo.png";
            string logoPath = 图片path;
            Bitmap logo = new Bitmap(logoPath);





            //构造二维码写码器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            hint.Add(EncodeHintType.MARGIN, 2);//旧版本不起作用，需要手动去除白边

            //生成二维码 
            BitMatrix bm = writer.encode(内容, BarcodeFormat.QR_CODE, width + 30, height + 30, hint);
            bm = deleteWhite(bm);
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);

            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = 图片X坐标;// Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = 图片Y坐标;//Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = 图片width; //(map.Width - middleW) / 2;
            int middleT = 图片height; //(map.Height - middleH) / 2;

            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, height);
                //白底将二维码插入图片
                g.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            return bmpimg;
        }

        /// <summary>
        /// 删除默认对应的空白
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private BitMatrix deleteWhite(BitMatrix matrix)
        {
            int[] rec = matrix.getEnclosingRectangle();
            int resWidth = rec[2] + 1;
            int resHeight = rec[3] + 1;

            ZXing.Common.BitMatrix resMatrix = new BitMatrix(resWidth, resHeight);
            resMatrix.clear();
            for (int i = 0; i < resWidth; i++)
            {
                for (int j = 0; j < resHeight; j++)
                {
                    if (matrix[i + rec[0], j + rec[1]])
                        resMatrix[i, j] = true;
                }
            }
            return resMatrix;
        }



        /// <summary>
        ///防止控件重画时闪动,用此语句: SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        /// </summary>
        public void 防止控件重画时闪动_无代码()
        {
            // SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        /// <summary>
        /// 画文本
        /// </summary>
        /// <param name="_graDraw"></param>
        /// <param name="_sSerialNo"></param>
        public void draw_string(Graphics _graDraw, string 内容, Font 字体属性, Color 字体颜色, int X坐标, int Y坐标, int width, int height)
        {
            SolidBrush brushXMark = new SolidBrush(字体颜色);
            _graDraw.DrawString(内容, 字体属性, brushXMark, new Rectangle(new Point(X坐标, Y坐标), new Size(width, height)));
        }

        public void draw_image(Graphics _graDraw, System.Drawing.Image image_, int x坐标, int y坐标, int width, int height)
        {
            Rectangle destRect1 = new Rectangle(x坐标, y坐标, width, height);
            _graDraw.DrawImage(image_, destRect1, 0, 0, 170, 170, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="内容"></param>
        /// <param name="字体"></param>
        /// <param name="字体大小">10f</param>
        /// <param name="字体属性">0</param>
        /// <param name="x坐标">1</param>
        /// <param name="y坐标">1</param>
        /// <param name="x比例">0.5f</param>
        /// <param name="y比例">0.8f</param>
        /// <param name="gra"> Graphics gra = 主_Form2.zhu_form.panel1.CreateGraphics();</param>
        public void draw_string可设置宽高(string 内容, string 字体, float 字体大小, int 字体属性, int x坐标, int y坐标, float x比例, float y比例, Graphics gra)
        {
            GraphicsPath path = new GraphicsPath();
            StringFormat strformat = new StringFormat();
            //  strformat.Alignment = StringAlignment.Center;
            // strformat.LineAlignment = StringAlignment.Center;

            path.AddString(内容, new FontFamily(字体), 字体属性, 字体大小, new Point(x坐标, y坐标), strformat);


            Matrix m = new Matrix();
            m.Scale(x比例, y比例);   //压扁
            path.Transform(m);
            gra.FillPath(new SolidBrush(Color.Black), path);


        }

        /// <summary>
        /// 需要用这个gra去画图
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="gra"></param>
        public void draw_防止失去焦点时图像丢失_起(int width, int height, Graphics gra, ref Bitmap bitmap变量)
        {
            bitmap变量 = new Bitmap(width, height);
            gra = Graphics.FromImage(bitmap变量);
        }

        public System.Drawing.Image draw_防止失去焦点时图像丢失_止(Bitmap bitmap变量, Graphics gra)
        {
            gra.Save();
            return bitmap变量;
        }




        public class SetGraphics
        {
            public static Graphics gra { set; get; }
            public static Bitmap BitmapGra { set; get; }
        }

        /// <summary>
        /// 使用方法  Graphics gra = pictureBox1.CreateGraphics();
        /// </summary>
        /// <param name="graphics">pictureBox1.CreateGraphics();</param>
        /// <param name="width">要画出控件的宽度</param>
        /// <param name="height">要画出控件的高度</param>
        public void Graphics_初始化(Graphics graphics, int width, int height)
        {
            SetGraphics.gra = graphics;
            SetGraphics.gra.SmoothingMode = SmoothingMode.HighQuality; //高质量
            SetGraphics.gra.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            SetGraphics.BitmapGra = new Bitmap(width, height);
            SetGraphics.gra = Graphics.FromImage(SetGraphics.BitmapGra);

        }


        public void Graphics_画图片(System.Drawing.Image image_, int left, int top, int width, int height)
        {
            SetGraphics.gra.DrawImage(image_, left, top, width, height);//二维码
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="color_"></param>
        /// <param name="字体"></param>
        /// <param name="字号"></param>
        /// <param name="fontStyle_">FontStyle.Bold</param>
        /// <param name="内容"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public void Grahics_画String(string 内容, Font font, Color color_, int left, int top)
        {
            //创建一个画刷，颜色是纯色          
            Brush brush = new SolidBrush(color_);
            //选择字体、字号、风格
            // Font font = new Font(字体, 字号, fontStyle_); //FontStyle.Bold

            SetGraphics.gra.DrawString(内容, font, brush, left, top);
        }





        /// <summary>
        /// 当画过所有元素后,需要用此方法来生成图片来赋值给控件
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image Graphics_生成图片给控件()
        {
            ///防止失去焦点时丢失
            SetGraphics.gra.Save();

            return SetGraphics.BitmapGra;
        }



        public System.Drawing.Image Graphics_画图片_防失去焦点消失(Control controls, System.Drawing.Image image_)
        {

            try
            {
                Graphics gras = controls.CreateGraphics();
                Bitmap xc = new Bitmap(controls.Width, controls.Height);
                gras = Graphics.FromImage(xc);


                gras.DrawImage(image_, 0, 0, controls.Width, controls.Height);
                gras.Save();
                controls.BackgroundImage = image_;
                return xc;
            }
            catch (Exception)
            {
            }

            controls.BackgroundImage = null;

            return null;


        }


        public void Graphics_画图片_控件无焦点(Control controls, System.Drawing.Image image_)
        {

            try
            {
                Graphics gras = controls.CreateGraphics();
                gras.DrawImage(image_, 0, 0, controls.Width, controls.Height);
            }
            catch (Exception)
            {


            }



            //gras.SmoothingMode = SmoothingMode.HighQuality; //高质量
            //gras.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            //Bitmap BitmapGra = new Bitmap(controls.Width, controls.Height);
            //gras = Graphics.FromImage(SetGraphics.BitmapGra);


        }



        /// <summary>
        /// 有时连结给picturebox赋值会死机,就必须用这个来转一下
        /// </summary>
        /// <param name="am"></param>
        /// <param name="PictureBoxControls"></param>
        /// <returns></returns>
        public System.Drawing.Image Graphics_画图片_防失去焦点消失_picturebox(System.Drawing.Image am, System.Windows.Forms.PictureBox PictureBoxControls)
        {

            try
            {



                Graphics gras = PictureBoxControls.CreateGraphics();
                //  Bitmap xc = new Bitmap(PictureBoxControls.Width, PictureBoxControls.Height);

                double xm = 1;
                if (am.Width > am.Height)
                {
                    xm = (double)PictureBoxControls.Width / (double)am.Width;
                }
                else
                {
                    xm = (double)PictureBoxControls.Height / (double)am.Height;
                }

                int x = (int)(am.Width * xm);
                int y = (int)(am.Height * xm);

                //int x = am.Size.Width;
                //int y = am.Size.Height;


                Bitmap xc = new Bitmap(x, y);

                gras = Graphics.FromImage(xc);
                gras.DrawImage(am, 0, 0, x, y);


                gras.Save();
                PictureBoxControls.Image = xc;
                return xc;
            }
            catch (Exception)
            {


            }
            return null;
        }






    }
}
