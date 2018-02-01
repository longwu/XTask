# XTask
XTask是一个轻量级客户端异步任务类库,简单易用.

## 运行环境: 
.Net Framework 3.5或以上.

## 用法
**无返回值的异步任务**
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
            MessageBox.Show("Task completed");//任务完成
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

**带返回值的异步任务**
```C#
private AsyncTask<string> taskWithResult = null;
/// <summary>
/// 执行任务
/// </summary>
private void Start()
{
    taskWithResult = new AsyncTask<string>(DoSomethingWithResult);

    taskWithResult.Run((result, ex) =>
    {
        //异步方法执行完DoSomethingWithResult后同步执行下面的代码
        //判断任务是否出现异常
        if (ex != null)
        {
            MessageBox.Show(ex.ToString()); //输出异常
        }
        else //任务完成
        {
            MessageBox.Show(result); //任务完成,显示DoSomethingWithResult返回的结果
        }
    });
}

/// <summary>
/// 取消任务
/// </summary>
private void Cancel()
{
    taskWithResult.Cancel();
}

/// <summary>
/// 执行一个方法,带返回值
/// </summary>
/// <returns>返回一个字符串</returns>
private string DoSomethingWithResult()
{
    Thread.Sleep(3000);
    return "Task completed";
}
```

**更多详见Demo代码**