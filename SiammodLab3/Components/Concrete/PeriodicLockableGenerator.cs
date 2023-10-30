using SaimmodLab3.Components.Abstract;

namespace SaimmodLab3.Components.Concrete
{
    public class PeriodicLockableGenerator : IComponent
    {
        private readonly IReceiver _next;
        private readonly int _period;
        private int _ticksBeforeGeneration;
        private bool _isLocked;
        private int _generatedCount;


        public int Generated => _generatedCount;


        public PeriodicLockableGenerator(IReceiver next, int period)
        {
            _next = next;
            _period = period;
            _ticksBeforeGeneration = period;
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
                _ticksBeforeGeneration--;

                if (_ticksBeforeGeneration == 0)
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
            _ticksBeforeGeneration = _period;
        }
    }
}
