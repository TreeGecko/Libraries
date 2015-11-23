namespace TreeGecko.Library.Common.Interfaces
{
    /// <summary>
    /// Similar to the IWorker, but an IRunnable doesn't 
    /// have the same concept of work completion.
    /// </summary>
    public interface IRunnable
    {
        void Start();
        void Stop();
    }
}
