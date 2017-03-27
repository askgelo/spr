using System;

namespace src
{
    public abstract class TimeProvider
    {
        class DefaultTimeProvider : TimeProvider
        {
            public override DateTime Now
            {
                get
                {
                    return DateTime.Now;
                }
            }
        }

        static TimeProvider current;

        static TimeProvider()
        {
            current = new DefaultTimeProvider();
        }

        public static TimeProvider Current
        {
            get { return current; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Time provider null value");
                }
                current = value;
            }
        }
        public abstract DateTime Now { get; }

        public static void ResetToDefault()
        {
            current = new DefaultTimeProvider();
        }
    }
}
