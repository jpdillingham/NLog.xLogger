using NLog;
using System;

namespace xLogger_Example
{
    static class VerboseLogExample
    {
        private static xLogger logger = (xLogger)LogManager.GetLogger("v", typeof(xLogger));

        private static string[] numbers = new string[10] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static Random random = new Random();

        public static void Process(int count, int max)
        {
            Guid guid = logger.EnterMethod(xLogger.Params(count, max), true);

            try
            {
                for (int i = 0; i < count; i++)
                    logger.Info(ProcessOne(random.Next(0, max)));
            }
            catch (Exception ex)
            {
                logger.Exception(ex, guid);
            }

            logger.ExitMethod(guid);
        }

        public static string ProcessOne(int number)
        {
            logger.EnterMethod(xLogger.Params(number));

            string retVal = numbers[number];

            logger.ExitMethod((object)retVal);
            return retVal;
        }
    }
}
