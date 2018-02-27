using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace XTask.WinformDemo
{
    public partial class UCDemo1 : UserControl
    {
        private AsyncTask task = null;

        public UCDemo1()
        {
            InitializeComponent();

            this.btnStartOne.Click += (s, e) => StartTaskOne();
            this.btnStartTwo.Click += (s, e) => StartTaskTwo();
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        private void StartTaskOne()
        {
            this.lsb.Items.Add("Task one started.");
            new AsyncTask(() =>
            {
                DoSomething();//异步执行
            }).Run(ex =>
            {
                //异步方法执行完后同步执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsb.Items.Add(string.Format("Task one errored: {0}", ex.Message));
                }
                else//任务完成
                {
                    this.lsb.Items.Add("Task one ended.");
                }
            });
        }

        private void StartTaskTwo()
        {
            this.lsb.Items.Add("Task two started.");
            new AsyncTask<string>(() =>
            {
                return DoSomethingWithResult();//异步执行
            }).Run((result, ex) =>
            {
                //异步方法执行完后同步执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsb.Items.Add(string.Format("Task two errored: {0}", ex.Message));
                }
                else//任务完成
                {
                    this.lsb.Items.Add(result);
                }
            });
        }

        /// <summary>
        /// 执行一个方法,需要耗时3秒钟
        /// </summary>
        private void DoSomething()
        {
            Thread.Sleep(3000);
        }

        /// <summary>
        /// 执行一个方法,带返回值
        /// </summary>
        /// <returns>返回一个字符串</returns>
        private string DoSomethingWithResult()
        {
            Thread.Sleep(3000);
            return "Task two ended.";
        }
    }
}
