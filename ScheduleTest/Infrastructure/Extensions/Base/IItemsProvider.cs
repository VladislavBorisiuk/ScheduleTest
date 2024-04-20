using System.Collections.Generic;

namespace ScheduleTest.Infrastructure.Extensions.Base
{

    public interface IItemsProvider<T>
    {
        int FetchCount();

        IList<T> FetchRange(int startIndex, int count);

        void AddItem(T item);

        void RemoveItem(int index);
    }
}
