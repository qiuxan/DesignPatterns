namespace Per_Thread_Singleton;

public sealed class PerThreadSingleton
{
    private static ThreadLocal<PerThreadSingleton> threadInstance = new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());
    public int id;

    private PerThreadSingleton()
    {
        id = Thread.CurrentThread.ManagedThreadId;
    }

    public static PerThreadSingleton Instance => threadInstance.Value;
}
internal class Program
{
    static void Main(string[] args)
    {
        var t1 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("t1:"+PerThreadSingleton.Instance.id);
        });

        var t2 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("t2:" + PerThreadSingleton.Instance.id);
            Console.WriteLine("t2:" + PerThreadSingleton.Instance.id);

        });

        Task.WaitAll(t1, t2);
    }
}
