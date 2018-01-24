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
    /// Demo3.xaml 的交互逻辑
    /// </summary>
    public partial class Demo3 : UserControl
    {
        public Demo3()
        {
            InitializeComponent();

            this.btnStart.Click += (s, e) => Start();
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        private void Start()
        {
            this.lsv.Items.Add("Task started.");
            new AsyncTask(DoSomethindWithException).Run(ex =>
            {
                //异步方法完成后执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsv.Items.Add(string.Format("Task errored: {0}", ex.ToString()));
                }
                else//任务完成
                {
                    this.lsv.Items.Add("Task ended");
                }
            });
        }

        /// <summary>
        /// 执行一个方法,中途会产生异常
        /// </summary>
        private void DoSomethindWithException()
        {
            int i = 0;
            while (true)
            {
                if (i > 1)
                {
                    throw new Exception("Sorry, something gets wrong");
                }

                Thread.Sleep(1000);
                i++;
            }
        }

    }
}
