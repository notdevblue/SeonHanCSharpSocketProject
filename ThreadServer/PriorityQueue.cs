using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();
        public int Count { get { return _heap.Count; } }

        public void Push(T data)
        {
            _heap.Add(data);

            int now = _heap.Count - 1; // 현재 힙의 인덱스 카운트
            while (now > 0)
            {
                int next = (now - 1) / 2; // 부모 찾아감
                if(_heap[now].CompareTo(_heap[next]) < 0) // 0보다 작으면 now 가 next 보다 작다.
                {
                    break;
                }

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;
            }
            //n 개 log2n
        }

        public T Pop()
        {
            T ret = _heap[0];

            int lastIndex = _heap.Count - 1; // 마지막 녀석을 가져옴
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            --lastIndex;

            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;

                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                {
                    next = left;
                }

                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                {
                    next = right;
                }

                if (next == now)
                {
                    break;
                }

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;
            }



            return ret;
        }

        public T Peek()
        {
            return _heap.Count == 0 ? default : _heap[0];
        }




    }
}
