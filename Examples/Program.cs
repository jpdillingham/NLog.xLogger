using System;
using System.Linq;
using NLog;

namespace NLog.xLogger.Examples
{
    /// <summary>
    /// Examples for the xLogger class.
    /// </summary>
    public class Program
    {
        #region Fields

        /// <summary>
        /// The logger for the class.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetLogger("x", typeof(xLogger));

        #endregion

        #region Methods

        /// <summary>
        /// The entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            EnterMethodExample<string, bool>(1, 2, new ExampleObject(3, "three", new double[] { 1.1, 2.2, 3.3 }.ToList()));
            ExitMethodPersistentExample(1, 2);
            CheckpointExample(1);
            ExceptionExample();
            StackTraceExample();
            OtherExamples();

            logger.SubSubHeading(LogLevel.Info, "Minimal Log Example:");
            MinimalLogExample.Process(10, 10);

            logger.SubSubHeading(LogLevel.Info, "Verbose Log Example:");
            VerboseLogExample.Process(10, 10);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Example of <see cref="xLogger.EnterMethod(Type[], object[], bool, string, string, int)"/> usage.
        /// </summary>
        /// <typeparam name="Tone">The first Type parameter.</typeparam>
        /// <typeparam name="Ttwo">The second Type parameter.</typeparam>
        /// <param name="one">A number.</param>
        /// <param name="two">Another number.</param>
        /// <param name="three">An instance of ExampleObject.</param>
        public static void EnterMethodExample<Tone, Ttwo>(int one, int two, ExampleObject three)
        {
            logger.EnterMethod(xLogger.TypeParams(typeof(Tone), typeof(Ttwo)), xLogger.Params(one, two, three));

            //// method body

            logger.Trace("Standard log message");
        }

        /// <summary>
        /// Example of <see cref="xLogger.ExitMethod(object, Guid, string, string, int)"/> usage.
        /// </summary>
        /// <param name="one">A number.</param>
        /// <param name="two">Another number.</param>
        /// <returns>An ExampleObject instance.</returns>
        public static ExampleObject ExitMethodPersistentExample(int one, int two)
        {
            Guid persistedGuid = logger.EnterMethod(xLogger.Params(one, two), true);

            //// method body
 
            logger.Trace("Standard log message");
            ExampleObject returnValue = new ExampleObject(1, "return", new double[] { 5.5 }.ToList());

            logger.ExitMethod(returnValue, persistedGuid);
            return returnValue;
        }

        /// <summary>
        /// Example of <see cref="xLogger.Checkpoint(object[], string[], Guid, string, string, int)"/> usage.
        /// </summary>
        /// <param name="one">A number.</param>
        /// <returns>Another number.</returns>
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

        /// <summary>
        /// Example of <see cref="xLogger.Exception(NLog.LogLevel, Exception, object[], string[], Guid, string, string, int)"/> usage.
        /// </summary>
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
                logger.Exception(LogLevel.Error, ex);
            }
            finally
            {
                logger.ExitMethod();
            }
        }

        /// <summary>
        /// Example of <see cref="xLogger.StackTrace(NLog.LogLevel, string, string, int)"/> usage.
        /// </summary>
        public static void StackTraceExample()
        {
            logger.StackTrace(LogLevel.Info);
        }

        /// <summary>
        /// Other examples.
        /// </summary>
        public static void OtherExamples()
        {
            logger.Multiline(LogLevel.Trace, "hello \n world!");
            logger.MultilineWrapped(LogLevel.Trace, new string[] { "hello", "again", "world!!" });
            logger.Separator(LogLevel.Trace);
            logger.Heading(LogLevel.Trace, "Hello world!");
            logger.SubHeading(LogLevel.Trace, "Hello world!");
            logger.SubSubHeading(LogLevel.Trace, "Hello world!");
        }

        #endregion
    }
}
