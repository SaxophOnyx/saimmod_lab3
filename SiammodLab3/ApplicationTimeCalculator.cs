using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3
{
    public class ApplicationTimeCalculator : IReceiver
    {
        private readonly IReceiver _from;
        private readonly IReceiver _to;
        private readonly Queue<int> _applicationQueue;
        private readonly Random _random;


        public ApplicationTimeCalculator(IReceiver from, IReceiver to)
        {
            _from = from;
            _to = to;
            _applicationQueue = new Queue<int>();
            _random = new Random();
        }

        public bool CanReceive => _to.CanReceive;

        public void Receive()
        {
            _to.Receive();
        }

        public void Tick()
        {
            
        }
    }
}
