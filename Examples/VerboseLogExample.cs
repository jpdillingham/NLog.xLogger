using System;
using NLog;

namespace xLogger.Examples
{
    /// <summary>
    /// A verbose log example.
    /// </summary>
    public static class VerboseLogExample
    {
        /// <summary>
        /// The logger for the class.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetLogger("v", typeof(xLogger));

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
            Guid guid = logger.EnterMethod(xLogger.Params(count, max), true);

            try
            {
                for (int i = 0; i < count; i++)
                {
                    logger.Info(ProcessOne(random.Next(0, max)));
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex, guid);
            }

            logger.ExitMethod(guid);
        }

        /// <summary>
        /// Returns the number at the specified index from the numbers array.
        /// </summary>
        /// <param name="number">The desired index in the numbers array.</param>
        /// <returns>The number at the specified index in the numbers array.</returns>
        public static string ProcessOne(int number)
        {
            logger.EnterMethod(xLogger.Params(number));

            string retVal = numbers[number];

            logger.ExitMethod((object)retVal);
            return retVal;
        }
    }
}
