﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Maticsoft.BLL.serviceinterface;
using Maticsoft.Common;
using Maticsoft.Common.dbUtility;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;

namespace Maticsoft.BLL.serviceimpl
{
    public class RecordServiceImpl : BaseServiceImpl, IRecordService
    {
        /// <summary>
        /// 查询起始时间到结束时间内的所有已处理的订单的统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TicketRecordStatistics getAllTicketedRecordStatistics(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT sum(1) AS orderNum,sum(ticket_money) AS ticketMoney,sum(total_tickets_num) AS ticketNum FROM lottery_order WHERE bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND ticket_date BETWEEN {6} AND {7};",
                new String[] { GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
               startTime,endTime});

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<TicketRecordStatistics> ltList = (List<TicketRecordStatistics>)CollectionHelper.ConvertTo<TicketRecordStatistics>(dt);
                    return ltList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有已处理的订单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<lottery_order> getAllTicketedRecord(String startTime, String endTime, String sno, String pageNo)
        {
            try
            {
                String sql = String.Format(@"SELECT * FROM lottery_order WHERE bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND ticket_date BETWEEN {6} AND {7} ORDER BY ticket_date DESC limit {8},{9};",
                new String[] { GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
               startTime,endTime,sno,pageNo});

                List<lottery_order> ltList = new List<lottery_order>();
                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                }
                return ltList;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有已处理的订单数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int getAllTicketedRecordNum(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT COUNT(*) FROM lottery_order WHERE bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND ticket_date BETWEEN {6} AND {7};",
               new String[] { GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
               startTime,endTime});

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }             
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有已反馈的订单的统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TicketRecordStatistics getAllFeedBackTicketedRecordStatistics(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT sum(1) AS orderNum,sum(ticket_money) AS ticketMoney,sum(total_tickets_num) AS ticketNum FROM lottery_order WHERE
bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND is_feedback IN('{6}','{7}') AND ticket_date BETWEEN {8} AND {9};",
                new String[] {GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
                                    GlobalConstants.FeedbackState.SUCCESS.ToString(),
                                     GlobalConstants.FeedbackState.FAILED_PROCESSED.ToString(),
               startTime,endTime});

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<TicketRecordStatistics> ltList = (List<TicketRecordStatistics>)CollectionHelper.ConvertTo<TicketRecordStatistics>(dt);
                    return ltList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有已反馈的订单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<lottery_order> getAllFeedBackTicketedRecord(String startTime, String endTime, String sno, String pageNo)
        {
            try
            {
                String sql = String.Format(@"SELECT * FROM lottery_order WHERE 
bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND is_feedback IN('{6}','{7}') AND ticket_date BETWEEN {8} AND {9} ORDER BY ticket_date DESC limit {10},{11};",
                new String[] { GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
                                    GlobalConstants.FeedbackState.SUCCESS.ToString(),
                                     GlobalConstants.FeedbackState.FAILED_PROCESSED.ToString(),
               startTime,endTime,sno,pageNo});

                List<lottery_order> ltList = new List<lottery_order>();
                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                }
                return ltList;
            }
            catch (Exception e)
            {
                throw e;
            }             
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有已反馈的订单数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int getAllFeedBackTicketedRecordNum(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT COUNT(*) FROM lottery_order WHERE 
bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND is_feedback IN('{6}','{7}') AND ticket_date BETWEEN {8} AND {9};",
                new String[] {GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
                                    GlobalConstants.FeedbackState.SUCCESS.ToString(),
                                     GlobalConstants.FeedbackState.FAILED_PROCESSED.ToString(),
               startTime,endTime});

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有未反馈的订单的统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TicketRecordStatistics getAllNotFeedBackTicketedRecordStatistics(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT sum(1) AS orderNum,sum(ticket_money) AS ticketMoney,sum(total_tickets_num) AS ticketNum FROM lottery_order WHERE 
bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND is_feedback IN('{6}','{7}') AND ticket_date BETWEEN {8} AND {9};",
                 new String[] {GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
                                    GlobalConstants.FeedbackState.FAILED.ToString(),
                                     GlobalConstants.FeedbackState.NOT_FEEDBACK.ToString(),
               startTime,endTime});

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<TicketRecordStatistics> ltList = (List<TicketRecordStatistics>)CollectionHelper.ConvertTo<TicketRecordStatistics>(dt);
                    return ltList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有未反馈的订单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<lottery_order> getAllNotFeedBackTicketedRecord(String startTime, String endTime, String sno, String pageNo)
        {
            try
            {
                String sql = String.Format(@"SELECT * FROM lottery_order WHERE 
bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND is_feedback IN('{6}','{7}') AND ticket_date BETWEEN {8} AND {9} ORDER BY ticket_date DESC limit {10},{11};",
                 new String[] {GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
                                      GlobalConstants.FeedbackState.FAILED.ToString(),
                                     GlobalConstants.FeedbackState.NOT_FEEDBACK.ToString(),
               startTime,endTime,sno,pageNo});

                List<lottery_order> ltList = new List<lottery_order>();
                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                }
                return ltList;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有未反馈的订单数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int getAllNotFeedBackTicketedRecordNum(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT COUNT(*) FROM lottery_order WHERE 
bet_state IN('{0}','{1}','{2}','{3}','{4}','{5}') AND is_feedback IN('{6}','{7}') AND ticket_date BETWEEN {8} AND {9};",
                new String[] {GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString(),
                                    GlobalConstants.FeedbackState.FAILED.ToString(),
                                     GlobalConstants.FeedbackState.NOT_FEEDBACK.ToString(),
               startTime,endTime});

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有含有撤票的订单的统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TicketRecordStatistics getAllCancelTicketedRecordStatistics(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT sum(1) AS orderNum,sum(total_money) AS ticketMoney,sum(total_tickets_num) AS ticketNum FROM lottery_order WHERE canceled_num > 0 AND ticket_date BETWEEN {0} AND {1};",
                 new String[] { startTime, endTime });

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<TicketRecordStatistics> ltList = (List<TicketRecordStatistics>)CollectionHelper.ConvertTo<TicketRecordStatistics>(dt);
                    return ltList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有撤票的订单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<lottery_order> getAllCancelTicketedRecord(String startTime, String endTime, String sno, String pageNo)
        {
            try
            {
                String sql = String.Format(@"SELECT * FROM lottery_order WHERE canceled_num > 0 AND ticket_date BETWEEN {0} AND {1} ORDER BY ticket_date DESC limit {2},{3};",
                new String[] { startTime, endTime, sno, pageNo });

                List<lottery_order> ltList = new List<lottery_order>();
                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                }
                return ltList;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
        /// <summary>
        /// 查询起始时间到结束时间内的所有撤票的订单数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int getAllCancelTicketedRecordNum(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT COUNT(*) FROM lottery_order WHERE canceled_num > 0 AND ticket_date BETWEEN {0} AND {1};",
                 new String[] { startTime, endTime });

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }             
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有逾期的订单的统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TicketRecordStatistics getAllOverdueTicketedRecordStatistics(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT sum(1) AS orderNum,sum(total_money) AS ticketMoney,sum(total_tickets_num) AS ticketNum FROM lottery_order WHERE expired_num > 0 AND ticket_date BETWEEN {0} AND {1};",
                 new String[] { startTime, endTime });

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<TicketRecordStatistics> ltList = (List<TicketRecordStatistics>)CollectionHelper.ConvertTo<TicketRecordStatistics>(dt);
                    return ltList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }             
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有逾期的订单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<lottery_order> getAllOverdueTicketedRecord(String startTime, String endTime, String sno, String pageNo)
        {
            try
            {
                String sql = String.Format(@"SELECT * FROM lottery_order WHERE expired_num > 0 AND ticket_date BETWEEN {0} AND {1} ORDER BY ticket_date DESC limit {2},{3};",
                new String[] { startTime, endTime, sno, pageNo });

                List<lottery_order> ltList = new List<lottery_order>();
                DataSet dt = SQLiteHelper.getBLLInstance().Query(sql, null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                }
                return ltList;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询起始时间到结束时间内的所有逾期的订单数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int getAllOverdueTicketedRecordNum(String startTime, String endTime)
        {
            try
            {
                String sql = String.Format(@"SELECT COUNT(*) FROM lottery_order WHERE expired_num > 0 AND ticket_date BETWEEN {0} AND {1};",
                 new String[] { startTime, endTime });

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 查询所有订单数量
        /// </summary>
        /// <returns></returns>
        public int getAllRecordNum()
        {
            try
            {
                String sql = @"SELECT COUNT(*) FROM lottery_order;";

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }             
        }

        /// <summary>
        /// 按条件查询订单
        /// </summary>
        /// <param name="strLicense"></param>
        /// <param name="strState"></param>
        /// <param name="strOrderId"></param>
        /// <param name="startPageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<lottery_order> getRecordStatisticsBy(string strLicense, string strState, string strOrderId, string startPageNo, string pageSize)
        {
            try
            {
                StringBuilder sqls = new StringBuilder("SELECT * FROM lottery_order");
                Boolean ishas = false;
                if (!string.IsNullOrEmpty(strLicense) && !strLicense.Equals("0"))
                {
                    sqls.AppendFormat((ishas ? " AND " : " WHERE ") + "license_id = '{0}'", strLicense);
                    ishas = true;
                }

                if (!string.IsNullOrEmpty(strState) && !strState.Equals("0"))
                {
                    sqls.AppendFormat((ishas ? " AND " : " WHERE ") + "bet_state = '{0}'", strState);
                    ishas = true;
                }

                if (!string.IsNullOrEmpty(strOrderId))
                {
                    sqls.AppendFormat((ishas ? " AND " : " WHERE ") + "id LIKE '%{0}%'", strOrderId);
                    ishas = true;
                }

                sqls.AppendFormat(" ORDER BY ticket_date DESC LIMIT " + startPageNo + "," + pageSize + ";");

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sqls.ToString(), null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<lottery_order> ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                    return ltList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public long getRecordStatisticsNumBy(string strLicense, string strState, string strOrderId)
        {
            try
            {
                StringBuilder sqls = new StringBuilder("SELECT COUNT(*) FROM lottery_order");
                Boolean ishas = false;
                if (!string.IsNullOrEmpty(strLicense) && !strLicense.Equals("0"))
                {
                    sqls.AppendFormat((ishas ? " AND " : " WHERE ") + "license_id = '{0}'", strLicense);
                    ishas = true;
                }

                if (!string.IsNullOrEmpty(strState) && !strState.Equals("0"))
                {
                    sqls.AppendFormat((ishas ? " AND " : " WHERE ") + "bet_state = '{0}'", strState);
                    ishas = true;
                }

                if (!string.IsNullOrEmpty(strOrderId))
                {
                    sqls.AppendFormat((ishas ? " AND " : " WHERE ") + "id LIKE '%{0}%'", strOrderId);
                    ishas = true;
                }

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sqls.ToString()).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<lottery_order> getAllRecord(string pageSize, string pageNo)
        {
            throw new NotImplementedException();
        }

        public long getTicketsNumByOrderIdAndTicketId(string orderId, string ticketId)
        {
            try
            {
                String sql = String.Empty;
                //如果是自动反馈，只需要查询反馈失败的即可；
                sql = String.Format("SELECT COUNT(*) FROM lottery_ticket WHERE order_id ='{0}' AND ticket_id = '{1}';",
                   orderId, ticketId);

                int count = 0;
                int.TryParse(SQLiteHelper.getBLLInstance().GetSingle(sql).ToString(), out count);

                return count;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<lottery_ticket> getTicketsByOrderIdAndTicketIdPagination(string orderId, string ticketId, long sno, long psize)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * from lottery_ticket WHERE order_id = '{0}' AND ticket_id = '{1}' ORDER BY ticket_date DESC limit {2},{3};", orderId, ticketId, sno.ToString(), psize.ToString());

                List<lottery_ticket> list = new List<lottery_ticket>();
                DataSet ds = SQLiteHelper.getBLLInstance().Query(sb.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = (List<lottery_ticket>)CollectionHelper.ConvertTo<lottery_ticket>(ds);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 通过订单号，起始票号，结束票号查询
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="startId">起始票号</param>
        /// <param name="endId">结束票号结束票号</param>
        /// <returns></returns>
        public IList<lottery_ticket> getTicketListByIdAndStartIndexAndEndIdex(int orderId, int startId, int endId)
        {
            try
            {
                List<lottery_ticket> list = new List<lottery_ticket>();
                String sql = String.Format("SELECT * FROM lottery_ticket WHERE order_id = '{0}' AND ticket_id BETWEEN {1} AND {2} ORDER BY ticket_date DESC;", orderId, startId, endId);

                DataSet ds = SQLiteHelper.getBLLInstance().Query(sql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = (List<lottery_ticket>)CollectionHelper.ConvertTo<lottery_ticket>(ds);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
