﻿using System;
using System.Collections.Generic;
using Maticsoft.Common.model;
namespace Maticsoft.BLL.serviceinterface
{
    interface ISystemSettingsService
    {
        #region 系统设置
        /// <summary>
        /// 初始化系统设置
        /// </summary>
        /// <returns></returns>
        int initSystemConfig ( );
        /// <summary>
        /// 查询系统配置对象列表
        /// </summary>
        /// <returns></returns>
        List<system_config> getSystemConfig ( );

        /// <summary>
        /// 修改sys_config信息
        /// </summary>
        /// <returns></returns>
        bool updateSystemConfig(Dictionary<String,String> kv);
        #endregion 系统设置

        #region 店铺彩机
        /// <summary>
        /// 查询所有的店铺彩机
        /// </summary>
        /// <returns></returns>
        List<store_machine> getAllStoreMachine();

        /// <summary>
        /// 根据Id获取机器信息
        /// </summary>
        /// <param name="mId"></param>
        /// <returns></returns>
        store_machine getStoreMachineById(string mId); 

        /// <summary>
        /// 插入一条店铺彩机数据
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        bool insertStoreMachine(store_machine machine, List<machine_can_print_license> l);

        /// <summary>
        /// 修改一条店铺彩机数据
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        bool updateStoreMachine(store_machine machine, List<machine_can_print_license> l);

        /// <summary>
        /// 删除一条店铺彩机数据
        /// </summary>
        /// <param name="mId"></param>
        /// <returns></returns>
        bool deleteStoreMachineById(string mId);

        /// <summary>
        /// 获取指定彩机所有的支持采种信息
        /// </summary>
        /// <returns></returns>
        List<machine_supported_license> getMachineSupportedLicenseByTId(String mId);

        /// <summary>
        /// 获取指定彩机所有的采种信息
        /// </summary>
        /// <returns></returns>
        List<machine_can_print_license> getMachineCanPrintLicenseByTId(string mId);

        /// <summary>
        /// 获取所有彩机支持的采种信息
        /// </summary>
        /// <returns></returns>
        List<machine_can_print_license> getMachineCanPrintLicense();

        /// <summary>
        /// 删除机器彩种表中对应机器的数据
        /// </summary>
        /// <returns></returns>
        Boolean delMachineLicenseBymId(string mId);

        /// <summary>
        /// 向店铺彩机表中插入数据
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        Boolean insertMachineLicense(store_machine sm, List<machine_can_print_license> l);

        #endregion 店铺彩机

        #region 流程控制
        /// <summary>
        /// 获取所有速度级别
        /// </summary>
        /// <returns></returns>
        List<speed_level_config> getAllSpeedLevelConfig();
        
        /// <summary>
        /// 根据参数查询对应的流程控制记录
        /// </summary>
        /// <param name="machine_id"></param>
        /// <param name="license_id"></param>
        /// <param name="numbering"></param>
        /// <returns></returns>
        List<speed_level_cmd> getSpeedLevelCmdByParams(Dictionary<string, string> param);
        #endregion 流程控制      

        #region 错误操作选择
        /// <summary>
        /// 查询所有的错误处理方式
        /// </summary>
        /// <returns></returns>
        List<error_handling> getAllErrorHandling();

        /// <summary>
        /// 修改错误处理方式
        /// </summary>
        /// <param name="errorhandling"></param>
        /// <returns></returns>
        Boolean updateErrorHandling(error_handling errorhandling);
        #endregion 错误操作选择
    }
}
