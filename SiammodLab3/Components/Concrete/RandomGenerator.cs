using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class RandomGenerator : IComponent
    {
        private readonly IReceiver _next;
        private readonly double _generationProbability;
        private readonly Random _random;
        private int _generatedCount;
        private int _rejectedCount;


        public int Generated => _generatedCount;

        public int Rejected => _rejectedCount;


        public RandomGenerator(IReceiver next, double generationProbability)
        {
            _next = next;
            _random = new();
            _generationProbability = generationProbability;
            _generatedCount = 0;
            _rejectedCount = 0;
        }

        public void Tick()
        {
            double probability = _random.NextDouble();

            if (probability < _generationProbability)
            {
                if (_next.CanReceive)
                {
                    _next.Receive();
                    _generatedCount++;
                }
                else
                {
                    _rejectedCount++;
                }

            }
        }
    }
}
