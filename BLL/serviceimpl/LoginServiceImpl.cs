﻿using Maticsoft.BLL.serviceimpl;
using Maticsoft.BLL.serviceinterface;
using Maticsoft.Common;
using Maticsoft.Common.dbUtility;
using Maticsoft.Common.model;
using Maticsoft.Common.model.httpmodel;
using Maticsoft.Common.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Maticsoft.Controller
{
    public class LoginServiceImpl : BaseServiceImpl, ILoginService
    {
        private ISystemSettingsService sysService = new SystemSettingsServiceImpl();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>0-结果正确。1-服务器异常。2-用户名或密码错误，请输入正确的用户名或密码。3-未知错误</returns>
        public int LoginRequest(string userName, string password)
        {
            string jsonMessageRequestLogin = "";
            string jsonLoginResponse = "";
            HttpRequestMsg<BodyLoginRequest> messageRequestLogin;
            HttpResponseMsg<BodyLoginResponse> messageResponseLogin;
            IList<store_machine> storeMachineList;
            IList<machine_can_print_license> machineCanPrintLicenseList;
            try
            {
                messageRequestLogin = new HttpRequestMsg<BodyLoginRequest>("UTF-8",Global.Transcode.LOGIN,"","1.0");
                messageRequestLogin.body.userName = userName;
                messageRequestLogin.body.password = password;

                // 1. MessageRequestLogin转为Json
                jsonMessageRequestLogin = JSonHelper.ObjectToJson(messageRequestLogin);

                // 2. 发送Json到接口
                try
                {
                    jsonLoginResponse = HTTPHelper.HttpHandler ( jsonMessageRequestLogin,
                        GlobalConstants.SERVER_URL_MAP [
                        Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.SERVER_TYPE ] ] );
                }
                catch (Exception)
                {
                    return 1;
                    //throw new Exception("服务器异常。");
                }

                // 3. 解析反馈结果
                messageResponseLogin = JSonHelper.JsonToMessageResponseLogin(jsonLoginResponse);
                
                if (!messageResponseLogin.head.resultCode.Equals(GlobalConstants.ResultCode.SUCCESS))
                {
                    if (messageResponseLogin.head.resultCode.Equals(GlobalConstants.ResultCode.PASSWORD_USERNAME_ERROR))
                    {
                        return 2;
                        //throw new Exception("用户名或密码错误，请输入正确的用户名或密码。");
                    }
                    else
                    {
                        return 3;
                        //throw new Exception("发生未知错误。");
                    }
                }

                // 4. 暂存storeId userId userName
                Global.STORE_ID = messageResponseLogin.body.storeId.ToString();

                //暂存时间毫秒数
                Global.SysDateMillisecond = messageResponseLogin.body.date;

                // 5. 暂存machine_can_print_license、store_machine表数据
                storeMachineList = sysService.getAllStoreMachine(); ;
                machineCanPrintLicenseList = sysService.getMachineCanPrintLicense();

                // 6. 删除store_machine、machine_supported_license、machine_can_print_license
                clearMachineInfo();

                if (null != messageResponseLogin.body.machineList && messageResponseLogin.body.machineList.Count > 0) {
                    // 7. 更新store_machine表、更新machine_supported_license表、更新machine_can_print_license表
                    addMultipleStoreMachine(storeMachineList, messageResponseLogin.body.machineList, machineCanPrintLicenseList);
                }
                
                return 0;
            }
            catch (Exception)
            {
                return 3;
                //throw;
            }
        }


        /// <summary>
        /// 清除机器对应的数据——机器表、机器支持彩种表、机器可出票彩种表
        /// </summary>
        /// <returns></returns>
        public Boolean clearMachineInfo()
        {
            ArrayList sqllist = new ArrayList();
            sqllist.Add("DELETE FROM machine_can_print_license;");
            sqllist.Add("DELETE FROM machine_supported_license;");
            sqllist.Add("DELETE FROM store_machine;");
            try
            {
                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("清除机器对应的数据出错!");
            }
        }

        private int addSingleStoreMachine(store_machine storeMachine)
        {
            string sql = String.Format("INSERT INTO store_machine ({0}) VALUES ({1})", storeMachineColumns, storeMachineParameters);
            try
            {
                return SQLiteHelper.getBLLInstance().ExecuteNonQuery(sql, GetStoreMachineParameterArr(storeMachine));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        private void addMultipleStoreMachine(IList<store_machine> storeMachineList, IList<BodyLoginResponse.Machine> httpMachineList, IList<machine_can_print_license> localLicenseList)
        {
            store_machine storeMachine;
            List<machine_can_print_license> httpLicenseList = new List<machine_can_print_license>();
            List<machine_can_print_license> overlapLicenseList = new List<machine_can_print_license>();
            try
            {
                foreach (BodyLoginResponse.Machine httpMachine in httpMachineList)
                {
                    // 设置store_machine的speed_level
                    storeMachine = setStoreMachineValue(httpMachine.storeMachine,storeMachineList);

                    // http返回的licenseList
                    httpLicenseList = turnMachineLicenseList(httpMachine.machineLicenseList);
                    
                    // 本地与http返回的licenseList的交集
                    overlapLicenseList = getOverlapOfHttpAndLocalMachineCanPrintLicense(httpLicenseList, localLicenseList);

                    // 1. 插入一条store_machine记录
                    addSingleStoreMachine(storeMachine);

                    // 2. 插入store_machine记录相应的machine_supported_license记录
                    addMultipleMachineSupportedLicense(httpLicenseList);

                    // 3. 插入store_machine记录相应的machine_can_print_license记录
                    addMultipleMachinCanPrinteLicense(overlapLicenseList);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 设置speed_level
        /// </summary>
        /// <param name="storeMachine"></param>
        /// <param name="storeMachineList"></param>
        /// <returns></returns>
        private store_machine setStoreMachineValue(storeMachine storeMachine, IList<store_machine> storeMachineList)
        {
            try
            {
                foreach (store_machine sm in storeMachineList)
                {
                    if (sm.terminal_number.Equals(storeMachine.terminalNumber))
                    {
                        storeMachine.comBaudrate = sm.com_baudrate;
                        storeMachine.comDatabits = sm.com_databits;
                        storeMachine.comParity = sm.com_parity;
                        storeMachine.comName = sm.com_name;
                        storeMachine.comStopbits = sm.com_stopbits;
                        storeMachine.isFeedBack = sm.is_feed_back;
                        storeMachine.speedLevel = sm.speed_level;
                        storeMachine.bigTicketAmount = sm.big_ticket_amount;
                        storeMachine.bigTicketPass = sm.big_ticket_pass;
                        storeMachine.isAutoTicket = sm.is_auto_ticket;
                        storeMachine.isContinuousTicket = sm.is_continuous_ticket;
                        storeMachine.isComplAutoStop = sm.is_compl_auto_stop;
                    }
                }
                return new store_machine(storeMachine);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 取本地和http返回的List<machine_can_print_license>的交集
        /// </summary>
        /// <param name="httpLicenseList"></param>
        /// <param name="localLicenseList"></param>
        /// <returns></returns>
        private List<machine_can_print_license> getOverlapOfHttpAndLocalMachineCanPrintLicense(List<machine_can_print_license> httpLicenseList, IList<machine_can_print_license> localLicenseList)
        {
            try
            {
                List<machine_can_print_license> licenseListTemp = new List<machine_can_print_license>();
                foreach (machine_can_print_license httpLicense in httpLicenseList)
                {
                    foreach (machine_can_print_license localLicense in localLicenseList)
                    {
                        if (httpLicense.license_id == localLicense.license_id &&
                            httpLicense.terminal_number.Equals(localLicense.terminal_number, StringComparison.CurrentCultureIgnoreCase))
                        {
                            licenseListTemp.Add(localLicense);
                        }
                    }
                }
                return licenseListTemp;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 把list<machine_can_print_license>转换为list<machineSupportedLicense>
        /// </summary>
        /// <param name="mList"></param>
        /// <returns></returns>
        private List<machine_can_print_license> turnMachineLicenseList(List<machineSupportedLicense> mList)
        {
            try
            {
                List<machine_can_print_license> _mList = new List<machine_can_print_license>();
                foreach (machineSupportedLicense m in mList)
                {
                    _mList.Add(new machine_can_print_license(m));
                }
                return _mList;
            }
            catch (Exception e)
            {
                throw e;
            }           
        }

        private int addMultipleMachineSupportedLicense(List<machine_can_print_license> machineSupportedLicenseList)
        {
            string sql = String.Format("INSERT INTO machine_supported_license ({0}) VALUES ({1})", machineCanPrintLicenseColumns, machineCanPrintLicenseParameters);
            List<SQLiteParameter[]> SQLParasList = new List<SQLiteParameter[]>();
            try
            {
                foreach (machine_can_print_license m in machineSupportedLicenseList)
                {
                    SQLParasList.Add(GetMachineCanPrintLicenseParameterArr(m));
                }
                return SQLiteHelper.getBLLInstance().ExecuteSqlParasTran(sql, SQLParasList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private int addMultipleMachinCanPrinteLicense(List<machine_can_print_license> machineSupportedLicenseList)
        {
            string sql = String.Format("INSERT INTO machine_can_print_license ({0}) VALUES ({1})", machineCanPrintLicenseColumns, machineCanPrintLicenseParameters);
            List<SQLiteParameter[]> SQLParasList = new List<SQLiteParameter[]>();
            try
            {
                foreach (machine_can_print_license m in machineSupportedLicenseList)
	            {
                    SQLParasList.Add(GetMachineCanPrintLicenseParameterArr(m));
	            }
                return SQLiteHelper.getBLLInstance().ExecuteSqlParasTran(sql, SQLParasList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private SQLiteParameter[] GetMachineCanPrintLicenseParameterArr(machine_can_print_license machineCanPrintLicense)
        {
            return new SQLiteParameter[]{
                new SQLiteParameter("@terminal_number",machineCanPrintLicense.terminal_number),
                new SQLiteParameter("@license_id",machineCanPrintLicense.license_id),
                new SQLiteParameter("@license_name",machineCanPrintLicense.license_name)
            };
        }

        public string machineCanPrintLicenseColumns = @"terminal_number,license_id,license_name";
        public string machineCanPrintLicenseParameters = @"@terminal_number,@license_id,@license_name";

        private SQLiteParameter[] GetStoreMachineParameterArr(store_machine sm)
        {
            return new SQLiteParameter[]{
                new SQLiteParameter("@machine_name",sm.machine_name),
		        new SQLiteParameter("@machine_code",sm.machine_code),
		        new SQLiteParameter("@machine_type",sm.machine_type),
		        new SQLiteParameter("@com_name",sm.com_name),
                new SQLiteParameter("@com_baudrate",sm.com_baudrate),
                new SQLiteParameter("@com_databits",sm.com_databits),
		        new SQLiteParameter("@com_stopbits",sm.com_stopbits),
		        new SQLiteParameter("@com_parity",sm.com_parity),
		        new SQLiteParameter("@terminal_number",sm.terminal_number),
                new SQLiteParameter("@is_feed_back",sm.is_feed_back),
                new SQLiteParameter("@speed_level",sm.speed_level),
                new SQLiteParameter("@big_ticket_amount",sm.big_ticket_amount),
                new SQLiteParameter("@big_ticket_pass",sm.big_ticket_pass),
                new SQLiteParameter("@is_auto_ticket",sm.is_auto_ticket),
                new SQLiteParameter("@is_continuous_ticket",sm.is_continuous_ticket),
                new SQLiteParameter("@is_compl_auto_stop",sm.is_compl_auto_stop)
            };
        }

        public string storeMachineColumns = @"machine_name,machine_code,machine_type,com_name,com_baudrate,com_databits,com_stopbits,com_parity,terminal_number,is_feed_back,speed_level,big_ticket_amount,big_ticket_pass,is_auto_ticket,is_continuous_ticket,is_compl_auto_stop";

        public string storeMachineParameters = @"@machine_name,@machine_code,@machine_type,@com_name,@com_baudrate,@com_databits,@com_stopbits,@com_parity,@terminal_number,@is_feed_back,@speed_level,@big_ticket_amount,@big_ticket_pass,@is_auto_ticket,@is_continuous_ticket,@is_compl_auto_stop";
    }
}
