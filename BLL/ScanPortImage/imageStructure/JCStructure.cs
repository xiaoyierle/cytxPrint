﻿using Maticsoft.Common.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage.imageStructure
{
   public class JCStructure: SPImageStructure
    {
       public JCStructure(String lNameStr,String pNameStr,int slipEventNumber)
       {
           this.SLIP_EVENT_NUMBER = slipEventNumber;
           this.ISHAS_CLEARANCE_TYPE = false;
           this.PTYPE_HEAD_DESC = SPImageGlobal.PTYPE_HEAD_DESC_DICTIONARY[lNameStr+"_" + pNameStr + "_" + this.SLIP_EVENT_NUMBER + "G"];
           int this_DATA_BLOCK_HIGH = 0;
           if (lNameStr.Equals("JCLQ",StringComparison.CurrentCultureIgnoreCase))//竞彩篮球
           {
               if (pNameStr.Equals("HHGG", StringComparison.CurrentCultureIgnoreCase))//混合过关
               {
                   this.HIGHLY_EFFECTIVE_CONTENT = (this.SLIP_EVENT_NUMBER == 3 ? 22 : (this.SLIP_EVENT_NUMBER == 6 ? 28 : 38)) * SPImageGlobal.BB_HIGH;
                   //过关类型
                   this.DATA_BLOCK_LIST.Add(new GGLXDataBlock(SPImageGlobal.LEFT_SMALL_BB_X, SPImageGlobal.START_POINT_Y + 2 * SPImageGlobal.BB_HIGH));
                   //投注块 
                   this_DATA_BLOCK_HIGH = this.SLIP_EVENT_NUMBER == 3 ? JL_HH3G_DataBlock.DATA_BLOCK_HIGH : JL_HH6G8G_DataBlock.DATA_BLOCK_HIGH;
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       if (this.SLIP_EVENT_NUMBER == 3)
                       {
                           this.DATA_BLOCK_LIST.Add(new JL_HH3G_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                               SPImageGlobal.START_POINT_Y + 3 * SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JL_HH3G_DataBlock.DATA_BLOCK_HIGH)
                               );
                       }
                       else
                       {
                           this.DATA_BLOCK_LIST.Add(new JL_HH6G8G_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                          SPImageGlobal.START_POINT_Y + 3 * SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JL_HH6G8G_DataBlock.DATA_BLOCK_HIGH)
                          );
                       }
                   }
               }
               else if (pNameStr.Equals("SFC", StringComparison.CurrentCultureIgnoreCase))
               {
                  this.HIGHLY_EFFECTIVE_CONTENT =(this.SLIP_EVENT_NUMBER == 3 ? 17 : (this.SLIP_EVENT_NUMBER == 6 ? 28 : 42)) * SPImageGlobal.BB_HIGH;
                  this_DATA_BLOCK_HIGH = JL_SFC_DataBlock.DATA_BLOCK_HIGH;
                  for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                  {
                      this.DATA_BLOCK_LIST.Add(new JL_SFC_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                              SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JL_SFC_DataBlock.DATA_BLOCK_HIGH)
                              );
                  }
               }
               else
               {
                   this.HIGHLY_EFFECTIVE_CONTENT =(this.SLIP_EVENT_NUMBER == 3 ? 14 : (this.SLIP_EVENT_NUMBER == 6 ? 22 : 30)) * SPImageGlobal.BB_HIGH;
                   this_DATA_BLOCK_HIGH = JL_DXF_SF_RFSF_DataBlock.DATA_BLOCK_HIGH;
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       this.DATA_BLOCK_LIST.Add(new JL_DXF_SF_RFSF_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                                (int)(SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + Math.Floor((i / 3d)) * JL_DXF_SF_RFSF_DataBlock.DATA_BLOCK_HIGH))
                                );
                   }
               }              
           }
           else
           {
               if (pNameStr.Equals("HHGG", StringComparison.CurrentCultureIgnoreCase))
               {
                   this.HIGHLY_EFFECTIVE_CONTENT = (this.SLIP_EVENT_NUMBER == 3 ? 30 : (this.SLIP_EVENT_NUMBER == 6 ? 30 : 35)) * SPImageGlobal.BB_HIGH;
                   this_DATA_BLOCK_HIGH = this.SLIP_EVENT_NUMBER == 3 ? JZ_HH3G_DataBlock.DATA_BLOCK_HIGH : (this.SLIP_EVENT_NUMBER == 6 ? JZ_HH6G_DataBlock.DATA_BLOCK_HIGH : JZ_HH8G_DataBlock.DATA_BLOCK_HIGH);
                   //过关类型
                   this.DATA_BLOCK_LIST.Add(new GGLXDataBlock(SPImageGlobal.LEFT_SMALL_BB_X, SPImageGlobal.START_POINT_Y + 2 * SPImageGlobal.BB_HIGH));
                   //投注块                   
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       if (this.SLIP_EVENT_NUMBER == 3)
                       {
                           this.DATA_BLOCK_LIST.Add(new JZ_HH3G_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                               SPImageGlobal.START_POINT_Y + 3 * SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_HH3G_DataBlock.DATA_BLOCK_HIGH));
                       }
                       else if(this.SLIP_EVENT_NUMBER == 6)
                       {
                           this_DATA_BLOCK_HIGH = JZ_HH6G_DataBlock.DATA_BLOCK_HIGH;
                           this.DATA_BLOCK_LIST.Add(new JZ_HH6G_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                          SPImageGlobal.START_POINT_Y + 3 * SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_HH6G_DataBlock.DATA_BLOCK_HIGH));
                       }
                       else
                       {
                           this_DATA_BLOCK_HIGH = JZ_HH8G_DataBlock.DATA_BLOCK_HIGH;
                           this.DATA_BLOCK_LIST.Add(new JZ_HH8G_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                         SPImageGlobal.START_POINT_Y + 3 * SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_HH8G_DataBlock.DATA_BLOCK_HIGH));
                       }
                   }
               }
               else if (pNameStr.Equals("SPF", StringComparison.CurrentCultureIgnoreCase))
               {
                   this.HIGHLY_EFFECTIVE_CONTENT = (this.SLIP_EVENT_NUMBER == 3 ? 14 : (this.SLIP_EVENT_NUMBER == 6 ? 22 : 30)) * SPImageGlobal.BB_HIGH;
                   this_DATA_BLOCK_HIGH = JZ_SPF_RQSPF_DataBlock.DATA_BLOCK_HIGH;
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       this.DATA_BLOCK_LIST.Add(new JZ_SPF_RQSPF_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                         SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_SPF_RQSPF_DataBlock.DATA_BLOCK_HIGH));
                   }
               }
               else if (pNameStr.Equals("BF", StringComparison.CurrentCultureIgnoreCase))//比分
               {
                   this.HIGHLY_EFFECTIVE_CONTENT = (this.SLIP_EVENT_NUMBER == 3 ? 21 : (this.SLIP_EVENT_NUMBER == 6 ? 36 : 51)) * SPImageGlobal.BB_HIGH;
                   this_DATA_BLOCK_HIGH = JZ_BF_DataBlock.DATA_BLOCK_HIGH;
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       this.DATA_BLOCK_LIST.Add(new JZ_BF_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                         SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_BF_DataBlock.DATA_BLOCK_HIGH));
                   }
               } else if (pNameStr.Equals("BQC", StringComparison.CurrentCultureIgnoreCase))//半全场
               {
                   this.HIGHLY_EFFECTIVE_CONTENT = (this.SLIP_EVENT_NUMBER == 3 ? 16 : (this.SLIP_EVENT_NUMBER == 6 ? 26 : 36)) * SPImageGlobal.BB_HIGH;
                   this_DATA_BLOCK_HIGH = JZ_BQC_DataBlock.DATA_BLOCK_HIGH;
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       this.DATA_BLOCK_LIST.Add(new JZ_BQC_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                         SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_BQC_DataBlock.DATA_BLOCK_HIGH));
                   }
               }
               else//总进球
               {
                   this.HIGHLY_EFFECTIVE_CONTENT = (this.SLIP_EVENT_NUMBER == 3 ? 15 : (this.SLIP_EVENT_NUMBER == 6 ? 24 : 33)) * SPImageGlobal.BB_HIGH;
                   this_DATA_BLOCK_HIGH = JZ_ZJQ_DataBlock.DATA_BLOCK_HIGH;
                   for (int i = 0; i < this.SLIP_EVENT_NUMBER; i++)
                   {
                       this.DATA_BLOCK_LIST.Add(new JZ_ZJQ_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (i % 3) * SPImageGlobal.RACE_BLOCK_WIDTH,
                         SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + (int)Math.Floor((i / 3d)) * JZ_ZJQ_DataBlock.DATA_BLOCK_HIGH));
                   }
               }
           }

            //过关方式块
           this.DATA_BLOCK_LIST.Add(new GGFSDataBlock(SPImageGlobal.LEFT_SMALL_BB_X,
                                SPImageGlobal.START_POINT_Y + (pNameStr.Equals("HHGG", StringComparison.CurrentCultureIgnoreCase)?3:1)* SPImageGlobal.BB_HIGH + (int)(Math.Ceiling(this.SLIP_EVENT_NUMBER / 3d) * this_DATA_BLOCK_HIGH), this.SLIP_EVENT_NUMBER));
            //倍数块
           this.DATA_BLOCK_LIST.Add(new MultipleDataBlock(SPImageGlobal.LEFT_SMALL_BB_X + 2 * SPImageGlobal.RACE_BLOCK_WIDTH,
                                SPImageGlobal.START_POINT_Y + (pNameStr.Equals("HHGG", StringComparison.CurrentCultureIgnoreCase) ? 3 : 1) * SPImageGlobal.BB_HIGH + (int)(Math.Ceiling(this.SLIP_EVENT_NUMBER / 3d) * this_DATA_BLOCK_HIGH)));

        }

        /// <summary>
        /// 根据票面数据，获取所有要描绘的点
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public override List<Point> getDrawPoints(lottery_ticket lt)
        {
            List<Point> points = new List<Point>();
            String[] codes = lt.bet_code.Split('|');
            int count = 0;
            for (int i = 0; i < this.DATA_BLOCK_LIST.Count; i++)
            {
                if (i < this.SLIP_EVENT_NUMBER + count)
                {
                    if (i < codes.Length + count)//codes.Length
                    {
                        if (this.DATA_BLOCK_LIST[i].GetType().FullName.Contains("GGLXDataBlock"))
                        {
                            count++;//如果有过关类型，这里需要往后延一个
                            this.DATA_BLOCK_LIST[i].getPointArrayByData(points,lt.play_type);
                        }
                        else
                        {
                            //this.DATA_BLOCK_LIST[i].getPointArrayByData(points, codes[i - count]);
                            this.DATA_BLOCK_LIST [ i ].getPointArrayByData ( points, codes [ i - count ] + "*" + lt.play_type );                            
                        }                        
                    }
                }
                else if (i == this.DATA_BLOCK_LIST.Count - 2)//串关
                {
                    this.DATA_BLOCK_LIST[i].getPointArrayByData(points, lt.play_type.Replace("null", "1c1").Split('-')[1]);
                }
                else if (i == this.DATA_BLOCK_LIST.Count - 1)//倍数
                {
                    this.DATA_BLOCK_LIST[i].getPointArrayByData(points, lt.license_id.ToString() + "-" + lt.multiple);
                }
            }
            return points;
        }

    }
}
