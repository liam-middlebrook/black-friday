using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStarPathfinding
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        /// <summary>
        /// The innerheap used to store data
        /// </summary>
        List<T> innerHeap;

        /// <summary>
        /// Creates a new PriorityQueue object
        /// </summary>
        public PriorityQueue()
        {
            this.innerHeap = new List<T>();
        }

        /// <summary>
        /// Enqueues an object of type T into the priority queue
        /// </summary>
        /// <param name="data">The object to add</param>
        public void Enqueue(T data)
        {
            //Add data to heap
            innerHeap.Add(data);

            int index = innerHeap.Count - 1;

            while (index > 0)
            {
                //Get the parent's index by checking if even or odd, then subtracting 1 or 2 respectively
                //And lastly dividing by 2
                int parentIndex = ((index & 1) == 1) ? index - 1 : index - 2;
                parentIndex >>= 1;

                //Is parent's data larger than childs? If soswap
                if (innerHeap[parentIndex].CompareTo(innerHeap[index]) > 0)
                {
                    SwapFromIndices(parentIndex, index);
                }
                index = parentIndex;
            }
        }

        /// <summary>
        /// Deuqueues the first element in the priority queue
        /// </summary>
        /// <returns>The first element</returns>
        public T Dequeue()
        {
            T dequeuedValue = innerHeap[0];
            //Replace the root with the last element in the heap, then remove the root
            SwapFromIndices(0, innerHeap.Count - 1);
            innerHeap.RemoveAt(innerHeap.Count - 1);

            //Re-sort the heap now that it has changed
            HeapSort();

            return dequeuedValue;
        }

        /// <summary>
        /// Gets the first element in the priority queue
        /// </summary>
        /// <returns>The first element</returns>
        public T Peek()
        {
            if (innerHeap.Count > 0)
            {
                return innerHeap[0];
            }
            throw new NullReferenceException("NO_DATA_IN_QUEUE");
        }

        /// <summary>
        /// Whether or not the priority queue is empty
        /// </summary>
        /// <returns>Whether or not the priority queue is empty</returns>
        public bool IsEmpty()
        {
            return innerHeap.Count <= 0;
        }

        /// <summary>
        /// Swaps two elements in an array based collection using their indicies
        /// </summary>
        /// <param name="i1">The index of the first item to swap</param>
        /// <param name="i2">The index of the second item to swap</param>
        private void SwapFromIndices(int i1, int i2)
        {
            T temp = innerHeap[i1];
            innerHeap[i1] = innerHeap[i2];
            innerHeap[i2] = temp;
        }

        /// <summary>
        /// The number of elements in the priority queue
        /// </summary>
        /// <returns>The number of elements in the priority queue</returns>
        public int Count()
        {
            return innerHeap.Count;
        }

        /// <summary>
        /// Sorts the inner heap of the priority queue
        /// </summary>
        public void HeapSort()
        {
            int index = 0;

            while (true)
            {
                //Get the indicies of the left and right child of the current node
                int leftChildIndex = (index << 1) + 1;
                int rightChildIndex = (index << 1) + 2;

                //If the left child index is outside of range, then sorting is done
                if (leftChildIndex > innerHeap.Count - 2)
                {
                    break;
                }

                bool greaterThanLeft = false;

                //Check whether or not the current node has a value greater than the left child
                if (innerHeap[index].CompareTo(innerHeap[leftChildIndex]) > 0)
                {
                    greaterThanLeft = true;
                }

                //If the left child is greater than the current node AND the right child is less than the current node
                //OR the left child is less than the current node and the left child is greater than the right child
                if ((!greaterThanLeft && innerHeap[index].CompareTo(innerHeap[rightChildIndex]) > 0) || (greaterThanLeft && innerHeap[leftChildIndex].CompareTo(innerHeap[rightChildIndex]) > 0))
                {
                    SwapFromIndices(index, rightChildIndex);
                    greaterThanLeft = false;
                    index = rightChildIndex;
                }
                //If the left child is greater than the current node
                else if (greaterThanLeft)
                {
                    SwapFromIndices(index, leftChildIndex);
                    index = leftChildIndex;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
