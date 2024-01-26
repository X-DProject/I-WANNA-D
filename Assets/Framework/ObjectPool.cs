using System;

namespace Tool
{
    /// <summary>
    /// Represents a pool of objects that we can pull from in order
    /// to prevent constantly reallocating new objects. This collection
    /// is meant to be fast, so we limit the "lock" that we use and do not
    /// track the instances that we hand out.
    /// </summary>
    /// <typeparam name="T">The type of object in the pool.</typeparam>
    public sealed class ObjectPool<T> where T : new()
    {
        /// <summary>
        /// Number of items to grow the array by if needed
        /// </summary>
        private int growSize = 20;

        /// <summary>
        /// Pool objects
        /// </summary>
        private T[] pool;

        /// <summary>
        /// Index into the pool
        /// </summary>
        private int nextIndex = 0;

        /// <summary>
        /// Initializes a new instance of the ObjectPool class.
        /// </summary>
        public ObjectPool()
        {
            // Initialize the pool
            Resize(10, false);
        }

        /// <summary>
        /// Initializes a new instance of the ObjectPool class.
        /// </summary>
        /// <param name="size">The size of the object pool.</param>
        public ObjectPool(int rSize)
        {
            // Initialize the pool
            Resize(rSize, false);
        }

        /// <summary>
        /// Initializes a new instance of the ObjectPool class.
        /// </summary>
        /// <param name="rSize">The initial size of the object pool.</param>
        /// <param name="rGrowize">Increment to grow the pool by when needed</param>
        public ObjectPool(int rSize, int rGrowSize)
        {
            growSize = rGrowSize;

            // Initialize the pool
            Resize(rSize, false);
        }

        /// <summary>
        /// The total size of the pool
        /// </summary>
        /// <value>The length.</value>
        public int Length { get { return pool.Length; } }

        /// <summary>
        /// The number of items available in the pool
        /// </summary>
        public int Available { get { return pool.Length - nextIndex; } }

        /// <summary>
        /// The number of items that have been allocated
        /// </summary>
        public int Allocated { get { return nextIndex; } }

        /// <summary>
        /// Pulls an item from the object pool or creates more
        /// if needed.
        /// </summary>
        /// <returns>Object of the specified type</returns>
        public T Allocate()
        {
            // Creates extra items if needed
            if (nextIndex >= pool.Length)
            {
                if (growSize <= 0) return default;
                Resize(pool.Length + growSize, true);
            }

            if (nextIndex >= 0 && nextIndex < pool.Length) return pool[nextIndex++];

            return default;
        }

        /// <summary>
        /// Sends an item back to the pool.
        /// </summary>
        /// <param name="rInstance">Object to return</param>
        public void Recycle(T rInstance)
        {
            if (nextIndex > 0)
            {
                nextIndex--;
                pool[nextIndex] = rInstance;
            }
        }

        /// <summary>
        /// Rebuilds the pool with new instances
        /// 
        /// Note:
        /// This is a fast pool so we don't track the instances
        /// that are handed out. Releasing an instance also overwrites
        /// what was there. That means we can't have a "ReleaseAll"
        /// function that allows the array to be used again. The best
        /// we can do is abandon what we have given out and rebuild all our instances.
        /// </summary>
        /// <param name="rInstance">Object to return</param>
        public void Reset()
        {
            // Determine the length to initialize
            int lLength = growSize;
            if (pool != null) lLength = pool.Length;

            // Rebuild our elements
            Resize(lLength, false);

            // Reset the pool stats
            nextIndex = 0;
        }

        /// <summary>
        /// Resize the pool array
        /// </summary>
        /// <param name="rSize">New size of the pool</param>
        /// <param name="rCopyExisting">Determines if we copy contents from the old pool</param>
        public void Resize(int rSize, bool rCopyExisting)
        {
            lock (this)
            {
                int lCount = 0;

                // Build the new array and copy the contents
                T[] lNewPool = new T[rSize];

                if (pool != null && rCopyExisting)
                {
                    lCount = pool.Length;
                    Array.Copy(pool, lNewPool, Math.Min(lCount, rSize));
                }

                // Allocate items in the new array
                for (int i = lCount; i < rSize; i++) lNewPool[i] = new T();

                // Replace the old array
                pool = lNewPool;
            }
        }
    }
}