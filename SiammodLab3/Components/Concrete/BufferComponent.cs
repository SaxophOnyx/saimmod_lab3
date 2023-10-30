using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class BufferComponent : IReceiver
    {
        private readonly IReceiver _next;
        private readonly int _capacity;
        private int _count;


        public bool CanReceive => _count < _capacity;


        public BufferComponent(IReceiver next, int capacity)
        {
            _next = next;
            _capacity = capacity;
            _count = 0;
        }


        public void Receive() => ++_count;

        public void Tick()
        {
            if (_count > 0 && _next.CanReceive)
            {
                _next.Receive();
                _count--;
            }
        }
    }
}
