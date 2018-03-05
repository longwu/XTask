using System;
using System.Threading;

namespace XTask
{
    public class AsyncTask<T> : AsyncTaskBase
    {
        /// <summary>
        /// 异步方法,带返回参数
        /// </summary>
        private event Func<T> Func = null;

        /// <summary>
        ///  异步任务执行过程中的回调方法
        /// </summary>
        private event Action<T, Exception> ActionEx = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="func">需要异步执行的方法,带返回值</param>
        public AsyncTask(Func<T> func)
        {
            this.syncContext = SynchronizationContext.Current;
            this.Func += func;
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="actionEx">任务完成后的同步回调方法</param>
        public void Run(Action<T, Exception> actionEx)
        {
            this.ActionEx += actionEx;
            this.Run();
        }

        /// <summary>
        /// 开始异步执行方法
        /// </summary>
        private void Run()
        {
            if (this.Func != null)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    T result = default(T);
                    try
                    {
                        this.isStarted = true;
                        result = this.Func.Invoke(); //执行异步方法
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
        /// 异步任务完成后继续同步回调
        /// </summary>
        /// <param name="result">任务返回值</param>
        /// <param name="ex">任务异常</param>
        private void ContinueWith(T result, Exception ex)
        {
            if (this.ActionEx != null)
            {
                this.syncContext.Send(obj =>
                {
                    if (this.ActionEx != null)
                    {
                        this.ActionEx.Invoke(result, ex);//同步回调
                    }
                }, null);
            }
        }
    }
}
