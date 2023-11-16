using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class Channel : IReceiver
    {
        private readonly double _processingProbability;
        private readonly Random _random;
        private bool _isProcessing;
        private int _passed;
        private int _ticksWhenProcessing;


        public int Passed => _passed;

        public bool CanReceive => !_isProcessing;

        public int TicksWhenProcessing => _ticksWhenProcessing;


        public Channel(double processingProbability)
        {
            _passed = 0;
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
                    _passed++;
                }
            }
        }
    }
}
