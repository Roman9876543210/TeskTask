namespace TestTask;

public class Task2
{
    private static int count = 0;
    private static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

    public static int GetCount()
    {
        locker.EnterReadLock();
        try
        {
            return count;
        }
        finally
        {
            locker.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        locker.EnterWriteLock();
        try
        {
            count += value;
        }
        finally
        {
            locker.ExitWriteLock();
        }
    }

    public static void RunTest()
    {
        Parallel.For(0, 100, i =>
        {
            if (i % 10 == 0)
                Task2.AddToCount(1);
            else
                Console.WriteLine(Task2.GetCount());
        });
    }
}