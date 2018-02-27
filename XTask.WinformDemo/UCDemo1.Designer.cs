namespace XTask.WinformDemo
{
    partial class UCDemo1
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
            this.btnStartOne = new System.Windows.Forms.Button();
            this.lsb = new System.Windows.Forms.ListBox();
            this.btnStartTwo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartOne
            // 
            this.btnStartOne.Location = new System.Drawing.Point(40, 200);
            this.btnStartOne.Name = "btnStartOne";
            this.btnStartOne.Size = new System.Drawing.Size(75, 23);
            this.btnStartOne.TabIndex = 4;
            this.btnStartOne.Text = "启动任务1";
            this.btnStartOne.UseVisualStyleBackColor = true;
            // 
            // lsb
            // 
            this.lsb.FormattingEnabled = true;
            this.lsb.ItemHeight = 12;
            this.lsb.Location = new System.Drawing.Point(22, 16);
            this.lsb.Name = "lsb";
            this.lsb.Size = new System.Drawing.Size(279, 160);
            this.lsb.TabIndex = 7;
            // 
            // btnStartTwo
            // 
            this.btnStartTwo.Location = new System.Drawing.Point(143, 200);
            this.btnStartTwo.Name = "btnStartTwo";
            this.btnStartTwo.Size = new System.Drawing.Size(148, 23);
            this.btnStartTwo.TabIndex = 5;
            this.btnStartTwo.Text = "启动任务2(带返回参数)";
            this.btnStartTwo.UseVisualStyleBackColor = true;
            // 
            // UCDemo1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsb);
            this.Controls.Add(this.btnStartTwo);
            this.Controls.Add(this.btnStartOne);
            this.Name = "UCDemo1";
            this.Size = new System.Drawing.Size(323, 245);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartOne;
        private System.Windows.Forms.ListBox lsb;
        private System.Windows.Forms.Button btnStartTwo;

    }
}
