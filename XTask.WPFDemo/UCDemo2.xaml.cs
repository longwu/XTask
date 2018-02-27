using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XTask.WPFDemo
{
    /// <summary>
    /// Demo2.xaml 的交互逻辑
    /// </summary>
    public partial class UCDemo2 : UserControl
    {
        private AsyncTask task = null;

        public UCDemo2()
        {
            InitializeComponent();

            this.btnStart.Click += (s, e) => Start();
            this.btnCancel.Click += (s, e) => Cancel();
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        private void Start()
        {
            this.lsv.Items.Add("Task started.");
            task = new AsyncTask(DoSomething);
            task.Run(ex =>
            {
                //异步方法执行完后同步执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsv.Items.Add(string.Format("Task errored: {0}", ex.Message));
                }
                else//任务完成
                {
                    this.lsv.Items.Add("Task ended.");
                }
            });
        }

        /// <summary>
        /// 撤销任务
        /// </summary>
        private void Cancel()
        {
            if (task != null)
            {
                task.Cancel();
                this.lsv.Items.Add("Task has been cancelled.");
            }
        }

        /// <summary>
        /// 执行一个方法,需要耗时3秒钟
        /// </summary>
        private void DoSomething()
        {
            Thread.Sleep(3000);
        }
    }
}
