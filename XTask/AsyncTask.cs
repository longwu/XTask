using System;
using System.Threading;

namespace XTask
{
    /// <summary>
    /// 一个异步任务类
    /// </summary>
    public class AsyncTask : AsyncTaskBase
    {
        /// <summary>
        /// 声明任务事件
        /// </summary>
        private event Action Action = null;

        /// <summary>
        /// 声明带异常参数的任务事件
        /// </summary>
        private event Action<Exception> ActionEx = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="action">需要异步执行的方法</param>
        public AsyncTask(Action action)
        {
            this.syncContext = SynchronizationContext.Current;
            this.Action += action;
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="actionEx">任务完成后的同步回调方法</param>
        public void Run(Action<Exception> actionEx)
        {
            this.ActionEx += actionEx;
            this.Run();
        }

        /// <summary>
        /// 开始异步执行方法
        /// </summary>
        private void Run()
        {
            if (this.Action != null)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    try
                    {
                        this.isStarted = true;
                        this.Action.Invoke(); //执行方法
                    }
                    catch (Exception ex)
                    {
                        this.exception = ex;
                        this.isFaulted = true; //执行失败
                    }
                    finally
                    {
                        if (!this.isCancelled)
                        {
                            this.isEnded = true;
                            ContinueWith(this.exception); //异步方法完成后进行同步回调
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 异步任务完成后继续同步回调
        /// </summary>
        /// <param name="ex">异常信息</param>
        private void ContinueWith(Exception ex)
        {
            if (this.ActionEx != null)
            {
                this.syncContext.Send(obj =>
                {
                    if (this.ActionEx != null)
                    {
                        this.ActionEx.Invoke(ex);//同步回调
                    }
                }, null);
            }
        }
    }
}
