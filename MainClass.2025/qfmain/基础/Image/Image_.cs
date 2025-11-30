using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace qfmain
{
    /// <summary>
    /// 安装 SixLabors.ImageSharp
    /// </summary>
    public class Image_
    {
        /// <summary>
        /// format =  ImageFormat.Jpeg;
        /// </summary>
        public virtual byte[] ImageToBytes(Image imagePath, ImageFormat format)
        {
            byte[] imgBytes;

            using (var bmp = new Bitmap(imagePath))
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, format); // PNG / BMP 都行
                imgBytes = ms.ToArray();
            }
            return imgBytes;
        }

       
        public virtual Bitmap BytesToImage(byte[] imgBytes)
        {
            using (var ms = new MemoryStream(imgBytes))
            {
                return new Bitmap(ms);
            }
        }
         


        /// <summary>
        /// 读取图片为bytes
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public virtual byte[] ReadImageToBytes(string imagePath)
        {
            byte[] bs = File.ReadAllBytes(imagePath);
            return bs;
        }


        /// <summary>
        /// bytes保存为图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="bytes"></param>
        public virtual void WriteBytesToImage(string imagePath, byte[] bytes)
        {
            File.WriteAllBytes(imagePath, bytes);
        }

         


        /// <summary>
        /// Convert Byte[] to a picture 并存储在文件中
        /// </summary>
        /// <param name="fileName">路径 .jpeg  .png  .bmp  .gif  .icon</param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public virtual string CreateImageFromBytes(string fileName, byte[] buffer)
        {
            string file = fileName;
            System.Drawing.Image image = BytesToImage(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            System.IO.FileInfo info = new System.IO.FileInfo(file);
            System.IO.Directory.CreateDirectory(info.Directory.FullName);
            File.WriteAllBytes(file, buffer);
            return file;
        }




        #region Bitmap与Image与 byte[]互转

        public virtual System.Drawing.Image Bitmap转Image(Bitmap Bitmap_)
        {
            System.Drawing.Image img = Bitmap_;
            return img;
        }



        public virtual Bitmap Image转Bitmap(System.Drawing.Image Image_)
        {
            System.Drawing.Image img = Image_;
            Bitmap map = new Bitmap(img);
            return map;
        }


        //byte[] 转换 Bitmap
        public virtual Bitmap Bytes转Bitmap(byte[] Bytes)
        {

            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                return new Bitmap((System.Drawing.Image)new Bitmap(ms));
            }

        }

        public virtual byte[] Bitmap转Bytes(Bitmap Bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;

            }

        }



        #endregion




        #region int与intptr互转

        public virtual int intPtrToint(IntPtr intPtr_)
        {
            return (int)intPtr_;
        }

        public virtual IntPtr intTointPrt(int i)
        {
            return new IntPtr(i);
        }

        #endregion


        #region IntPtr转Image

        [DllImport("gdi32.dll")]
        static internal extern bool DeleteObject(IntPtr hObject);
        public System.Drawing.Image IntPtrToImage(IntPtr pr)
        {
            System.Drawing.Image img = System.Drawing.Image.FromHbitmap(pr);
            DeleteObject(pr);
            return img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="hight">高度</param>
        /// <param name="stride">像素大小</param>
        /// <param name="point">句柄指针</param>
        /// <returns></returns>
        public virtual Bitmap IntPtrToImage2(int width, int height, int stride, uint point)
        {
            Bitmap am = null;
            try
            {
                am = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppRgb, (IntPtr)point);
            }
            catch (Exception)
            {
            }
            return am;
        }


        #endregion



        #region 合并图像

        /// <summary>
        /// 合并图片
        /// </summary>
        /// <param name="imageIds">图片ID集合</param>
        public virtual System.Drawing.Image 合并Image(int width, int height, params System.Drawing.Image[] imageIds)
        {

            Bitmap bg = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bg);
            //清除画布,背景透明
            g.Clear(System.Drawing.Color.Transparent);

            for (int i = 0; i < imageIds.Length; i++)
            {
                ////路径
                //string url = @"E:\Projects\ConsoleApplication\ConsoleApplication\";
                ////取图
                //Image img = Image.FromFile(url + imageIds[i] + ".png");
                //绘图
                g.DrawImage(imageIds[i], 0, 0);
            }
            g.Dispose();


            ////情况1、保存文件，自己再加下路径
            //bg.Save("ok", ImageFormat.Png);

            ////情况2、保存二进制流入数据库
            //MemoryStream ms = new MemoryStream();
            //bg.Save(ms, ImageFormat.Png);
            //byte[] byteImage = new Byte[ms.Length];
            //byteImage = ms.ToArray();


            return bg;
        }

        #endregion




        #region 图片文件
        public virtual System.Drawing.Image 读取图片文件(string path)
        {

            using (FileStream picfile = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                System.Drawing.Image rt = System.Drawing.Image.FromStream(picfile);
                return new Bitmap(rt);
            }

        }


        public virtual bool 读取图片文件(string path, out System.Drawing.Bitmap img, out string msgErr)
        {
            bool rt = true;
            img = null;
            msgErr = string.Empty;
            try
            {
                using (FileStream picfile = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    Bitmap img1 = (Bitmap)System.Drawing.Image.FromStream(picfile);
                    img = new Bitmap(img1);
                }
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }


            return rt;
        }


        public virtual bool 读取图片文件(string path, out System.Drawing.Image img, out string msgErr)
        {
            bool rt = true;
            img = null;
            msgErr = string.Empty;
            try
            {
                using (FileStream picfile = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    Image img1 = System.Drawing.Image.FromStream(picfile);
                    img = new Bitmap(img1);
                }
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }


            return rt;
        }


        public virtual System.Drawing.Image 读取图片文件2(string path)
        {
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(path))
            {
                return new Bitmap(img);
            }//加载图片

        }

        public virtual bool 读取图片文件2(string path, out System.Drawing.Image img, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            img = null;
            try
            {
                img = 读取图片文件2(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;

        }

        public virtual System.Drawing.Image 读取图片文件_gif(string path)
        {

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                return img;
            }

        }




        public virtual System.Drawing.Bitmap 读取图片文件_读取后删除不掉源文件(string path)
        {
            using (Bitmap img = new Bitmap(path))
            {
                return new Bitmap(img);
            }
        }

        public virtual bool 读取图片文件_读取后删除不掉源文件(string path, out System.Drawing.Bitmap img, out string msgErr)
        {
            img = null;
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                img = 读取图片文件_读取后删除不掉源文件(path);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="beginX">原始图片取图坐标</param>
        /// <param name="beginY">原始图片取图坐标</param>
        /// <param name="getX">取出大小</param>
        /// <param name="getY">取出大小</param>
        /// <param name="MsgErr">错误信息</param>
        /// <returns></returns>
        public virtual System.Drawing.Image 裁剪图片(System.Drawing.Image img, int beginX, int beginY, int getX, int getY, ref string MsgErr)
        {

            if ((beginX < getX) && (beginY < getY))
            {
                using (Bitmap bitmap = new Bitmap(img))//原图 
                {
                    if (((beginX + getX) <= bitmap.Width) && ((beginY + getY) <= bitmap.Height))
                    {
                        using (Bitmap destBitmap = new Bitmap(getX, getY))//目标图 
                        {
                            Rectangle destRect = new Rectangle(0, 0, getX, getY);//矩形容器 
                            Rectangle srcRect = new Rectangle(beginX, beginY, getX, getY);

                            using (var grahics = Graphics.FromImage(destBitmap))
                            {
                                grahics.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);

                                //ImageFormat format = ImageFormat.Png;
                                //switch (fileExt.ToLower())
                                //{
                                //    case "png":
                                //        format = ImageFormat.Png;
                                //        break;
                                //    case "bmp":
                                //        format = ImageFormat.Bmp;
                                //        break;
                                //    case "gif":
                                //        format = ImageFormat.Gif;
                                //        break;
                                //}
                                //destBitmap.Save(savePath + "//" + fileName, format);                

                                // return savePath + "\\" + "*" + fileName.Split('.')[0] + "." + fileExt;

                                return new Bitmap(destBitmap);
                            }
                        }
                    }
                    else
                    {
                        MsgErr = "截取范围超出图片范围";
                        return null;
                    }
                }
            }
            else
            {
                MsgErr = "请确认(beginX < getX)&&(beginY < getY)";
                return null;
            }
        }


        /// <summary>
        /// 格式->0:png;1:Gif;2:Bmp;3:Icon;4:Jpeg;5:MemoryBmp;6:Tiff;7:Emf;8:Exif;9:Wmf
        /// </summary>
        /// <param name="bitmap_"></param>
        /// <param name="path">要保存的地址</param>
        /// <param name="格式">0:png;1:Gif;2:Bmp;3:Icon;4:Jpeg;5:MemoryBmp;6:Tiff;7:Emf;8:Exif;9:Wmf</param>
        public virtual void 保存_bitmap(Bitmap bitmap_, string path, int 格式)
        {
            switch (格式)
            {
                case 0:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 1:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case 2:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 3:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case 4:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case 5:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.MemoryBmp);
                    break;
                case 6:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                case 7:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Emf);
                    break;
                case 8:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Exif);
                    break;
                case 9:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Wmf);
                    break;


            }

            // bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Png);

        }


        /// <summary>
        /// 不用指定格式
        /// </summary>
        /// <param name="bitmap_"></param>
        /// <param name="path"></param>
        public virtual void 保存_bitmap_1(Bitmap bitmap_, string path)
        {

            bitmap_.Save(path);
            // bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Png);

        }


        /// <summary>
        /// 不用指定格式
        /// </summary>
        /// <param name="img"></param>
        /// <param name="path"></param>
        /// <param name="格式"></param>
        public virtual void 保存_image_1(System.Drawing.Image img, string path, int 格式)
        {
            img.Save(path);
        }







        /// <summary>
        ///  格式->0:png;1:Gif;2:Bmp;3:Icon;4:Jpeg;5:MemoryBmp;6:Tiff;7:Emf;8:Exif;9:Wmf
        /// </summary>
        /// <param name="img"></param>
        /// <param name="path"></param>
        /// <param name="格式">0:png;1:Gif;2:Bmp;3:Icon;4:Jpeg;5:MemoryBmp;6:Tiff;7:Emf;8:Exif;9:Wmf</param>
        public virtual void 保存_image(System.Drawing.Image img, string path, int 格式)
        {
            switch (格式)
            {
                case 0:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 1:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case 2:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 3:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case 4:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case 5:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.MemoryBmp);
                    break;
                case 6:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                case 7:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Emf);
                    break;
                case 8:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Exif);
                    break;
                case 9:
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Wmf);
                    break;


            }

        }

        /// <summary>
        /// new Bitmap(width, height);
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public virtual System.Drawing.Image 生成空白图片(int width, int height)
        {
            return new Bitmap(width, height);
        }


        public virtual void 保存_pictruebox(System.Windows.Forms.PictureBox picturebox_, string path, int 格式)
        {
            Bitmap bitmap_ = new Bitmap(picturebox_.Width, picturebox_.Height);
            picturebox_.DrawToBitmap(bitmap_, new Rectangle(0, 0, picturebox_.Width, picturebox_.Height));
            switch (格式)
            {
                case 0:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 1:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case 2:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 3:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case 4:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case 5:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.MemoryBmp);
                    break;
                case 6:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                case 7:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Emf);
                    break;
                case 8:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Exif);
                    break;
                case 9:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Wmf);
                    break;
            }
        }


        public virtual void 保存_Bitmap(Bitmap bitmap_, string path, int 格式)
        {
            //Bitmap bitmap_ = new Bitmap(picturebox_.Width, picturebox_.Height);
            //picturebox_.DrawToBitmap(bitmap_, new Rectangle(0, 0, picturebox_.Width, picturebox_.Height));
            switch (格式)
            {
                case 0:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 1:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case 2:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 3:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case 4:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case 5:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.MemoryBmp);
                    break;
                case 6:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                case 7:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Emf);
                    break;
                case 8:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Exif);
                    break;
                case 9:
                    bitmap_.Save(path, System.Drawing.Imaging.ImageFormat.Wmf);
                    break;
            }
        }



        public virtual Bitmap pictruebox_转bitmap(System.Windows.Forms.PictureBox picturebox_)
        {
            Bitmap bitmap_ = new Bitmap(picturebox_.Width, picturebox_.Height);
            picturebox_.DrawToBitmap(bitmap_, new Rectangle(0, 0, picturebox_.Width, picturebox_.Height));
            return bitmap_;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="color_">指定为透明的颜色</param>
        /// <returns></returns>
        public virtual Bitmap 指定颜色背景转透明_image(System.Drawing.Image img, System.Drawing.Color color_)
        {
            // Image image = System.Drawing.Image.FromFile(@"C:\A.JPG");
            Bitmap pbitmap = new Bitmap(img);
            pbitmap.MakeTransparent(color_);

            return pbitmap;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="color_">指定为透明的颜色</param>
        /// <returns></returns>
        public virtual Bitmap 指定颜色背景转透明_Bitemap(Bitmap bm, System.Drawing.Color color_)
        {
            // Image image = System.Drawing.Image.FromFile(@"C:\A.JPG");
            // Bitmap pbitmap = new Bitmap(img);
            bm.MakeTransparent(color_);

            return bm;

        }



        public virtual void 保存到Temp内存_image(System.Drawing.Image img, string Name)
        {
            img.Save(Path.GetTempPath() + Name, ImageFormat.Png);
        }


        #endregion

        /// 

        /// <summary>
        /// 将彩色图片变成黑白色的照片
        /// </summary>
        /// <param name="image">原来图片</param>
        /// <returns>返回的黑白照片</returns>
        public virtual Bitmap 转换为黑白图片(System.Drawing.Bitmap image)
        {
            //原来图片的长度
            int width = image.Width;
            //原来图片的高度
            int height = image.Height;
            //改变色素
            //横坐标
            for (int x = 0; x < width; x++)
            {
                //纵坐标
                for (int y = 0; y < height; y++)
                {
                    //获得坐标(x,y)颜色
                    System.Drawing.Color color = image.GetPixel(x, y);
                    //获得该颜色下的黑白色
                    int value = (color.R + color.G + color.B) / 3;
                    //设置颜色
                    image.SetPixel(x, y, System.Drawing.Color.FromArgb(value, value, value));
                }
            }
            return image;
        }


        /// <summary>
        /// 图片转成二进制
        /// </summary>
        /// <param name="image_"></param>
        /// <returns></returns>
        public virtual string ImageToBase64string(System.Drawing.Image image_)
        {
            //byte[] bs = ImageToBytes(image_);
            //string code = Convert.ToBase64String(bs);
            //return code;

            string s = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                image_.Save(stream, ImageFormat.Jpeg);
                byte[] by = stream.ToArray();
                s = System.Convert.ToBase64String(by);
                stream.Close();
                stream.Dispose();
            }

            return s;

        }


        /// <summary>
        ///  二进制转成图片
        /// </summary>
        /// <param name="Base64string"></param>
        /// <returns></returns>
        public virtual System.Drawing.Image Base64stringToImage(string Base64string)
        {

            //byte[] bytes = Convert.FromBase64String(Base64string);
            //MemoryStream ms = new MemoryStream(bytes);
            //Image im = Image.FromStream(ms);

            //return im;


            System.Drawing.Image myimge = null;
            byte[] b = Convert.FromBase64String(Base64string);
            using (MemoryStream ms = new MemoryStream(b))
            {
                myimge = System.Drawing.Image.FromStream(ms);
                // pb.Image = myimge;
                //pb.BorderStyle = BorderStyle.FixedSingle;
                //pb.SizeMode = PictureBoxSizeMode.StretchImage;
                ms.Close();
                ms.Dispose();
            }
            return myimge;
        }



        /// <summary>
        /// 有时连结给picturebox赋值会死机,就必须用这个来转一下
        /// </summary>
        /// <param name="am"></param>
        /// <param name="PictureBoxControls"></param>
        /// <returns></returns>
        public virtual System.Drawing.Image 显示到picturebox(System.Drawing.Image am, System.Windows.Forms.PictureBox PictureBoxControls)
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

        /// <summary>
        /// 将image转换成数据流
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Stream ImageToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            // 创建内存流
            MemoryStream stream = new MemoryStream();

            // 将图像保存到内存流中，指定图像格式
            image.Save(stream, format);

            // 将流的位置重置到起始点，以便后续读取
            stream.Position = 0;

            return stream;
        }

    }
}
