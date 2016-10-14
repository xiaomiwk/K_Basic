using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public partial class TextBoxK : UserControl
    {
        public override string Text { get { return this.in输入.Text; } set { this.in输入.Text = value; } }

        public string 水印 { get { return this.out水印.Text; } set { this.out水印.Text = value; } }

        public Color 水印颜色 { get { return this.out水印.ForeColor; } set { this.out水印.ForeColor = value; } }

        public TextBoxK()
        {
            InitializeComponent();
            水印颜色 = Color.Gray;
            this.out水印.Enter += Out水印_Enter;
            this.in输入.Enter += Out输入_Enter;
            this.out水印.Leave += Out水印_Leave;
            this.in输入.Leave += Out水印_Leave;
            this.in输入.MultilineChanged += In输入_MultilineChanged;
        }

        private void In输入_MultilineChanged(object sender, EventArgs e)
        {
            this.out水印.Multiline = this.in输入.Multiline;
        }

        private void Out水印_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.in输入.Text))
            {
                this.out水印.Visible = true;
            }
        }

        private void Out水印_Enter(object sender, EventArgs e)
        {
            this.out水印.Visible = false;
            this.in输入.Focus();
        }

        private void Out输入_Enter(object sender, EventArgs e)
        {
            this.out水印.Visible = false;
        }
    }
}
