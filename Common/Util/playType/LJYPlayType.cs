﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.Common.Util.playType
{
    /// <summary>
    /// 六加一玩法
    /// </summary>
   public class LJYPlayType : IPlayType
    {
        /*********************单例模式********************************************/
        private LJYPlayType() { }
        private static LJYPlayType instance = new LJYPlayType();
        public static LJYPlayType getInstance()
        {
            return instance;
        }
        /*********************单例模式********************************************/
        public const int ZHX_DS = 1;    // 直选单式
        public const int ZHX_FS = 2;	// 直选复式

        /// <summary>
        /// 用playId作为key
        /// </summary>
        private static Dictionary<String, String> playPId2PNameDictionary = new Dictionary<String, String>()
        {
            {ZHX_DS.ToString(),"直选单式" }, { ZHX_FS.ToString(),"直选复式"}
        };

        /// <summary>
        /// 获取玩法名称
        /// </summary>
        /// <param name="playID"></param>
        /// <returns></returns>
        public string getTypeName(string playID)
        {
            if (playPId2PNameDictionary.ContainsKey(playID))
            {
                return playPId2PNameDictionary[playID];
            }

            return "未知玩法";
        }

    }
}
