namespace DXAppXingyun28
{
    partial class XtraFormUpdate
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormUpdate));
            this.button_update = new DevExpress.XtraEditors.SimpleButton();
            this.button_search = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl_search = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEdit_isPlayNumber = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit_autozidong = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit_zidingmoni = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit_autotongji = new DevExpress.XtraEditors.CheckEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.textEdit_autoReport = new DevExpress.XtraEditors.TextEdit();
            this.checkEdit_autoReport = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit_autoUpdate = new DevExpress.XtraEditors.CheckEdit();
            this.rb_pk10 = new System.Windows.Forms.RadioButton();
            this.rb_bjkl8 = new System.Windows.Forms.RadioButton();
            this.labelControl_updateDb = new DevExpress.XtraEditors.LabelControl();
            this.timerUpdateDb = new System.Windows.Forms.Timer(this.components);
            this.labelControl_UpdateTIme = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_isPlayNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autozidong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_zidingmoni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autotongji.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_autoReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autoReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autoUpdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(12, 206);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(185, 35);
            this.button_update.TabIndex = 0;
            this.button_update.Text = "更新";
            this.button_update.ToolTip = "更新数据到今天";
            this.button_update.Click += new System.EventHandler(this.Button_update_Click);
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(12, 247);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(185, 35);
            this.button_search.TabIndex = 1;
            this.button_search.Text = "查看";
            this.button_search.ToolTip = "查看数据库中有哪些日期的数据";
            this.button_search.Click += new System.EventHandler(this.Button_search_Click);
            // 
            // gridControl_search
            // 
            this.gridControl_search.Location = new System.Drawing.Point(229, 59);
            this.gridControl_search.MainView = this.gridView1;
            this.gridControl_search.Name = "gridControl_search";
            this.gridControl_search.Size = new System.Drawing.Size(148, 292);
            this.gridControl_search.TabIndex = 4;
            this.gridControl_search.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl_search;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkEdit_isPlayNumber);
            this.groupControl1.Controls.Add(this.checkEdit_autozidong);
            this.groupControl1.Controls.Add(this.checkEdit_zidingmoni);
            this.groupControl1.Controls.Add(this.checkEdit_autotongji);
            this.groupControl1.Controls.Add(this.button1);
            this.groupControl1.Controls.Add(this.checkEdit1);
            this.groupControl1.Controls.Add(this.textEdit_autoReport);
            this.groupControl1.Controls.Add(this.checkEdit_autoReport);
            this.groupControl1.Controls.Add(this.checkEdit_autoUpdate);
            this.groupControl1.Controls.Add(this.rb_pk10);
            this.groupControl1.Controls.Add(this.rb_bjkl8);
            this.groupControl1.Controls.Add(this.labelControl_updateDb);
            this.groupControl1.Controls.Add(this.button_update);
            this.groupControl1.Controls.Add(this.button_search);
            this.groupControl1.Location = new System.Drawing.Point(5, 57);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(218, 294);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "更新数据";
            // 
            // checkEdit_isPlayNumber
            // 
            this.checkEdit_isPlayNumber.Location = new System.Drawing.Point(12, 159);
            this.checkEdit_isPlayNumber.Name = "checkEdit_isPlayNumber";
            this.checkEdit_isPlayNumber.Properties.Caption = "语音报号";
            this.checkEdit_isPlayNumber.Size = new System.Drawing.Size(82, 19);
            this.checkEdit_isPlayNumber.TabIndex = 15;
            // 
            // checkEdit_autozidong
            // 
            this.checkEdit_autozidong.Location = new System.Drawing.Point(28, 90);
            this.checkEdit_autozidong.Name = "checkEdit_autozidong";
            this.checkEdit_autozidong.Properties.Caption = "更新后 自动点击 模拟自动投注";
            this.checkEdit_autozidong.Size = new System.Drawing.Size(192, 19);
            this.checkEdit_autozidong.TabIndex = 14;
            // 
            // checkEdit_zidingmoni
            // 
            this.checkEdit_zidingmoni.Location = new System.Drawing.Point(28, 113);
            this.checkEdit_zidingmoni.Name = "checkEdit_zidingmoni";
            this.checkEdit_zidingmoni.Properties.Caption = "更新后 自动点击 自定义算法";
            this.checkEdit_zidingmoni.Size = new System.Drawing.Size(192, 19);
            this.checkEdit_zidingmoni.TabIndex = 13;
            // 
            // checkEdit_autotongji
            // 
            this.checkEdit_autotongji.Location = new System.Drawing.Point(28, 69);
            this.checkEdit_autotongji.Name = "checkEdit_autotongji";
            this.checkEdit_autotongji.Properties.Caption = "更新后 自动点击 统计";
            this.checkEdit_autotongji.Size = new System.Drawing.Size(192, 19);
            this.checkEdit_autotongji.TabIndex = 12;
            this.checkEdit_autotongji.ToolTip = "更新后自动点击统计按钮";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(12, 184);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "总在最前";
            this.checkEdit1.Size = new System.Drawing.Size(75, 19);
            this.checkEdit1.TabIndex = 10;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // textEdit_autoReport
            // 
            this.textEdit_autoReport.EditValue = "本期开奖:";
            this.textEdit_autoReport.Location = new System.Drawing.Point(93, 133);
            this.textEdit_autoReport.Name = "textEdit_autoReport";
            this.textEdit_autoReport.Size = new System.Drawing.Size(104, 20);
            this.textEdit_autoReport.TabIndex = 9;
            this.textEdit_autoReport.ToolTip = "自动报号前缀";
            // 
            // checkEdit_autoReport
            // 
            this.checkEdit_autoReport.Location = new System.Drawing.Point(12, 134);
            this.checkEdit_autoReport.Name = "checkEdit_autoReport";
            this.checkEdit_autoReport.Properties.Caption = "QQ报号";
            this.checkEdit_autoReport.Size = new System.Drawing.Size(75, 19);
            this.checkEdit_autoReport.TabIndex = 8;
            // 
            // checkEdit_autoUpdate
            // 
            this.checkEdit_autoUpdate.Location = new System.Drawing.Point(12, 47);
            this.checkEdit_autoUpdate.Name = "checkEdit_autoUpdate";
            this.checkEdit_autoUpdate.Properties.Caption = "显示并自动更新";
            this.checkEdit_autoUpdate.Size = new System.Drawing.Size(113, 19);
            this.checkEdit_autoUpdate.TabIndex = 7;
            this.checkEdit_autoUpdate.CheckedChanged += new System.EventHandler(this.CheckEditAutoUpdate_CheckedChanged);
            // 
            // rb_pk10
            // 
            this.rb_pk10.AutoSize = true;
            this.rb_pk10.Location = new System.Drawing.Point(100, 28);
            this.rb_pk10.Name = "rb_pk10";
            this.rb_pk10.Size = new System.Drawing.Size(52, 18);
            this.rb_pk10.TabIndex = 6;
            this.rb_pk10.Tag = "pk10";
            this.rb_pk10.Text = "pk10";
            this.rb_pk10.UseVisualStyleBackColor = true;
            this.rb_pk10.Visible = false;
            // 
            // rb_bjkl8
            // 
            this.rb_bjkl8.AutoSize = true;
            this.rb_bjkl8.Checked = true;
            this.rb_bjkl8.Location = new System.Drawing.Point(14, 26);
            this.rb_bjkl8.Name = "rb_bjkl8";
            this.rb_bjkl8.Size = new System.Drawing.Size(80, 18);
            this.rb_bjkl8.TabIndex = 5;
            this.rb_bjkl8.TabStop = true;
            this.rb_bjkl8.Tag = "bjkl8";
            this.rb_bjkl8.Text = "北京快乐8";
            this.rb_bjkl8.UseVisualStyleBackColor = true;
            // 
            // labelControl_updateDb
            // 
            this.labelControl_updateDb.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl_updateDb.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl_updateDb.Location = new System.Drawing.Point(74, 2);
            this.labelControl_updateDb.Name = "labelControl_updateDb";
            this.labelControl_updateDb.Size = new System.Drawing.Size(121, 19);
            this.labelControl_updateDb.TabIndex = 4;
            // 
            // timerUpdateDb
            // 
            this.timerUpdateDb.Interval = 1000;
            this.timerUpdateDb.Tick += new System.EventHandler(this.TimerUpdateDb_Tick);
            // 
            // labelControl_UpdateTIme
            // 
            this.labelControl_UpdateTIme.AllowHtmlString = true;
            this.labelControl_UpdateTIme.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl_UpdateTIme.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl_UpdateTIme.Location = new System.Drawing.Point(5, 3);
            this.labelControl_UpdateTIme.Name = "labelControl_UpdateTIme";
            this.labelControl_UpdateTIme.Size = new System.Drawing.Size(372, 50);
            this.labelControl_UpdateTIme.TabIndex = 8;
            // 
            // XtraFormUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 357);
            this.Controls.Add(this.labelControl_UpdateTIme);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gridControl_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "XtraFormUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新数据库";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormUpdate_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_search)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_isPlayNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autozidong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_zidingmoni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autotongji.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_autoReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autoReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_autoUpdate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton button_update;
        private DevExpress.XtraEditors.SimpleButton button_search;
        private DevExpress.XtraGrid.GridControl gridControl_search;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl_updateDb;
        private System.Windows.Forms.RadioButton rb_pk10;
        private System.Windows.Forms.RadioButton rb_bjkl8;
        private System.Windows.Forms.Timer timerUpdateDb;
        private DevExpress.XtraEditors.CheckEdit checkEdit_autoUpdate;
        private DevExpress.XtraEditors.LabelControl labelControl_UpdateTIme;
        private DevExpress.XtraEditors.CheckEdit checkEdit_autoReport;
        private DevExpress.XtraEditors.TextEdit textEdit_autoReport;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.CheckEdit checkEdit_autozidong;
        private DevExpress.XtraEditors.CheckEdit checkEdit_zidingmoni;
        private DevExpress.XtraEditors.CheckEdit checkEdit_autotongji;
        private DevExpress.XtraEditors.CheckEdit checkEdit_isPlayNumber;
    }
}