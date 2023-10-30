using SaimmodLab3.Components.Abstract;
using SaimmodLab3.Components.Concrete;

namespace SaimmodLab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Main_16();
        }

        private static void Main_16()
        {
            int ticks = 10000000;
            double p = 0.5;
            double p1 = 0.48;
            double p2 = 0.5;

            Channel channel = new(1 - p2);
            ThrowingChannel throwingChannel = new(channel, 1 - p1);
            BufferComponent buffer = new(throwingChannel, 2);
            RandomLockableGenerator generator = new(buffer, 1 - p);

            List<IComponent> components = new()
            {
                channel, throwingChannel, buffer, generator
            };

            for (int i = 0; i < ticks; ++i)
            {
                foreach (var component in components)
                    component.Tick();
            }

            double a = channel.Passed * 1.0 / ticks;
            double q = channel.Passed * 1.0 / generator.Generated;

            Console.WriteLine($"A = {a}");
            Console.WriteLine($"Q = {q}");
        }

        private static void Main_29()
        {
            int ticks = 10000;
            int period = 2;
            double p1 = 0.4;
            double p2 = 0.4;

            Channel channel = new(1 - p2);
            BufferComponent buffer = new(channel, 1);
            ThrowingChannel throwingChannel = new(buffer, 1 - p1);
            PeriodicLockableGenerator generator = new(throwingChannel, period);

            List<IComponent> components = new()
            {
                channel, buffer, throwingChannel, generator
            };


            for (int i = 0; i < ticks; ++i)
            {
                foreach (var component in components)
                    component.Tick();
            }
        }
    }
}