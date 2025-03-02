﻿using Maticsoft.Common.model;
using Maticsoft.Common.Util.playType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage
{
    public class TicketSplitUtil
    {

        /// <summary>
        /// 获取组合的个数
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int getCombiningNum(int m, int n) {

            if (m<n)
            {
                return 0;
            }
            else if (m==n)
            {
                return 1;
            }

            int mm = 1, nm = 1;
            for (int i = 0; i < n; i++)
            {
                mm = mm*(m-i);
            }

            for (int i = 0; i < n; i++)
            {
                nm=nm*(n-i);
            }

            return (int)mm / nm;
        }
        /// <summary>
        /// 获得组合数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<String> getCombiningData(String[] data,int startIndex, int n) {
            List<String> datalist = new List<string>();
            if (data.Length < n + startIndex)
            {
                String code = String.Empty;
                for (int i = 0; i < data.Length; i++)
                {
                    code += data[i] + ((i != data.Length - 1) ? "," : "");
                }
                datalist.Add(code);
                return datalist;
            }

            if (n==1)
            {
                for (int i = startIndex; i < data.Length; i++)
                {
                    datalist.Add(data[i]);
                }

                return datalist;
            }
            else
            {
                for (int i = startIndex; i <= data.Length - n; i++)
                {
                    List<String> templist = TicketSplitUtil.getCombiningData(data, i+ 1, n - 1);
                    foreach (String item in templist)
                    {
                        datalist.Add(data[i]+","+item);
                    }
                }
            }

            return datalist;
        }
        /// <summary>
        /// PL3不支持玩法拆分
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public static List<lottery_ticket> pl3TicketSplit(lottery_ticket lt) {
            List<lottery_ticket> templist = new List<lottery_ticket>();
            templist.Add(lt);
            return templist;
        }

        /// <summary>
        /// SSQ不支持玩法拆分
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public static List<lottery_ticket> ssqTicketSplit(lottery_ticket lt)
        {
            List<lottery_ticket> templist = new List<lottery_ticket>();
            List<lottery_ticket> returnplist = new List<lottery_ticket>();            

            //先拆倍数
            if (lt .multiple.Equals("1"))
            {
               templist.Add(lt);
            }
            else
            {
                int[] multiples = { 10, 5, 3, 2, 1 };
                int ltm = 1, price = 2;
                int.TryParse(lt.multiple, out ltm);
                int.TryParse(lt.bet_price, out price);
                int onemultipmoney = price / ltm;

                for (int i = 0; i < multiples.Length; i++)
                {
                    if (ltm >= multiples[i])
                    {
                        lottery_ticket lttemp = lt.copy();
                        lttemp.multiple = multiples[i].ToString();
                        lttemp.bet_price = (onemultipmoney * multiples[i]).ToString();
                        templist.Add(lttemp);

                        ltm -= multiples[i];
                        i--;
                    }
                }
            }            

            foreach (lottery_ticket item in templist)
            {
                if (lt.play_type.Equals(SSQPlayType.DS))
                {
                    returnplist.Add(item);
                }
                else//复式
                {
                    String[] balls = lt.bet_code.Split('+');
                    String[] redballs = balls[0].Split(',');
                    String[] blueballs = balls[1].Split(',');
                    if (redballs.Length == 6 || blueballs.Length == 1)
                    {
                        returnplist.Add(item);
                    }
                    else
                    {
                        int itemprice = 2;
                        int.TryParse(item.bet_price, out itemprice);
                        if (getCombiningNum(redballs.Length, 6) > blueballs.Length)//拆蓝球
                        {
                            for (int i = 0; i < blueballs.Length; i+=5)
			                {
			                    lottery_ticket thislt1 = new lottery_ticket();
                                thislt1.license_id = item.license_id;
                                thislt1.username = item.username;
                                thislt1.multiple = item.multiple;
                                thislt1.play_type = item.play_type;
                                thislt1.ticket_id = item.ticket_id;
                                thislt1.order_id = item.order_id;

                                int j = 0;
                                for (j = 0; i * 5 + j < blueballs.Length || j<5; j++)
                                {
                                    thislt1.bet_code += balls[0] + "+" + blueballs[i]+(j<4?";":"");
                                }                                
                                thislt1.bet_price = (itemprice*j / blueballs.Length).ToString();
                                returnplist.Add(thislt1);
			                }
                        }
                        else
                        {
                            List<String> redcom = getCombiningData(redballs,0,6);
                            for (int i = 0; i < redcom.Count ; i+=5)
                            {
                                lottery_ticket thislt1 = new lottery_ticket();
                                thislt1.license_id = item.license_id;
                                thislt1.username = item.username;
                                thislt1.multiple = item.multiple;
                                thislt1.play_type = item.play_type;
                                thislt1.ticket_id = item.ticket_id;
                                thislt1.order_id = item.order_id;
                                int j = 0;
                                for (j = 0; i * 5 + j < blueballs.Length || j<5; j++)
                                {
                                    thislt1.bet_code += redcom[i] + "+" + balls[1] + (j < 4 ? ";" : "");
                                }
                                thislt1.bet_price = (itemprice * j / blueballs.Length).ToString();
                                returnplist.Add(thislt1);
                            }
                        }
                    }                    
                }
            }           

            return returnplist;
        }

        /// <summary>
        /// 11X5不支持玩法拆分
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public static List<lottery_ticket> syx5TicketSplit(lottery_ticket lt)
        {
            List<lottery_ticket> templist = new List<lottery_ticket>();
            
            int betnum = 0,multiple = 0;
            int.TryParse(lt.bet_num,out betnum);
            int.TryParse(lt.multiple, out multiple);

            if (lt.play_type.Equals(SD11X5PlayType.R2DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.Q2ZX_DT.ToString())||
                lt.play_type.Equals(SD11X5PlayType.R3DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.Q3ZX_DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.R4DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.R5DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.R6DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.R7DT.ToString()) ||
                lt.play_type.Equals(SD11X5PlayType.R8DT.ToString()))//任二胆拖拆成任二单式
            {
                String danma = lt.bet_code.Split(')')[0].Replace("(", "");
                String[] tuoma = lt.bet_code.Split(')')[1].Split(',');

                String newPlay = String.Empty;//拆票后的玩法
                int codeNum = 0;
                if (lt.play_type.Equals(SD11X5PlayType.R2DT.ToString())) {
                    newPlay = SD11X5PlayType.R2DS.ToString();
                    codeNum = 2;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.Q2ZX_DT.ToString()))
                {
                    newPlay = SD11X5PlayType.Q2ZX.ToString();
                    codeNum = 2;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.R3DT.ToString()))
                {
                    newPlay = SD11X5PlayType.R3DS.ToString();
                    codeNum = 3;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.Q3ZX_DT.ToString()))
                {
                    newPlay = SD11X5PlayType.Q3ZX.ToString();
                    codeNum = 3;
                }else if (lt.play_type.Equals(SD11X5PlayType.R4DT.ToString()))
                {
                    newPlay = SD11X5PlayType.R4DS.ToString();
                    codeNum = 4;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.R5DT.ToString()))
                {
                    newPlay = SD11X5PlayType.R5DS.ToString();
                    codeNum = 5;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.R6DT.ToString()))
                {
                    newPlay = SD11X5PlayType.R6DS.ToString();
                    codeNum = 6;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.R7DT.ToString()))
                {
                    newPlay = SD11X5PlayType.R7DS.ToString();
                    codeNum = 7;
                }
                else if (lt.play_type.Equals(SD11X5PlayType.R8DT.ToString()))
                {
                    newPlay = SD11X5PlayType.R8DS.ToString();
                    codeNum = 8;
                }

                List<String> codelist = TicketSplitUtil.getCombiningData(tuoma, 0, codeNum - danma.Split(',').Length);
                for (int i = 0; i < Math.Ceiling(betnum/5d); i++)
                {
                    lottery_ticket thislt1 = new lottery_ticket();
                    thislt1.license_id = lt.license_id;
                    thislt1.username = lt.username;
                    thislt1.multiple = lt.multiple;
                    thislt1.play_type = newPlay;
                    thislt1.ticket_id = lt.ticket_id;
                    thislt1.order_id = lt.order_id;

                    for (int j = 0; j + i * 5 < codelist.Count && j<5; j++)
                    {
                        thislt1.bet_code += danma + "," + codelist[j + i * 5] + ((((j + i * 5 + 1) % 5 != 0) && (j + i * 5 + 1) != codelist.Count) ? ";" : "");
                        thislt1.bet_price = ((int)(j+1) * 2 * multiple).ToString();
                    }

                    templist.Add(thislt1);
                }            
            }
            else
            {
                templist.Add(lt);
            }            
            return templist;
        }


        /// <summary>
        /// 不能打5注的，需要拆开
        /// </summary>
        /// <param name="lt"></param>
        /// <param name="betnum">支持的注数</param>
        /// <returns></returns>
        public static List<lottery_ticket> splitByBetNum(lottery_ticket lt,int betnum) {
            List<lottery_ticket> templist = new List<lottery_ticket>();
            String[] betcodes = lt.bet_code.Split(';');
            int price = 0;
            int.TryParse(lt.bet_price, out price);
            double onemoney = price / betcodes.Length;
            if (betcodes.Length > betnum)
            {
                lottery_ticket thislt1 = new lottery_ticket();
                thislt1.order_id = lt.order_id;
                thislt1.license_id = lt.license_id;
                thislt1.username = lt.username;
                thislt1.multiple = lt.multiple;
                thislt1.play_type = lt.play_type;
                thislt1.ticket_id = lt.ticket_id;
                thislt1.bet_price = ((int)onemoney * betnum).ToString();

                lottery_ticket thislt2 = new lottery_ticket();
                thislt2.order_id = lt.order_id;
                thislt2.license_id = lt.license_id;
                thislt2.username = lt.username;
                thislt2.multiple = lt.multiple;
                thislt2.play_type = lt.play_type;
                thislt2.ticket_id = lt.ticket_id;
                thislt2.bet_price = (price-(int)onemoney * betnum).ToString();

                if (betnum == 3)
                {
                    thislt1.bet_code = betcodes[0] + ";" + betcodes[1] + ";" + betcodes[2];
                    thislt2.bet_code = betcodes[3] + ";" + (betcodes.Length == 5 ? ";" + betcodes[4] : "");
                }
                else
                {
                    thislt1.bet_code = betcodes[0] + ";" + betcodes[1] + ";" + betcodes[2] + ";" + betcodes[3];
                    thislt2.bet_code = betcodes[4];
                }               

                templist.Add(thislt1);
                templist.Add(thislt2);
            }
            else
            {
                templist.Add(lt);
            }

            return templist;
        }
        /// <summary>
        /// 有些倍数支持不全，需要拆票
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public static List<lottery_ticket> splitByMuliplt(lottery_ticket lt) {
            List<lottery_ticket> templist = new List<lottery_ticket>();
            int multiple = 0;
            int.TryParse(lt.multiple, out multiple);
            int price = 0;
            int.TryParse(lt.bet_price, out price);
            double onemoney = price / multiple;
            if (multiple > 10)//0-10不用拆
            {
                while (multiple > 0)
                {
                    lottery_ticket thislt = new lottery_ticket();
                    thislt.license_id = lt.license_id;
                    thislt.username = lt.username;
                    if (multiple >= 20)
                    {
                        thislt.multiple = "20";
                        thislt.bet_price = (20 * (int)onemoney).ToString();
                        multiple -= 20;
                    }
                    else if (multiple >= 15)
                    {
                        thislt.multiple = "15";
                        thislt.bet_price = (15 * (int)onemoney).ToString();
                        multiple -= 15;
                    }
                    else if (multiple >= 10)
                    {
                        thislt.multiple = "10";
                        thislt.bet_price = (10 * (int)onemoney).ToString();
                        multiple -= 10;
                    }
                    else
                    {
                        thislt.multiple = multiple.ToString();
                        thislt.bet_price = (multiple * (int)onemoney).ToString();
                        multiple -= multiple;
                    }

                    thislt.play_type = lt.play_type;
                    thislt.ticket_id = lt.ticket_id;
                    thislt.bet_code = lt.bet_code;
                    thislt.order_id = lt.order_id;

                    templist.Add(thislt);
                }
            }
            else
            {
                templist.Add(lt);
            }

           return templist;
        }
    }
}
