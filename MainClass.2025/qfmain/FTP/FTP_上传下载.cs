using DotNet.Utilities;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace qfmain
{

    ///// <summary>
    ///// 最好与FTPHelper_OK配合起来用
    ///// <para>安装 FluentFTP</para>
    ///// </summary>
    //public class FTP_上传下载
    //{

    //    #region 相关参数
    //    /// <summary>
    //    /// FtpClient
    //    /// </summary>
    //    private FtpClient ftpClient = null;
    //    /// <summary>
    //    /// FTP IP地址(127.0.0.1)
    //    /// </summary>
    //    private string strFtpUri = string.Empty;
    //    /// <summary>
    //    /// FTP端口
    //    /// </summary>
    //    private int intFtpPort = 21;
    //    /// <summary>
    //    /// FTP用户名
    //    /// </summary>
    //    private string strFtpUserID = string.Empty;
    //    /// <summary>
    //    /// FTP密码
    //    /// </summary>
    //    private string strFtpPassword = string.Empty;
    //    /// <summary>
    //    /// 重试次数
    //    /// </summary>
    //    private int intRetryTimes = 3;
    //    /// <summary>
    //    /// FTP工作目录
    //    /// </summary>
    //    private string _workingDirectory = string.Empty;
    //    /// <summary>
    //    /// FTP工作目录
    //    /// </summary>
    //    public string WorkingDirectory
    //    {
    //        get
    //        {
    //            return _workingDirectory;
    //        }
    //    }
    //    #endregion










    //    #region 构造函数


    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="host">FTP IP地址</param>
    //    /// <param name="port">FTP端口</param>
    //    /// <param name="username">FTP用户名</param>
    //    /// <param name="password">FTP密码</param>
    //    public void Set设置连接参数(string ip, int port, string username, string password)
    //    {
    //        strFtpUri = ip;
    //        intFtpPort = port;
    //        strFtpUserID = username;
    //        strFtpPassword = password;
    //        //创建ftp客户端
    //        GetFtpClient();
    //    }
    //    #endregion



    //    #region 创建ftp客户端
    //    /// <summary>
    //    /// 创建ftp客户端
    //    /// </summary>
    //    public bool GetFtpClient()
    //    {

    //        try
    //        {


    //            ftpClient = new FtpClient(strFtpUri, intFtpPort, strFtpUserID, strFtpPassword);
    //            ftpClient = new FtpClient(strFtpUri, intFtpPort, strFtpUserID, strFtpPassword);
    //            ftpClient.RetryAttempts = intRetryTimes;
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }

    //    }
    //    #endregion


    //    #region FTP是否已连接
    //    /// <summary>
    //    /// FTP是否已连接
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool isConnected()
    //    {
    //        bool result = false;
    //        if (ftpClient != null)
    //        {
    //            result = ftpClient.IsConnected;
    //        }
    //        return result;
    //    }
    //    #endregion


    //    #region 连接FTP
    //    /// <summary>
    //    /// 连接FTP
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool Connect()
    //    {
    //        bool result = false;
    //        if (ftpClient != null)
    //        {
    //            if (ftpClient.IsConnected)
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                ftpClient.Connect();
    //                return true;
    //            }
    //        }
    //        return result;
    //    }
    //    #endregion


    //    #region 断开FTP
    //    /// <summary>
    //    /// 断开FTP
    //    /// </summary>
    //    public void DisConnect()
    //    {
    //        if (ftpClient != null)
    //        {
    //            if (ftpClient.IsConnected)
    //            {
    //                ftpClient.Disconnect();
    //            }
    //        }
    //    }
    //    #endregion




    //    #region 取得文件或目录列表
    //    /// <summary>
    //    /// 取得文件或目录列表;type类型:file-文件,dic-目录
    //    /// </summary>
    //    /// <param name="remoteDic">远程目录比如"\\数据库"或""</param>
    //    /// <param name="type">类型:file-文件,dic-目录</param>
    //    /// <returns></returns>
    //    public List<string> ListDirectory(string remoteDic, string type = "file")
    //    {
    //        List<string> list = new List<string>();
    //        type = type.ToLower();

    //        try
    //        {
    //            if (Connect())
    //            {
    //                FtpListItem[] files = ftpClient.GetListing(remoteDic);
    //                foreach (FtpListItem file in files)
    //                {
    //                    if (type == "file")
    //                    {
    //                        if (file.Type == FtpFileSystemObjectType.File)
    //                        {
    //                            list.Add(file.Name);
    //                        }
    //                    }
    //                    else if (type == "dic")
    //                    {
    //                        if (file.Type == FtpFileSystemObjectType.Directory)
    //                        {
    //                            list.Add(file.Name);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        list.Add(file.Name);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return list;
    //    }
    //    #endregion




    //    #region 上传单文件
    //    /// <summary>
    //    /// 上传单文件
    //    /// </summary>
    //    /// <param name="localPath">本地路径(@"D:\abc.txt")</param>
    //    /// <param name="remoteDic">远端目录("/test")</param>
    //    /// <returns></returns>
    //    public bool UploadFile(string localPath, string remoteDic)
    //    {
    //        bool boolResult = false;
    //        FileInfo fileInfo = null;

    //        try
    //        {
    //            //本地路径校验
    //            if (!File.Exists(localPath))
    //            {
    //                //// Log4NetUtil.Error(this, "UploadFile->本地文件不存在:" + localPath);
    //                return boolResult;
    //            }
    //            else
    //            {
    //                fileInfo = new FileInfo(localPath);
    //            }
    //            //远端路径校验
    //            if (string.IsNullOrEmpty(remoteDic))
    //            {
    //                remoteDic = "/";
    //            }
    //            if (!remoteDic.StartsWith("/"))
    //            {
    //                remoteDic = "/" + remoteDic;
    //            }
    //            if (!remoteDic.EndsWith("/"))
    //            {
    //                remoteDic += "/";
    //            }

    //            //拼接远端路径
    //            remoteDic += fileInfo.Name;

    //            if (Connect())
    //            {
    //                using (FileStream fs = fileInfo.OpenRead())
    //                {
    //                    //重名覆盖
    //                    ftpClient.UploadFiles (fs, remoteDic, FtpRemoteExists.Overwrite, true);
    //                    //  FtpExists.Overwrite

    //                    return true;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //// Log4NetUtil.Error(this, "UploadFile->上传文件 异常:" + ex.ToString() + "|*|localPath:" + localPath);
    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return boolResult;
    //    }
    //    #endregion



    //    #region 上传多文件
    //    /// <summary>
    //    /// 上传多文件
    //    /// </summary>
    //    /// <param name="localFiles">本地路径列表</param>
    //    /// <param name="remoteDic">远端目录("/test")</param>
    //    /// <returns></returns>
    //    public int UploadFiles(IEnumerable<string> localFiles, string remoteDic)
    //    {
    //        int count = 0;
    //        List<FileInfo> listFiles = new List<FileInfo>();

    //        if (localFiles == null)
    //        {
    //            return 0;
    //        }

    //        try
    //        {
    //            foreach (string file in localFiles)
    //            {
    //                if (!File.Exists(file))
    //                {
    //                    // // Log4NetUtil.Error(this, "UploadFiles->本地文件不存在:" + file);
    //                    continue;
    //                }
    //                listFiles.Add(new FileInfo(file));
    //            }

    //            //远端路径校验
    //            if (string.IsNullOrEmpty(remoteDic))
    //            {
    //                remoteDic = "/";
    //            }
    //            if (!remoteDic.StartsWith("/"))
    //            {
    //                remoteDic = "/" + remoteDic;
    //            }
    //            if (!remoteDic.EndsWith("/"))
    //            {
    //                remoteDic += "/";
    //            }

    //            if (Connect())
    //            {
    //                if (listFiles.Count > 0)
    //                {
    //                    count = ftpClient.UploadFiles(listFiles, remoteDic, FtpRemoteExists.Overwrite, true).Count;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //  // Log4NetUtil.Error(this, "UploadFiles->上传文件 异常:" + ex.ToString());

    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return count;
    //    }
    //    #endregion




    //    #region 下载单文件
    //    /// <summary>
    //    /// 下载单文件
    //    /// </summary>
    //    /// <param name="localDic">本地目录(@"D:\test")</param>
    //    /// <param name="remotePath">远程路径("/test/abc.txt")</param>
    //    /// <returns></returns>
    //    public bool DownloadFile(string localDic, string remotePath)
    //    {
    //        bool boolResult = false;
    //        string strFileName = string.Empty;

    //        try
    //        {
    //            //本地目录不存在，则自动创建
    //            if (!Directory.Exists(localDic))
    //            {
    //                Directory.CreateDirectory(localDic);
    //            }
    //            //取下载文件的文件名
    //            strFileName = Path.GetFileName(remotePath);

    //            //拼接本地路径
    //            localDic = Path.Combine(localDic, strFileName);

    //            if (Connect())
    //            {
    //                ftpClient.DownloadFile(localDic, remotePath, FtpLocalExists.Overwrite);
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Log4NetUtil.Error(this, "DownloadFile->下载文件 异常:" + ex.ToString() + "|*|remotePath:" + remotePath);
    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return boolResult;
    //    }
    //    #endregion





    //    #region 下载多文件
    //    /// <summary>
    //    /// 下载多文件
    //    /// </summary>
    //    /// <param name="localDic">本地目录(@"D:\test")</param>
    //    /// <param name="remotePath">远程路径列表</param>
    //    /// <returns></returns>
    //    public int DownloadFiles(string localDic, IEnumerable<string> remoteFiles)
    //    {
    //        int count = 0;
    //        if (remoteFiles == null)
    //        {
    //            return 0;
    //        }

    //        try
    //        {
    //            //本地目录不存在，则自动创建
    //            if (!Directory.Exists(localDic))
    //            {
    //                Directory.CreateDirectory(localDic);
    //            }

    //            if (Connect())
    //            {
    //                count = ftpClient.DownloadFiles(localDic, remoteFiles, FtpLocalExists.Overwrite).Count;


    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log4NetUtil.Error(this, "DownloadFiles->下载文件 异常:" + ex.ToString());
    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return count;
    //    }
    //    #endregion

    //    #region 判断文件是否存在
    //    /// <summary>
    //    /// 判断文件是否存在
    //    /// </summary>
    //    /// <param name="remotePath">远程路径("/test/abc.txt")</param>
    //    /// <returns></returns>
    //    public bool IsFileExists(string remotePath)
    //    {
    //        bool boolResult = false;

    //        try
    //        {
    //            if (Connect())
    //            {
    //                boolResult = ftpClient.FileExists(remotePath);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log4NetUtil.Error(this, "IsFileExists->判断文件是否存在 异常:" + ex.ToString() + "|*|remotePath:" + remotePath);
    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return boolResult;
    //    }
    //    #endregion

    //    #region 判断目录是否存在
    //    /// <summary>
    //    /// 判断目录是否存在
    //    /// </summary>
    //    /// <param name="remotePath">远程路径("/test")</param>
    //    /// <returns></returns>
    //    public bool IsDirExists(string remotePath)
    //    {
    //        bool boolResult = false;

    //        try
    //        {
    //            if (Connect())
    //            {
    //                boolResult = ftpClient.DirectoryExists(remotePath);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // // Log4NetUtil.Error(this, "IsDirExists->判断目录是否存在 异常:" + ex.ToString() + "|*|remotePath:" + remotePath);
    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return boolResult;
    //    }
    //    #endregion

    //    #region 新建目录
    //    /// <summary>
    //    /// 新建目录
    //    /// </summary>
    //    /// <param name="remoteDic">远程目录("/test")</param>
    //    /// <returns></returns>
    //    public bool MakeDir(string remoteDic)
    //    {
    //        bool boolResult = false;

    //        try
    //        {
    //            if (Connect())
    //            {
    //                ftpClient.CreateDirectory(remoteDic);

    //                boolResult = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log4NetUtil.Error(this, "MakeDir->新建目录 异常:" + ex.ToString() + "|*|remoteDic:" + remoteDic);
    //        }
    //        finally
    //        {
    //            DisConnect();
    //        }

    //        return boolResult;
    //    }
    //    #endregion

    //    #region 清理
    //    /// <summary>
    //    /// 清理
    //    /// </summary>
    //    public void Clean()
    //    {
    //        //断开FTP
    //        DisConnect();

    //        if (ftpClient != null)
    //        {
    //            ftpClient.Dispose();
    //        }
    //    }
    //    #endregion






















    //    //public  string ip;
    //    //public  string user;
    //    //public  string password;


    //    //public  void Set设置参数(string ip_, string user_, string password_)
    //    //{
    //    //    ip = ip_;
    //    //    user = user_;
    //    //    password = password_;
    //    //}


    //    //public  bool 连接()
    //    //{
    //    //    try
    //    //    {
    //    //        FtpClient conn = new FtpClient();
    //    //        conn.Host = ip;
    //    //        conn.Credentials = new NetworkCredential(user, password);
    //    //        conn.Connect();
    //    //        bool rt = conn.IsConnected;
    //    //        return rt;

    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        return false;
    //    //    }

    //    //}

    //    //public  void 下载文件()
    //    //{
    //    //    连接();
    //    //}

    //    //public void 获取所有文件及文件夹(FtpClient client, String dir, ref List<FtpListItem> list, ref Dictionary<string, Exception> errorDirDic)
    //    //{
    //    //    FtpListItem[] items = null;
    //    //    try
    //    //    {
    //    //        string safeDir = dir.Replace("/", @"\").ToLower();
    //    //        items = client.GetListing(safeDir);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        errorDirDic.Add(dir, ex);
    //    //        return;
    //    //    }
    //    //    foreach (FtpListItem item in items)
    //    //    {
    //    //        if (item.Type == FtpFileSystemObjectType.Directory)
    //    //        {
    //    //            LoadFtpFileRecursion(client, item.FullName, ref list, ref errorDirDic);
    //    //        }
    //    //        else if (item.Type == FtpFileSystemObjectType.File)
    //    //        {
    //    //            list.Add(item);
    //    //        }
    //    //    }
    //    //}


    //    //// <summary>
    //    ///// FTP服务器文件下载到本地
    //    ///// </summary>
    //    ///// <param name="ftphost">ftp地址：ftp://192.168.1.200/</param>
    //    ///// <param name="user">ftp用户名</param>
    //    ///// <param name="password">ftp密码</param>
    //    ///// <param name="saveLocalPath">下载到本地的地址：d:\\doctument\\0F5GAHRT4A484TRA5D15FEA.pdf</param>
    //    ///// <param name="downPath">将要下载的文件在FTP上的路径：/DownFile/0F5GAHRT4A484TRA5D15FEA</param>
    //    //public  void DownloadFile(string ftphost, string user, string password, string saveLocalPath, string downPath)
    //    //{
    //    //    using (FtpClient conn = new FtpClient())
    //    //    {
    //    //        conn.Host = ftphost;
    //    //        conn.Credentials = new NetworkCredential(user, password);

    //    //        byte[] outBuffs = new byte[] { };
    //    //        bool flag = conn.Download(out outBuffs, downPath);

    //    //        string s = saveLocalPath.Substring(0, saveLocalPath.LastIndexOf('\\'));
    //    //        Directory.CreateDirectory(s);//如果文件夹不存在就创建它

    //    //        FileStream fs = new FileStream(saveLocalPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
    //    //        fs.Write(outBuffs, 0, outBuffs.Length);
    //    //        //清空缓冲区、关闭流
    //    //        fs.Flush();
    //    //        fs.Close();
    //    //    }
    //    //}




    //    ///// <summary>
    //    ///// 将文件上传到FTP服务器
    //    ///// </summary>
    //    ///// <param name="ftphost">ftp地址</param>
    //    ///// <param name="user">ftp用户名</param>
    //    ///// <param name="password">ftp密码</param>
    //    ///// <param name="localPath">本地文件所在的路径："D:\doctument\test.pdf"</param>
    //    //public  void UploadFile(string ftphost, string user, string password, string localPath)
    //    //{

    //    //    using (FtpClient conn = new FtpClient())
    //    //    {
    //    //        conn.Host = ftphost;
    //    //        conn.Credentials = new NetworkCredential(user, password);
    //    //        using (FileStream fs = new FileStream(localPath, FileMode.Open))
    //    //        {
    //    //            string path = localPath.Substring(localPath.LastIndexOf('\\') + 1);       //取文件名
    //    //            conn.Upload(fs, path);
    //    //        }
    //    //    }

    //    //}
















    //}


    //public class ftp_class_结构配置
    //{
    //    #region 构造函数
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    public void FtpConfig()
    //    {
    //        this.int_FtpReadWriteTimeout = 60000;
    //        this.bool_FtpUseBinary = true;
    //        this.bool_FtpUsePassive = true;
    //        this.bool_FtpKeepAlive = true;
    //        this.bool_FtpEnableSsl = false;
    //        this.int_RetryTimes = 3;
    //    }
    //    #endregion

    //    /// <summary>
    //    /// Ftp 标识
    //    /// </summary>
    //    public string str_Name { get; set; }
    //    /// <summary>
    //    /// FTP地址
    //    /// </summary>
    //    public string str_FtpUri { get; set; }
    //    /// <summary>
    //    /// FTP端口
    //    /// </summary>
    //    public int int_FtpPort { get; set; }
    //    /// <summary>
    //    /// FTP路径(/test)
    //    /// </summary>
    //    public string str_FtpPath { get; set; }
    //    /// <summary>
    //    /// FTP用户名
    //    /// </summary>
    //    public string str_FtpUserID { get; set; }
    //    /// <summary>
    //    /// FTP密码
    //    /// </summary>
    //    public string str_FtpPassword { get; set; }
    //    /// <summary>
    //    /// FTP密码是否被加密
    //    /// </summary>
    //    public bool bool_IsEncrypt { get; set; }
    //    /// <summary>
    //    /// 读取或写入超时之前的毫秒数。默认值为 30,000 毫秒。
    //    /// </summary>
    //    public int int_FtpReadWriteTimeout { get; set; }
    //    /// <summary>
    //    /// true，指示服务器要传输的是二进制数据；false，指示数据为文本。默认值为true。
    //    /// </summary>
    //    public bool bool_FtpUseBinary { get; set; }
    //    /// <summary>
    //    /// true，被动模式；false，主动模式(主动模式可能被防火墙拦截)。默认值为true。
    //    /// </summary>
    //    public bool bool_FtpUsePassive { get; set; }
    //    /// <summary>
    //    /// 是否保持连接。
    //    /// </summary>
    //    public bool bool_FtpKeepAlive { get; set; }
    //    /// <summary>
    //    /// 是否启用SSL。
    //    /// </summary>
    //    public bool bool_FtpEnableSsl { get; set; }
    //    /// <summary>
    //    /// 描述
    //    /// </summary>
    //    public string str_Describe { get; set; }
    //    /// <summary>
    //    /// 重试次数
    //    /// </summary>
    //    public int int_RetryTimes { get; set; }
    //    /// <summary>
    //    /// 版本号
    //    /// </summary>
    //    public string str_Ver { get; set; }
    //}









}





