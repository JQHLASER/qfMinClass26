using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qf_pipe 
{
    /// <summary>
    /// 工具
    /// </summary>  

    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public static class PipeHelper
    {
        public const byte TYPE_STRING = 0x01;
        public const byte TYPE_IMAGE = 0x02;

        public static async Task SendStringAsync(Stream stream, string text)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            await SendMessageAsync(stream, TYPE_STRING, bytes);
        }

        public static async Task SendImageAsync(Stream stream, byte[] bytes)
        {
            await SendMessageAsync(stream, TYPE_IMAGE, bytes);
        }

        public static async Task<(byte type, byte[] data)> ReceiveMessageAsync(Stream stream, CancellationToken token)
        {
            byte[] lenBytes = await ReadExactAsync(stream, 4, token);
            int length = BitConverter.ToInt32(lenBytes, 0);

            int type = stream.ReadByte();
            if (type < 0) throw new EndOfStreamException("管道断开");

            byte[] data = await ReadExactAsync(stream, length - 1, token);
            return ((byte)type, data);
        }

        public static async Task SendMessageAsync(Stream stream, byte type, byte[] data)
        {
            byte[] lenBytes = BitConverter.GetBytes(data.Length + 1);
            await stream.WriteAsync(lenBytes, 0, 4);
            await stream.WriteAsync(new byte[] { type }, 0, 1);
            await stream.WriteAsync(data, 0, data.Length);
            await stream.FlushAsync();
        }

        private static async Task<byte[]> ReadExactAsync(Stream stream, int size, CancellationToken token)
        {
            byte[] buffer = new byte[size];
            int read = 0;
            while (read < size)
            {
                int r = await stream.ReadAsync(buffer, read, size - read, token);
                if (r == 0) throw new EndOfStreamException("管道断开");
                read += r;
            }
            return buffer;
        }
    }



}
