using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_work_6
{
    class TimerDecorator : DecoratorForSerializer
    {
        public TimerDecorator(Serializer ser)
            : base(ser)
        { }
        public override string Serialize(F instance)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            string result = _serializer.Serialize(instance);
            timer.Stop();
            this.DurationSerialization = timer.Elapsed;
            return result;
        }
        public override F DeSerialize(string data)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            F result = _serializer.DeSerialize(data);
            timer.Stop();
            this.DurationDeSerialization = timer.Elapsed;
            return result;
        }
    }
}
