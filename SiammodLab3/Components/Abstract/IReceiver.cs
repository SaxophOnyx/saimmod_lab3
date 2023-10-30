namespace SaimmodLab3.Components.Abstract
{
    public interface IReceiver : IComponent
    {
        bool CanReceive { get; }

        void Receive();
    }
}
