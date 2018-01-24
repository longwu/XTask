using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace XTask
{
    public class AsyncTask<T>
    {
        /// <summary>
        /// 任务是否已经启动
        /// </summary>
        private bool isStarted = false;

        /// <summary>
        /// 任务是否已经结束
        /// </summary>
        private bool isEnded = false;

        /// <summary>
        /// 任务是否取消了
        /// </summary>
        private bool isCancelled = false;

        /// <summary>
        /// 任务是否出错了
        /// </summary>
        private bool isFaulted = false;

        /// <summary>
        /// 异步方法,带返回参数
        /// </summary>
        private Func<T> asyncFunc = null;

        /// <summary>
        ///  异步任务执行过程中的回调方法
        /// </summary>
        private Action<T, Exception> callBack = null;

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
        /// 任务是否已经结束
        /// </summary>
        public bool IsEnded
        {
            get { return this.isEnded; }
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

        public AsyncTask(Func<T> func)
        {
            this.syncContext = SynchronizationContext.Current;
            this.asyncFunc = func;
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="callback">任务完成后的同步回调方法</param>
        public void Run(Action<T, Exception> callback)
        {
            this.callBack = callback;
            this.Run();
        }

        /// <summary>
        /// 开始异步执行方法
        /// </summary>
        private void Run()
        {
            if (this.asyncFunc != null)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    T result = default(T);
                    try
                    {
                        this.isStarted = true;
                        result = this.asyncFunc.Invoke(); //执行异步方法
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
                            this.isEnded = true;
                            ContinueWith(result,this.exception); //异步任务完成后进行同步回调
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
        /// <param name="result">任务返回值</param>
        /// <param name="exception">任务异常</param>
        private void ContinueWith(T result, Exception exception)
        {
            if (this.callBack != null)
            {
                this.syncContext.Send(obj =>
                {
                    if (this.callBack != null)
                    {
                        this.callBack.Invoke(result, exception);//同步回调
                    }
                }, null);
            }
        }
    }
}
