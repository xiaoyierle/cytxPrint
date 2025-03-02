﻿using Maticsoft.BLL.log;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.SingleUpload;
using Maticsoft.Common.Util;
using Maticsoft.Common.Util.playType;
using Maticsoft.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class TabUploadTxt : UserControl
    {
        int licenseId;//彩种
        int schNum; //	场次数
        int passNum;// 过关方式（竞彩）,2x1,3x1等
        int playId;  // 竞彩子玩法
        int betNum; // 注数
        int multiple = 1;// 倍数
        string betCode;
        string fileName;//文件名

        public List<GuessFootball> gfList = new List<GuessFootball>();

        public int LicenseId
        {
            get { return licenseId; }
            set { licenseId = value; }
        }

        public int SchNum
        {
            get { return schNum; }
            set { schNum = value; }
        }

        public int PassNum
        {
            get { return passNum; }
            set { passNum = value; }
        }

        public int PlayId
        {
            get { return playId; }
            set { playId = value; }
        }

        public string BetCode
        {
            get { return betCode; }
            set { betCode = value; }
        }

        public int BetNum
        {
            get { return betNum; }
            set
            {
                betNum = value;
                this.lbBetPrice.Text = string.Format("您选择了：共{0}注、金额{1}元", this.BetNum, 0);
            }
        }

        public int Multiple
        {
            get { return multiple; }
            set
            {
                multiple = value;
                this.lbBetPrice.Text = string.Format("您选择了：共{0}注、金额{1}元", this.BetNum, this.price * multiple);
            }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        UploadController uploadController = new UploadController();

        public TabUploadTxt()
        {
            InitializeComponent();

            this.lbJczqRQSPF.Click += new System.EventHandler(this.common_Click);
            this.lbJczqBQC.Click += new System.EventHandler(this.common_Click);
            this.lbJczqZJQ.Click += new System.EventHandler(this.common_Click);
            this.lbJczqBF.Click += new System.EventHandler(this.common_Click);
            this.lbJczqSPF.Click += new System.EventHandler(this.common_Click);
            this.lbRFSF.Click += new System.EventHandler(this.common_Click);
            this.lbJclqSFC.Click += new System.EventHandler(this.common_Click);
            this.lbJclqDXF.Click += new System.EventHandler(this.common_Click);
            this.lbJclqSF.Click += new System.EventHandler(this.common_Click);
            this.lbZ6_DS.Click += new System.EventHandler(this.common_Click);
            this.lbZ3_DS.Click += new System.EventHandler(this.common_Click);
            this.lbZHX_DS.Click += new System.EventHandler(this.common_Click);
            this.lbZLDS.Click += new System.EventHandler(this.common_Click);
            this.lbZSDS3D.Click += new System.EventHandler(this.common_Click);
            this.lbZXDS3D.Click += new System.EventHandler(this.common_Click);
        }

        private void btnUpload_MouseEnter(object sender, EventArgs e)
        {
            this.btnUpload.BackgroundImage = global::Demo.Properties.Resources.btnUploadEnter;
        }

        private void btnUpload_MouseLeave(object sender, EventArgs e)
        {
            this.btnUpload.BackgroundImage = global::Demo.Properties.Resources.btnUploadLeave;
        }

        //打开单式上传TXT文件
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "ir files (*.txt)|*.txt|All files (*.*)|*.*"; //过滤文件类型
            fd.InitialDirectory = Application.StartupPath + "\\Temp\\";//设定初始目录
            fd.ShowReadOnly = true; //设定文件是否只读
            DialogResult r = fd.ShowDialog();
            fd.RestoreDirectory = true;
            this.FileName = fd.SafeFileName.Replace(".txt", "").Replace(".TXT", "");
            if (r == DialogResult.OK)
            {
                FileInfo file = new FileInfo(fd.FileName);
                this.upload(file);
            }
        }

        public int price { set; get; }

        /// <summary>
        /// 解析单式上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="licenseId"></param>
        /// <param name="schNum"></param>
        /// <param name="passNum"></param>
        /// <param name="playId"></param>
        /// <returns></returns>
        private bool upload(FileInfo file)
        {
            try
            {
                using (StreamReader sr = file.OpenText())
                {
                    string testCode = "";
                    StringBuilder betCode = new StringBuilder();
                    int lineNum = 0;

                    while (sr.Peek() > 0)
                    {
                        string strLine = sr.ReadLine().Trim();
                        //listBoxTxtContent.Items.Add(strLine);
                        testCode = strLine;
                        if ("".Equals(testCode.Trim()))
                        {
                            continue;//如果这一行没有数据，则跳过这次循环
                        }
                        if (this.LicenseId == LicenseContants.License.GAMEIDPLS || this.LicenseId == LicenseContants.License.GAMEIDF3D)
                        {
                            testCode = this.uploadController.parsePls(this.LicenseId, this.PlayId, testCode);
                            betCode.Append(testCode);
                            if (sr.Peek() > 0)
                            {
                                betCode.Append(";");
                            }
                        }
                        else if (this.LicenseId > 100)
                        {
                            //testCode = this.uploadService.parseS11x5(licenseId, playId, testCode);
                        }
                        else if (this.LicenseId == LicenseContants.License.GAMEIDJCZQ || this.LicenseId == LicenseContants.License.GAMEIDJCLQ ||
                                this.LicenseId == LicenseContants.License.GAMEIDBJDC || this.LicenseId == LicenseContants.License.GAMEIDSFGG)
                        {
                            testCode = this.uploadController.parseGuessLine(this.LicenseId, this.PlayId, testCode, this.SchNum, this.PassNum, false);
                            betCode.Append(testCode);
                            if (sr.Peek() > 0)
                            {
                                betCode.Append("|");
                            }
                        }
                        else
                        {
                            testCode = this.uploadController.parse(this.LicenseId, testCode);
                            betCode.Append(testCode);
                            if (sr.Peek() > 0)
                            {
                                betCode.Append(";");
                            }
                        }

                        if (null != testCode)
                        {
                            lineNum++;
                        }
                        else
                        {
                            this.label5.Text = "第" + (lineNum + 1) + "行格式有误";
                            return false;
                        }
                    }

                    this.BetCode = betCode.ToString();
                    this.price = lineNum * 2 * this.Multiple;
                    this.BetNum = lineNum;
                    this.lbBetPrice.Text = string.Format("您选择了：共{0}注、金额{1}元", this.BetNum, this.price);
                    this.label5.Text = string.Format("共{0}注", lineNum);
                }

                this.btnMultipleMinus.Enabled = true;
                this.btnMultiplePlus.Enabled = true;
                this.btnSubmit.Enabled = true;

                return true;
            }
            catch (Exception e)
            {
                LogUtil.getInstance().addLogDataToQueue(e.StackTrace, GlobalConstants.LOGTYPE_ENUM.EXCEOTION);
                return false;
            }
        }

        private void picPai3_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("排列三"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "排列三";
                // 
                //this.LicenseId = LicenseContants.License.GAMEIDPLS;
                //this.PlayId = 0;
                //this.PassNum = 0;
                //this.SchNum = 0;

                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plPLS.BringToFront();
                this.plPLS.Visible = true;
                this.plsMark.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }
            }
        }

        private Control curControl = null;

        private void picPai5_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("排列五"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "排列五";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDPLW;
                this.PlayId = PL5PlayType.ZHX_DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.plwMark.Visible = true;
                this.curControl = this.plwMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picQXC_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("七星彩"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "七星彩";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDQXC;
                this.PlayId = QXCPlayType.ZHX_DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.qxcMark.Visible = true;
                this.curControl = this.qxcMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picDLT_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("大乐透"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "大乐透";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDDLT;
                this.PlayId = DLTPlayType.DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.dltMark.Visible = true;
                this.curControl = this.dltMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picFC3D_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("福彩3D"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "福彩3D";
                // 
                //this.LicenseId = LicenseContants.License.GAMEIDF3D;
                //this.PlayId = 0;
                //this.PassNum = 0;
                //this.SchNum = 0;

                plsPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plFC3D.BringToFront();
                this.plFC3D.Visible = true;
                this.fc3dMark.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }
            }
        }

        private void picSSQ_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("双色球"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "双色球";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDSSQ;
                this.PlayId = SSQPlayType.DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.ssqMark.Visible = true;
                this.curControl = this.ssqMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picQLC_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("七乐彩"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "七乐彩";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDQLC;
                this.PlayId = 1;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.qlcMark.Visible = true;
                this.curControl = this.qlcMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void jczqPanelShow(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("竞彩足球"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "竞彩足球";
                // 
                //this.LicenseId = LicenseContants.License.GAMEIDJCZQ;
                //this.PlayId = 0;
                //this.PassNum = 0;
                //this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jclqPanelHide();

                this.plJCZQ.BringToFront();
                this.plJCZQ.Visible = true;
                this.jczqMark.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }
            }
        }

        private void jclqPanelShow(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("竞彩篮球"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "竞彩篮球";
                // 
                //this.LicenseId = LicenseContants.License.GAMEIDJCLQ;
                //this.PlayId = 0;
                //this.PassNum = 0;
                //this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();

                this.plJCLQ.BringToFront();
                this.plJCLQ.Visible = true;
                this.jclqMark.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }
            }
        }

        private void plsPanelShow(object sender, EventArgs e)
        {
            jczqPanelHide();

            this.plPLS.BringToFront();
            this.plPLS.Visible = true;
            this.plsMark.Visible = true;
        }

        private void plsPanelHide()
        {
            this.plPLS.Visible = false;
            this.plsMark.Visible = false;
        }
        
        private void plfc3dPanelHide()
        {
            this.plFC3D.Visible = false;
            this.fc3dMark.Visible = false;
        }

        private void jczqPanelHide()
        {
            this.plJCZQ.Visible = false;
            this.jczqMark.Visible = false;
        }

        private void jclqPanelHide()
        {
            this.plJCLQ.Visible = false;
            this.jclqMark.Visible = false;
        }

        private void lbJczqSPF_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "胜平负/让球";

            LicenseId = LicenseContants.License.GAMEIDJCZQ;
            PlayId = JCZQPlayType.SPF;
            PassNum = 0;
            SchNum = 0;

            FrmJcSingleLoad jsu = new FrmJcSingleLoad(this);
            jsu.ShowDialog();

            this.btnUpload.Enabled = true;
        }

        private void lbJczqBF_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "比分";

            LicenseId = LicenseContants.License.GAMEIDJCZQ;
            PlayId = JCZQPlayType.BF;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbJczqZJQ_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "总进球数";

            LicenseId = LicenseContants.License.GAMEIDJCZQ;
            PlayId = JCZQPlayType.ZJQ;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbJczqBQC_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "半全场";

            LicenseId = LicenseContants.License.GAMEIDJCZQ;
            PlayId = JCZQPlayType.BQC;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbJczqSXDS_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "让球胜平负";

            LicenseId = LicenseContants.License.GAMEIDJCZQ;
            PlayId = JCZQPlayType.RQSPF;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbJclqSF_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "胜负";

            LicenseId = LicenseContants.License.GAMEIDJCLQ;
            PlayId = JCLQPlayType.SF;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbJclqDXF_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "大小分";

            LicenseId = LicenseContants.License.GAMEIDJCLQ;
            PlayId = JCLQPlayType.DXF;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbJclqSFC_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "胜分差";

            LicenseId = LicenseContants.License.GAMEIDJCLQ;
            PlayId = JCLQPlayType.SFC;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void ShowTXTFormalStandard(object sender, EventArgs e)
        {
            FrmTxtShow frmTxtShow = new FrmTxtShow();
            string strTxt = "";

            switch (LicenseId)
            {
                case LicenseContants.License.GAMEIDPLS:
                    strTxt = @"<排列三>单式上传标准格式
393
394
474
483
484
393
394
474";
                    break;
                case LicenseContants.License.GAMEIDPLW:
                    strTxt = @"<排列五>单式上传标准格式
39312
39423
47434
48345
48423
39334
39445
47456";
                    break;
                case LicenseContants.License.GAMEIDQXC:
                    strTxt = @"<七星彩>单式上传标准格式
2,8,4,6,5,3,4
2,4,3,6,6,1,9
2,4,3,3,6,8,4
2,2,3,6,2,6,9
2,8,4,6,5,3,4
2,4,3,6,6,1,9
2,4,3,3,6,8,4
2,2,3,6,2,6,9";
                    break;
                case LicenseContants.License.GAMEIDDLT:
                    strTxt = @"<大乐透>单式上传标准格式
06,11,21,25,28|01,02
01,13,22,24,28|03,08
04,16,24,26,28|01,04
08,18,23,24,28|02,09
05,12,24,25,27|01,05
03,18,22,25,26|02,09
05,19,23,25,28|05,06
09,14,22,25,29|06,07";
                    break;
                case LicenseContants.License.GAMEIDF3D:
                    strTxt = @"<福彩3D>单式上传标准格式
393 
394
474
483
484
393
394
474";
                    break;
                case LicenseContants.License.GAMEIDSSQ:
                    strTxt = @"<双色球>单式上传标准格式
06,11,21,25,28,30|02
01,13,22,24,28,30|08
04,16,24,26,28,30|04
08,18,23,24,28,30|09
05,12,24,25,27,30|05
03,18,22,25,26,30|09
05,19,23,25,28,30|06
09,14,22,25,29,30|07";
                    break;
                case LicenseContants.License.GAMEIDQLC:
                    strTxt = @"<七乐彩>单式上传标准格式
06,09,11,21,25,28,30
01,09,13,22,24,28,30
04,09,16,24,26,28,30
08,09,18,23,24,28,30
05,09,12,24,25,27,30
03,09,18,22,25,26,30
05,09,19,23,25,28,30
09,10,14,22,25,29,30";
                    break;
                case LicenseContants.License.GAMEIDSFC:
                    strTxt = @"<胜负彩>单式标准格式

1、请以.txt文件上传。

2、每行一注。

3、号码格式：

1,3,0,1,1,3,3,1,3,0,1,1,3,3

13011331301133";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                    break;
                case LicenseContants.License.GAMEIDRXJ:
                    strTxt = @"<任选九>单式标准格式

1、请以.txt文件上传。

2、每行一注。

3、号码格式：

##03#0#331#130

**03*0*331*130";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                    break;
                case LicenseContants.License.GAMEIDBQC:
                    strTxt = @"<六场半全场>单式标准格式

1、请以.txt文件上传。

2、每行一注。

3、号码格式：

1,3,0,1,1,3,3,1,3,0,1,1

1*3*0*1*1*3*3*1*3*0*1*1

1#3#0#1#1#3#3#1#3#0#1#1

130113313011";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                    break;
                case LicenseContants.License.GAMEIDJQC:
                    strTxt = @"<进球彩>单式标准格式

1、请以.txt文件上传。

2、每行一注。

3、号码格式：

1,3,2,0,1,3,2,0

13011331";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                    break;
                case LicenseContants.License.GAMEIDJCZQ:
                    switch (PlayId)
                    {
                        case JCZQPlayType.RQSPF:
                            strTxt = @"<竞彩足球-胜平负>单式上传标准格式
标准格式要求：
1、只接受后缀名为“.txt”的记事本文本文档。

2、一行一注。

3、逗号、空格、横杠均为半角格式下。

4、支持逗号、空格、横杠进行分隔，不投的场次用*或者#占位（仅支持*和#）。

标准格式如下：
（3代表胜，1代表平，0代表负） 
不带场次 
3,3,1,0,3,1 
3-3-1-0-3-1 
331303 
3 3 1 0 3 1";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCZQPlayType.SPF:
                            strTxt = @"<竞彩足球-让球胜平负>单式上传标准格式
标准格式要求：
1、只接受后缀名为“.txt”的记事本文本文档。

2、一行一注。

3、逗号、空格、横杠均为半角格式下。

4、支持逗号、空格、横杠进行分隔，不投的场次用*或者#占位（仅支持*和#）。

标准格式如下：
（3代表胜，1代表平，0代表负） 
不带场次 
3,3,1,0,3,1 
3-3-1-0-3-1 
331303 
3 3 1 0 3 1";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCZQPlayType.BF:
                            strTxt = @"<竞彩足球-比分>单式上传标准格式
一、标准格式要求
1、只接受后缀名为“.txt”的记事本文本文档。 
2、一行一注。 
3、逗号、空格、横杠均为半角格式下。

二、标准格式
（31代表3：1，3A代表胜其他，1A代表平其他，0A代表负其他）
不带场次 
11,31,30 
11-31-30 
113130 
11 31 30";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCZQPlayType.ZJQ:
                            strTxt = @"<竞彩足球-总进球>单式上传标准格式
一、标准格式要求
1、只接受后缀名为“.txt”的记事本文本文档。 
2、一行一注。 
3、逗号、空格、横杠均为半角格式下。

二、标准格式如下
（0-7代表进球数0-7个） 

不带场次 
0,1,2,4,5,6 
0-1-2-4-5-6 
012456 
0 1 2 4 5 6 ";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCZQPlayType.BQC:
                            strTxt = @"<竞彩足球-半全场>单式上传标准格式
一、标准格式要求
1、只接受后缀名为“.txt”的记事本文本文档。 
2、一行一注。 
3、逗号、空格、横杠均为半角格式下。

二、标准格式如下
（0-7代表进球数0-7个） 

不带场次 
0,1,2,4,5,6 
0-1-2-4-5-6 
012456 
0 1 2 4 5 6 ";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                    }
                    break;
                case LicenseContants.License.GAMEIDJCLQ:
                    switch (PlayId)
                    {
                        case JCLQPlayType.SF:
                            strTxt = @"<竞彩篮球-胜负>单式上传标准格式
1、只接受后缀名为“.txt”的记事本文本文档。

2、一行一注。

3、逗号、空格、横杠均为半角格式下。

标准格式如下：
（3代表主胜，0代表客胜） 
不带场次 
3,3,0,0,3,0 
3-3-0-0-3-0 
330303 
3 3 0 0 3 0";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCLQPlayType.RFSF:
                            strTxt = @"<竞彩篮球-让分胜负>单式上传标准格式
1、只接受后缀名为“.txt”的记事本文本文档。

2、一行一注。

3、逗号、空格、横杠均为半角格式下。

标准格式如下：
（3代表主胜，0代表客胜） 
不带场次 
3,3,0,0,3,0 
3-3-0-0-3-0 
330303 
3 3 0 0 3 0";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCLQPlayType.DXF:
                            strTxt = @"<竞彩篮球-大小分>单式上传标准格式
1、只接受后缀名为“.txt”的记事本文本文档。

2、一行一注。

3、逗号、空格、横杠均为半角格式下。

标准格式如下：
（0代表大分，1代表小分） 
不带场次 
1,1,0,0,1,0 
1-1-0-0-1-0 
110101 
1 1 0 0 1 0";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                        case JCLQPlayType.SFC:
                            strTxt = @"<竞彩篮球-胜分差>单式上传标准格式
一、标准格式要求
1、只接受后缀名为“.txt”的记事本文本文档。 
2、一行一注。 
3、逗号、空格、横杠均为半角格式下。

二、标准格式
（01代表客胜1-5，02代表客胜6-10，03代表客胜11-15，04代表客胜16-20， 05代表客胜21-25，06代表客胜26+，11代表主胜1-5，12代表主胜6-10，13代表主胜11-15，14代表主胜16-20， 15代表主胜21-25，16代表主胜26+）
不带场次 
11,01,12 
11-01-12 
110112 
11 01 12";
            frmTxtShow.Txt.TextAlign = HorizontalAlignment.Left;
                            break;
                    }
                    break;
            }

            frmTxtShow.Txt.Text = strTxt;
            frmTxtShow.ShowDialog();
        }

        private void lbZHX_DS_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "直选单式";
            
            LicenseId = LicenseContants.License.GAMEIDPLS;
            PlayId = PL3PlayType.PLSZHXDS;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbZ3_DS_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "组三单式";
            
            LicenseId = LicenseContants.License.GAMEIDPLS;
            PassNum = 0;
            SchNum = 0;
            PlayId = PL3PlayType.PLSZLDS;//组三单式和组六单式一样
            this.btnUpload.Enabled = true;
        }

        private void lbZ6_DS_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "组六单式";
            
            LicenseId = LicenseContants.License.GAMEIDPLS;
            PassNum = 0;
            SchNum = 0;
            PlayId = PL3PlayType.PLSZLDS;
            this.btnUpload.Enabled = true;
        }

        private void lbRFSF_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "让分胜负";

            LicenseId = LicenseContants.License.GAMEIDJCLQ;
            PlayId = JCLQPlayType.RFSF;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void picSFC_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("胜负彩"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "胜负彩";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDSFC;
                this.PlayId = SFCPlayType.DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.sfcMark.Visible = true;
                this.curControl = this.sfcMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picRXJ_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("任选九"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "任选九";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDRXJ;
                this.PlayId = R9PlayType.DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.rxjMark.Visible = true;
                this.curControl = this.rxjMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picBQC_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("半全场"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "半全场";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDBQC;
                this.PlayId = BQCPlayType.DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.bqcMark.Visible = true;
                this.curControl = this.bqcMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void picJQC_Click(object sender, EventArgs e)
        {
            if (!this.lbNoteboardLicense.Text.Contains("四场进球"))
            {
                // 1. 上传按钮
                this.btnUpload.Enabled = false;
                // 2. 彩种、玩法label
                this.lbNoteboardLicense.Text = "";
                this.lbNoteboardPlay.Text = "";
                this.lbNoteboardLicense.Text = "四场进球";
                // 
                this.LicenseId = LicenseContants.License.GAMEIDJQC;
                this.PlayId = JQCPlayType.DS;
                this.PassNum = 0;
                this.SchNum = 0;

                plsPanelHide();
                plfc3dPanelHide();
                jczqPanelHide();
                jclqPanelHide();

                this.plOtherLicense.BringToFront();
                this.plOtherLicense.Visible = true;

                if (this.curControl != null)
                {
                    this.curControl.Visible = false;
                }

                this.jqcMark.Visible = true;
                this.curControl = this.jqcMark;

                this.btnUpload.Enabled = true;
            }
        }

        private void btnMultipleMinus_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtMultiple.Text) > 1)
            {
                this.Multiple = int.Parse(txtMultiple.Text) - 1;
                this.txtMultiple.Text = this.Multiple.ToString();
            }
        }

        private void btnMultiplePlus_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtMultiple.Text) < 10000)
            {
                this.Multiple = int.Parse(txtMultiple.Text) + 1;
                this.txtMultiple.Text = this.Multiple.ToString();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string response = HTTPHelper.HttpHandler("", string.Format("http://m.cp020.com/h5servTime.json?storeId={0}&licenseId={1}", Global.STORE_ID, this.LicenseId == 6 ? 5 : this.LicenseId)).Replace("callbak(", "").Replace(");", "").Trim();

                SingleUploadTemp singleUploadTemp = (SingleUploadTemp)JSonHelper.JsonToSingleUploadTemp<SingleUploadTemp>(response);

                lottery_order lotteryOrder = new lottery_order();
                lotteryOrder.bet_state = GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT.ToString();
                lotteryOrder.bet_code = BetCode;
                lotteryOrder.bet_num = this.BetNum;
                lotteryOrder.bet_price = (this.BetNum * this.Multiple * 2).ToString();
                lotteryOrder.total_money = long.Parse(lotteryOrder.bet_price);
                lotteryOrder.license_id = LicenseId;
                lotteryOrder.play_type = PlayId.ToString();
                lotteryOrder.issue = singleUploadTemp.issue;
                lotteryOrder.multiple = txtMultiple.Text.ToString();
                lotteryOrder.stop_time = singleUploadTemp.stoptime;
                lotteryOrder.userid = 0;
                lotteryOrder.username = this.FileName;
                lotteryOrder.storeid = long.Parse(Global.STORE_ID.ToString());

                //根据betCode和data对象生成订单和票
                lotteryOrder.id = this.uploadController.GetSingleOrderId();
                this.uploadController.CreateSingleOrder(lotteryOrder);
                this.uploadController.CreateSingleTickets(lotteryOrder);
                MsgBox.getInstance().Show("上传成功");
                this.label5.Text = "注：上述格式符必须与您将要上传的文本中的格式符一致。";

                this.btnMultipleMinus.Enabled = false;
                this.btnMultiplePlus.Enabled = false;
                this.btnSubmit.Enabled = false;
            }
            catch (Exception e2)
            {
                LogUtil.getInstance().addLogDataToQueue(e2.StackTrace, GlobalConstants.LOGTYPE_ENUM.EXCEOTION);
                MsgBox.getInstance().Show("上传失败，有可能是服务器发生故障，请联系运营商。");
            }
            //finally
            //{
            //    this.btnMultipleMinus.Enabled = false;
            //    this.btnMultiplePlus.Enabled = false;
            //    this.btnSubmit.Enabled = false;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string response = HTTPHelper.HttpHandler("", "http://collection.cp020.com/global/schudle/9/schedules.js").Replace("var schedules = ", "");

                IList<GuessFootball> responsemsg = (List<GuessFootball>)JSonHelper.JsonToGuessFootballList<List<GuessFootball>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ZXDS3D_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "直选单式";

            LicenseId = LicenseContants.License.GAMEIDF3D;
            PlayId = 1;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void ZSDS3D_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "组三单式";

            LicenseId = LicenseContants.License.GAMEIDF3D;
            PlayId = 1;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private void lbZLDS_Click(object sender, EventArgs e)
        {
            this.lbNoteboardPlay.Text = "组六单式";

            LicenseId = LicenseContants.License.GAMEIDF3D;
            PlayId = 1;
            PassNum = 0;
            SchNum = 0;
            this.btnUpload.Enabled = true;
        }

        private Label selectedLb = new Label();

        private void common_Click(object sender, EventArgs e)
        {
            //this.selectedLb.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.selectedLb.ForeColor = System.Drawing.Color.White;

            this.selectedLb = (Label)sender;
        }

        private void labelEnter(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            //lb.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lb.ForeColor = System.Drawing.Color.Red;
        }

        private void labelLeave(object sender, EventArgs e)
        {
            if (!sender.Equals(this.selectedLb))
            {
                Label lb = sender as Label;
                //lb.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lb.ForeColor = System.Drawing.Color.White;
            }
        }
    }
}
