using qfmain;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace qfWPFmain
{
    public class bitmapImage_
    {
        /// <summary>
        /// 数据流读取,可以删除源文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <param name="msgErr"></param>
        /// <param name="BitmapCacheOption_"></param>
        /// <returns></returns>
        public bool Read_1(string path, out BitmapImage img, out string msgErr, BitmapCacheOption BitmapCacheOption_ = BitmapCacheOption.OnLoad)
        {
            bool rt = true;
            msgErr = string.Empty;
            img = null;
            try
            {


                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption_; // 关键设置
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();

                    img = bitmap;
                }


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// 先复制到内存然后再读取,可以删除源文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <param name="msgErr"></param>
        /// <param name="BitmapCacheOption_"></param>
        /// <returns></returns>
        public bool Read_11(string path, out BitmapImage img, out string msgErr, BitmapCacheOption BitmapCacheOption_ = BitmapCacheOption.OnLoad)
        {
            bool rt = true;
            msgErr = string.Empty;
            img = null;
            try
            {
                //将图片复制到内存,然后从内存读取
                byte[] imageData = File.ReadAllBytes(path);
                using (MemoryStream stream = new MemoryStream(imageData))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption_;
                    bitmap.EndInit();
                    img = bitmap;
                }


            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }



        /// <summary>
        /// 不可删除源文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <param name="msgErr"></param>
        /// <param name="UriKind_"></param>
        /// <returns></returns>
        public bool Read_2(string path, out BitmapImage img, out string msgErr, UriKind UriKind_ = UriKind.Absolute)
        {
            bool rt = true;
            msgErr = string.Empty;
            img = null;
            try
            {
                img.BeginInit();
                //绝对的PACK URI (省略了当前程序集)
                //  img.UriSource =new Uri("pack://application:,,,/Images/cat.bmp"); 

                //绝对的PACK URI ( 有当前程序集)
                // img.UriSource =new Uri("pack://application:,,,/016;component/Images/cat.bmp");

                //相对的PACK URI ( 省略了当前程序集)
                img.UriSource = new Uri(path, UriKind_);

                //相对的PACK URI ( 带当前程序集)
                //  img.UriSource = new Uri("/016;component/Images/cat.bmp", UriKind.Relative);

                img.EndInit();

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        /// <summary>
        /// 不删除源文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <param name="msgErr"></param>
        /// <param name="UriKind_"></param>
        /// <returns></returns>
        public bool Read_22(string path, out BitmapImage img, out string msgErr, UriKind UriKind_ = UriKind.Absolute)
        {
            bool rt = true;
            msgErr = string.Empty;
            img = null;
            try
            {
                img = new BitmapImage(new Uri(path, UriKind_));

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        /// <summary>
        /// 将HBITMAP句柄（IntPtr）转换为WPF的ImageSource
        /// </summary>
        public   ImageSource IntPtrToImageSource(IntPtr hBitmap)
        {
            if (hBitmap == IntPtr.Zero)
                return null;

            // 使用InteropBitmap包装非托管位图句柄
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // 确保图像在非托管资源释放后仍可使用
            imageSource.Freeze(); // 冻结以提高性能并允许跨线程访问
            return imageSource;
        }




        /// <summary>
        /// 将System.Drawing.Image安全转换为WPF的BitmapImage
        /// </summary>
        /// <param name="sourceImage">源图像</param>
        /// <returns>转换后的BitmapImage</returns>
        /// <exception cref="ArgumentNullException">当源图像为null时抛出</exception>
        /// <exception cref="InvalidOperationException">转换失败时抛出</exception>
        public bool ImageToBitmapImage(System.Drawing.Image sourceImage, out BitmapImage bitmapImage, out string msgErr)
        {
            bitmapImage = new BitmapImage();
            msgErr = string.Empty;
            if (sourceImage is null)
            {
                msgErr = Language_.Get语言("源图像不能为null");
                return false;
            }
            bool rt = true;

            // 创建内存流保存图像数据
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    // 关键：使用明确的图像格式保存（避免GDI+格式错误）
                    // 根据实际图像类型选择合适的格式（Png/Jpeg等）
                    sourceImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                    // 重置流位置到起始点，确保BitmapImage能正确读取
                    memoryStream.Position = 0;

                    // 创建并配置BitmapImage

                    bitmapImage.BeginInit();

                    // 关键设置：立即加载并缓存，解除对流的依赖
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                    // 设置流源
                    bitmapImage.StreamSource = memoryStream;

                    // 禁止图像元数据的自动旋转（可选，根据需求设置）
                    bitmapImage.Rotation = Rotation.Rotate0;

                    bitmapImage.EndInit();

                    // 冻结图像以支持跨线程访问（如果需要）
                    bitmapImage.Freeze();


                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    rt = false;
                    msgErr = $"{Language_.Get语言("GDI+错误,图像转换失败,")},{ex.Message}";

                }
                catch (Exception ex)
                {
                    rt = false;
                    msgErr = $"{Language_.Get语言("图像转换过程中发生错误")},{ex.Message}";

                }
            }

            return rt;
        }


        /// <summary>
        /// 将image转换成数据流
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Stream ImageToStream(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
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
