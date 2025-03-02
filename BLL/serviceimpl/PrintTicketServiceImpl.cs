﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Common.Util;
using Maticsoft.Common.model;
using Maticsoft.Common.dbUtility;
using Maticsoft.BLL.serviceinterface;

namespace Maticsoft.BLL.serviceimpl
{
    /// <summary>
    /// 出票模块相关业务
    /// </summary>
    public class PrintTicketServiceImpl : BaseServiceImpl, IPrintTicketService
    {
        /// <summary>
        /// 根据机器id取其对应的彩种
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public List<machine_can_print_license> getMachineLicenseListByMID(String mid)
        {

            DataSet ds = SQLiteHelper.getBLLInstance().Query(SQLBuilderUtil.dictionaryToSelectSQLString((new machine_can_print_license()).GetType(), new Dictionary<string, string>() { { "terminal_number", mid } }));
            return (List<machine_can_print_license>)CollectionHelper.ConvertTo<machine_can_print_license>(ds);
        }

        /// <summary>
        /// 根据licenseId、彩机类别和速度级别查询对应的流程配置数据(lids可以是0-n个，为0时表示查询所有)
        /// </summary>
        /// <param name="llist"></param>
        /// <param name="machineId"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public List<speed_level_cmd> getProConfigByLicenseIds(List<machine_can_print_license> llist, String speed)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append("SELECT * FROM speed_level_cmd WHERE ");
            if (null != llist && llist.Count > 0)
            {
                if (llist.Count == 1)
                {
                    sbsql.Append("license_id ='" + llist[0].license_id + "'");
                }
                else
                {
                    sbsql.Append("license_id in(");
                    for (int i = 0; i < llist.Count; i++)
                    {
                        sbsql.Append("'" + llist[i].license_id + "'" + (i == llist.Count - 1 ? ")" : ","));
                    }
                }

                sbsql.AppendFormat(" and speed_level='{0}'", speed);
            }
            else
            {
                sbsql.AppendFormat(" speed_level='{0}'", speed);
            }

            DataSet dt = SQLiteHelper.getBLLInstance().Query(sbsql.ToString(), null);
            if (dt.Tables[0].Rows.Count > 0)
            {
                List<speed_level_cmd> ltList = (List<speed_level_cmd>)CollectionHelper.ConvertTo<speed_level_cmd>(dt);
                return ltList;
            }
            else
            {
                return new List<speed_level_cmd>();
            }
        }

        /// <summary>
        /// 查询所有可放置于出票界面上的订单——用于第一次加载
        /// </summary>
        /// <returns></returns>
        public Queue<lottery_order> getAllCanInPrintFormOrders(out int errorNum)
        {
            /* 1、先加载错漏票被停止的订单或是正常被停止的订单
             * 2、加载错漏票的订单
             * 3、加载等待出票的订单
             */
            try
            {
                Queue<lottery_order> queue = new Queue<lottery_order>();
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString(), ref queue);
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString(), ref queue);
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString(), ref queue);

                errorNum = queue.Count;//记录错漏票的个数

                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.PRINTTING.ToString(), ref queue);
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.PAUSE.ToString(), ref queue);
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString(), ref queue);
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString(), ref queue);
                getOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString(), ref queue);

                //查找单式上传订单
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString(), ref queue);
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString(), ref queue);
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString(), ref queue);

                errorNum = queue.Count;//记录错漏票的个数

                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.PRINTTING.ToString(), ref queue);
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.PAUSE.ToString(), ref queue);
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString(), ref queue);
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString(), ref queue);
                getSingleOrdersToQueueByState(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString(), ref queue);

                return queue;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void getOrdersToQueueByState(String state, ref Queue<lottery_order> queue)
        {
            List<lottery_order> ol1 = this.getLotteryOrderListByState(state);
            if (ol1.Count > 0)
            {
                foreach (lottery_order lo in ol1)
                {
                    queue.Enqueue(lo);
                }
            }
        }

        private void getSingleOrdersToQueueByState(String state, ref Queue<lottery_order> queue)
        {
            List<lottery_order> ol1 = this.getSingleOrdersToQueueByState(state);
            if (ol1.Count > 0)
            {
                foreach (lottery_order lo in ol1)
                {
                    queue.Enqueue(lo);
                }
            }
        }

        /// <summary>
        /// 修改订单及其下对应的彩票的状态（带着之前的状态作为条件——主要用于出票首页）
        /// </summary>
        /// <param name="oId"></param>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        public Boolean updateOrderAndTicketState(String oId, String oldState, String newState)
        {
            try
            {

                System.Collections.ArrayList sqlList = new System.Collections.ArrayList();
                sqlList.Add(String.Format("UPDATE lottery_order SET bet_state='{0}' WHERE id='{1}';", new String[] { newState, oId }));
                sqlList.Add(String.Format("UPDATE lottery_ticket SET ticket_state='{0}' WHERE order_id='{1}' AND ticket_state='{2}';", new String[] { newState, oId, oldState }));

                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqlList);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改对应订单是否在反馈界面状态
        /// </summary>
        /// <param name="lol"></param>
        /// <returns></returns>
        public Boolean updateIsInPrintForm(List<lottery_order> lol, bool isInForm)
        {
            if (null == lol || lol.Count == 0)
            {
                return false;
            }
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.AppendFormat("update lottery_order set is_in_print_form = {0} WHERE id in (", isInForm ? "1" : "0");
            for (int i = 0; i < lol.Count; i++)
            {
                sqlsb.Append("'" + lol[i].id.ToString() + "'" + (i < lol.Count - 1 ? "," : ")"));
            }

            try
            {
                return (SQLiteHelper.getBLLInstance().ExecuteNonQuery(sqlsb.ToString()) > 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 处理出票结果
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public bool ticketResultHandler(lottery_ticket lt)
        {
            try
            {
                System.Collections.ArrayList sqllist = new System.Collections.ArrayList();
                bool issucc = (lt.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString())
                      || lt.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString())
                      || lt.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString()));

                //修改票的信息；修改订单信息(如果是最后一张票，则需要改订单的状态)
                string sql = "update lottery_ticket set ticket_state = '{0}',exception_description = '{1}',ticket_date='" + DateUtil.getServerDateTime(DateUtil.DATE_FMT_STR1) + "',ticket_info ='{2}' ,sent_num = sent_num + 1,"
                + @"return_pass_type='{3}',return_license_id='{4}',return_license_name='{5}',return_issue='{6}',return_issue_num='{7}',return_play_id='{8}',return_play_name='{9}',return_multiple='{10}',
                    return_money='{11}',return_bet_info='{12}',ticket_odds='{13}',ticket_rqs='{14}'
                   where order_id = '{15}' and ticket_id='{16}';";
                sqllist.Add(String.Format(sql, new String[] { lt.ticket_state, lt.exception_description, lt.ticket_info,lt.return_pass_type.ToString(),lt.return_license_id.ToString()
                  ,lt.return_license_name,lt.return_issue.ToString(),lt.return_issue_num.ToString(),lt.return_play_id.ToString(),lt.return_play_name,
                  lt.return_multiple.ToString(),lt.return_money.ToString(),lt.return_bet_info,lt.ticket_odds,lt.ticket_rqs, lt.order_id.ToString(), lt.ticket_id.ToString() }));

                lottery_order lo = this.getLotteryOrderById(lt.order_id.ToString());
                StringBuilder sb = new StringBuilder();
                sb.Append("update lottery_order set ");
                if (issucc)
                {
                    //如果是成功出票
                    sb.Append("ticket_num = ticket_num + 1,ticket_money = ticket_money +" + lt.bet_price);
                }
                else
                {
                    sb.Append("errticket_num = errticket_num + 1");
                }

                String ostate = "";
                if (lo.total_tickets_num == lo.ticket_num + lo.canceled_num + lo.errticket_num + 1)
                { //最后一张票了
                    if (lo.errticket_num > 0 || !issucc)//出票完成，里面有错漏票
                    {
                        ostate = GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString();
                    }
                    else
                    {
                        if (lo.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.PRINTTING.ToString()))
                        {
                            ostate = GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString();
                        }
                        else if (lo.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()))
                        {
                            ostate = GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString();
                        }
                        else
                        {
                            ostate = GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString();
                        }
                    }

                    sb.AppendFormat(",bet_state = '{0}',ticket_date = '{1}',is_in_feedback_form='{2}',is_in_print_form='{3}',is_in_error_form='{4}'", new String[] { ostate, DateUtil.getServerDateTime(DateUtil.DATE_FMT_STR1), GlobalConstants.TrueFalseSign.FALSE.ToString(), GlobalConstants.TrueFalseSign.FALSE.ToString(), GlobalConstants.TrueFalseSign.FALSE.ToString() });
                }

                sb.Append(" WHERE id = '" + lt.order_id.ToString() + "'");
                sqllist.Add(sb.ToString());

                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据订单状态查询一个订单,用来出票
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        public lottery_order getTopOneLotteryOrderByState(String state)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM lottery_order WHERE bet_state='{0}';", state);

            DataSet dt = SQLiteHelper.getBLLInstance().Query(sb.ToString(), null);
            if (dt.Tables[0].Rows.Count > 0)
            {
                List<lottery_order> ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
                return ltList[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据state查询指定订单下一张票
        /// </summary>
        /// <param name="oId"></param>
        /// <param name="stateList"></param>
        /// <returns></returns>
        public lottery_ticket getTopOneTicket(String oId, string[] stateList)
        {
            try
            {
                StringBuilder sbsql = new StringBuilder();
                String states = "";
                int count = 0;
                foreach (String item in stateList)
                {
                    count++;
                    states += ("'" + item + "'" + (count < stateList.Length ? "," : ""));
                }
                sbsql.AppendFormat(@"SELECT * FROM lottery_ticket where order_id = '{0}' 
            and ticket_state in ({1}) ORDER BY ticket_id ASC limit 0,1;", new String[] { oId, states });
                DataSet ds = SQLiteHelper.getBLLInstance().Query(sbsql.ToString());
                lottery_ticket lt = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    lt = (lottery_ticket)(null == dt || 0 == dt.Rows.Count ? null : Maticsoft.Common.Util.DataUtil.ToEntity(dt.Rows[0], typeof(lottery_ticket)));
                }
                return lt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 把订单置为手工处理
        /// </summary>
        /// <returns></returns>
        public Boolean orderToManualProcess(String oId)
        {
            //1、把订单下的出票中和等待出票的票置为等待手工出票
            //2、把订单置为等待手工出票
            try
            {
                System.Collections.ArrayList sqllist = new System.Collections.ArrayList();
                sqllist.Add(String.Format("UPDATE lottery_ticket SET ticket_state = '{0}' WHERE ticket_state in({1},{2},{3},{4},{5},{6},{7})  AND order_id = '{8}';",
                new String[]{GlobalConstants.ORDER_TICKET_STATE.MANUAL_WAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.PAUSE.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString(),
          oId}));

                sqllist.Add(String.Format("UPDATE lottery_order SET bet_state ='{0}',is_in_feedback_form='{1}',is_in_print_form='{2}',is_in_error_form='{3}' WHERE id='{4}';", new String[]{
         GlobalConstants.ORDER_TICKET_STATE.MANUAL_WAITING_PRINT.ToString(),
         GlobalConstants.TrueFalseSign.FALSE.ToString(),GlobalConstants.TrueFalseSign.FALSE.ToString(),GlobalConstants.TrueFalseSign.FALSE.ToString(), oId }));

                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 今日出票金额
        /// </summary>
        /// <returns>今日出票额</returns>
        public int selectTodayTicketMoney()
        {
            string sql = @"SELECT sum(bet_price) FROM lottery_ticket WHERE ticket_state IN('{0}','{1}','{2}','{3}') AND ticket_date BETWEEN datetime('now', 'start of day') and datetime('now', '+1 day','start of day');";
            int totalTicketMoney = 0;
            object o = SQLiteHelper.getBLLInstance().GetSingle(String.Format(sql, new String[]{
              GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString(),
                                     GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString()}));

            if (null != o && int.TryParse(o.ToString(), out totalTicketMoney))
            {//数据库为空
                return totalTicketMoney;
            }
            return 0;
        }

        public IList<lottery_order> getLotteryOrderListForSort(int sortBy)
        {
            string sql = String.Format("SELECT * FROM lottery_order WHERE bet_state = {0} AND is_in_print_form = {1} ORDER BY {2} ASC", GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT, GlobalConstants.TrueFalseSign.TRUE, (sortBy == 0) ? "total_tickets_num" : "ticket_date");
            DataSet dt = SQLiteHelper.getBLLInstance().Query(sql.ToString(), null);
            List<lottery_order> ltList = new List<lottery_order>();
            if (dt.Tables[0].Rows.Count > 0)
            {
                ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
            }
            return ltList;
        }

        /// <summary>
        /// 根据状态查询订单列表，显示在出票界面上的订单
        /// </summary>
        /// <returns></returns>
        public List<lottery_order> getLotteryOrderListByStateInForm(String state)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM lottery_order WHERE bet_state ='{0}' AND is_in_print_form = '{1}';", new String[] { state, GlobalConstants.TrueFalseSign.TRUE.ToString() });
            DataSet dt = SQLiteHelper.getBLLInstance().Query(sb.ToString(), null);
            List<lottery_order> ltList = new List<lottery_order>();
            if (dt.Tables[0].Rows.Count > 0)
            {
                ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
            }
            return ltList;
        }

        /// <summary>
        /// 根据状态查询订单列表，未显示在出票界面上的订单
        /// </summary>
        /// <returns></returns>
        public List<lottery_order> getLotteryOrderListByStateNotInForm(String state)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM lottery_order WHERE (bet_state ='{0}' OR bet_state = '" + GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString() + "') AND is_in_print_form = '{1}';", new String[] { state, GlobalConstants.TrueFalseSign.FALSE.ToString() });
            DataSet dt = SQLiteHelper.getBLLInstance().Query(sb.ToString(), null);
            List<lottery_order> ltList = new List<lottery_order>();
            if (dt.Tables[0].Rows.Count > 0)
            {
                ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
            }
            return ltList;
        }

        /// <summary>
        /// 根据状态查询订单列表，未显示在出票界面上的订单
        /// </summary>
        /// <returns></returns>
        public List<lottery_order> getLotteryOrderListByStateNotInForm2(String state)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM lottery_order WHERE bet_state ='{0}' AND is_in_print_form = '{1}';", new String[] { state, GlobalConstants.TrueFalseSign.FALSE.ToString() });
            DataSet dt = SQLiteHelper.getBLLInstance().Query(sb.ToString(), null);
            List<lottery_order> ltList = new List<lottery_order>();
            if (dt.Tables[0].Rows.Count > 0)
            {
                ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
            }
            return ltList;
        }

        /// <summary>
        /// 取订单下可打印的票
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        public lottery_ticket getTopOnePrintTicket(String oId)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.AppendFormat("SELECT * FROM lottery_ticket where order_id = '{0}' and ticket_state in ( '{1}','{2}','{3}') ORDER BY ticket_id ASC limit 0,1;", new String[]{
          oId,
          GlobalConstants.ORDER_TICKET_STATE.PRINTTING.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()});
            DataTable dt = SQLiteHelper.getBLLInstance().ExecuteTable(sbsql.ToString(), null);
            return (lottery_ticket)(null == dt || 0 == dt.Rows.Count ? null : Maticsoft.Common.Util.DataUtil.ToEntity(dt.Rows[0], typeof(lottery_ticket)));
        }

        /// <summary>
        /// 根据状态查询订单列表，未显示在出票界面上的订单
        /// </summary>
        /// <returns></returns>
        public List<lottery_order> getLotteryOrderSingleListByStateNotInForm(string state)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM lottery_order_single WHERE (bet_state ='{0}' OR bet_state = '" + GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString() + "') AND is_in_print_form = '{1}';", new String[] { state, GlobalConstants.TrueFalseSign.FALSE.ToString() });
            DataSet dt = SQLiteHelper.getBLLInstance().Query(sb.ToString(), null);
            List<lottery_order> ltList = new List<lottery_order>();
            if (dt.Tables[0].Rows.Count > 0)
            {
                ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
            }
            return ltList;
        }

        /// <summary>
        /// 把单式上传订单置为手工处理
        /// </summary>
        /// <returns></returns>
        public bool orderToManualProcessSingle(string oId)
        {
            //1、把订单下的出票中和等待出票的票置为等待手工出票
            //2、把订单置为等待手工出票
            try
            {
                System.Collections.ArrayList sqllist = new System.Collections.ArrayList();
                sqllist.Add(String.Format("UPDATE lottery_ticket_single SET ticket_state = '{0}' WHERE ticket_state in({1},{2},{3},{4},{5},{6},{7})  AND order_id = '{8}';",
                new String[]{GlobalConstants.ORDER_TICKET_STATE.MANUAL_WAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.PAUSE.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString(),
          GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString(),
          oId}));

                sqllist.Add(String.Format("UPDATE lottery_order_single SET bet_state ='{0}',is_in_feedback_form='{1}',is_in_print_form='{2}',is_in_error_form='{3}' WHERE id='{4}';", new String[]{
         GlobalConstants.ORDER_TICKET_STATE.MANUAL_WAITING_PRINT.ToString(),
         GlobalConstants.TrueFalseSign.FALSE.ToString(),GlobalConstants.TrueFalseSign.FALSE.ToString(),GlobalConstants.TrueFalseSign.FALSE.ToString(), oId }));

                SQLiteHelper.getBLLInstance().ExecuteSqlTran(sqllist);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改对应单式上传订单是否在反馈界面状态
        /// </summary>
        /// <param name="lol"></param>
        /// <returns></returns>
        public bool updateIsInPrintFormSingle(List<lottery_order> lol, bool isInForm)
        {
            if (null == lol || lol.Count == 0)
            {
                return false;
            }
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.AppendFormat("update lottery_order_single set is_in_print_form = {0} WHERE id in (", isInForm ? "1" : "0");
            for (int i = 0; i < lol.Count; i++)
            {
                sqlsb.Append("'" + lol[i].id.ToString() + "'" + (i < lol.Count - 1 ? "," : ")"));
            }

            try
            {
                return (SQLiteHelper.getBLLInstance().ExecuteNonQuery(sqlsb.ToString()) > 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据订单id查询订单
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        public lottery_order getLotteryOrderSingleById(string oId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * FROM lottery_order_single WHERE id='{0}';", oId);

                DataSet dt = SQLiteHelper.getBLLInstance().Query(sb.ToString(), null);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<lottery_order> ltList = (List<lottery_order>)CollectionHelper.ConvertTo<lottery_order>(dt);
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
        /// 根据订单Id和状态获取订单下的所有票
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<lottery_ticket> getSingleTicketsByOrderIdAndStatesPagination(string orderId, List<String> states, Int64 sno, Int64 psize)
        {
            try
            {
                String sql = String.Empty;
                String statestr = String.Empty;
                if (null != states && states.Count > 0)
                {
                    if (states.Count == 1)
                    {
                        statestr = String.Format("AND ticket_state ='{0}'", states[0]);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder("AND ticket_state in (");
                        for (int i = 0; i < states.Count; i++)
                        {
                            sb.Append(states[i] + (i + 1 == states.Count ? ")" : ","));
                        }
                        statestr = sb.ToString();
                    }
                }

                //如果是自动反馈，只需要查询反馈失败的即可；
                sql = String.Format("SELECT * FROM lottery_ticket_single WHERE order_id ='{0}' {1} limit {2},{3};",
                   new String[] { orderId, statestr, sno.ToString(), psize.ToString() });

                List<lottery_ticket> list = new List<lottery_ticket>();
                DataSet ds = SQLiteHelper.getBLLInstance().Query(sql);
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
        /// 查询指定订单下所有票的数量
        /// </summary>
        /// <returns></returns>
        public Int64 getSingleTicketsNumByOrderId(string oId)
        {
            try
            {
                String sql = String.Empty;
                //如果是自动反馈，只需要查询反馈失败的即可；
                sql = String.Format("SELECT COUNT(*) FROM lottery_ticket_single WHERE order_id ='{0}';",
                   oId);

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
        /// 根据订单Id获取订单下的所有票——分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<lottery_ticket> getSingleTicketsByOrderIdPagination(string orderId, Int64 sno, Int64 psize)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * from lottery_ticket_single WHERE order_id='{0}' limit {1},{2};", orderId, sno.ToString(), psize.ToString());

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
    }
}