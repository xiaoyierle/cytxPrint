﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage.imageStructure
{
    public class GGFSDataBlock : ImageDataBlock
    { 
        public GGFSDataBlock(int startx, int starty,int raceNum)
        {
            this.startPoint = new System.Drawing.Point(startx, starty);
            this.RaceNum = raceNum;
        }


        public override void getPointArrayByData(List<Point> pointlist, String data)
        {
            String[][] thisggfs = SPImageGlobal.GGFS_STR_ARRAY[this.RaceNum];
            for (int i = 0; i < thisggfs.Length; i++)
            {
                for (int j = 0; j < thisggfs[i].Length; j++)
                {
                    if (thisggfs[i][j].Equals(data, StringComparison.CurrentCultureIgnoreCase))
                    {
                        pointlist.Add(new Point(SPImageGlobal.LEFT_SMALL_BB_X + j * SPImageGlobal.S2S_WIDTH,
                            this.startPoint.Y + (1 + i) * SPImageGlobal.BB_HIGH + SPImageGlobal.B2S_HIGH));
                        i = 1000;//外层也不需要循环
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 场次数量
        /// </summary>
        public int RaceNum
        {get;set;}

    }
}
