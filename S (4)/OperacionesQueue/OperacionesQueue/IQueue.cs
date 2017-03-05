
namespace ExcerciseQueues
{
    interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        int Size();
        bool IsEmpty();
        T Front();
    }
}
