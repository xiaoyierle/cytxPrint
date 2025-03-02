﻿using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Common.dencrypt;
using Maticsoft.Common.dbUtility;
using Maticsoft.Common.Util.playType;

namespace Maticsoft.Common.Util
{
    public class BetCodeTranslationUtil
    {
        //投注号码翻译工具
        public static String betCode2ShowStr(String betCode, String license, String play)
        {
            String mcn = "单关";
            if (play.Contains("-"))
            {
                mcn = play.Split('-')[1];
                play = play.Split('-')[0];
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(LicenseContants.licenseId2NameDictionary[license] + "    " + PlayTypeFactory.getInstance(license).getTypeName(play) + "    " + mcn);
            sb.Append("\r\n---------------------------------------------\r\n");
            switch (int.Parse(license))
            {
                case LicenseContants.License.GAMEIDJCZQ:
                    sb.Append(jcTranslator(betCode, license, play));
                    break;
                case LicenseContants.License.GAMEIDJCLQ:
                    sb.Append(jcTranslator(betCode, license, play));
                    break;
                default:
                    sb.Append(BetCodeTranslationUtil.universalTranslator(betCode));
                    break;
            }

            return sb.ToString();
        }

        //投注号码翻译工具
        public static String[] betCode2ShowArray(String betCode, String license, String play)
        {
            String[] showArray = new String[2];
            String mcn = "单关";

            try
            {
                play = play.Replace("null", "固定单关");
                if (play.Contains("-")) //Replace("null","1C1")匹配冠亚军
                {
                    mcn = play.Split('-')[1];
                    play = play.Split('-')[0];
                }

                showArray[0] = LicenseContants.licenseId2NameDictionary[license] + "    (" + PlayTypeFactory.getInstance(license).getTypeName(play) + "    " + mcn + ")";

                String decryptBetCode = DESEncrypt.Decrypt(betCode, GlobalConstants.KEY);
                if (license.Equals(LicenseContants.License.GAMEIDJCZQ.ToString()) &&
                (play.Contains("10") || play.Contains("11")))
                {
                    showArray[1] = decryptBetCode.Replace(",", Environment.NewLine);
                }
                else
                {
                    switch (int.Parse(license))
                    {
                        case LicenseContants.License.GAMEIDJCZQ:
                            showArray[1] = jcTranslator(decryptBetCode, license, play);
                            break;
                        case LicenseContants.License.GAMEIDJCLQ:
                            showArray[1] = jcTranslator(decryptBetCode, license, play);
                            break;
                        default:
                            showArray[1] = BetCodeTranslationUtil.universalTranslator(decryptBetCode);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return new string[] { "未知", "------" };
            }
            return showArray;
        }

        //投注号码翻译工具 (明码)
        public static String[] betCode2ShowArrayNoCrypt(String betCode, String license, String play)
        {
            String[] showArray = new String[2];
            String mcn = "单关";

            try
            {
                play = play.Replace("null", "固定单关");
                if (play.Contains("-")) //Replace("null","1C1")匹配冠亚军
                {
                    mcn = play.Split('-')[1];
                    play = play.Split('-')[0];
                }

                showArray[0] = LicenseContants.licenseId2NameDictionary[license] + "    (" + PlayTypeFactory.getInstance(license).getTypeName(play) + "    " + mcn + ")";

                String decryptBetCode = license.Equals("21") ? betCode.Replace("|", ";") : betCode;// DESEncrypt.Decrypt(betCode, GlobalConstants.KEY);
                if (license.Equals(LicenseContants.License.GAMEIDJCZQ.ToString()) &&
                (play.Contains("10") || play.Contains("11")))
                {
                    showArray[1] = decryptBetCode.Replace(",", Environment.NewLine);
                }
                else
                {
                    switch (int.Parse(license))
                    {
                        case LicenseContants.License.GAMEIDJCZQ:
                            showArray[1] = jcTranslator(decryptBetCode, license, play);
                            break;
                        case LicenseContants.License.GAMEIDJCLQ:
                            showArray[1] = jcTranslator(decryptBetCode, license, play);
                            break;
                        default:
                            showArray[1] = BetCodeTranslationUtil.universalTranslator(decryptBetCode);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return new string[] { "未知", "------" };
            }
            return showArray;
        }


        //投注号码翻译工具 明码
        public static String[] betCode2ShowArrayNoEncrypt(String betCode, String license, String play)
        {
            String[] showArray = new String[2];
            String mcn = "单关";

            try
            {
                play = play.Replace("null", "固定单关");
                if (play.Contains("-")) //Replace("null","1C1")匹配冠亚军
                {
                    mcn = play.Split('-')[1];
                    play = play.Split('-')[0];
                }

                showArray[0] = LicenseContants.licenseId2NameDictionary[license] + "    (" + PlayTypeFactory.getInstance(license).getTypeName(play) + "    " + mcn + ")";

                String decryptBetCode = license.Equals("21")?betCode.Replace("|",";"):betCode;
                if (license.Equals(LicenseContants.License.GAMEIDJCZQ.ToString()) &&
                (play.Contains("10") || play.Contains("11")))
                {
                    showArray[1] = decryptBetCode.Replace(",", Environment.NewLine);
                }
                else
                {
                    switch (int.Parse(license))
                    {
                        case LicenseContants.License.GAMEIDJCZQ:
                            showArray[1] = jcTranslator(decryptBetCode, license, play);
                            break;
                        case LicenseContants.License.GAMEIDJCLQ:
                            showArray[1] = jcTranslator(decryptBetCode, license, play);
                            break;
                        default:
                            showArray[1] = BetCodeTranslationUtil.universalTranslator(decryptBetCode);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return new string[] { "未知", "------" };
            }
            return showArray;
        }


        /// <summary>
        /// 通用转换
        /// </summary>
        /// <param name="betCode"></param>
        /// <returns></returns>
        private static String universalTranslator(String betCode)
        {
            return betCode.Replace(",", " , ").Replace(";", System.Environment.NewLine) + System.Environment.NewLine;
        }

        /// <summary>
        /// 竞彩号码翻译
        /// </summary>
        /// <param name="betCode"></param>
        /// <param name="play"></param>
        /// <returns></returns>
        private static String jcTranslator(String betCode, String license, String play)
        {

            String subplay = play;//混合过关时各个选择的玩法
            String[] bets = betCode.Split('|');
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (String b in bets)
            {
                String[] bet = b.Split(':');
                sb.Append(DateUtil.data2weekDayCStrTranslation(bet[0].Substring(0, 8)));//周一
                sb.Append(bet[0].Substring(8, 3));//001
                sb.Append("   (");
                String[] betitem = bet[1].Split(',');
                foreach (String i in betitem)
                {
                    if (license.Equals(LicenseContants.License.GAMEIDJCZQ.ToString()))
                    {
                        switch (int.Parse(play))
                        {
                            case JCZQPlayType.HHGG:
                                subplay = i.Split('-')[0];
                                sb.Append("[" + BettingItemConstant.JZ_BETTINGITEM[i.Split('-')[0]][i.Split('-')[1]] + "]");
                                break;
                            default:
                                sb.Append("[" + BettingItemConstant.JZ_BETTINGITEM[play][i] + "]");
                                break;
                        }
                    }
                    else
                    {
                        switch (int.Parse(play))
                        {
                            case JCLQPlayType.HHGG:
                                subplay = i.Split('-')[0];
                                sb.Append("[" + BettingItemConstant.JL_BETTINGITEM[i.Split('-')[0]][i.Split('-')[1]] + "]");
                                break;
                            default:
                                sb.Append("[" + BettingItemConstant.JL_BETTINGITEM[play][i] + "]");
                                break;
                        }
                    }


                }

                sb.Append(")");
                if (license.Equals(LicenseContants.License.GAMEIDJCZQ.ToString()))
                {
                    if (play.Equals(JCZQPlayType.HHGG.ToString()))
                    {
                        sb.Append("____" + PlayTypeFactory.getInstance(license).getTypeName(subplay));
                    }
                }
                else
                {
                    if (play.Equals(JCLQPlayType.HHGG.ToString()))
                    {
                        sb.Append("____" + PlayTypeFactory.getInstance(license).getTypeName(subplay));
                    }
                }

                sb.Append(count < bets.Length - 1 ? System.Environment.NewLine : "");
            }
            return sb.ToString();
        }
    }
}
