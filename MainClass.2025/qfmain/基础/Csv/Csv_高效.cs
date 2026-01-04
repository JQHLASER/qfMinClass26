using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// CSV 高性能读写工具（.NET Framework 4.8）
    /// </summary>
    public class Csv_高效
    {

        private readonly char _separator;
        private readonly Encoding _encoding;


        public Csv_高效(
            char separator = ',',
            Encoding encoding = null)
        {
            _separator = separator;
            _encoding = encoding ?? Encoding.UTF8;
        }

        #region ===== 写 =====

        /// <summary>
        /// 写入一行（线程安全）    
        /// </summary>
        public void WriteLine(
                string filePath,
                IEnumerable<string> values,
                bool 追加写入)
        {

            WriteLineInternal(filePath, values, 追加写入);

        }

        private void WriteLineInternal(
            string filePath,
            IEnumerable<string> values,
            bool append)
        {
            using (var fs = new FileStream(
                filePath,
                append ? FileMode.Append : FileMode.Create,
                FileAccess.Write,
                FileShare.Read))
            using (var writer = new StreamWriter(fs, _encoding))
            {
                bool first = true;

                foreach (var value in values)
                {
                    if (!first)
                        writer.Write(_separator);

                    writer.Write(Escape(value));
                    first = false;
                }

                writer.WriteLine();
            }
        }

        /// <summary>
        /// 批量写入（最快方式）      
        /// </summary>
        public void WriteLines(
                string filePath,
                IEnumerable<IEnumerable<string>> rows,
                bool 追加写入)
        {
            using (var fs = new FileStream(
                filePath,
                追加写入 ? FileMode.Append : FileMode.Create,
                FileAccess.Write,
                FileShare.Read))
            using (var writer = new StreamWriter(fs, _encoding))
            {
                foreach (var row in rows)
                {
                    bool first = true;
                    foreach (var value in row)
                    {
                        if (!first)
                            writer.Write(_separator);

                        writer.Write(Escape(value));
                        first = false;
                    }
                    writer.WriteLine();
                }
            }
        }

        #endregion

        #region ===== 读 =====

        /// <summary>
        /// 流式读取（推荐，大文件）
        /// </summary>
        public IEnumerable<string[]> ReadLines(string filePath)
        {
            using (var fs = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            using (var reader = new StreamReader(fs, _encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return ParseLine(line);
                }
            }
        }

        #endregion


        #region ===== 异步写 =====

        /// <summary>
        /// 异步写一行（可选线程安全）
        /// </summary>
        public async Task WriteLineAsync(
            string filePath,
            IEnumerable<string> values,
            bool 追加写入,

            CancellationToken ct = default)
        {

            await WriteLineInternalAsync(filePath, values, 追加写入, ct)
                .ConfigureAwait(false);

        }

        private async Task WriteLineInternalAsync(
            string filePath,
            IEnumerable<string> values,
            bool 追加写入,
            CancellationToken ct)
        {
            using (var fs = new FileStream(
                filePath,
                追加写入 ? FileMode.Append : FileMode.Create,
                FileAccess.Write,
                FileShare.Read,
                bufferSize: 8192,
                useAsync: true))
            using (var writer = new StreamWriter(fs, _encoding))
            {
                bool first = true;

                foreach (var value in values)
                {
                    if (!first)
                        await writer.WriteAsync(_separator).ConfigureAwait(false);

                    await writer.WriteAsync(Escape(value)).ConfigureAwait(false);
                    first = false;
                }

                await writer.WriteLineAsync().ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 异步批量写（最快）
        /// </summary>
        public async Task WriteLinesAsync(
            string filePath,
            IEnumerable<IEnumerable<string>> rows,
            bool 追加写入  ,
            CancellationToken ct = default)
        {
            using (var fs = new FileStream(
                filePath,
                追加写入 ? FileMode.Append : FileMode.Create,
                FileAccess.Write,
                FileShare.Read,
                bufferSize: 8192,
                useAsync: true))
            using (var writer = new StreamWriter(fs, _encoding))
            {
                foreach (var row in rows)
                {
                    ct.ThrowIfCancellationRequested();

                    bool first = true;
                    foreach (var value in row)
                    {
                        if (!first)
                            await writer.WriteAsync(_separator).ConfigureAwait(false);

                        await writer.WriteAsync(Escape(value)).ConfigureAwait(false);
                        first = false;
                    }
                    await writer.WriteLineAsync().ConfigureAwait(false);
                }

                await writer.FlushAsync().ConfigureAwait(false);
            }
        }




        #endregion

        #region ===== 异步读 =====

        /// <summary>
        /// 异步读取（回调式，最省内存）
        /// </summary>
        public async Task ReadLinesAsync(
            string filePath,
            Func<string[], Task> onRowAsync,
            CancellationToken ct = default)
        {
            using (var fs = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite,
                bufferSize: 8192,
                useAsync: true))
            using (var reader = new StreamReader(fs, _encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    ct.ThrowIfCancellationRequested();
                    await onRowAsync(ParseLine(line)).ConfigureAwait(false);
                }
            }
        }

        public string ReadLinesAsync回调试的用法()
        {
            string xt = " await csv.ReadLinesAsync(\"data.csv\", row =>\r\n    {\r\n        // UI 线程\r\n        this.BeginInvoke(new Action(() =>\r\n        {\r\n            listBox1.Items.Add(row[0]);\r\n        }));\r\n        return Task.CompletedTask;\r\n    });";
            return xt;
        }



        /// <summary>
        /// 异步读取到 List（中小文件）
        /// </summary>
        public async Task<List<string[]>> ReadAllAsync(
            string filePath,
            CancellationToken ct = default)
        {
            var result = new List<string[]>();

            await ReadLinesAsync(filePath, row =>
            {
                result.Add(row);
                return Task.CompletedTask;
            }, ct);

            return result;
        }

        #endregion



        #region ===== CSV 核心处理 =====

        /// <summary>
        /// 转义 CSV 字段
        /// </summary>
        private string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            bool mustQuote =
                value.IndexOf(_separator) >= 0 ||
                value.IndexOf('"') >= 0 ||
                value.IndexOf('\n') >= 0 ||
                value.IndexOf('\r') >= 0;

            if (!mustQuote)
                return value;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        /// <summary>
        /// 解析一行 CSV
        /// </summary>
        private string[] ParseLine(string line)
        {
            var result = new List<string>();
            var sb = new StringBuilder();

            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == _separator && !inQuotes)
                {
                    result.Add(sb.ToString());
                    sb.Length = 0;
                }
                else
                {
                    sb.Append(c);
                }
            }

            result.Add(sb.ToString());
            return result.ToArray();
        }

        #endregion

    }


}


