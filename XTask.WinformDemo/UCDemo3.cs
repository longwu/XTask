using System;
using System.Threading;
using System.Windows.Forms;

namespace XTask.WinformDemo
{
    public partial class UCDemo3 : UserControl
    {
        public UCDemo3()
        {
            InitializeComponent();

            this.btnStart.Click += (s, e) => Start();
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        private void Start()
        {
            this.lsb.Items.Add("Task started.");
            new AsyncTask(DoSomethindWithException).Run(ex =>
            {
                //异步方法完成后执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsb.Items.Add(string.Format("Task errored: {0}", ex.Message));
                }
                else//任务完成
                {
                    this.lsb.Items.Add("Task ended");
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
