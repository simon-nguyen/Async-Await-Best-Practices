using System.Runtime.CompilerServices;

namespace GoComics.Core.Extensions
{
    public static class IAsyncEnumerableExtensions
    {
        public static async IAsyncEnumerable<IEnumerable<T>> AsBatch<T>(this IAsyncEnumerable<T> source
            , int size
            , [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var batch = new List<T>();

            await foreach (var item in source)
            {
                if (cancellationToken.IsCancellationRequested) yield break;

                batch.Add(item);
                if (batch.Count >= size)
                {
                    yield return batch;

                    batch.Clear();
                }
            }

            if (batch.Count > 0) yield return batch;
        }

        public static async IAsyncEnumerable<T> AsBuffer<T>(this IAsyncEnumerable<T> source
            , int size
            , [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var queue = new Queue<T>();

            await foreach (var item in source)
            {
                if (cancellationToken.IsCancellationRequested) yield break;

                queue.Enqueue(item);
                while (queue.Count >= size) yield return queue.Dequeue();
            }

            while (queue.Count > 0) yield return queue.Dequeue();
        }
    }
}
