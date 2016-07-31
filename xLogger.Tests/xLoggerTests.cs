using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using Xunit;

namespace xLogger.Tests
{
    /// <summary>
    /// Tests for the <see cref="xLogger"/> class.
    /// </summary>
    public class xLoggerTests
    {
        #region Fields

        /// <summary>
        /// The xLogger instance to use for the tests.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetCurrentClassLogger(typeof(xLogger));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="xLoggerTests"/> class.
        /// </summary>
        public xLoggerTests()
        {
            // configure the logger with a debugger target
            LoggingConfiguration config = new LoggingConfiguration();
            DebuggerTarget debug = new DebuggerTarget();
            config.AddTarget("debug", debug);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, debug));
            LogManager.Configuration = config;
        }

        #endregion

        #region Methods

        #region Public Methods

        #region Public Instance Methods

        /// <summary>
        /// Tests <see cref="xLogger.PrunePersistedMethods(int)"/>.
        /// </summary>
        [Fact]
        public void PrunePersistedMethods()
        {
            Guid guid = logger.EnterMethod(true);

            Assert.NotEmpty(logger.PersistedMethods);

            int methodCount = logger.PersistedMethods.Count;

            Assert.Equal(methodCount, logger.PrunePersistedMethods(0));

            Assert.Empty(logger.PersistedMethods);

            logger.ExitMethod(guid);
        }

        /// <summary>
        /// Tests <see cref="xLogger.Multiline(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void MultilineScalar()
        {
            logger.Multiline(LogLevel.Debug, "Hello World!");
        }

        /// <summary>
        /// Tests <see cref="xLogger.Multiline(NLog.LogLevel, string[])"/>.
        /// </summary>
        [Fact]
        public void MultilineArray()
        {
            logger.Multiline(LogLevel.Debug, new string[] { "Hello!", "World!" });
        }

        /// <summary>
        /// Tests <see cref="xLogger.MultilineWrapped(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void MultilineWrappedScalar()
        {
            logger.MultilineWrapped(LogLevel.Debug, "Hello World!");
        }

        /// <summary>
        /// Tests <see cref="xLogger.MultilineWrapped(NLog.LogLevel, string[])"/>.
        /// </summary>
        [Fact]
        public void MultilineWrappedArray()
        {
            logger.MultilineWrapped(LogLevel.Debug, new string[] { "Hello!", "World!" });
        }

        /// <summary>
        /// Tests <see cref="xLogger.Separator(NLog.LogLevel)"/>.
        /// </summary>
        [Fact]
        public void Separator()
        {
            logger.Separator(LogLevel.Debug);
        }

        /// <summary>
        /// Tests <see cref="xLogger.Heading(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void Heading()
        {
            logger.Heading(LogLevel.Debug, "Hello World!");
        }

        /// <summary>
        /// Tests <see cref="xLogger.SubHeading(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void SubHeading()
        {
            logger.SubHeading(LogLevel.Debug, "Hello World!");
        }

        /// <summary>
        /// Tests <see cref="xLogger.SubSubHeading(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void SubSubHeading()
        {
            logger.SubSubHeading(LogLevel.Debug, "Hello World!");
        }

        #region EnterMethod

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethod()
        {
            logger.EnterMethod();
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(Type[], string, string, int)"/>
        /// </summary>
        [Fact]
        public void EnterMethodType()
        {
            Helpers.EnterMethodTypeHelper<int>();
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(object[], string, string, int)"/>.
        /// </summary>
        /// <param name="one">Parameter one.</param>
        /// <param name="two">Parameter two.</param>
        [Theory]
        [InlineData(1, 2)]
        public void EnterMethodParameters(int one, int two)
        {
            logger.EnterMethod(xLogger.Params(one, two));
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(bool, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethodPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            Assert.NotEqual(new Guid(), guid);
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(Type[], object[], string, string, int)"/>
        /// </summary>
        /// <param name="one">Parameter one.</param>
        [Theory]
        [InlineData(1)]
        public void EnterMethodTypeParameters(int one)
        {
            Helpers.EnterMethodTypeParameterHelper<int>(one);
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(Type[], bool, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethodTypePersistent()
        {
            Guid guid = Helpers.EnterMethodTypePersistent<int>(true);
            Assert.NotEqual(new Guid(), guid);
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(object[], bool, string, string, int)"/>.
        /// </summary>
        /// <param name="one">Parameter one.</param>
        /// <param name="two">Parameter two.</param>
        [Theory]
        [InlineData(1, 2)]
        public void EnterMethodParametersPersistent(int one, int two)
        {
            Guid guid = logger.EnterMethod(xLogger.Params(one, two), true);
            Assert.NotEqual(new Guid(), guid);
        }

        /// <summary>
        /// Tests <see cref="xLogger.EnterMethod(Type[], object[], bool, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethodTypeParametersPersistent()
        {
            Guid guid = Helpers.EnterMethodTypeParametersPersistent<int>(1, true);
            Assert.NotEqual(new Guid(), guid);
        }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
