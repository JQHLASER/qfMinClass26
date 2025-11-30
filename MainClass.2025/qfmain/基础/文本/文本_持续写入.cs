using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 文本_持续写入 : IDisposable
    {
        private StreamWriter _sw;

        public 文本_持续写入(string path, bool 是否覆盖, int buffsize = 1024 * 100, Encoding encoding = null)
        {
            FileMode fileModel = 是否覆盖 ? FileMode.Create : FileMode.Append;
            encoding= encoding ?? Encoding.Default ;    
            _sw = new StreamWriter(
                new FileStream(path, fileModel, FileAccess.Write, FileShare.Read, buffsize),
                encoding
            );
        }

        public void WriteLine(string msg)
        {
            _sw.WriteLine(msg);
        }

        public void Write(string msg)
        {
            _sw.Write(msg);
        }


        public void WriteLineAsync(string msg)
        {
            _sw.WriteLineAsync(msg);
        }

        public void WriteAsync(string msg)
        {
            _sw.WriteAsync(msg);
        }


        public void Dispose()
        {
            _sw?.Dispose();
        }

    }
}
