
using System;

namespace ProgrammeFrame.AidedForm
{
    partial class NetRecoveryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrRecover = new System.Windows.Forms.Timer(this.components);
            this.tmrUnload = new System.Windows.Forms.Timer(this.components);
            this.pnlbg = new System.Windows.Forms.Panel();
            this.tmr_Now_Recover = new System.Windows.Forms.Timer(this.components);
            this.Layoutbg = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_SystemTime = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.tlpLogon = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlStationinfo = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.lbl_Infor = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_RecoverShutDown = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_StationName = new System.Windows.Forms.Label();
            this.lbl_SystemName = new System.Windows.Forms.Label();
            this.pnlbg.SuspendLayout();
            this.Layoutbg.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.tlpLogon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnlStationinfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlbg
            // 
            this.pnlbg.Controls.Add(this.Layoutbg);
            this.pnlbg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlbg.Location = new System.Drawing.Point(0, 0);
            this.pnlbg.Name = "pnlbg";
            this.pnlbg.Size = new System.Drawing.Size(1916, 1076);
            this.pnlbg.TabIndex = 79;
            // 
            // Layoutbg
            // 
            this.Layoutbg.BackgroundImage = global::ProgrammeFrame.Properties.Resources.bg0;
            this.Layoutbg.ColumnCount = 1;
            this.Layoutbg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Layoutbg.Controls.Add(this.panel2, 0, 6);
            this.Layoutbg.Controls.Add(this.pictureBox6, 0, 5);
            this.Layoutbg.Controls.Add(this.pictureBox10, 0, 4);
            this.Layoutbg.Controls.Add(this.pictureBox12, 0, 3);
            this.Layoutbg.Controls.Add(this.panel3, 0, 1);
            this.Layoutbg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layoutbg.Location = new System.Drawing.Point(0, 0);
            this.Layoutbg.Name = "Layoutbg";
            this.Layoutbg.RowCount = 7;
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.Layoutbg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.Layoutbg.Size = new System.Drawing.Size(1916, 1076);
            this.Layoutbg.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.panel2.BackgroundImage = global::ProgrammeFrame.Properties.Resources.bg1;
            this.panel2.Controls.Add(this.lbl_SystemTime);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 1038);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1916, 38);
            this.panel2.TabIndex = 0;
            // 
            // lbl_SystemTime
            // 
            this.lbl_SystemTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SystemTime.AutoSize = true;
            this.lbl_SystemTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SystemTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_SystemTime.ForeColor = System.Drawing.Color.White;
            this.lbl_SystemTime.Location = new System.Drawing.Point(1734, 8);
            this.lbl_SystemTime.Name = "lbl_SystemTime";
            this.lbl_SystemTime.Size = new System.Drawing.Size(179, 22);
            this.lbl_SystemTime.TabIndex = 1;
            this.lbl_SystemTime.Text = "2017-12-01 12:35:42";
            this.lbl_SystemTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.BackgroundImage = global::ProgrammeFrame.Properties.Resources.FISCAN_SYSTEMS;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox5.Location = new System.Drawing.Point(7, 7);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(196, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox6.Location = new System.Drawing.Point(0, 1032);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(1916, 6);
            this.pictureBox6.TabIndex = 1;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.Color.Black;
            this.pictureBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox10.Location = new System.Drawing.Point(0, 1030);
            this.pictureBox10.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(1916, 2);
            this.pictureBox10.TabIndex = 1;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackColor = System.Drawing.Color.White;
            this.pictureBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox12.Location = new System.Drawing.Point(0, 1029);
            this.pictureBox12.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(1916, 1);
            this.pictureBox12.TabIndex = 1;
            this.pictureBox12.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.pnlLogin);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 154);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1916, 618);
            this.panel3.TabIndex = 3;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Controls.Add(this.tlpLogon);
            this.pnlLogin.Location = new System.Drawing.Point(558, 109);
            this.pnlLogin.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Padding = new System.Windows.Forms.Padding(1);
            this.pnlLogin.Size = new System.Drawing.Size(800, 400);
            this.pnlLogin.TabIndex = 3;
            // 
            // tlpLogon
            // 
            this.tlpLogon.ColumnCount = 3;
            this.tlpLogon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpLogon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpLogon.Controls.Add(this.pictureBox13, 0, 1);
            this.tlpLogon.Controls.Add(this.pictureBox14, 0, 2);
            this.tlpLogon.Controls.Add(this.pictureBox15, 0, 3);
            this.tlpLogon.Controls.Add(this.panel4, 1, 5);
            this.tlpLogon.Controls.Add(this.panel1, 0, 0);
            this.tlpLogon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLogon.Location = new System.Drawing.Point(1, 1);
            this.tlpLogon.Name = "tlpLogon";
            this.tlpLogon.RowCount = 7;
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpLogon.Size = new System.Drawing.Size(796, 396);
            this.tlpLogon.TabIndex = 0;
            // 
            // pictureBox13
            // 
            this.pictureBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(162)))), ((int)(((byte)(0)))));
            this.tlpLogon.SetColumnSpan(this.pictureBox13, 3);
            this.pictureBox13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox13.Location = new System.Drawing.Point(0, 36);
            this.pictureBox13.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(796, 2);
            this.pictureBox13.TabIndex = 1;
            this.pictureBox13.TabStop = false;
            // 
            // pictureBox14
            // 
            this.pictureBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.tlpLogon.SetColumnSpan(this.pictureBox14, 3);
            this.pictureBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox14.Location = new System.Drawing.Point(0, 38);
            this.pictureBox14.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(796, 2);
            this.pictureBox14.TabIndex = 1;
            this.pictureBox14.TabStop = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackColor = System.Drawing.Color.White;
            this.tlpLogon.SetColumnSpan(this.pictureBox15, 3);
            this.pictureBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox15.Location = new System.Drawing.Point(0, 40);
            this.pictureBox15.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(796, 1);
            this.pictureBox15.TabIndex = 1;
            this.pictureBox15.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.pnlStationinfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(120, 91);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(1);
            this.panel4.Size = new System.Drawing.Size(556, 225);
            this.panel4.TabIndex = 3;
            // 
            // pnlStationinfo
            // 
            this.pnlStationinfo.BackgroundImage = global::ProgrammeFrame.Properties.Resources.bg3;
            this.pnlStationinfo.ColumnCount = 4;
            this.pnlStationinfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlStationinfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.pnlStationinfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlStationinfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlStationinfo.Controls.Add(this.pictureBox18, 1, 0);
            this.pnlStationinfo.Controls.Add(this.lbl_Infor, 2, 0);
            this.pnlStationinfo.Controls.Add(this.panel5, 0, 1);
            this.pnlStationinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStationinfo.Location = new System.Drawing.Point(1, 1);
            this.pnlStationinfo.Name = "pnlStationinfo";
            this.pnlStationinfo.RowCount = 2;
            this.pnlStationinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlStationinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.pnlStationinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlStationinfo.Size = new System.Drawing.Size(552, 221);
            this.pnlStationinfo.TabIndex = 3;
            // 
            // pictureBox18
            // 
            this.pictureBox18.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox18.Image = global::ProgrammeFrame.Properties.Resources.loading;
            this.pictureBox18.Location = new System.Drawing.Point(20, 0);
            this.pictureBox18.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(72, 161);
            this.pictureBox18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox18.TabIndex = 0;
            this.pictureBox18.TabStop = false;
            // 
            // lbl_Infor
            // 
            this.lbl_Infor.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Infor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Infor.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Infor.ForeColor = System.Drawing.Color.White;
            this.lbl_Infor.Location = new System.Drawing.Point(95, 0);
            this.lbl_Infor.Name = "lbl_Infor";
            this.lbl_Infor.Size = new System.Drawing.Size(434, 161);
            this.lbl_Infor.TabIndex = 0;
            this.lbl_Infor.Text = "服务器或网络可能出现故障，站点正在尝试自动恢复，请速报告管理员！（等待......）";
            this.lbl_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel5.BackgroundImage = global::ProgrammeFrame.Properties.Resources.bg2;
            this.pnlStationinfo.SetColumnSpan(this.panel5, 4);
            this.panel5.Controls.Add(this.btn_RecoverShutDown);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 161);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(552, 60);
            this.panel5.TabIndex = 5;
            // 
            // btn_RecoverShutDown
            // 
            this.btn_RecoverShutDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_RecoverShutDown.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_RecoverShutDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RecoverShutDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_RecoverShutDown.ForeColor = System.Drawing.Color.White;
            this.btn_RecoverShutDown.Image = global::ProgrammeFrame.Properties.Resources.btn_cancel;
            this.btn_RecoverShutDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RecoverShutDown.Location = new System.Drawing.Point(214, 6);
            this.btn_RecoverShutDown.Name = "btn_RecoverShutDown";
            this.btn_RecoverShutDown.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btn_RecoverShutDown.Size = new System.Drawing.Size(125, 48);
            this.btn_RecoverShutDown.TabIndex = 2;
            this.btn_RecoverShutDown.Text = "关闭系统";
            this.btn_RecoverShutDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RecoverShutDown.UseVisualStyleBackColor = false;
            this.btn_RecoverShutDown.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::ProgrammeFrame.Properties.Resources.bg1;
            this.tlpLogon.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.lbl_StationName);
            this.panel1.Controls.Add(this.lbl_SystemName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 36);
            this.panel1.TabIndex = 0;
            // 
            // lbl_StationName
            // 
            this.lbl_StationName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_StationName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_StationName.ForeColor = System.Drawing.Color.White;
            this.lbl_StationName.Location = new System.Drawing.Point(180, 0);
            this.lbl_StationName.Name = "lbl_StationName";
            this.lbl_StationName.Size = new System.Drawing.Size(496, 36);
            this.lbl_StationName.TabIndex = 1;
            this.lbl_StationName.Text = "大屏工作站";
            this.lbl_StationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SystemName
            // 
            this.lbl_SystemName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SystemName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_SystemName.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbl_SystemName.ForeColor = System.Drawing.Color.White;
            this.lbl_SystemName.Location = new System.Drawing.Point(0, 0);
            this.lbl_SystemName.Name = "lbl_SystemName";
            this.lbl_SystemName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_SystemName.Size = new System.Drawing.Size(178, 36);
            this.lbl_SystemName.TabIndex = 0;
            this.lbl_SystemName.Text = "工作站";
            this.lbl_SystemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NetRecoveryForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1916, 1076);
            this.ControlBox = false;
            this.Controls.Add(this.pnlbg);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NetRecoveryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注意";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlbg.ResumeLayout(false);
            this.Layoutbg.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.tlpLogon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.panel4.ResumeLayout(false);
            this.pnlStationinfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Infor;
        private System.Windows.Forms.Timer tmrRecover;
        private System.Windows.Forms.Timer tmrUnload;
        private System.Windows.Forms.Button btn_RecoverShutDown;
        private System.Windows.Forms.Panel pnlbg;
        private System.Windows.Forms.TableLayoutPanel Layoutbg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_SystemTime;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TableLayoutPanel tlpLogon;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel pnlStationinfo;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_StationName;
        private System.Windows.Forms.Label lbl_SystemName;
        private System.Windows.Forms.Timer tmr_Now_Recover;
    }
}