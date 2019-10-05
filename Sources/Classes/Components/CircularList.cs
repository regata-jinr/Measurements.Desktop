using System.Collections.Generic;

namespace Measurements.UI.Desktop.Components
{
    public class CircularList<T>
    {
        private LinkedList<T> _linkedList;
        public CircularList(IEnumerable<T> source)
        {
            _linkedList = new LinkedList<T>(source);
            CurrentItem = LastItem;
        }

        public T CurrentItem { get; set; }

        public T NextItem
        {
            get
            {
                if (_linkedList.Find(CurrentItem) == _linkedList.Last)
                    return FirstItem;
                return _linkedList.Find(CurrentItem).Next.Value; 
            }
        }

        public T PrevItem
        {
            get
            {
                if (_linkedList.Find(CurrentItem) == _linkedList.First)
                    return LastItem;
                return _linkedList.Find(CurrentItem).Previous.Value; 
            }
        }

        public T FirstItem
        {
            get
            {
                return _linkedList.First.Value;
            }
        }

        public T LastItem
        {
            get
            {
                return _linkedList.Last.Value;
            }
        }

        public void Next()
        {
            CurrentItem = NextItem;
        }

        public void Prev()
        {
            CurrentItem = PrevItem;
        }

    }
}
