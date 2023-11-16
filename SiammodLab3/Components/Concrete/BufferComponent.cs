using SaimmodLab3.Components.Abstract;
using System.Collections.ObjectModel;

namespace SaimmodLab3.Components.Concrete
{
    public class BufferComponent : IReceiver
    {
        private readonly IReceiver _next;
        private readonly int _capacity;
        private readonly int[] _countPerTick;
        private int _count;


        public bool CanReceive => _count < _capacity;

        public int Count => _count;

        public ReadOnlyCollection<int> CountPerTick { get; }


        public BufferComponent(IReceiver next, int capacity)
        {
            _next = next;
            _capacity = capacity;
            _count = 0;
            _countPerTick = new int[_capacity + 1];
            CountPerTick = new(_countPerTick);
        }


        public void Receive()
        {
            if (_next.CanReceive)
            {
                _next.Receive();
            }
            else
            {
                ++_count;
            }
        }

        public void Tick()
        {
            _countPerTick[_count]++;

            if (_count > 0 && _next.CanReceive)
            {
                _next.Receive();
                _count--;
            }
        }
    }
}
