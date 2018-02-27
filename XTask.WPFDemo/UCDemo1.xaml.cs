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
    /// Demo1.xaml 的交互逻辑
    /// </summary>
    public partial class UCDemo1 : UserControl
    {
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
            this.lsv.Items.Add("Task one started.");
            new AsyncTask(() =>
            {
                DoSomething();//异步执行
            }).Run(ex =>
            {
                //异步方法执行完后同步执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsv.Items.Add(string.Format("Task one errored: {0}", ex.Message));
                }
                else//任务完成
                {
                    this.lsv.Items.Add("Task one ended.");
                }
            });
        }

        private void StartTaskTwo()
        {
            this.lsv.Items.Add("Task two started.");
            new AsyncTask<string>(() =>
            {
                return DoSomethingWithResult();//异步执行
            }).Run((result, ex) =>
            {
                //异步方法执行完后同步执行下面的代码
                //判断任务是否出现异常
                if (ex != null)
                {
                    this.lsv.Items.Add(string.Format("Task two errored: {0}", ex.Message));
                }
                else//任务完成
                {
                    this.lsv.Items.Add(result);
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
