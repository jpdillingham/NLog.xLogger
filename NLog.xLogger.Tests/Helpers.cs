/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄█    █▄
      █     ███    ███
      █     ███    ███      ▄█████  █          █████▄    ▄█████    █████   ▄█████
      █    ▄███▄▄▄▄███▄▄   ██   █  ██         ██   ██   ██   █    ██  ██   ██  ▀
      █   ▀▀███▀▀▀▀███▀   ▄██▄▄    ██         ██   ██  ▄██▄▄     ▄██▄▄█▀   ██
      █     ███    ███   ▀▀██▀▀    ██       ▀██████▀  ▀▀██▀▀    ▀███████ ▀███████
      █     ███    ███     ██   █  ██▌    ▄   ██        ██   █    ██  ██    ▄  ██
      █     ███    █▀      ███████ ████▄▄██  ▄███▀      ███████   ██  ██  ▄████▀
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Helper methods for unit tests.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The MIT License (MIT)
      █
      █  Copyright (c) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  Permission is hereby granted, free of charge, to any person obtaining a copy
      █  of this software and associated documentation files (the "Software"), to deal
      █  in the Software without restriction, including without limitation the rights
      █  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
      █  copies of the Software, and to permit persons to whom the Software is
      █  furnished to do so, subject to the following conditions:
      █
      █  The above copyright notice and this permission notice shall be included in all
      █  copies or substantial portions of the Software.
      █
      █  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
      █  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
      █  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
      █  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
      █  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
      █  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
      █  SOFTWARE.
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                            */

using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLog.xLogger.Tests
{
    /// <summary>
    ///     Helper methods for <see cref="xLoggerTests"/>.
    /// </summary>
    public class Helpers
    {
        #region Private Fields

        /// <summary>
        ///     The xLogger instance to use for the tests.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetCurrentClassLogger(typeof(xLogger));

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes static members of the <see cref="Helpers"/> class.
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

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Helps the <see cref="xLoggerTests.EnterMethodType()"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        public static void EnterMethodTypeHelper<T>()
        {
            logger.EnterMethod(xLogger.TypeParams(typeof(T)));
        }

        /// <summary>
        ///     Helps the <see cref="xLoggerTests.EnterMethodDirtyInput(int, int)"/> method.
        /// </summary>
        /// <typeparam name="T1">First Type parameter.</typeparam>
        /// <typeparam name="T2">Second Type parameter.</typeparam>
        public static void EnterMethodTypeMismatch<T1, T2>()
        {
            logger.EnterMethod(xLogger.TypeParams(typeof(T1)));
        }

        /// <summary>
        ///     Helps the <see cref="xLoggerTests.EnterMethodTypeParameters{T}(int)"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        /// <param name="one">The parameter.</param>
        public static void EnterMethodTypeParameterHelper<T>(int one)
        {
            logger.EnterMethod(xLogger.TypeParams(typeof(T)), xLogger.Params(one));
        }

        /// <summary>
        ///     Helps the <see cref="xLoggerTests.EnterMethodTypeParametersPersistent"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        /// <param name="one">A parameter.</param>
        /// <param name="persistent">The persistence flag.</param>
        /// <returns>The Guid returned by the call.</returns>
        public static Guid EnterMethodTypeParametersPersistent<T>(int one, bool persistent)
        {
            return logger.EnterMethod(xLogger.TypeParams(typeof(T)), xLogger.Params(one, persistent), true);
        }

        /// <summary>
        ///     Helps the <see cref="xLoggerTests.EnterMethodTypePersistent"/> method.
        /// </summary>
        /// <typeparam name="T">The Type parameter.</typeparam>
        /// <param name="persistent">The persistence flag.</param>
        /// <returns>The Guid returned by the call.</returns>
        public static Guid EnterMethodTypePersistent<T>(bool persistent)
        {
            return logger.EnterMethod(xLogger.TypeParams(typeof(T)), true);
        }

        #endregion Public Methods
    }
}