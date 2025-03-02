﻿using Maticsoft.BLL.log;
using Maticsoft.BLL.ScanPortImage;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Demo
{
    public partial class Login : Form
    {
        private readonly LoginController loginController = new LoginController();
        //初始化系统配置
        SystemSettingsController con = new SystemSettingsController();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //
            if (String.IsNullOrEmpty(this.txtUserName.Text))
            {
                MsgBox.getInstance().Show("请输入用户名。");
                return;
            }
            else if (String.IsNullOrEmpty(this.txtPassword.Text))
            {
                MsgBox.getInstance().Show("请输入密码。");
                return;
            }
            else
            {
                try
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(login));
                }
                catch (Exception e2)
                {
                    //MsgBox.getInstance().Show("登陆时发生异常。");
                    MsgBox.getInstance().Show(e2.ToString());
                }
            }
        }

        private void login(object o)
        {
            int result = 0;
            this.btnLogin.Invoke ( new EventHandler ( delegate ( object o2, EventArgs e2 )
            {
                try
                {
                    this.btnLogin.Enabled = false;
                    result = loginController.LoginRequest ( this.txtUserName.Text, this.txtPassword.Text );
                    
                    //0-结果正确。1-服务器异常。2-用户名或密码错误，请输入正确的用户名或密码。3-未知错误
                    if ( result == 0 )
                    {
                        if ( !this.txtUserName.Text.Equals ( Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.LAST_LOGIN_NAME ] ) )
                        {
                            Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.LAST_LOGIN_NAME ] = this.txtUserName.Text;
                            this.con.updateSystemConfig ( new Dictionary<String, String> ( ) { 
                        {GlobalConstants.SYSTEM_CONFIG_KEYS.LAST_LOGIN_NAME,Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.LAST_LOGIN_NAME ]}} );
                        }
                        //记录日志——登陆成功
                        LogUtil.getInstance ( ).addLogDataToQueue ( "登陆成功", Maticsoft.Common.Util.GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR );
                        LogUtil.getInstance ( ).addLogDataToQueue ( "登陆成功", Maticsoft.Common.Util.GlobalConstants.LOGTYPE_ENUM.SYSTEM_OPERATION );
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close ( );

                    }
                    else if ( result == 1 )
                    {
                        MsgBox.getInstance ( ).Show ( "服务器异常,请稍后重试。", "提示", MsgBox.MyButtons.OK );
                        if ( this.btnLogin.InvokeRequired )
                        {
                            this.btnLogin.Invoke ( new EventHandler ( delegate ( object o3, EventArgs e )
                            {
                                this.btnLogin.Enabled = true;
                            } ) );
                        }
                        else
                        {
                            this.btnLogin.Enabled = true;
                        }
                    }
                    else if ( result == 2 )
                    {
                        MsgBox.getInstance ( ).Show ( "用户名或密码错误，请输入正确的用户名或密码。", "提示", MsgBox.MyButtons.OK );
                        if ( this.btnLogin.InvokeRequired )
                        {
                            this.btnLogin.Invoke ( new EventHandler ( delegate ( object o3, EventArgs e )
                            {
                                this.btnLogin.Enabled = true;
                            } ) );
                        }
                        else
                        {
                            this.btnLogin.Enabled = true;
                        }
                    }
                    else if ( result == 3 )
                    {
                        MsgBox.getInstance ( ).Show ( "未知错误", "提示", MsgBox.MyButtons.OK );
                        if ( this.btnLogin.InvokeRequired )
                        {
                            this.btnLogin.Invoke ( new EventHandler ( delegate ( object o3, EventArgs e )
                            {
                                this.btnLogin.Enabled = true;
                            } ) );
                        }
                        else
                        {
                            this.btnLogin.Enabled = true;
                        }
                    }
                }
                catch ( Exception ce )
                {
                    MsgBox.getInstance ( ).Show ( ce.Message.ToString ( ), "提示", MsgBox.MyButtons.OK );
                    LogUtil.getInstance ( ).addLogDataToQueue ( ce.StackTrace, Maticsoft.Common.Util.GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR );
                    if ( this.btnLogin.InvokeRequired )
                    {
                        this.btnLogin.Invoke ( new EventHandler ( delegate ( object o3, EventArgs e )
                        {
                            this.btnLogin.Enabled = true;
                        } ) );
                    }
                    else
                    {
                        this.btnLogin.Enabled = true;
                    }
                }
            } ) );

        }

        private void Login_Load(object sender, EventArgs e)
        {       
            try
            {
                GlobalConstants.ConnectionStringBll = string.Format ( "data source=\"{0}\"", System.Windows.Forms.Application.StartupPath + "\\db\\sysdb.db" );
                GlobalConstants.ConnectionStringLog = string.Format ( "data source=\"{0}\"", System.Windows.Forms.Application.StartupPath + "\\db\\log4net.db" );

                //初始化系统配置
                con.initSystemConfig ( );
                //读取系统配置
                List<system_config> sclist = con.getSysConfig ( );
                foreach ( system_config item in sclist )
                {
                    Global.SYSTEM_CONFIG_MAP.Add (item.key,item.value );
                }

                this.txtUserName.Text = Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.LAST_LOGIN_NAME ];
                SPImageGlobal.IS_PRINT_SCAN_IMAGE = Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PRINT_TYPE ].Equals ( GlobalConstants.PRINT_TYPE.PRINTER );
            }
            catch (Exception ce)
            {
                LogUtil.getInstance().addLogDataToQueue("初始化参数异常"+ce.StackTrace, Maticsoft.Common.Util.GlobalConstants.LOGTYPE_ENUM.EXCEOTION);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnCloseEnter;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnClose;
        }

        private void picMinimize_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnMinimizeEnter;
        }

        private void picMinimize_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnMinimize;
        }

        private void lbSet_Click(object sender, EventArgs e)
        {
            FrmInit frmInit = new FrmInit();
            frmInit.ShowDialog();
        }
    }
}
