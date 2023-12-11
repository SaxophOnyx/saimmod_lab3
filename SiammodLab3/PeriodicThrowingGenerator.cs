using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class PeriodicThrowingGenerator : IComponent
    {
        private readonly IReceiver _next;
        private readonly int _period;
        private int _ticksBeforeGeneration;
        private int _generatedCount;
        private int _rejectedCount;


        public int Generated => _generatedCount;

        public int Rejected => _rejectedCount;


        public PeriodicThrowingGenerator(IReceiver next, int period)
        {
            _next = next;
            _period = period;
            _ticksBeforeGeneration = period;
            _generatedCount = 0;
            _rejectedCount = 0;
        }

        public void Tick()
        {
            _ticksBeforeGeneration--;

            if (_ticksBeforeGeneration == 0)
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

                _ticksBeforeGeneration = _period;
            }
        }
    }
}
