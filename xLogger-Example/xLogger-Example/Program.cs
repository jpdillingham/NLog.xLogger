using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace xLogger_Example
{
    class Program
    {
        private static xLogger logger = (xLogger)LogManager.GetLogger("x", typeof(xLogger));

        static void Main(string[] args)
        {
            EnterMethodExample(1, 2, new ExampleObject(3, "three", new double[] { 1.1, 2.2, 3.3 }.ToList()));
            ExitMethodPersistentExample(1, 2);
            CheckpointExample(1);
            ExceptionExample();
            StackTraceExample();
            OtherExamples();  
        }

        public static void EnterMethodExample(int one, int two, ExampleObject three)
        {
            logger.EnterMethod(xLogger.Params(one, two, three));

            // method body
            logger.Trace("Standard log message");
        }

        public static ExampleObject ExitMethodPersistentExample(int one, int two)
        {
            Guid persistedGuid = logger.EnterMethod(xLogger.Params(one, two), true);

            // method body

            logger.Trace("Standard log message");
            ExampleObject returnValue = new ExampleObject(1, "return", new double[] { 5.5 }.ToList());

            logger.ExitMethod(returnValue, persistedGuid);
            return returnValue;
        }

        public static int CheckpointExample(int one)
        {
            Guid persistedGuid = logger.EnterMethod(xLogger.Params(one), true);

            logger.Trace("Standard log message");
            int two = 2;
            int three = 3;

            logger.Checkpoint("example checkpoint", xLogger.Vars(one, two, three), xLogger.Names("one", "two", "three"), persistedGuid);

            logger.Trace("Another standard log message");

            int returnValue = one + two + three;

            logger.ExitMethod(returnValue, persistedGuid);
            return returnValue;
        }

        public static void ExceptionExample()
        {
            logger.EnterMethod();

            try
            {
                // intentionally raise an exception
                var arr = new string[5];
                Console.WriteLine(arr[5]);
            }
            catch (Exception ex)
            {
                logger.Exception(logger.Error, ex);
            }
            finally
            {
                logger.ExitMethod();
            }
        }

        public static void StackTraceExample()
        {
            logger.StackTrace(logger.Info);
        }

        public static void OtherExamples()
        {
            logger.Multiline(logger.Trace, "hello \n world!");
            logger.MultilineWrapped(logger.Trace, new string[] { "hello", "again", "world!!" });
            logger.Separator(logger.Trace);
            logger.Heading(logger.Trace, "Hello world!");
            logger.SubHeading(logger.Trace, "Hello world!");
            logger.SubSubHeading(logger.Trace, "Hello world!");
        }
    }

    class ExampleObject
    {
        public int num { get; set; }
        public string str { get; set; }
        public List<double> list { get; set; }

        public ExampleObject(int num, string str, List<double> list)
        {
            this.num = num;
            this.str = str;
            this.list = list;
        }
    }
}
