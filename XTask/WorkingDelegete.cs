
namespace XTask
{
    public delegate void Action();

    public delegate void Action<T>(T args);

    public delegate T Func<T>();

    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);
}
