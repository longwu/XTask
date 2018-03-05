using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XTask
{
    public class AsyncTaskBase
    {
        /// <summary>
        /// 任务是否已经启动
        /// </summary>
        protected bool isStarted = false;

        /// <summary>
        /// 任务是否已经结束
        /// </summary>
        protected bool isEnded = false;

        /// <summary>
        /// 任务是否取消了
        /// </summary>
        protected bool isCancelled = false;

        /// <summary>
        /// 任务是否出错了
        /// </summary>
        protected bool isFaulted = false;

        /// <summary>
        /// 执行异步中产生的异常
        /// </summary>
        protected Exception exception;

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
        protected Exception Exception
        {
            get { return this.exception; }
        }

        /// <summary>
        /// 同步模型,用于向UI线程发送同步消息
        /// </summary>
        protected SynchronizationContext syncContext = null;

        /// <summary>
        /// 取消任务
        /// </summary>
        public void Cancel()
        {
            this.isCancelled = true;
        }
    }
}
