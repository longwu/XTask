# XTask

XTask是一个轻量级客户端异步任务类库,简单易用.

运行环境: .Net Framework 3.5或以上.

用法
```C#
private AsyncTask task = null;
/// <summary>
/// 执行任务
/// </summary>
private void Start()
{
    task = new AsyncTask(DoSomething);//创建一个异步任务
    //执行任务
    task.Run(ex =>
    {
        //异步方法执行完DoSomething()后同步执行下面的代码
        //判断任务是否出现异常
        if (ex != null)
        {
            MessageBox.Show(ex.ToString());//输出异常
        }
        else//任务完成
        {
            MessageBox.Show("Task comleted");//任务完成
        }
    });
}

/// <summary>
/// 取消任务
/// </summary>
private void Cancel()
{
    task.Cancel();
}

/// <summary>
/// 执行一个方法,需要耗时3秒钟
/// </summary>
private void DoSomething()
{
    Thread.Sleep(3000);
}
```

更多详见Demo代码