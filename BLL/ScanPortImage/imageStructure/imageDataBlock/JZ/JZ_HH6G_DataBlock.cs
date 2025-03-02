﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage.imageStructure
{
    public class JZ_HH6G_DataBlock : ImageDataBlock
    {
        public static int DATA_BLOCK_HIGH = 11 * SPImageGlobal.BB_HIGH;
        private String play="";

        public JZ_HH6G_DataBlock ( int startx, int starty )
        {
            this.startPoint = new System.Drawing.Point ( startx, starty );
        }

        /// <summary>
        /// 根据传入的数据，得到需要描的几个点的起始坐标
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override void getPointArrayByData ( List<Point> pointlist, String data )
        {
            String[] dz = data.Split ( ':' );
            //记录当前的玩法
            this.play = dz [ 2 ].Split ( '*' ) [ 1 ];
            //取对阵的点
            this.getSMGRacePoints ( pointlist, dz [ 0 ], this.startPoint );//对阵的点
            //取竞猜结果的点
            //0,1,2,3,4,5,6,7
            this.getGuessResultPoints ( pointlist, dz [ 1 ].Split ( ',' ) );
        }

        /// <summary>
        /// 取得竞猜结果的点位值
        /// </summary>
        /// <param name="resultdata"></param>
        private void getGuessResultPoints ( List<Point> plist, String [ ] resultdata )
        {
            foreach ( String item in resultdata )
            {
                String[] playresult = item.Split ( '-' );//5-1,5-2,5-3,5-4
                int x = 0, y = 0;
                String thisplay="",thisresult="";

                if ( !this.play.StartsWith ( "6-" ) )//非混合过关用混合过关单子
                {
                    thisplay = this.play.Split ( '-' ) [ 0 ];
                    thisresult = playresult [ 0 ];
                }
                else
                {
                    thisplay = playresult [ 0 ];
                    thisresult = playresult [ 1 ];
                }

                switch ( thisplay )
                {
                    case "1"://胜平负
                        y = 7;
                        switch ( thisresult )
                        {
                            case "3":
                                x = 0;
                                break;
                            case "1":
                                x = 1;
                                break;
                            default:
                                x = 2;
                                break;
                        }
                        break;
                    case "2"://让球胜平负
                        y = 8;
                        switch ( thisresult )
                        {
                            case "3":
                                x = 0;
                                break;
                            case "1":
                                x = 1;
                                break;
                            default:
                                x = 2;
                                break;
                        }
                        break;
                    default://总进球
                        switch ( thisresult )
                        {
                            case "0":
                                x = 0;
                                y = 9;
                                break;
                            case "1":
                                x = 1;
                                y = 9;
                                break;
                            case "2":
                                x = 2;
                                y = 9;
                                break;
                            case "3":
                                x = 3;
                                y = 9;
                                break;
                            case "4":
                                x = 0;
                                y = 10;
                                break;
                            case "5":
                                x = 1;
                                y = 10;
                                break;
                            case "6":
                                x = 2;
                                y = 10;
                                break;
                            default:
                                x = 3;
                                y = 10;
                                break;
                        }
                        break;
                }

                plist.Add ( new Point ( startPoint.X + x * SPImageGlobal.S2S_WIDTH,
                this.startPoint.Y + y * SPImageGlobal.BB_HIGH + SPImageGlobal.B2S_HIGH ) );
            }
        }
    }
}