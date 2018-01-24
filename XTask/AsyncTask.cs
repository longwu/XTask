using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace XTask
{
     /// <summary>
    /// 一个异步任务类
    /// </summary>
    public class AsyncTask
    {
        /// <summary>
        /// 任务是否已经启动
        /// </summary>
        private bool isStarted = false;

        /// <summary>
        /// 任务是否已经完成
        /// </summary>
        private bool isCompleted = false;

        /// <summary>
        /// 任务是否取消了
        /// </summary>
        private bool isCancelled = false;

        /// <summary>
        /// 任务是否出错了
        /// </summary>
        private bool isFaulted = false;

        /// <summary>
        /// 异步方法
        /// </summary>
        private Action asyncAction = null;

        /// <summary>
        ///  异步任务执行过程中的回调方法
        /// </summary>
        private Action callBack = null;

        /// <summary>
        /// 同步模型,用于向UI线程发送同步消息
        /// </summary>
        private SynchronizationContext syncContext = null;

        /// <summary>
        /// 执行异步中产生的异常
        /// </summary>
        private Exception exception;

        /// <summary>
        /// 任务是否已经启动
        /// </summary>
        public bool IsStarted
        {
            get { return this.isStarted; }
        }

        /// <summary>
        /// 任务是否已经完成
        /// </summary>
        public bool IsCompleted
        {
            get { return this.isCompleted; }
        }

        /// <summary>
        /// 任务是否取消了
        /// </summary>
        public bool IsCancelled
        {
            get { return this.isCancelled; }
        }

        /// <summary>
        /// 任务是否出错了
        /// </summary>
        public bool IsFaulted
        {
            get { return this.isFaulted; }
        }

        /// <summary>
        /// 获取任务执行过程中产生的异常
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
        }

        /// <summary>
        /// 创建一个异步任务
        /// </summary>
        /// <param name="action">需要异步执行的方法</param>
        public AsyncTask(Action action)
        {
            this.syncContext = SynchronizationContext.Current;
            this.asyncAction = action;
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="exCallback">回调方法</param>
        public void Run(Action exCallback)
        {
            this.callBack = exCallback;
            this.Run();
        }

        /// <summary>
        /// 开始异步执行方法
        /// </summary>
        private void Run()
        {
            if (this.asyncAction != null)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    try
                    {
                        this.isStarted = true;
                        this.asyncAction.Invoke(); //执行异步方法
                    }
                    catch (Exception ex)
                    {
                        this.exception = ex;
                        this.isFaulted = true; //执行任务失败
                    }
                    finally
                    {
                        if (!this.isCancelled)
                        {
                            this.isCompleted = true;
                            ContinueWith(); //异步任务完成后进行同步回调
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 取消任务
        /// </summary>
        public void Cancel()
        {
            this.isCancelled = true;
        }

        /// <summary>
        /// 异步任务完成后继续同步回调
        /// </summary>
        private void ContinueWith()
        {
            if (callBack != null)
            {
                this.syncContext.Send(obj =>
                {
                    if (this.callBack != null)
                    {
                        callBack.Invoke();//同步回调
                    }
                }, null);
            }
        }
    }
}
