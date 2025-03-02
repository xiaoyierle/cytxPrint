﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using Maticsoft.BLL.log;

namespace Demo
{
    public partial class FrmConfigPopupAddMachines : Form
    {
        private store_machine smachine=null;
        SystemSettingsController ssc = new SystemSettingsController();
        private speed_level_config slc = null;//当前选择的彩机速度级别

        public speed_level_config SpeedLevelConfig
        {
            get { return slc; }
            set
            {
                slc = value;
                this.labTicketInterval.Text = String.Format("{0}毫秒",slc.ticket_interval);
                this.labDigitalInterval.Text = String.Format("{0}毫秒", slc.digital_interval);
                this.labEnterInterval.Text = String.Format("{0}毫秒", slc.enter_interval);

                this.labDynamicInterval.Text = String.Format("{0}毫秒至{1}毫秒",new String[]{ slc.dynamic_interval_min.ToString(),slc.dynamic_interval_max.ToString()});
            }
        }

        public FrmConfigPopupAddMachines(store_machine storeMachine)
        {
            InitializeComponent();
            this.smachine = storeMachine;           
        }

        /// <summary>
        /// 界面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfigPopupAddMachines_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化速度级别
                foreach (String key in GlobalConstants.SpeedLevelDictionary.Keys)
                {
                    this.cbspeedsign.Items.Add(new ComboboxItem(key, GlobalConstants.SpeedLevelDictionary[key]));
                }

                //初始化校验位
                foreach (String key in GlobalConstants.ParityDic.Keys)
                {
                    this.cbCheck.Items.Add(new ComboboxItem(GlobalConstants.ParityDic[key], key));
                }

                if (null != this.smachine)
                {
                    this.SpeedLevelConfig = Global.SLC_DICTIONARY[this.smachine.speed_level.ToString()];
                    this.labTerminateNum.Text = this.smachine.terminal_number;
                    this.labMachineName.Text = this.smachine.machine_name;
                    this.labMachineType.Text = GlobalConstants.machineTypeDictionary[this.smachine.machine_type];

                    this.cbPort.Text = this.smachine.com_name;
                    this.cbBaud.Text = this.smachine.com_baudrate.ToString();
                    this.cbCheck.Text = this.smachine.com_parity;
                    this.cbData.Text = this.smachine.com_databits.ToString();
                    this.cbStop.Text = this.smachine.com_stopbits.ToString();
                    this.txtBigMoey.Text = this.smachine.big_ticket_amount.ToString();
                    this.txtBigPassword.Text = this.smachine.big_ticket_pass;

                    this.cbspeedsign.Text = GlobalConstants.SpeedLevelDictionary[this.smachine.speed_level.ToString()];

                    this.chIsFeedback.CheckState = this.smachine.is_feed_back.ToString().Equals(GlobalConstants.TrueFalseSign.TRUE) ? 
                        CheckState.Checked : CheckState.Unchecked;
                    this.chIsComplAutoStop.CheckState = this.smachine.is_compl_auto_stop.ToString().Equals(GlobalConstants.TrueFalseSign.TRUE)?
                        CheckState.Checked : CheckState.Unchecked;
                    this.chIsContinuousTicket.CheckState = this.smachine.is_continuous_ticket.ToString().Equals(GlobalConstants.TrueFalseSign.TRUE) ?
                        CheckState.Checked : CheckState.Unchecked;

                    //彩机支持彩种
                    List<machine_supported_license> msllist = ssc.getMachineSupportedLicenseByTId(this.smachine.terminal_number);
                    List<machine_can_print_license> mcpllist = ssc.getMachineCanPrintLicenseByTId(this.smachine.terminal_number);


                    //初始化采种的选择
                    if (null != msllist && msllist.Count > 0)
                    {
                        foreach (machine_supported_license item in msllist)
                        {
                            CheckBox cb = new CheckBox();
                            cb.Text = item.license_name;
                            cb.Name = "cb_license_" + item.license_id;
                            cb.CheckState = CheckState.Unchecked;//默认未选中
                            this.flPlLicense.Controls.Add(cb);

                            if (null != mcpllist && mcpllist.Count > 0)
                            {
                                for (int i = 0; i < mcpllist.Count; i++)
                                {
                                    if (item.license_id == mcpllist[i].license_id)
                                    {
                                        mcpllist.RemoveAt(i);
                                        cb.CheckState = CheckState.Checked;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MsgBox.getInstance().Show("配置数据不是最新的,请先进行数据升级!", "提示", MsgBox.MyButtons.OK);
            }
        }


        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseWindow_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        /// <summary>
        /// 修改彩机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.smachine.com_name = this.cbPort.Text;
            this.smachine.com_baudrate = long.Parse(this.cbBaud.Text);
            this.smachine.com_parity = this.cbCheck.Text;
            this.smachine.com_databits = long.Parse(this.cbData.Text);
            this.smachine.com_stopbits = this.cbStop.Text;
            this.smachine.big_ticket_amount = long.Parse(this.txtBigMoey.Text);
            this.smachine.big_ticket_pass = this.txtBigPassword.Text;
            this.smachine.terminal_number = this.labTerminateNum.Text;

            this.smachine.speed_level = long.Parse(((ComboboxItem)this.cbspeedsign.SelectedItem).Key.ToString());

            this.smachine.is_feed_back = this.chIsFeedback.CheckState == CheckState.Checked ? 1 : 0;
            this.smachine.is_compl_auto_stop = this.chIsComplAutoStop.CheckState == CheckState.Checked ? 1 : 0;
            this.smachine.is_continuous_ticket = this.chIsContinuousTicket.CheckState == CheckState.Checked ? 1 : 0;

            List<machine_can_print_license> llist = new List<machine_can_print_license>();
            
            foreach (CheckBox cb in this.flPlLicense.Controls)
            {
                if(cb.CheckState == CheckState.Checked){
                    Int64 lid = Int64.Parse(cb.Name.Split('_')[2]);
                    llist.Add(new machine_can_print_license(this.labTerminateNum.Text,lid,cb.Text));
                }                
            }

            //选择的采种
            if (llist.Count == 0)
            {
                MsgBox.getInstance().Show("请选择彩机支持出票的彩种!", "提示", MsgBox.MyButtons.OK);
                return;
            }

            try
            {
                if (ssc.updateStoreMachine(this.smachine, llist))
                {
                    LogUtil.getInstance().addLogDataToQueue("修改彩机信息成功!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                    //重新初始化彩机可出票采种
                    Dictionary<String, machine_can_print_license> ldic = new Dictionary<string, machine_can_print_license>();
                    foreach (machine_can_print_license item in llist)
                    {
                        ldic.Add(item.license_id.ToString(), item);
                    }
                    Global.MachineCanPrintLicenseDic[this.smachine.terminal_number] = ldic;

                    MsgBox.getInstance().Show("修改彩机成功!", "提示", MsgBox.MyButtons.OK);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    LogUtil.getInstance().addLogDataToQueue("修改彩机信息失败!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                    MsgBox.getInstance().Show("修改彩机失败!", "提示", MsgBox.MyButtons.OK);
                }
            }
            catch (Exception)
            {
                LogUtil.getInstance().addLogDataToQueue("修改彩机信息异常!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                LogUtil.getInstance().addLogDataToQueue("修改彩机信息异常!", GlobalConstants.LOGTYPE_ENUM.EXCEOTION);
                MsgBox.getInstance().Show("修改彩机异常!", "提示", MsgBox.MyButtons.OK);
            }
        }

        private void btnUpdate_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnSaveModificationEnter;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnSaveModification;
        }

        private void cbspeedsign_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.smachine.speed_level = long.Parse(((ComboboxItem)this.cbspeedsign.SelectedItem).Key.ToString());
            this.SpeedLevelConfig = Global.SLC_DICTIONARY[this.smachine.speed_level.ToString()];
        }

    }
}
