﻿using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.BLL;
using Maticsoft.BLL.serviceimpl;
using Maticsoft.Common.model;

namespace Maticsoft.Controller
{

    /// <summary>
    /// 错漏票处理控制器，处理业务包括：
    /// 1、读取所有的错漏票订单——用于初始化
    /// 2、读取所有未放置于界面上的错漏票订单——用于追加
    /// 3、暂存错漏票的选择
    /// 4、确定错漏票的选择
    /// </summary>
    public class ErrorTicketController
    {
        ErrorTicketServiceImpl etsimpl = new ErrorTicketServiceImpl();
        /// <summary>
        /// 读取所有的错漏票订单——用于初始化
        /// </summary>
        /// <param name="terminalNumber"></param>
        /// <returns></returns>
        public List<lottery_order> getAllErrorTicketOrder(String terminalNumber)
        {
            try
            {
                return etsimpl.getAllErrorTicketOrder(terminalNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 查询所有的错漏票订单——分页
        /// </summary>
        /// <param name="sno"></param>
        /// <param name="psize"></param>
        /// <returns></returns>
        public List<lottery_order> getAllErrorTicketOrderPagination(Int64 sno, Int64 psize)
        {
            try
            {
                return etsimpl.getAllErrorTicketOrderPagination(sno, psize);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取所有含有错漏票的订单的数量
        /// </summary>
        /// <returns></returns>
        public int getAllErrorTicketOrderNum()
        {
            try
            {
                return etsimpl.getAllErrorTicketOrderNum();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 读取所有未放置于界面上的错漏票订单——用于追加
        /// </summary>
        /// <param name="terminalNumber"></param>
        /// <returns></returns>
        public List<lottery_order> getNotInFormErrorTicketOrder(String terminalNumber)
        {
            return null;
        }

        /// <summary>
        /// 根据订单Id获取订单下的所有错漏票
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<lottery_ticket> getErrorTicketsByOrderId(String orderId)
        {
            try
            {
                return etsimpl.getErrorTicketsByOrderId(orderId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 暂存错漏票的选择
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean stagingOperatingResult(String orderId, Dictionary<String, String> param)
        {
            try
            {
                return etsimpl.stagingOperatingResult(orderId, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 确定错漏票的选择
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean saveOperatingResult(lottery_order order, Dictionary<String, String[]> param, ref bool finished)
        {
            try
            {
                return etsimpl.saveOperatingResult(order, param, ref finished);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lottery_order"></param>
        /// <param name="p"></param>
        public bool saveAllOperatingResult(lottery_order lottery_order, int state)
        {
            try
            {
                return etsimpl.saveAllOperatingResult(lottery_order, state);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取指定订单下所有的票数量
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public long getTicketsNumByOrderId(string oid)
        {
            try
            {
                return etsimpl.getTicketsNumByOrderId(oid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<lottery_ticket> getTicketsByOrderIdPagination(string oid, long sno, long size)
        {
            try
            {
                return etsimpl.getTicketsByOrderIdPagination(oid, sno, size);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据订单Id和状态获取订单下的所有票——分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<lottery_ticket> getTicketsByOrderIdAndStatesPagination(string orderId, List<String> states, Int64 sno, Int64 psize)
        {
            try
            {
                return etsimpl.getTicketsByOrderIdAndStatesPagination(orderId, states, sno, psize);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据订单Id和状态获取订单下的所有票的数量
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Int64 getTicketsNumByOrderIdAndStates(string orderId, List<String> states)
        {
            try
            {
                return etsimpl.getTicketsNumByOrderIdAndStates(orderId, states);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
