﻿using Maticsoft.BLL.serviceimpl;
using Maticsoft.BLL.serviceinterface;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using System.Collections.Generic;

namespace Maticsoft.Controller
{
    public class UploadController
    {
        private IUploadService trs = new UploadServiceImpl();

        /// <summary>
        /// 解析一行数据
        /// </summary>
        /// <param name="licenseId"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public string parse(int licenseId, string line)
        {
            return trs.parse(licenseId, line);
        }

        /// <summary>
        /// 解析txt文件 排列3
        /// </summary>
        /// <param name="licenseId"></param>
        /// <param name="playId"></param>
        /// <param name="betCode"></param>
        /// <returns></returns>
        public string parsePls(int licenseId, int playId, string betCode)
        {
            return trs.parsePls(licenseId, playId, betCode);
        }

        /// <summary>
        /// 解析txt文件 竞彩足球 竞彩篮球
        /// </summary>
        /// <param name="licenseId"></param>
        /// <param name="playId"></param>
        /// <param name="fileContent"></param>
        /// <param name="schNum"></param>
        /// <param name="passLen"></param>
        /// <param name="withSch"></param>
        /// <returns></returns>
        public string parseGuessLine(int licenseId, int playId, string fileContent, int schNum, int passLen, bool withSch)
        {
            return trs.parseGuessLine(licenseId, playId, fileContent, schNum, passLen, withSch);
        }

        /// <summary>
        /// 生成单式上传订单
        /// </summary>
        /// <param name="lotteryOrder"></param>
        public int CreateSingleOrder(lottery_order lotteryOrder)
        {
            try
            {
                return trs.createSingleOrder(lotteryOrder);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 生成票
        /// </summary>
        /// <param name="lotteryOrder"></param>
        public void CreateSingleTickets(lottery_order lotteryOrder)
        {
            try
            {
                if (lotteryOrder.license_id > 100)
                {

                }
                else if (lotteryOrder.license_id == LicenseContants.License.GAMEIDJCZQ || lotteryOrder.license_id == LicenseContants.License.GAMEIDJCLQ ||
                        lotteryOrder.license_id == LicenseContants.License.GAMEIDBJDC || lotteryOrder.license_id == LicenseContants.License.GAMEIDSFGG)
                {

                }
                else
                {
                    IList<lottery_ticket> ltList = new List<lottery_ticket>();
                    string betCode = lotteryOrder.bet_code;
                    string[] betCodeArr = betCode.Split(';');

                    string strTemp = "";

                    int betNumTemp = 0;
                    int ticketsNum = 0;
                    for (int i = 1; i <= betCodeArr.Length; i++)
                    {
                        betNumTemp++;
                        strTemp += betCodeArr[i-1];
                        if (i % 5 == 0 || i == betCodeArr.Length)
                        {
                            lottery_ticket lt = new lottery_ticket();
                            lt.bet_code = strTemp;
                            lt.bet_num = betNumTemp.ToString();
                            lt.bet_price = (int.Parse(lt.bet_num) * 2 * int.Parse(lotteryOrder.multiple)).ToString();
                            lt.username = lotteryOrder.username;
                            lt.userid = 0;
                            lt.storeid = lotteryOrder.storeid;
                            lt.ticket_state = GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString();
                            lt.zj_flag = "0";
                            lt.err_ticket_sign = 0;
                            lt.is_feedback = 0;
                            lt.order_id = lotteryOrder.id;
                            lt.multiple = lotteryOrder.multiple;
                            lt.license_id = lotteryOrder.license_id;
                            lt.play_type = lotteryOrder.play_type;
                            lt.issue = lotteryOrder.issue;
                            lt.stop_time = lotteryOrder.stop_time;
                            ltList.Add(lt);
                            strTemp = "";
                            betNumTemp = 0;
                            ticketsNum++;
                        }
                        else
                        {
                            strTemp += ";";
                        }
                    }
                    lotteryOrder.total_tickets_num = ticketsNum;
                    trs.createSingleTicket(ltList);
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public void isSingleOrderExist()
        {
            try
            {
                trs.isSingleOrderExist();
            }
            catch
            {
                throw;
            }
        }

        public void isSingleTicketExist()
        {
            try
            {
                trs.isSingleTicketExist();
            }
            catch
            {
                throw;
            };
        }

        public long GetSingleOrderId()
        {
            try
            {
                return trs.GetSingleOrderId();
            }
            catch
            {
                throw;
            }
        }
    }
}
