using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 数组
    {

        // 通用拼接方法
        public virtual T[] 拼接Concat<T>(params T[][] arrays)
        {
            // 计算总长度
            int totalLength = 0;
            foreach (var arr in arrays)
            {
                if (arr != null)
                    totalLength += arr.Length;
            }

            // 创建结果数组
            T[] result = new T[totalLength];

            // 拷贝所有数组
            int offset = 0;
            foreach (var arr in arrays)
            {
                if (arr != null)
                {
                    Array.Copy(arr, 0, result, offset, arr.Length);
                    offset += arr.Length;
                }
            }

            return result;
        }




        #region 稀疏压缩（Sparse Compression）
        // 压缩：只保存非默认值
        public static List<(int index, T value)> SparseCompress<T>(T[] arr, T defaultValue = default)
        {
            var compressed = new List<(int, T)>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(arr[i], defaultValue))
                    compressed.Add((i, arr[i]));
            }
            return compressed;
        }

        // 解压：还原数组
        public static T[] SparseDecompress<T>(List<(int index, T value)> compressed, int length, T defaultValue = default)
        {
            T[] arr = Enumerable.Repeat(defaultValue, length).ToArray();
            foreach (var item in compressed)
                arr[item.index] = item.value;
            return arr;
        }
        #endregion

        #region RLE 压缩（Run-Length Encoding）
        public static List<(T value, int count)> RLECompress<T>(T[] arr)
        {
            var compressed = new List<(T, int)>();
            if (arr.Length == 0) return compressed;

            T last = arr[0];
            int count = 1;

            for (int i = 1; i < arr.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(arr[i], last))
                    count++;
                else
                {
                    compressed.Add((last, count));
                    last = arr[i];
                    count = 1;
                }
            }
            compressed.Add((last, count));
            return compressed;
        }

        public static T[] RLEDecompress<T>(List<(T value, int count)> compressed)
        {
            var list = new List<T>();
            foreach (var item in compressed)
                list.AddRange(Enumerable.Repeat(item.value, item.count));
            return list.ToArray();
        }
        #endregion

        #region 布尔数组位压缩
        // 压缩 bool 数组为 byte[]
        public static byte[] BoolCompress(bool[] arr)
        {
            int byteCount = (arr.Length + 7) / 8;
            byte[] compressed = new byte[byteCount];

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i])
                    compressed[i / 8] |= (byte)(1 << (i % 8));
            }
            return compressed;
        }

        // 解压 byte[] 为 bool[]
        public static bool[] BoolDecompress(byte[] compressed, int length)
        {
            bool[] arr = new bool[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = (compressed[i / 8] & (1 << (i % 8))) != 0;
            }
            return arr;
        }
        #endregion

    }
}
