﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Common.Util;
using Maticsoft.Common.model;
using Maticsoft.Common.dbUtility;
using Maticsoft.BLL.serviceimpl;
using Maticsoft.BLL.serviceinterface;

namespace Maticsoft.BLL.serviceimpl
{
    public class ErrorTicketServiceImpl : BaseServiceImpl,IErrorTicketService
    {
        /// <summary>
        /// 读取所有的错漏票订单——用于初始化
        /// </summary>
        /// <param name="terminalNumber"></param>
        /// <returns></returns>
        public List<lottery_order> getAllErrorTicketOrder(String terminalNumber)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * from lottery_order WHERE bet_state ='{0}';",
                    new String[] { GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString() });

                List<lottery_order> list = new List<lottery_order>();
                DataSet ds = SQLiteHelper.getBLLInstance().Query(sb.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(ds);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        /// <summary>
        /// 读取所有的错漏票订单——分页
        /// </summary>
        /// <param name="terminalNumber"></param>
        /// <returns></returns>
       public List<lottery_order> getAllErrorTicketOrderPagination(Int64 sno, Int64 psize) {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * from lottery_order WHERE bet_state ='{0}' limit {1},{2};",
                    new String[] { GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString(), sno.ToString(), psize.ToString() });

                List<lottery_order> list = new List<lottery_order>();
                DataSet ds = SQLiteHelper.getBLLInstance().Query(sb.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    list = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(ds);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }            
       }

        /// <summary>
        /// 获取所有含有错漏票的订单的数量
        /// </summary>
        /// <returns></returns>
       public int getAllErrorTicketOrderNum() {
            try
            {
                String sql = String.Format("SELECT COUNT(*) FROM lottery_order WHERE bet_state ='{0}';",
               GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString());

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
        /// 读取所有未放置于界面上的错漏票订单——用于追加
        /// </summary>
        /// <param name="terminalNumber"></param>
        /// <returns></returns>
        public List<lottery_order> getNotInFormErrorTicketOrder(String terminalNumber)
        {
            //Dictionary<String, String> param = new Dictionary<string, string>() {
            //{"terminal_number",terminalNumber},{"is_in_error_form",GlobalConstants.isInErrorForm.NO.ToString()} };
            //return dal.QueryOrderListByParam(param);
            return null;
        }

       /// <summary>
       /// 根据订单Id获取订单下的所有错漏票
       /// </summary>
       /// <param name="orderId"></param>
       /// <returns></returns>
        public List<lottery_ticket> getErrorTicketsByOrderId(String orderId) {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * from lottery_ticket WHERE ticket_state ='{0}' and order_id='{1}';",
                    new String[] { GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString(), orderId });

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
        /// 暂存错漏票的选择
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean stagingOperatingResult(String orderId, Dictionary<String, String> param)
        {
            try
            {
                if (param.Count == 0)
                {
                    return false;
                }
                int ordererrorsignnum = 0;
                System.Collections.ArrayList sqllist = new System.Collections.ArrayList();
                foreach (String key in param.Keys)
                {
                    String sql = "UPDATE lottery_ticket set err_ticket_sign ='{0}' where ticket_id = '{1}' and order_id ='{2}'";
                    String err_ticket_sign = param[key];
                    //只要有一个选择了，就是操作中
                    if (!err_ticket_sign.Equals(GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION.ToString())) {
                        ordererrorsignnum++;
                    }
                    sql = String.Format(sql, new String[] { param[key], key, orderId });
                    sqllist.Add(sql);
                }
                //修改订单状态
                sqllist.Add(String.Format("UPDATE lottery_order SET err_ticket_sign_num='{0}' WHERE id='{1}';",
                    new String[] { ordererrorsignnum.ToString(), orderId }));

                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("修改错漏票的暂存状态数据库操作出错!");
            }
        }

        /// <summary>
        /// 确定错漏票的选择
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean saveOperatingResult(lottery_order order, Dictionary<String, String[]> param, ref bool finished)
        {
            //参数中的string[]:0-最终状态选择，1-金额
            //1、修改票；2、修改订单
            //bet_state、cancel_num、cancel_money、ticket_num、ticket_money等
            String bet_state = String.Empty;
            int cancel_num = 0, cancel_money = 0, ticket_num = 0, ticket_money = 0, wait_num = 0;
            System.Collections.ArrayList sqllist = new System.Collections.ArrayList();
            try
            {
                foreach (String k in param.Keys)
                {
                    String sql = "UPDATE lottery_ticket set err_ticket_sign ={0},ticket_state ='{1}' where ticket_id = '{2}' and order_id ='{3}'";
                    String ticketState = "";
                    
                    switch (int.Parse(param[k][0]))
                    {
                        case GlobalConstants.TICKET_ERR_SIGN.TICKET_AGAIN:
                            ticketState= GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString();//等待出票
                            wait_num++;//等待出票数量
                            break;
                        case GlobalConstants.TICKET_ERR_SIGN.COMPLETE_TICKET:
                            ticketState= GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString();//出票完成
                            ticket_num++;//出票数量
                            ticket_money += int.Parse(param[k][1]);
                            break;
                        default:
                            ticketState= GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString();//撤票
                            cancel_num++;//撤票数量
                            cancel_money += int.Parse(param[k][1]);
                            break;
                    }

                    sql = String.Format(sql, new String[] { GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION.ToString(), ticketState, k, order.id.ToString() });
                    sqllist.Add(sql);
                    SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);
                }
                int i = int.Parse(SQLiteHelper.getBLLInstance().GetSingle(String.Format("SELECT count(*) FROM lottery_ticket WHERE ticket_state = {0} AND order_id = {1}", GlobalConstants.ORDER_TICKET_STATE.ERROR,order.id.ToString())).ToString());
                if (i <= 0)
                {
                    finished = true;
                    //订单信息
                    StringBuilder sb = new StringBuilder();

                    sb.AppendFormat("UPDATE lottery_order SET err_ticket_sign_num ='{0}'", GlobalConstants.ORDER_ERR_SIGN.NO_OPERATION.ToString());
                    sb.AppendFormat(",bet_state ='{0}'", wait_num > 0 ? GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString() :
                        (order.ticket_num + ticket_num > 0 ? GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString() :
                        GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString()));
                    sb.AppendFormat(",canceled_num ='{0}'", (cancel_num + order.canceled_num).ToString());
                    sb.AppendFormat(",canceled_money ='{0}'", (cancel_money + order.canceled_money).ToString());
                    sb.AppendFormat(",ticket_num ='{0}'", (ticket_num + order.ticket_num).ToString());
                    sb.AppendFormat(",ticket_money ='{0}'", (ticket_money + order.ticket_money).ToString());
                    sb.AppendFormat(",is_in_print_form ='0'");
                    sb.Append(",errticket_num ='0'");

                    sb.AppendFormat(" where id ='{0}';", order.id.ToString());
                    SQLiteHelper.getBLLInstance().ExecuteSql(sb.ToString());
                }
                return true;
            }
            catch (Exception)
            {
                throw new Exception("保存错漏票最终处理结果出错!");
            }
        }


        /// <summary>
        /// 确定错漏票的选择
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean saveAllOperatingResult(lottery_order order, int state)
        {
            //参数中的string[]:0-最终状态选择，1-金额
            //1、修改票；2、修改订单
            //bet_state、cancel_num、cancel_money、ticket_num、ticket_money等
            int cancel_num = 0;
            int cancel_money = 0;
            int ticket_num = 0;
            int ticket_money = 0;
            int wait_num = 0;
            int ticketState = 6;

            System.Collections.ArrayList sqllist = new System.Collections.ArrayList();
            try
            {
                int ticketsNum = int.Parse(SQLiteHelper.getBLLInstance().GetSingle(String.Format("SELECT count(*) FROM lottery_ticket WHERE ticket_state = '6' AND order_id = {0}", order.id.ToString())).ToString());
                int ticketsPrice = int.Parse(SQLiteHelper.getBLLInstance().GetSingle(String.Format("SELECT sum(bet_price) FROM lottery_ticket WHERE ticket_state = '6' AND order_id = {0}", order.id.ToString())).ToString());

                switch (state)
                {
                    case GlobalConstants.TICKET_ERR_SIGN.TICKET_AGAIN:
                        ticketState = GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT;//等待出票
                        wait_num = ticketsNum;//等待出票数量
                        break;
                    case GlobalConstants.TICKET_ERR_SIGN.COMPLETE_TICKET:
                        ticketState= GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE;//出票完成
                        ticket_num = ticketsNum;//出票数量
                        ticket_money = ticketsPrice;
                        break;
                    default:
                        ticketState= GlobalConstants.ORDER_TICKET_STATE.CANCEL;//撤票
                        cancel_num = ticketsNum;//撤票数量
                        cancel_money = ticketsPrice;
                        break;
                }

                String sqlTicket = String.Format("UPDATE lottery_ticket set err_ticket_sign ={0},ticket_state ='{1}' where order_id ='{2}' AND ticket_state = '6'", GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION.ToString(), ticketState.ToString(), order.id.ToString());
                sqllist.Add(sqlTicket);

                String sqlOrder = String.Format("UPDATE lottery_order SET err_ticket_sign_num ='{0}',bet_state ='{1}',canceled_num ='{2}',canceled_money ='{3}',ticket_num ='{4}',ticket_money ='{5}',is_in_print_form ='0',errticket_num ='0' where id ='{6}';", 
                    GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION.ToString(),
                    ticketState.ToString(),
                    (cancel_num + order.canceled_num).ToString(),
                    (cancel_money + order.canceled_money).ToString(),
                    (ticket_num + order.ticket_num).ToString(),
                    (ticket_money + order.ticket_money).ToString(),
                    order.id.ToString());
                sqllist.Add(sqlOrder);

                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("保存错漏票最终处理结果出错!");
            }
        }
    }
}
