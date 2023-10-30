using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class RandomLockableGenerator : IComponent
    {
        private readonly IReceiver _next;
        private readonly double _generationProbability;
        private readonly Random _random;
        private bool _isLocked;
        private int _generatedCount;


        public int Generated => _generatedCount;


        public RandomLockableGenerator(IReceiver next, double generationProbability)
        {
            _next = next;
            _random = new();
            _generationProbability = generationProbability;
            _isLocked = false;
            _generatedCount = 0;
        }

        public void Tick()
        {
            if (_isLocked)
            {
                if (_next.CanReceive)
                {
                    Generate();
                }
            }
            else
            {
                double probability = _random.NextDouble();

                if (probability < _generationProbability)
                {
                    if (_next.CanReceive)
                    {
                        Generate();
                    }
                    else
                    {
                        _isLocked = true;
                    }
                }
            }
        }

        private void Generate()
        {
            _next.Receive();
            _isLocked = false;
            _generatedCount++;
        }
    }
}
