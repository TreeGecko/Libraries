using TreeGecko.Library.Common.Delegates;

namespace TreeGecko.Library.Common.Interfaces
{
    public interface IWorker
    {
        void Start();
        void Stop();

		event WorkCompleteHandler WorkComplete;
    }
}
