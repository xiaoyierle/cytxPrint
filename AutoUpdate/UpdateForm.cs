﻿using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class UpdateForm : Form
    {
        //ftp操作工具
        SFTPHelper objSFTPHelper = new SFTPHelper();
        ArrayList fileNameList;

        public UpdateForm()
        {
            InitializeComponent();
        }

        //下载服务器上的relealist文件
        private void UpdateForm_Shown(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.upgradeForm));
        }

        private void upgradeForm(object state)
        {
            try
            {
                objSFTPHelper.Connect();
                if (objSFTPHelper.Connected)
                {
                    //objSFTPHelper.Download(SFTPGlobal.FtpPath + SFTPGlobal.ReleaseConfigFileName, SFTPGlobal.tempPath + SFTPGlobal.ReleaseConfigFileName);
                    //objSFTPHelper.Close();

                    if (!File.Exists(Application.StartupPath + "\\" + SFTPGlobal.ReleaseConfigFileName))
                    {
                        //不存在
                        this.upgrade();
                    }
                    else
                    {
                        //获取服务器版本信息,把relealist.xml文件下载到本地后，从本地读取
                        if (objSFTPHelper.Download(SFTPGlobal.FtpPath + SFTPGlobal.ReleaseConfigFileName, Application.StartupPath + "\\tempList.xml"))
                        {
                            //获取本地和服务器的relealist文件
                            SFTPGlobal.remoteRelease = new ReleaseList(Application.StartupPath + "\\tempList.xml");
                            SFTPGlobal.localRelease = new ReleaseList(string.Format("{0}\\{1}", Application.StartupPath, SFTPGlobal.ReleaseConfigFileName));

                            //比较本机文件和服务器上的XML文件,如果不同则升级
                            if (null == SFTPGlobal.localRelease || null == SFTPGlobal.remoteRelease || SFTPGlobal.localRelease.Compare(SFTPGlobal.remoteRelease) != 0 || !this.FileExist())
                            {
                                this.upgrade();
                            }
                        }
                    }
                }
            }
            catch
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                    {
                        this.Close();
                    }));
                }
                else
                {
                    this.Close();
                }
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                {
                    this.Close();
                }));
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        private bool FileExist()
        {
            try
            {
                fileNameList = objSFTPHelper.GetFileList(SFTPGlobal.FtpPath);
                if (null != fileNameList)
                {
                    foreach (String fileName in fileNameList)
                    {
                        if (!File.Exists(Application.StartupPath + "\\" + fileName))
                        {
                            //不存在
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upgrade()
        {
            try
            {
                fileNameList = objSFTPHelper.GetFileList(SFTPGlobal.FtpPath);
                if (CheckProcessing() == DialogResult.OK && null != fileNameList)
                {
                    this.moduleProgressBar.Value = 0;
                    this.moduleProgressBar.Maximum = fileNameList.Count;

                    //加载音频文件
                    DirectoryInfo dinfo = new DirectoryInfo(Application.StartupPath);
                    FileInfo[] fsi = dinfo.GetFiles();
                    foreach (var item in fsi)
                    {
                        if (!(item.Name.Contains("CYTXPrint.exe")||item.Name.Contains("DiffieHellman.dll")||item.Name.Contains("Org.Mentalis.Security.dll") || item.Name.Contains("Tamir.SharpSSH.dll")))
                        {
                            try
                            {
                                item.Delete();
                            }
                            catch (Exception)
                            {}
                        }
                    }

                    foreach (String fileName in fileNameList)
                    {
                        if (objSFTPHelper.Download(SFTPGlobal.FtpPath + fileName, SFTPGlobal.tempPath + fileName))
                        {
                            if (this.moduleProgressBar.InvokeRequired)
                            {
                                this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                                {
                                    this.moduleProgressBar.Value++;
                                }));
                            }
                            else
                            {
                                this.moduleProgressBar.Value++;
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("下载文件{0}时发生错误,请重启程序。", fileName));
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                        {
                            string tempFileName = Application.StartupPath + "\\ReleaseList.xml";
                            //判断文件是不是存在
                            if (File.Exists(tempFileName))
                            {
                                //如果存在则删除
                                File.Delete(tempFileName);
                            }
                            this.Close();
                        }));
                    }
                    else
                    {
                        string tempFileName = Application.StartupPath + "\\ReleaseList.xml";
                        //判断文件是不是存在
                        if (File.Exists(tempFileName))
                        {
                            //如果存在则删除
                            File.Delete(tempFileName);
                        }
                        this.Close();
                    }
                        }
                    }
                }
            }
            catch (Exception)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVersionInfo_Click(object sender, EventArgs e)
        {
            //先从服务器获取版本信息文件
            Boolean b = objSFTPHelper.Download(SFTPGlobal.FtpPath + "cytx-version-info.version", Application.StartupPath + "\\cytx-version-info.version");
            if (b)
            {
                this.Read(Application.StartupPath + "\\cytx-version-info.version");
            }
            else
            {
                //this.tBoxMsg.AppendText(DateTime.Now.ToShortTimeString() + ":获取版本信息失败******" + Environment.NewLine);
            }
        }

        /// <summary>
        /// 判断现在是否有主项目在运行
        /// </summary>
        /// <returns></returns>
        static DialogResult CheckProcessing()
        {
            if (Process.GetProcessesByName("Dome").Length > 0)
            {
                var rs = MessageBox.Show("请先退出正在运行的Dome.exe", "警告", MessageBoxButtons.RetryCancel,
                                         MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.Retry)
                {
                    return CheckProcessing();
                }
                return rs;
            }
            return DialogResult.OK;
        }

        /// <summary>
        /// 写txt文件
        /// </summary>
        /// <param name="versionFilePath"></param>
        /// <param name="lines"></param>
        public void Write(String versionFilePath, string[] lines)
        {
            using (StreamWriter streamWriter = new StreamWriter(versionFilePath, true))
            {
                foreach (string line in lines)
                {
                    streamWriter.WriteLine(line);// 直接追加文件末尾，换行   
                }
            }
        }

        /// <summary>
        /// 读txt文件
        /// </summary>
        /// <param name="versionFilePath"></param>
        public void Read(String versionFilePath)
        {
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(versionFilePath);
                foreach (string line in lines)
                {
                    //this.tBoxMsg.AppendText(line);
                }
                //this.tBoxMsg.AppendText(Environment.NewLine+ Environment.NewLine);
            }
            catch (FileNotFoundException)
            {
                using (StreamWriter streamWriter = new StreamWriter(versionFilePath, true))
                {
                    streamWriter.WriteLine("");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\Demo.exe");
                objSFTPHelper.Close();
            }
            catch
            {
                MessageBox.Show("程序打开失败！");
            }
            finally
            {
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }
    }
}
