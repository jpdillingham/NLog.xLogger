using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace xLogger.Tests
{
    /// <summary>
    /// Helper methods for <see cref="xLoggerTests"/>.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// The xLogger instance to use for the tests.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetCurrentClassLogger(typeof(xLogger));

        /// <summary>
        /// Initializes static members of the <see cref="Helpers"/> class.
        /// </summary>
        static Helpers()
        {
            // configure the logger with a debugger target
            LoggingConfiguration config = new LoggingConfiguration();
            DebuggerTarget debug = new DebuggerTarget();
            config.AddTarget("debug", debug);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, debug));
            LogManager.Configuration = config;
        }

        /// <summary>
        /// Helps the <see cref="xLoggerTests.EnterMethodType()"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        public static void EnterMethodTypeHelper<T>()
        {
            logger.EnterMethod(xLogger.TypeParams(typeof(T)));
        }

        /// <summary>
        /// Helps the <see cref="xLoggerTests.EnterMethodTypeParameters{T}(int)"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        /// <param name="one">The parameter.</param>
        public static void EnterMethodTypeParameterHelper<T>(int one)
        {
            logger.EnterMethod(xLogger.TypeParams(typeof(T)), xLogger.Params(one));
        }

        /// <summary>
        /// Helps the <see cref="xLoggerTests.EnterMethodTypePersistent"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        /// <param name="persistent">The persistence flag.</param>
        /// <returns>The Guid returned by the call.</returns>
        public static Guid EnterMethodTypePersistent<T>(bool persistent)
        {
            return logger.EnterMethod(xLogger.TypeParams(typeof(T)), true);
        }

        /// <summary>
        /// Helps the <see cref="xLoggerTests.EnterMethodTypeParametersPersistent"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        /// <param name="one">A parameter.</param>
        /// <param name="persistent">The persistence flag.</param>
        /// <returns>The Guid returned by the call.</returns>
        public static Guid EnterMethodTypeParametersPersistent<T>(int one, bool persistent)
        {
            return logger.EnterMethod(xLogger.TypeParams(typeof(T)), xLogger.Params(one, persistent), true);
        }
    }
}
