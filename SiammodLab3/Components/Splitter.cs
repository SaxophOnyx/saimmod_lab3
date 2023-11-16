using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components
{
    public class Splitter : IReceiver
    {
        private IReceiver _first;
        private IReceiver _second;


        public Splitter(IReceiver first, IReceiver second)
        {
            _first = first;
            _second = second;
        }

        public bool CanReceive => _first.CanReceive || _second.CanReceive;

        public void Receive()
        {
            if (_first.CanReceive)
            {
                _first.Receive();
            } else if (_second.CanReceive)
            {
                _second.Receive();
            }
        }

        public void Tick()
        {
            
        }
    }
}
