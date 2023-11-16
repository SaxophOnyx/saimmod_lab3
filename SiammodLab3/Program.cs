using SaimmodLab3.Components;
using SaimmodLab3.Components.Abstract;
using SaimmodLab3.Components.Concrete;

namespace SaimmodLab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Main_29();
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

        private static void Main_36()
        {
            int ticks = 100000;
            double pGen = 0.3;
            double p1 = 0.7;
            double p2 = 0.7;

            Channel channel_1 = new(1 - p1);
            Channel channel_2 = new(1 - p2);
            BufferComponent buffer = new(channel_1, 2);
            Splitter splitter = new(buffer, channel_2);
            RandomGenerator generator = new(splitter, 1 - pGen);

            List<IComponent> components = new()
            {
                channel_1, buffer, channel_2, generator
            };

            for (int i = 0; i < ticks; ++i)
            {
                foreach (var component in components)
                    component.Tick();
            }
        }

        private static void Main_29()
        {
            int ticks = 100000;
            int period = 2;
            double p1 = 0.4;
            double p2 = 0.4;

            int applicationsCounter = 0;

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

                if (!channel.CanReceive)
                    ++applicationsCounter;

                if (!throwingChannel.CanReceive)
                    ++applicationsCounter;

                applicationsCounter += buffer.Count;
            }

            double a = throwingChannel.Passed * 1.0 / ticks;
            double q = throwingChannel.Passed * 1.0 / generator.Generated;
            double rejProb = (generator.Generated - throwingChannel.Passed) * 1.0 / generator.Generated;
            double lockProb = generator.TicksWhenLocked * 1.0 / ticks;
            double avgBufferLength = buffer.CountPerTick.Skip(0).Select((count, tick) => count * tick).Sum() * 1.0 / ticks;
            double avgApplicationsCount = applicationsCounter * 1.0 / ticks;
            double k1 = throwingChannel.TicksWhenProcessing * 1.0 / ticks;
            double k2 = channel.TicksWhenProcessing * 1.0 / ticks;
            double lambda = a / (1 - lockProb);
            double avgTimeInBuffer = avgBufferLength / lambda;
            double avgTimeInSystem = avgApplicationsCount / lambda;

            Console.WriteLine($"A: {a}");
            Console.WriteLine($"Q: {q}");
            Console.WriteLine($"Вероятность отказа: {rejProb}");
            Console.WriteLine($"Вероятность блокировки: {lockProb}");
            Console.WriteLine($"Средн. длина очереди: {avgBufferLength}");
            Console.WriteLine($"Средн. число заявок: {avgApplicationsCount}");
            Console.WriteLine($"Средн. время в очереди: {avgTimeInBuffer}");
            Console.WriteLine($"Средн. время в системе: {avgTimeInSystem}");
            Console.WriteLine($"Коэфф. загрузки канала 1: {k1}");
            Console.WriteLine($"Коэфф. загрузки канала 2: {k2}");
        }
    }
}
