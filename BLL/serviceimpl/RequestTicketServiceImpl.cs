﻿using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Maticsoft.BLL.serviceinterface;
using Maticsoft.Common;
using Maticsoft.Common.dbUtility;
using Maticsoft.Common.dencrypt;
using Maticsoft.Common.model;
using Maticsoft.Common.model.httpmodel;
using Maticsoft.Common.Util;
using System;

namespace Maticsoft.BLL.serviceimpl
{
    public class RequestTicketServiceImpl : BaseServiceImpl, IRequestTicketService
    {
        public bool requestTickets(ref bool isEndOfOrder)
        {
            try
            {
                HttpRequestMsg<Body1000Request> hrmsg = new HttpRequestMsg<Body1000Request>("UTF-8", GlobalConstants.TRANSCODE.REQUEST_TICKETS, Global.STORE_ID.ToString(), "1.0");
                hrmsg.body.orderId = Global.ORDER_ID;
                hrmsg.body.repeat = Global.TICKET_REQUEST_REPEAT;
                string request = JSonHelper.ObjectToJson(hrmsg);
                string response = null;

                try
                {
                    response = HTTPHelper.HttpHandler ( request, GlobalConstants.SERVER_URL_MAP [
                        Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.SERVER_TYPE ]] );
                }
                catch
                {
                    throw;
                }

                if (null == response || response.Equals("")) {
                    return false;//抛出流不可读异常后，返回字符串为null。在此处理
                }
                HttpResponseMsg<Body1000Response> responsemsg = (HttpResponseMsg<Body1000Response>)JSonHelper.JsonToHttpResponseMsg<Body1000Response>(response);

                Body1000Response body = (Body1000Response)responsemsg.body;

                if (null != body)
                {
                    //记录当前服务器时间
                    Global.SysDateMillisecond = body.timeInMillis;
                    IList<lottery_ticket> ltList = new List<lottery_ticket>();
                    foreach (lotteryTicket item in body.tickets)
                    {
                        lottery_ticket lt = new lottery_ticket(item);
                        ltList.Add(lt);
                    }
                    if (AddTickets(ltList) > 0)
                    {
                        if (body.endFlag)
                        {
                            // # 在本次取票请求结束后,设置下次的orderId以及是否重发
                            // 1. 查看上一次的respose的endFlag
                            // 2. endFlag为true,检查是否所有票都接收完全
                            if (body.totalTicketsNum == this.getTicketsNumber(body.tickets[0].orderId.ToString()))
                            {
                                // 2-1. 接收完全,设置'重发字段'为false,生成订单,发送orderId为空的取票请求
                                if ((this.insertOrder(new lottery_order(body)) > 0) && (this.alterOrderStateByOrderId(body) > 0))
                                {
                                    Global.ORDER_ID = -1;
                                    Global.TICKET_REQUEST_REPEAT = false;
                                    return true;
                                }
                            }
                            else
                            {
                                // 2-2. 接受不完全,设置'重发字段'为true,orderId
                                Global.ORDER_ID = body.tickets[0].orderId;
                                Global.TICKET_REQUEST_REPEAT = true;

                                // 2-3. 删除本地票信息
                                this.ClearTickets(body.tickets[0].orderId.ToString());

                                return true;
                            }
                        }
                        else
                        {
                            // 3. endFlag为false,设置orderId,发送取票请求
                            isEndOfOrder = false;
                            Global.ORDER_ID = body.tickets[0].orderId;
                            Global.TICKET_REQUEST_REPEAT = false;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        private int AddTickets(IList<lottery_ticket> ticketsList)
        {
            try
            {
                if (ticketsList.Count <= 0)
                    return -1;
                foreach (lottery_ticket lt in ticketsList)
                {
                    //设置ticketState为“等待分配端口”（Global.ORDER_TICKET_STATE.AWAITING_ALLOT）
                    lt.ticket_state = GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString();
                    lt.sent_num = "0";
                    lt.issue = lt.issue;
                    lt.bet_code = DESEncrypt.Encrypt(lt.bet_code, GlobalConstants.KEY);

                    //给返回字段赋默认值
                    lt.return_pass_type = "";
                    lt.return_license_id = 0;
                    lt.return_license_name = "";
                    lt.return_issue = "";
                    lt.return_issue_num = 0;
                    lt.return_play_name = "";
                    lt.return_multiple = 0;
                    lt.return_money = 0;
                    lt.return_bet_info = "";
                }

                return this.AddTran(ticketsList);
            }
            catch
            {
                throw;
            }        
        }

        private int getTicketsNumber(string orderId)
        {
            try
            {
                int ticketsNumber = -1;
                string sql = string.Format("select count(*) from lottery_ticket WHERE order_id = {0}", orderId);
                DataTable dt = SQLiteHelper.getBLLInstance().ExecuteTable(sql);
                if (dt.Rows.Count > 0)
                {
                    ticketsNumber = int.Parse((dt.Rows[0]).ItemArray[0].ToString());
                }
                return ticketsNumber;
            }
            catch
            {
                throw;
            }
        }

        private int alterOrderStateByOrderId(Body1000Response body1000)
        {
            try
            {
                string sql = "update lottery_order set bet_state = " + GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT + " where id = " + body1000.orderId;
                return SQLiteHelper.getBLLInstance().ExecuteNonQuery(sql);
            }
            catch
            {
                throw;
            }
        }

        private int AddTran(IList<lottery_ticket> tList)
        {
            try
            {
                string sql = "insert into lottery_ticket(" + lotteryTicketColumns + ") values(" + lotteryTicketParameters + ")";
                List<SQLiteParameter[]> sqlParasList = new List<SQLiteParameter[]>();
                foreach (lottery_ticket lt in tList)
                {
                    sqlParasList.Add(GetSQLiteParameterArr(lt));
                }
                return SQLiteHelper.getBLLInstance().ExecuteSqlParasTran(sql, sqlParasList);
            }
            catch
            {
                throw;
            }
        }

        private int ClearTickets(string orderId)
        {
            try
            {
                string sql = string.Format("delete from lottery_ticket where order_id = {0}", orderId);
                return SQLiteHelper.getBLLInstance().ExecuteNonQuery(sql);
            }
            catch
            {
                throw;
            }            
        }

        private int insertOrder(lottery_order lo)
        {
            try
            {
                SQLiteParameter[] paras = new SQLiteParameter[]{
                new SQLiteParameter("@bet_code",lo.bet_code),
                new SQLiteParameter("@bet_from",lo.bet_from),
                new SQLiteParameter("@bet_num",lo.bet_num),
                new SQLiteParameter("@bet_price",lo.bet_price),
                new SQLiteParameter("@bet_state",lo.bet_state),
                new SQLiteParameter("@bet_type",lo.bet_type),
                new SQLiteParameter("@canceled_num",lo.canceled_num),
                new SQLiteParameter("@canceled_money",lo.canceled_money),
                new SQLiteParameter("@com_port",lo.com_port),
                new SQLiteParameter("@del_date",lo.del_date),
                new SQLiteParameter("@errticket_num",lo.errticket_num),
                new SQLiteParameter("@ticket_money",lo.ticket_money),
                new SQLiteParameter("@id",lo.id),
                new SQLiteParameter("@issue",lo.issue),
                new SQLiteParameter("@license_id",lo.license_id),
                new SQLiteParameter("@mult_info",lo.mult_info),
                new SQLiteParameter("@multiple",lo.multiple),
                new SQLiteParameter("@order_date",lo.order_date),
                new SQLiteParameter("@err_ticket_sign_num",lo.err_ticket_sign_num),
                new SQLiteParameter("@pass_type",lo.pass_type),
                new SQLiteParameter("@play_type",lo.play_type),
                new SQLiteParameter("@sch_info",lo.sch_info),
                new SQLiteParameter("@single_flag",lo.single_flag),
                new SQLiteParameter("@storeid",lo.storeid),
                new SQLiteParameter("@ticket_num",lo.ticket_num),
                new SQLiteParameter("@ticket_date",lo.ticket_date),
                new SQLiteParameter("@ticket_oper",lo.ticket_oper),
                new SQLiteParameter("@total_money",lo.total_money),
                new SQLiteParameter("@total_tickets_num",lo.total_tickets_num),
                new SQLiteParameter("@userid",lo.userid),
                new SQLiteParameter("@username",lo.username),
                new SQLiteParameter("@is_in_feedback_form",lo.is_in_feedback_form),
                new SQLiteParameter("@is_in_print_form",lo.is_in_print_form),
                new SQLiteParameter("@is_in_error_form",lo.is_in_error_form),
                new SQLiteParameter("@stop_time",lo.stop_time),
                new SQLiteParameter("@expired_num",lo.expired_num),
                new SQLiteParameter("@expired_money",lo.expired_money)
            };

                string sql = "insert into lottery_order(" + lotteryOrderColumns + ") values(" + lotteryOrderParameters + ")";
                return SQLiteHelper.getBLLInstance().ExecuteNonQuery(sql, paras);
            }
            catch
            {
                throw;
            }
        }

        public lottery_order GetLotteryOrderById(string id)
        {
            try
            {
                string sql = "select * from lottery_order where id=" + id;
                DataTable dt = SQLiteHelper.getBLLInstance().ExecuteTable(sql);
                lottery_order lo = new lottery_order();
                if (dt.Rows.Count > 0)
                {
                    lo = RowToLotteryOrder(dt.Rows[0]);
                }
                return lo;
            }
            catch
            {
                throw;
            }           
        }

        //Table to object
        private lottery_order RowToLotteryOrder(DataRow dr)
        {
            lottery_order lo = new lottery_order();
            lo = Maticsoft.Common.Util.DataUtil.ToEntity<lottery_order>(dr, lo);
            return lo;
        }
    }
}
