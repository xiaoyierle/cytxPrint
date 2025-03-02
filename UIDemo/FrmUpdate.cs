﻿using Maticsoft.BLL.log;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.model.httpmodel;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Demo
{
    public partial class FrmUpdate : Form
    {
        FrmMain _frmMain = null;
        public FrmUpdate(FrmMain frmMain)
        {
            InitializeComponent();
            _frmMain = frmMain;
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 数据升级
        /// </summary>
        public void DataUpdate(object sender, EventArgs e)
        {
            try
            {
                //new FrmDataUpdate().ShowDialog();
                //1、请求数据；2、删除数据库数据；3、更新新的数据
                HttpRequestMsg<HttpBody> hrmsg = new HttpRequestMsg<HttpBody>("UTF-8", GlobalConstants.TRANSCODE.SYNC, Global.STORE_ID.ToString(), "1.0");
                string requestSYNC = JSonHelper.ObjectToJson(hrmsg);
                string responseSYNC = null;
                try
                {
                    responseSYNC = HTTPHelper.HttpHandler ( requestSYNC, GlobalConstants.SERVER_URL_MAP [
                        Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.SERVER_TYPE ] ] );
                }
                catch (Exception)
                {
                    
                    throw;
                }
                HttpResponseMsg<Body1002Response> responsemsgSYNC = (HttpResponseMsg<Body1002Response>)JSonHelper.JsonToHttpResponseMsg<Body1002Response>(responseSYNC);

                //hrmsg = new HttpRequestMsg<HttpBody>("UTF-8", GlobalConstants.TRANSCODE.LICENSE, Global.STORE_ID.ToString(), "1.0");
                //string requestLICENSE = JSonHelper.ObjectToJson(hrmsg);
                //string responseLICENSE = HTTPHelper.HttpHandler(requestLICENSE, Global.sysconfig.server_url);
                //HttpResponseMsg<BodyLicenseResponse> responsemsgLICENSE = (HttpResponseMsg<BodyLicenseResponse>)JSonHelper.JsonToHttpResponseMsg<BodyLicenseResponse>(responseLICENSE);

                if (responsemsgSYNC.head.resultCode.Equals(GlobalConstants.ResultCode.SUCCESS, StringComparison.InvariantCultureIgnoreCase))
                {
                    //取到数据
                    Body1002Response body = (Body1002Response)responsemsgSYNC.body;
                    AutoTaskController atc = new AutoTaskController();
                    if (atc.updateSpeedLevel(body.Speedconlist, body.Speedcmdlist)) {
                        //速度级别及对应配置的初始化
                        SystemSettingsController con = new SystemSettingsController();
                        List<speed_level_config> slclist = con.getAllSpeedLevelConfig();
                        if (null != slclist && slclist.Count > 0)
                        {
                            Global.SLC_DICTIONARY.Clear();//先清空再赋值
                            foreach (speed_level_config item in slclist)
                            {
                                List<speed_level_cmd> cmdList = con.getSpeedCmdByLevel(item.speed_level);
                                if (null != cmdList && cmdList.Count > 0)
                                {
                                    SpeedConfigCmd scc = new SpeedConfigCmd(item, cmdList);
                                    Global.SLC_DICTIONARY.Add(item.speed_level, scc);
                                }
                            }

                            this.progressBar.Value = 100;
                            this.lbTips.Text = "数据升级成功!";

                            Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.CONTROLDATA_UPDATE_DATE ] =
                                DateUtil.getServerDateTime ( DateUtil.DATE_FMT_STR4 );

                            con.updateSystemConfig(new Dictionary<string, string> ( ) { { GlobalConstants.SYSTEM_CONFIG_KEYS.CONTROLDATA_UPDATE_DATE, Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.CONTROLDATA_UPDATE_DATE ] } });
                            _frmMain.TStripStatusLabUpdate.Text = Global.SYSTEM_CONFIG_MAP [ 
                                GlobalConstants.SYSTEM_CONFIG_KEYS.CONTROLDATA_UPDATE_DATE ];
                        }
                        else
                        {
                            this.lbTips.Text = "数据升级失败!";
                        }
                    }
                    else
                    {
                        this.lbTips.Text = "数据升级失败!";
                    }      
                    

                    //if (responsemsgLICENSE.head.resultCode.Equals(GlobalConstants.ResultCode.SUCCESS, StringComparison.InvariantCultureIgnoreCase))
                    //{
                    //    //取到数据
                    //    BodyLicenseResponse bodyLicenseResponse = (BodyLicenseResponse)responsemsgLICENSE.body;
                    //    LicenseController licenseController = new LicenseController();
                    //    licenseController.UpdateLicense(bodyLicenseResponse.LicenseList, bodyLicenseResponse.PlayList);

                    //    this.progressBar.Value = 100;
                    //}
                    
                    //LogUtil.getInstance().addLogDataToQueue("首页升级数据成功!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                    //MsgBox.getInstance().Show("数据升级成功!", "提示", MsgBox.MyButtons.OK);
                }
                else
                {
                    this.lbTips.Text = "数据升级失败!";
                    //LogUtil.getInstance().addLogDataToQueue("首页升级数据失败!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                    //MsgBox.getInstance().Show("数据升级失败!", "提示", MsgBox.MyButtons.OK);
                }

            }
            catch (Exception)
            {
                this.lbTips.Text = "数据升级失败!";
                //LogUtil.getInstance().addLogDataToQueue("首页升级数据失败!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                //LogUtil.getInstance().addLogDataToQueue("首页升级数据异常!", GlobalConstants.LOGTYPE_ENUM.EXCEOTION);
                //MsgBox.getInstance().Show("数据升级失败!", "提示", MsgBox.MyButtons.OK);
            }
            finally
            {
                this.btnUpdate.Enabled = false;
            }
        }

        private void btnUpdateAgain_Click(object sender, EventArgs e)
        {
            this.lbTips.Text = "";
            this.progressBar.Value = 0;
            this.btnUpdate.Enabled = false;
            this.DataUpdate(null,null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = this.btnUpdate.BackgroundImage = global::Demo.Properties.Resources.btnUpdateFocused;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = this.btnUpdate.BackgroundImage = global::Demo.Properties.Resources.btnUpdateUnfocused;
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = this.btnCancel.BackgroundImage = global::Demo.Properties.Resources.btnCancelFocused;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = this.btnCancel.BackgroundImage = global::Demo.Properties.Resources.btnCancelUnfocused;
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
