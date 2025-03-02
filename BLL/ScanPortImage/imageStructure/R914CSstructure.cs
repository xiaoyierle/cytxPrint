﻿using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using Maticsoft.Common.Util.playType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage.imageStructure
{
    class R914CSstructure: SPImageStructure
    {
        public R914CSstructure(String lNameStr)
       {
           this.SLIP_EVENT_NUMBER = 3;
           this.ISHAS_CLEARANCE_TYPE = false;
           this.PTYPE_HEAD_DESC = SPImageGlobal.PTYPE_HEAD_DESC_DICTIONARY[lNameStr];
           this.HIGHLY_EFFECTIVE_CONTENT = 18 * SPImageGlobal.BB_HIGH;

           for (int i = 0; i < 3; i++)
           {
               this.DATA_BLOCK_LIST.Add(new R9_14C_DataBlock(SPImageGlobal.LEFT_SMALL_BB_X + (2 - i) * SPImageGlobal.RACE_BLOCK_WIDTH,
                               SPImageGlobal.START_POINT_Y + 2 * SPImageGlobal.BB_HIGH));
           }
            //倍数块
           this.DATA_BLOCK_LIST.Add(new MultipleDataBlock(SPImageGlobal.LEFT_SMALL_BB_X,
                                SPImageGlobal.START_POINT_Y + 17 * SPImageGlobal.BB_HIGH));

        }

        /// <summary>
        /// 根据票面数据，获取所有要描绘的点
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public override List<Point> getDrawPoints(lottery_ticket lt)
        {
            List<Point> points = new List<Point>();
            //彩种
            points.Add(new Point(SPImageGlobal.LEFT_SMALL_BB_X + (lt.license_id == LicenseContants.License.GAMEIDRXJ ? 11 : 10) * SPImageGlobal.S2S_WIDTH,
                                SPImageGlobal.START_POINT_Y + SPImageGlobal.BB_HIGH + SPImageGlobal.B2S_HIGH));
            String[] codes = lt.bet_code.Split(';');
            int m = 1,p =0;
            int.TryParse(lt.multiple,out m);
            int.TryParse(lt.bet_price,out p);

            if (lt.play_type.Equals(R9PlayType.FS.ToString()))//复式
            {
                points.Add(new Point(SPImageGlobal.LEFT_SMALL_BB_X,
                                SPImageGlobal.START_POINT_Y + 5 * SPImageGlobal.BB_HIGH + SPImageGlobal.B2S_HIGH));

                this.DATA_BLOCK_LIST[2].getPointArrayByData(points, codes[0]);
                this.DATA_BLOCK_LIST[3].getPointArrayByData(points, lt.license_id.ToString() + "-" + lt.multiple);
            }
            else
            {
                for (int i = 0; i < this.DATA_BLOCK_LIST.Count; i++)
                {
                    if (i < this.SLIP_EVENT_NUMBER)
                    {
                        if (i < codes.Length)//codes.Length
                        {
                            this.DATA_BLOCK_LIST[i].getPointArrayByData(points, codes[i]);
                        }
                    }
                    else if (i == this.DATA_BLOCK_LIST.Count - 1)//倍数
                    {
                        this.DATA_BLOCK_LIST[i].getPointArrayByData(points, lt.license_id.ToString() + "-" + lt.multiple);
                    }
                }
            }            
            return points;
        }

    }
}

