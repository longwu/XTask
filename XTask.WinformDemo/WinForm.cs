using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace XTask.WinformDemo
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();

            this.tabPage1.Controls.Add(new UCDemo1());
            this.tabPage2.Controls.Add(new UCDemo2());
            this.tabPage3.Controls.Add(new UCDemo3());
        }
    }
}
