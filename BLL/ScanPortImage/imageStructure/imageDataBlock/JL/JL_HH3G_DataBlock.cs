﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage.imageStructure
{
    class JL_HH3G_DataBlock: ImageDataBlock
    {
       public static int DATA_BLOCK_HIGH = 14 * SPImageGlobal.BB_HIGH;

       public JL_HH3G_DataBlock(int startx, int starty)
       {
            this.startPoint = new System.Drawing.Point(startx, starty);
        }

        /// <summary>
        /// 根据传入的数据，得到需要描的几个点的起始坐标
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
       public override void getPointArrayByData(List<Point> pointlist, String data)
        {
            String[] dz = data.Split(':');
            //取对阵的点
            this.getSMGRacePoints(pointlist, dz[0], this.startPoint);//对阵的点
            //取竞猜结果的点
            this.getGuessResultPoints(pointlist, new String[] { dz[1] });
        }

        /// <summary>
        /// 取得竞猜结果的点位值
        /// </summary>
        /// <param name="resultdata"></param>
        private void getGuessResultPoints(List<Point> plist, String[] resultdata) {
            foreach (String item in resultdata)
            {
                String[] bets = item.Split(',');
                foreach (String bet in bets)
                {
                    String[] betrs = bet.Split('-');
                    int h = 0,w=0;
                    int result = 0;
                    int.TryParse(betrs[1],out result);
                    switch (betrs[0])
                    {
                        case "1"://胜负
                            h = 13;
                            w = betrs[1].Equals("0") ? 0 : 1;
                            break;
                        case "2"://让分胜负
                            h=8;
                            w = betrs[1].Equals("0") ? 0 : 1;
                            break;
                        case "3"://胜分差
                            h = 10 +(result > 5 ? result - 6 : result) / 2;
                            w = (result % 2 + (result > 5 ? 2 : 0));
                            break;
                        default://大小分
                            h=7;
                            w = betrs[1].Equals("0") ? 0 : 1;
                            break;
                    }

                    plist.Add(new Point(startPoint.X + w * SPImageGlobal.S2S_WIDTH,
                    this.startPoint.Y + h * SPImageGlobal.BB_HIGH + SPImageGlobal.B2S_HIGH));
                }
                
            }
        }
    }
}