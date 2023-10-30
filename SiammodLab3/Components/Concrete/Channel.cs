using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class Channel : IReceiver
    {
        private readonly double _processingProbability;
        private readonly Random _random;
        private bool _isProcessing;
        private int _passed;


        public int Passed => _passed;

        public bool CanReceive => !_isProcessing;


        public Channel(double processingProbability)
        {
            _passed = 0;
            _processingProbability = processingProbability;
            _random = new Random();
            _isProcessing = false;
        }


        public void Receive()
        {
            _isProcessing = true;
        }

        public void Tick()
        {
            if (_isProcessing)
            {
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
