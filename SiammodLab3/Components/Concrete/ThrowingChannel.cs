using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class ThrowingChannel : IReceiver
    {
        private readonly IReceiver _next;
        private readonly double _processingProbability;
        private readonly Random _random;
        private bool _isProcessing;
        private int _passed;
        private int _rejected;
        private int _ticksWhenProcessing;


        public int Passed => _passed;

        public int Rejected => _rejected;

        public int Total => Passed + Rejected;

        public bool CanReceive => !_isProcessing;

        public int TicksWhenProcessing => _ticksWhenProcessing;


        public ThrowingChannel(IReceiver next, double processingProbability)
        {
            _next = next;
            _passed = 0;
            _rejected = 0;
            _processingProbability = processingProbability;
            _random = new Random();
            _isProcessing = false;
            _ticksWhenProcessing = 0;
        }


        public void Receive()
        {
            _isProcessing = true;
        }

        public void Tick()
        {
            if (_isProcessing)
            {
                ++_ticksWhenProcessing;
                double probability = _random.NextDouble();

                if (probability < _processingProbability)
                {
                    _isProcessing = false;

                    if (_next.CanReceive)
                    {
                        _next.Receive();
                        _passed++;
                    }
                    else
                    {
                        _rejected++;
                    }
                }
            }
        }
    }
}
