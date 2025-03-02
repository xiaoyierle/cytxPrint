﻿using Maticsoft.Common.Util.playType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage.imageStructure
{
    public class BD_ZJQ_DataBlock: ImageDataBlock
    {
        public static int DATA_BLOCK_HIGH = 3 * SPImageGlobal.BD_BB_HIGH;

        public BD_ZJQ_DataBlock(int startx, int starty)
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
            String[] qqhq = data.Split(':');
            this.getBDRacePoints(pointlist, data, BJDCPlayType.ZJQ, this.startPoint);//取赛事的点
            String[] dz = qqhq[1].Split(',');
            for (int j = 0; j < dz.Length; j++)
            {
                int x =0;
                int.TryParse(dz[j],out x);
                pointlist.Add(new Point(this.startPoint.X + (int)(Math.Floor(x / 3d) * SPImageGlobal.BD_HEAD_BB_WIDTH), this.startPoint.Y + (x % 3) * SPImageGlobal.BD_BB_HIGH_SHORT));
            }            
        }
    }
}
