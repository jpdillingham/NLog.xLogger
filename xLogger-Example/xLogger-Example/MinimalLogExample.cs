using NLog;
using System;

namespace xLogger_Example
{
    static class MinimalLogExample
    {
        private static Logger logger = LogManager.GetLogger("m");

        private static string[] numbers = new string[10] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static Random random = new Random();

        public static void Process(int count, int max)
        {
            try
            {
                for (int i = 0; i < count; i++)
                    logger.Info(ProcessOne(random.Next(0, max)));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static string ProcessOne(int number)
        {
            return numbers[number];
        }
    }
}
