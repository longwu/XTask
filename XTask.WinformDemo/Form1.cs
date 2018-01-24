using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace XTask.WinformDemo
{
    public partial class Form1 : Form
    {
        private AsyncTask task = null;

        public Form1()
        {
            InitializeComponent();

            this.btnStart.Click += (s, e) => Start(DoSomething);
            this.btnCancel.Click += (s, e) => Cancel();
            this.btnStartWithError.Click += (s, e) => Start(DoSomethindWithException);
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="action">需要异步执行的方法</param>
        private void Start(Action action)
        {
            task = new AsyncTask(action);

            task.Run(() =>
            {
                //判断任务是否出现异常
                if (task.Exception != null)
                {
                    this.txt.Text = task.Exception.ToString();
                }
                else//任务完成
                {
                    this.txt.Text = "Task Completed";
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
                this.txt.Text = "Task has been cancelled";
            }
        }

        /// <summary>
        /// 执行一个方法,需要耗时5秒钟
        /// </summary>
        private void DoSomething()
        {
            Thread.Sleep(5000);
        }

        /// <summary>
        /// 执行一个方法,中途会产生异常
        /// </summary>
        private void DoSomethindWithException()
        {
            int i = 0;
            while (true)
            {
                if (i > 3)
                {
                    throw new Exception("Sorry, something gets wrong");
                }

                Thread.Sleep(1000);
                i++;
            }
        }
    }
}
