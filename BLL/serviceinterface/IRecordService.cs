﻿using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Common.model;

namespace Maticsoft.BLL.serviceinterface
{
   public interface IRecordService:IBaseService
    {
       /// <summary>
       /// 根据条件查询订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="license_id"></param>
       /// <param name="orderId"></param>
       /// <returns></returns>
       List<lottery_order> getRecordStatisticsBy(String strLicense, String strState, String strOrderId, String startPageNo, String pageSize);

       /// <summary>
       /// 查询起始时间到结束时间内的所有已处理的订单的统计
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       TicketRecordStatistics getAllTicketedRecordStatistics(String startTime,String endTime);

       /// <summary>
       /// 查询起始时间到结束时间内的所有已处理的订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       List<lottery_order> getAllTicketedRecord(String startTime, String endTime, String pageSize, String pageNo);

       /// <summary>
       /// 查询所有订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       List<lottery_order> getAllRecord(String pageSize, String pageNo);

       /// <summary>
       /// 查询起始时间到结束时间内的所有已处理的订单数量
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       int getAllTicketedRecordNum(String startTime, String endTime);


       /// <summary>
       /// 查询起始时间到结束时间内的所有已反馈的订单的统计
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       TicketRecordStatistics getAllFeedBackTicketedRecordStatistics(String startTime, String endTime);

       /// <summary>
       /// 查询起始时间到结束时间内的所有反馈的订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       List<lottery_order> getAllFeedBackTicketedRecord(String startTime, String endTime, String pageSize, String pageNo);
       /// <summary>
       /// 查询起始时间到结束时间内的所有反馈的订单数量
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       int getAllFeedBackTicketedRecordNum(String startTime, String endTime);


       /// <summary>
       /// 查询所有订单数量
       /// </summary>
       /// <returns></returns>
       int getAllRecordNum();


       /// <summary>
       /// 查询起始时间到结束时间内的所有未反馈的订单的统计
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       TicketRecordStatistics getAllNotFeedBackTicketedRecordStatistics(String startTime, String endTime);

       /// <summary>
       /// 查询起始时间到结束时间内的所有未反馈的订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       List<lottery_order> getAllNotFeedBackTicketedRecord(String startTime, String endTime, String pageSize, String pageNo);
       /// <summary>
       /// 查询起始时间到结束时间内的所有未反馈的订单数量
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       int getAllNotFeedBackTicketedRecordNum(String startTime, String endTime);


        /// <summary>
       /// 查询起始时间到结束时间内的所有撤票的订单的统计
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       TicketRecordStatistics getAllCancelTicketedRecordStatistics(String startTime, String endTime);

       /// <summary>
       /// 查询起始时间到结束时间内的所有撤票的订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       List<lottery_order> getAllCancelTicketedRecord(String startTime, String endTime, String pageSize, String pageNo);
       /// <summary>
       /// 查询起始时间到结束时间内的所有撤票的订单数量
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       int getAllCancelTicketedRecordNum(String startTime, String endTime);


       /// <summary>
       /// 查询起始时间到结束时间内的所有逾期的订单的统计
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       TicketRecordStatistics getAllOverdueTicketedRecordStatistics(String startTime, String endTime);

       /// <summary>
       /// 查询起始时间到结束时间内的所有逾期的订单
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       List<lottery_order> getAllOverdueTicketedRecord(String startTime, String endTime, String pageSize, String pageNo);
       /// <summary>
       /// 查询起始时间到结束时间内的所有逾期的订单数量
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       int getAllOverdueTicketedRecordNum(String startTime, String endTime);

       /// <summary>
       /// 按条件查询订单总数
       /// </summary>
       /// <param name="strLicense"></param>
       /// <param name="strState"></param>
       /// <param name="strOrderId"></param>
       /// <returns></returns>
       long getRecordStatisticsNumBy(string strLicense, string strState, string strOrderId);

       long getTicketsNumByOrderIdAndTicketId(string orderId, string ticketId);

       List<lottery_ticket> getTicketsByOrderIdAndTicketIdPagination(string orderId, string ticketId, long sno, long psize);

       /// <summary>
       /// 通过订单号，起始票号，结束票号查询
       /// </summary>
       /// <param name="orderId">订单号</param>
       /// <param name="startId">起始票号</param>
       /// <param name="endId">结束票号结束票号</param>
       /// <returns></returns>
       IList<lottery_ticket> getTicketListByIdAndStartIndexAndEndIdex(int orderId, int startId, int endId);
    }
}
