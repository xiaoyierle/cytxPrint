﻿namespace Demo
{
    partial class UnitMyConfig
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitMyConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbPrintType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labsysconfig = new System.Windows.Forms.Label();
            this.cbDataKeepTime = new System.Windows.Forms.ComboBox();
            this.cbPrinterModel = new System.Windows.Forms.ComboBox();
            this.chIsAutoFeedback = new System.Windows.Forms.CheckBox();
            this.lbPrinterModelTitle = new System.Windows.Forms.Label();
            this.btnUpdateSysParam = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::Demo.Properties.Resources.Body;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.cbPrintType);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbLocation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.labsysconfig);
            this.panel1.Controls.Add(this.cbDataKeepTime);
            this.panel1.Controls.Add(this.cbPrinterModel);
            this.panel1.Controls.Add(this.chIsAutoFeedback);
            this.panel1.Controls.Add(this.lbPrinterModelTitle);
            this.panel1.Controls.Add(this.btnUpdateSysParam);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 183);
            this.panel1.TabIndex = 0;
            // 
            // cbPrintType
            // 
            this.cbPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrintType.FormattingEnabled = true;
            this.cbPrintType.Location = new System.Drawing.Point(71, 43);
            this.cbPrintType.Name = "cbPrintType";
            this.cbPrintType.Size = new System.Drawing.Size(143, 20);
            this.cbPrintType.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "出票方式:";
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(297, 43);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(70, 20);
            this.cbLocation.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(238, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "地区选择:";
            // 
            // labsysconfig
            // 
            this.labsysconfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.labsysconfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.labsysconfig.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.labsysconfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(112)))), ((int)(((byte)(189)))));
            this.labsysconfig.Location = new System.Drawing.Point(0, 0);
            this.labsysconfig.Name = "labsysconfig";
            this.labsysconfig.Size = new System.Drawing.Size(732, 29);
            this.labsysconfig.TabIndex = 0;
            this.labsysconfig.Text = "系统参数";
            this.labsysconfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDataKeepTime
            // 
            this.cbDataKeepTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataKeepTime.FormattingEnabled = true;
            this.cbDataKeepTime.Location = new System.Drawing.Point(616, 43);
            this.cbDataKeepTime.Name = "cbDataKeepTime";
            this.cbDataKeepTime.Size = new System.Drawing.Size(73, 20);
            this.cbDataKeepTime.TabIndex = 20;
            // 
            // cbPrinterModel
            // 
            this.cbPrinterModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrinterModel.FormattingEnabled = true;
            this.cbPrinterModel.Location = new System.Drawing.Point(446, 43);
            this.cbPrinterModel.Name = "cbPrinterModel";
            this.cbPrinterModel.Size = new System.Drawing.Size(71, 20);
            this.cbPrinterModel.TabIndex = 1;
            // 
            // chIsAutoFeedback
            // 
            this.chIsAutoFeedback.AutoSize = true;
            this.chIsAutoFeedback.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chIsAutoFeedback.Location = new System.Drawing.Point(15, 82);
            this.chIsAutoFeedback.Name = "chIsAutoFeedback";
            this.chIsAutoFeedback.Size = new System.Drawing.Size(171, 21);
            this.chIsAutoFeedback.TabIndex = 15;
            this.chIsAutoFeedback.Text = "自动向服务器回馈出票结果";
            this.chIsAutoFeedback.UseVisualStyleBackColor = true;
            // 
            // lbPrinterModelTitle
            // 
            this.lbPrinterModelTitle.AutoSize = true;
            this.lbPrinterModelTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPrinterModelTitle.Location = new System.Drawing.Point(387, 46);
            this.lbPrinterModelTitle.Name = "lbPrinterModelTitle";
            this.lbPrinterModelTitle.Size = new System.Drawing.Size(59, 17);
            this.lbPrinterModelTitle.TabIndex = 22;
            this.lbPrinterModelTitle.Text = "机型选择:";
            // 
            // btnUpdateSysParam
            // 
            this.btnUpdateSysParam.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateSysParam.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateSysParam.BackgroundImage")));
            this.btnUpdateSysParam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateSysParam.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpdateSysParam.FlatAppearance.BorderSize = 0;
            this.btnUpdateSysParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateSysParam.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdateSysParam.ForeColor = System.Drawing.Color.White;
            this.btnUpdateSysParam.Location = new System.Drawing.Point(621, 141);
            this.btnUpdateSysParam.Margin = new System.Windows.Forms.Padding(0);
            this.btnUpdateSysParam.Name = "btnUpdateSysParam";
            this.btnUpdateSysParam.Size = new System.Drawing.Size(83, 24);
            this.btnUpdateSysParam.TabIndex = 10;
            this.btnUpdateSysParam.UseVisualStyleBackColor = false;
            this.btnUpdateSysParam.Click += new System.EventHandler(this.btnOk_Click);
            this.btnUpdateSysParam.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnUpdateSysParam.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(532, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据保存时间:";
            // 
            // UnitMyConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "UnitMyConfig";
            this.Size = new System.Drawing.Size(732, 233);
            this.Load += new System.EventHandler(this.UnitMyConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labsysconfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpdateSysParam;
        private System.Windows.Forms.CheckBox chIsAutoFeedback;
        private System.Windows.Forms.ComboBox cbDataKeepTime;
        private System.Windows.Forms.ComboBox cbPrinterModel;
        private System.Windows.Forms.Label lbPrinterModelTitle;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPrintType;
        private System.Windows.Forms.Label label5;
    }
}
