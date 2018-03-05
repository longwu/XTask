using System.Windows.Forms;

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
