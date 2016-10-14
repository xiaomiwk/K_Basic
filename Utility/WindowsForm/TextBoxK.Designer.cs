namespace Utility.WindowsForm
{
    partial class TextBoxK
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.in输入 = new System.Windows.Forms.TextBox();
            this.out水印 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // in输入
            // 
            this.in输入.Dock = System.Windows.Forms.DockStyle.Fill;
            this.in输入.Location = new System.Drawing.Point(0, 0);
            this.in输入.Name = "in输入";
            this.in输入.Size = new System.Drawing.Size(100, 21);
            this.in输入.TabIndex = 0;
            // 
            // out水印
            // 
            this.out水印.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out水印.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.out水印.ForeColor = System.Drawing.Color.Silver;
            this.out水印.Location = new System.Drawing.Point(4, 3);
            this.out水印.Name = "out水印";
            this.out水印.Size = new System.Drawing.Size(90, 14);
            this.out水印.TabIndex = 1;
            // 
            // TextBoxK
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.out水印);
            this.Controls.Add(this.in输入);
            this.Name = "TextBoxK";
            this.Size = new System.Drawing.Size(100, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox in输入;
        private System.Windows.Forms.TextBox out水印;
    }
}
