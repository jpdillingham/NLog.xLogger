using System;
using NLog;

namespace xLogger.Examples
{
    /// <summary>
    /// A minimal log example.
    /// </summary>
    public static class MinimalLogExample
    {
        /// <summary>
        /// The logger for the class.
        /// </summary>
        private static Logger logger = LogManager.GetLogger("m");

        /// <summary>
        /// Some numbers.
        /// </summary>
        private static string[] numbers = new string[10] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        /// <summary>
        /// A random number generator.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Runs the example process.
        /// </summary>
        /// <param name="count">The number of times to run.</param>
        /// <param name="max">The maximum number for the random generator.</param>
        public static void Process(int count, int max)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    logger.Info(ProcessOne(random.Next(0, max)));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// Returns the number at the specified index from the numbers array.
        /// </summary>
        /// <param name="number">The desired index in the numbers array.</param>
        /// <returns>The number at the specified index in the numbers array.</returns>
        public static string ProcessOne(int number)
        {
            return numbers[number];
        }
    }
}
