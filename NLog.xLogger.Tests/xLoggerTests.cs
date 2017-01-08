using System;
using System.Collections.Generic;
using NLog.Config;
using NLog.Targets;
using Utility.BigFont;
using Xunit;

namespace NLog.xLogger.Tests
{
    /// <summary>
    ///     Tests for the <see cref="xLogger"/> class.
    /// </summary>
    public class xLoggerTests
    {
        #region Fields

        /// <summary>
        ///     The xLogger instance to use for the tests.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetCurrentClassLogger(typeof(xLogger));

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="xLoggerTests"/> class.
        /// </summary>
        public xLoggerTests()
        {
            // configure the logger with a debugger target
            LoggingConfiguration config = new LoggingConfiguration();
            DebuggerTarget debug = new DebuggerTarget();
            config.AddTarget("debug", debug);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, debug));

            config.Variables.Add("xLogger.Indent", new Layouts.SimpleLayout("5"));

            LogManager.Configuration = config;
        }

        #endregion Constructors

        #region Methods

        #region Public Methods

        #region Public Instance Methods

        #region Constructor Tests

        /// <summary>
        ///     Tests the constructor for <see cref="xLogger"/>.
        /// </summary>
        [Fact]
        public void Constructor()
        {
            xLogger testLogger = (xLogger)LogManager.GetLogger("test", typeof(xLogger));

            Assert.IsType<xLogger>(testLogger);

            List<Tuple<Guid, DateTime>> persistedMethods = testLogger.PersistedMethods;

            Assert.Equal(0, persistedMethods.Count);
        }

        #endregion Constructor Tests

        #region Property Tests

        #region Prefix

        /// <summary>
        ///     Tests <see cref="xLogger.Prefix"/> with the default value.
        /// </summary>
        [Fact]
        public void Prefix()
        {
            string test = logger.Prefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Prefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void PrefixVariable()
        {
            SetVariable("xLogger.Prefix", "prefix");
            Assert.Equal("prefix", logger.Prefix);
        }

        #endregion Prefix

        #region Header

        /// <summary>
        ///     Tests <see cref="xLogger.Header"/> with the default value.
        /// </summary>
        [Fact]
        public void Header()
        {
            string test = logger.Header;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Header"/> with a variable value.
        /// </summary>
        [Fact]
        public void HeaderVariable()
        {
            SetVariable("xLogger.Header", "header");
            Assert.Equal("header", logger.Header);
        }

        #endregion Header

        #region EnterPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.EnterPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void EnterPrefix()
        {
            string test = logger.EnterPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void EnterPrefixVariable()
        {
            SetVariable("xLogger.EnterPrefix", "enterPrefix");
            Assert.Equal("enterPrefix", logger.EnterPrefix);
        }

        #endregion EnterPrefix

        #region ExitPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.ExitPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void ExitPrefix()
        {
            string test = logger.ExitPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExitPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void ExitPrefixVariable()
        {
            SetVariable("xLogger.ExitPrefix", "exitPrefix");
            Assert.Equal("exitPrefix", logger.ExitPrefix);
        }

        #endregion ExitPrefix

        #region CheckpointPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.CheckpointPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void CheckpointPrefix()
        {
            string test = logger.CheckpointPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.CheckpointPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void CheckpointPrefixVariable()
        {
            SetVariable("xLogger.CheckpointPrefix", "checkpointPrefix");
            Assert.Equal("checkpointPrefix", logger.CheckpointPrefix);
        }

        #endregion CheckpointPrefix

        #region ExceptionPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void ExceptionPrefix()
        {
            string test = logger.ExceptionPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void ExceptionPrefixVariable()
        {
            SetVariable("xLogger.ExceptionPrefix", "exceptionPrefix");
            Assert.Equal("exceptionPrefix", logger.ExceptionPrefix);
        }

        #endregion ExceptionPrefix

        #region StackTracePrefix

        /// <summary>
        ///     Tests <see cref="xLogger.StackTracePrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void StackTracePrefix()
        {
            string test = logger.StackTracePrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.StackTracePrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void StackTracePrefixVariable()
        {
            SetVariable("xLogger.StackTracePrefix", "StackTracePrefix");
            Assert.Equal("StackTracePrefix", logger.StackTracePrefix);
        }

        #endregion StackTracePrefix

        #region ExecutionDurationPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.ExecutionDurationPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void ExecutionDurationPrefix()
        {
            string test = logger.ExecutionDurationPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExecutionDurationPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void ExecutionDurationPrefixVariable()
        {
            SetVariable("xLogger.ExecutionDurationPrefix", "ExecutionDurationPrefix");
            Assert.Equal("ExecutionDurationPrefix", logger.ExecutionDurationPrefix);
        }

        #endregion ExecutionDurationPrefix

        #region LinePrefix

        /// <summary>
        ///     Tests <see cref="xLogger.LinePrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void LinePrefix()
        {
            string test = logger.LinePrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.LinePrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void LinePrefixVariable()
        {
            SetVariable("xLogger.LinePrefix", "LinePrefix");
            Assert.Equal("LinePrefix", logger.LinePrefix);
        }

        #endregion LinePrefix

        #region FinalLinePrefix

        /// <summary>
        ///     Tests <see cref="xLogger.FinalLinePrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void FinalLinePrefix()
        {
            string test = logger.FinalLinePrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.FinalLinePrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void FinalLinePrefixVariable()
        {
            SetVariable("xLogger.FinalLinePrefix", "FinalLinePrefix");
            Assert.Equal("FinalLinePrefix", logger.FinalLinePrefix);
        }

        #endregion FinalLinePrefix

        #region LinePrefixVariable

        /// <summary>
        ///     Tests <see cref="xLogger.LinePrefixVariable"/> with the default value.
        /// </summary>
        [Fact]
        public void LinePrefixVar()
        {
            string test = logger.LinePrefixVariable;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.LinePrefixVariable"/> with a variable value.
        /// </summary>
        [Fact]
        public void LinePrefixVarVariable()
        {
            SetVariable("xLogger.LinePrefixVariable", "LinePrefixVariable");
            Assert.Equal("LinePrefixVariable", logger.LinePrefixVariable);
        }

        #endregion LinePrefixVariable

        #region Footer

        /// <summary>
        ///     Tests <see cref="xLogger.Footer"/> with the default value.
        /// </summary>
        [Fact]
        public void Footer()
        {
            string test = logger.Footer;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Footer"/> with a variable value.
        /// </summary>
        [Fact]
        public void FooterVariable()
        {
            SetVariable("xLogger.Footer", "Footer");
            Assert.Equal("Footer", logger.Footer);
        }

        #endregion Footer

        #region InnerSeparator

        /// <summary>
        ///     Tests <see cref="xLogger.InnerSeparator"/> with the default value.
        /// </summary>
        [Fact]
        public void InnerSeparator()
        {
            string test = logger.InnerSeparator;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.InnerSeparator"/> with a variable value.
        /// </summary>
        [Fact]
        public void InnerSeparatorVariable()
        {
            SetVariable("xLogger.InnerSeparator", "InnerSeparator");
            Assert.Equal("InnerSeparator", logger.InnerSeparator);
        }

        #endregion InnerSeparator

        #region OuterSeparator

        /// <summary>
        ///     Tests <see cref="xLogger.OuterSeparator"/> with the default value.
        /// </summary>
        [Fact]
        public void OuterSeparator()
        {
            string test = logger.OuterSeparator;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.OuterSeparator"/> with a variable value.
        /// </summary>
        [Fact]
        public void OuterSeparatorVariable()
        {
            SetVariable("xLogger.OuterSeparator", "OuterSeparator");
            Assert.Equal("OuterSeparator", logger.OuterSeparator);
        }

        #endregion OuterSeparator

        #region ExceptionHeaderPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionHeaderPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void ExceptionHeaderPrefix()
        {
            string test = logger.ExceptionHeaderPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionHeaderPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void ExceptionHeaderPrefixVariable()
        {
            SetVariable("xLogger.ExceptionHeaderPrefix", "ExceptionHeaderPrefix");
            Assert.Equal("ExceptionHeaderPrefix", logger.ExceptionHeaderPrefix);
        }

        #endregion ExceptionHeaderPrefix

        #region ExceptionLinePrefix

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionLinePrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void ExceptionLinePrefix()
        {
            string test = logger.ExceptionLinePrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionLinePrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void ExceptionLinePrefixVariable()
        {
            SetVariable("xLogger.ExceptionLinePrefix", "ExceptionLinePrefix");
            Assert.Equal("ExceptionLinePrefix", logger.ExceptionLinePrefix);
        }

        #endregion ExceptionLinePrefix

        #region ExceptionFooterPrefix

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionFooterPrefix"/> with the default value.
        /// </summary>
        [Fact]
        public void ExceptionFooterPrefix()
        {
            string test = logger.ExceptionFooterPrefix;
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExceptionFooterPrefix"/> with a variable value.
        /// </summary>
        [Fact]
        public void ExceptionFooterPrefixVariable()
        {
            SetVariable("xLogger.ExceptionFooterPrefix", "ExceptionFooterPrefix");
            Assert.Equal("ExceptionFooterPrefix", logger.ExceptionFooterPrefix);
        }

        #endregion ExceptionFooterPrefix

        #region HeadingFont

        /// <summary>
        ///     Tests <see cref="xLogger.HeadingFont"/> with good values.
        /// </summary>
        /// <param name="value">Inline data.</param>
        [Theory]
        [InlineData("Block")]
        [InlineData("Graffiti")]
        public void HeadingFontGoodProperty(object value)
        {
            SetVariable("xLogger.HeadingFont", value.ToString());

            Font expected;
            Enum.TryParse(value.ToString(), out expected);

            if (expected == default(Font))
            {
                throw new System.Exception("The InlineData for the test wasn't parsed to a valid enum value for BigFont.Font.");
            }

            Assert.Equal(expected, logger.HeadingFont);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.HeadingFont"/> with a bad value.
        /// </summary>
        [Fact]
        public void HeadingFontBadProperty()
        {
            SetVariable("xLogger.HeadingFont", "test");

            Font test = logger.HeadingFont;

            Assert.Equal(Font.Block, test);
        }

        #endregion HeadingFont

        #region SubHeadingFont

        /// <summary>
        ///     Tests <see cref="xLogger.SubHeadingFont"/> with good values.
        /// </summary>
        /// <param name="value">Inline data.</param>
        [Theory]
        [InlineData("Block")]
        [InlineData("Graffiti")]
        public void SubHeadingFontGoodProperty(object value)
        {
            SetVariable("xLogger.SubHeadingFont", value.ToString());

            Font expected;
            Enum.TryParse(value.ToString(), out expected);

            if (expected == default(Font))
            {
                throw new System.Exception("The InlineData for the test wasn't parsed to a valid enum value for BigFont.Font.");
            }

            Assert.Equal(expected, logger.SubHeadingFont);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.SubHeadingFont"/> with a bad value.
        /// </summary>
        [Fact]
        public void SubHeadingFontBadProperty()
        {
            SetVariable("xLogger.SubHeadingFont", "test");

            Font test = logger.SubHeadingFont;

            Assert.Equal(Font.Block, test);
        }

        #endregion SubHeadingFont

        #region SubSubHeadingFont

        /// <summary>
        ///     Tests <see cref="xLogger.SubSubHeadingFont"/> with good values.
        /// </summary>
        /// <param name="value">Inline data.</param>
        [Theory]
        [InlineData("Block")]
        [InlineData("Graffiti")]
        public void SubSubHeadingFontGoodProperty(object value)
        {
            SetVariable("xLogger.SubSubHeadingFont", value.ToString());

            Font expected;
            Enum.TryParse(value.ToString(), out expected);

            if (expected == default(Font))
            {
                throw new System.Exception("The InlineData for the test wasn't parsed to a valid enum value for BigFont.Font.");
            }

            Assert.Equal(expected, logger.SubSubHeadingFont);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.SubSubHeadingFont"/> with a bad value.
        /// </summary>
        [Fact]
        public void SubSubHeadingFontBadProperty()
        {
            SetVariable("xLogger.SubSubHeadingFont", "test");

            Font test = logger.SubSubHeadingFont;

            Assert.Equal(Font.Block, test);
        }

        #endregion SubSubHeadingFont

        #region Indent

        /// <summary>
        ///     Tests <see cref="xLogger.Indent"/> with good values.
        /// </summary>
        /// <param name="value">Inline data.</param>
        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void IndentGoodProperty(object value)
        {
            SetVariable("xLogger.Indent", value.ToString());

            Assert.Equal(value, logger.Indent);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Indent"/> with a bad value.
        /// </summary>
        [Fact]
        public void IndentBadProperty()
        {
            SetVariable("xLogger.Indent", "three");

            int test = logger.Indent;

            Assert.Equal(3, test);
        }

        #endregion Indent

        #region AutoPruneEnabled

        /// <summary>
        ///     Tests <see cref="xLogger.AutoPruneEnabled"/> with good values.
        /// </summary>
        /// <param name="value">Inline data.</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AutoPruneEnabledGoodProperty(object value)
        {
            SetVariable("xLogger.AutoPruneEnabled", value.ToString());

            Assert.Equal(value, logger.AutoPruneEnabled);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.AutoPruneEnabled"/> with a bad value.
        /// </summary>
        [Fact]
        public void AutoPruneEnabledBadProperty()
        {
            SetVariable("xLogger.AutoPruneEnabled", "yes");

            bool test = logger.AutoPruneEnabled;

            Assert.Equal(true, test);
        }

        #endregion AutoPruneEnabled

        #region AutoPruneAge

        /// <summary>
        ///     Tests <see cref="xLogger.AutoPruneAge"/> with good values.
        /// </summary>
        /// <param name="value">Inline data.</param>
        [Theory]
        [InlineData(300)]
        [InlineData(15000)]
        public void AutoPruneAgeGoodProperty(object value)
        {
            SetVariable("xLogger.AutoPruneAge", value.ToString());

            Assert.Equal(value, logger.AutoPruneAge);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.AutoPruneAge"/> with a bad value.
        /// </summary>
        [Fact]
        public void AutoPruneAgeBadProperty()
        {
            SetVariable("xLogger.AutoPruneAge", "ten");

            int test = logger.AutoPruneAge;

            Assert.Equal(300, test);
        }

        #endregion AutoPruneAge

        #endregion Property Tests

        #region Method Tests

        #region Static Methods

        /// <summary>
        ///     Tests <see cref="xLogger.Params()"/>.
        /// </summary>
        [Fact]
        public void Params()
        {
            object[] test = xLogger.Params(1, 2);
            Assert.Equal(2, test.Length);
            Assert.Equal(1, test[0]);
            Assert.Equal(2, test[1]);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.TypeParams()"/>.
        /// </summary>
        [Fact]
        public void TypeParams()
        {
            Type[] test = xLogger.TypeParams(typeof(int), typeof(string));
            Assert.Equal(2, test.Length);
            Assert.Equal(typeof(int), test[0]);
            Assert.Equal(typeof(string), test[1]);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Vars()"/>.
        /// </summary>
        [Fact]
        public void Vars()
        {
            object[] test = xLogger.Vars(1, 2);
            Assert.Equal(2, test.Length);
            Assert.Equal(1, test[0]);
            Assert.Equal(2, test[1]);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Names()"/>.
        /// </summary>
        [Fact]
        public void Names()
        {
            string[] test = xLogger.Names("one", "two");
            Assert.Equal(2, test.Length);
            Assert.Equal("one", test[0]);
            Assert.Equal("two", test[1]);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exclude()"/>.
        /// </summary>
        [Fact]
        public void Exclude()
        {
            xLogger.ExcludedParam test = xLogger.Exclude();
            Assert.IsType<xLogger.ExcludedParam>(test);
        }

        #endregion Static Methods

        #region Instance Methods

        /// <summary>
        ///     Tests <see cref="xLogger.PrunePersistedMethods(int)"/>.
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
        ///     Tests <see cref="xLogger.Multiline(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void MultilineScalar()
        {
            logger.Multiline(LogLevel.Debug, "Hello World!");
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Multiline(NLog.LogLevel, string[])"/>.
        /// </summary>
        [Fact]
        public void MultilineArray()
        {
            logger.Multiline(LogLevel.Debug, new string[] { "Hello!", "World!" });

            LogManager.DisableLogging();
            logger.Multiline(LogLevel.Debug, new string[] { "Hello!", "World!" });
            LogManager.EnableLogging();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.MultilineWrapped(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void MultilineWrappedScalar()
        {
            logger.MultilineWrapped(LogLevel.Debug, "Hello World!");

            LogManager.DisableLogging();
            logger.MultilineWrapped(LogLevel.Debug, "Hello World!");
            LogManager.EnableLogging();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.MultilineWrapped(NLog.LogLevel, string[])"/>.
        /// </summary>
        [Fact]
        public void MultilineWrappedArray()
        {
            logger.MultilineWrapped(LogLevel.Debug, new string[] { "Hello!", "World!" });
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Separator(NLog.LogLevel)"/>.
        /// </summary>
        [Fact]
        public void Separator()
        {
            logger.Separator(LogLevel.Debug);

            LogManager.DisableLogging();
            logger.Separator(LogLevel.Debug);
            LogManager.EnableLogging();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Heading(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void Heading()
        {
            logger.Heading(LogLevel.Debug, "Hello World!");
            logger.Heading(LogLevel.Debug, "Hello World!", true);

            LogManager.DisableLogging();
            logger.Heading(LogLevel.Debug, "Hello World!");
            LogManager.EnableLogging();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.SubHeading(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void SubHeading()
        {
            logger.SubHeading(LogLevel.Debug, "Hello World!");
            logger.SubHeading(LogLevel.Debug, "Hello World!", true);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.SubSubHeading(NLog.LogLevel, string)"/>.
        /// </summary>
        [Fact]
        public void SubSubHeading()
        {
            logger.SubSubHeading(LogLevel.Debug, "Hello World!");
        }

        #region EnterMethod

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethod()
        {
            logger.EnterMethod();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(Type[], string, string, int)"/>
        /// </summary>
        [Fact]
        public void EnterMethodType()
        {
            Helpers.EnterMethodTypeHelper<int>();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(object[], string, string, int)"/>.
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
        ///     Tests <see cref="xLogger.EnterMethod(bool, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethodPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            Assert.NotEqual(new Guid(), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(Type[], object[], string, string, int)"/>
        /// </summary>
        /// <param name="one">Parameter one.</param>
        [Theory]
        [InlineData(1)]
        public void EnterMethodTypeParameters(int one)
        {
            Helpers.EnterMethodTypeParameterHelper<int>(one);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(Type[], bool, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethodTypePersistent()
        {
            Guid guid = Helpers.EnterMethodTypePersistent<int>(true);
            Assert.NotEqual(new Guid(), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(object[], bool, string, string, int)"/>.
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
        ///     Tests <see cref="xLogger.EnterMethod(Type[], object[], bool, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void EnterMethodTypeParametersPersistent()
        {
            Guid guid = Helpers.EnterMethodTypeParametersPersistent<int>(1, true);
            Assert.NotEqual(new Guid(), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.EnterMethod(string, string, int)"/> to ensure no exceptions are thrown if the input is "dirty".
        /// </summary>
        /// <param name="one">Parameter one.</param>
        /// <param name="two">Parameter two.</param>
        [Theory]
        [InlineData(1, 2)]
        public void EnterMethodDirtyInput(int one, int two)
        {
            Helpers.EnterMethodTypeMismatch<int, int>();  // tests for type parameter count mismatch
            logger.EnterMethod(xLogger.Params(one)); // tests for parameter count mismatch

            logger.EnterMethod(xLogger.Params(one, xLogger.Exclude())); // tests for excluded parameters

            string test = "test";
            logger.EnterMethod(xLogger.Params(one, test)); // tests for parameter type mismatch

            logger.EnterMethod(xLogger.Params(one, null)); // tests for null handling
        }

        #endregion EnterMethod

        #region ExitMethod

        /// <summary>
        ///     Tests <see cref="xLogger.ExitMethod(string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExitMethod()
        {
            logger.ExitMethod();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExitMethod(object, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExitMethodReturn()
        {
            logger.ExitMethod(true);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExitMethod(Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExitMethodPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            logger.ExitMethod(guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExitMethod(Guid, string, string, int)"/> with a bad Guid.
        /// </summary>
        [Fact]
        public void ExitMethodPersistentBadGuid()
        {
            Guid guid = Guid.NewGuid();
            logger.ExitMethod(guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExitMethod(object, Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExitMethodPersistentReturn()
        {
            Guid guid = logger.EnterMethod(true);

            logger.ExitMethod(true, guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.ExitMethod(string, string, int)"/> to ensure no exceptions are throw if the input is "dirty".
        /// </summary>
        [Fact]
        public void ExitMethodDirtyInput()
        {
            logger.ExitMethod(null, new Guid(), "caller", "path", 0);
        }

        #endregion ExitMethod

        #region Checkpoint

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, string, int)"/>.
        /// </summary>
        [Fact]
        public void Checkpoint()
        {
            logger.Checkpoint();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string)"/>.
        /// </summary>
        [Fact]
        public void CheckpointNamed()
        {
            logger.Checkpoint("test");
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointNamed2()
        {
            logger.Checkpoint("test", "caller", "filePath", 0);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            logger.Checkpoint(guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(Guid, string, string, int)"/> with a bad Guid.
        /// </summary>
        [Fact]
        public void CheckpointPersistentBad()
        {
            Guid guid = Guid.NewGuid();
            logger.Checkpoint(guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointNamedPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            logger.Checkpoint("test", guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(object[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointVariables()
        {
            int one = 1;
            int two = 2;
            logger.Checkpoint(xLogger.Vars(one, two));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, object[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointNamedVariables()
        {
            int one = 1;
            int two = 2;
            logger.Checkpoint("test", xLogger.Vars(one, two));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(object[], Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointVariablesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Checkpoint(xLogger.Vars(one, two), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, object[], Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointNamedVariablesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Checkpoint("test", xLogger.Vars(one, two), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(object[], string[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointVariablesNames()
        {
            int one = 1;
            int two = 2;
            logger.Checkpoint(xLogger.Vars(one, two), xLogger.Names("one", "two"));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, object[], string[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointNamedVariableNames()
        {
            int one = 1;
            int two = 2;
            logger.Checkpoint("test", xLogger.Vars(one, two), xLogger.Names("one", "two"));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(object[], string[], Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void CheckpointVariablesNamesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Checkpoint(xLogger.Vars(one, two), xLogger.Names("one", "two"), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string, object[], string[], Guid, string, string, int)"/>
        /// </summary>
        [Fact]
        public void CheckpointNamedVariableNamesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Checkpoint("test", xLogger.Vars(one, two), xLogger.Names("one", "two"), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Checkpoint(string)"/> to ensure no exceptions are thrown with "dirty" input.
        /// </summary>
        [Fact]
        public void CheckpointDirtyInput()
        {
            int one = 1;
            int two = 2;
            logger.Checkpoint("test", xLogger.Vars(one, two), xLogger.Names("one")); // tests name/var count mismatch
        }

        #endregion Checkpoint

        #region Exception

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(System.Exception, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void Exception()
        {
            logger.Exception(new Exception());
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(LogLevel, System.Exception, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionLevel()
        {
            logger.Exception(LogLevel.Debug, new Exception());

            Exception inner = new System.Exception();

            logger.Exception(LogLevel.Debug, new Exception("", inner));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(System.Exception, Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionPersistent()
        {
            Guid guid = logger.EnterMethod(true);

            logger.Exception(new Exception(), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(LogLevel, System.Exception, Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionLevelPersistent()
        {
            Guid guid = logger.EnterMethod(true);

            logger.Exception(LogLevel.Debug, new Exception(), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(System.Exception, object[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionVariables()
        {
            int one = 1;
            int two = 2;
            logger.Exception(new Exception(), xLogger.Vars(one, two));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(LogLevel, System.Exception, object[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionLevelVariables()
        {
            int one = 1;
            int two = 2;
            logger.Exception(LogLevel.Debug, new Exception(), xLogger.Vars(one, two));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(System.Exception, object[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionVariablesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Exception(new Exception(), xLogger.Vars(one, two), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(LogLevel, System.Exception, object[], Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionLevelVariablesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Exception(LogLevel.Debug, new Exception(), xLogger.Vars(one, two), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(System.Exception, object[], string[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionVariablesNames()
        {
            int one = 1;
            int two = 2;
            logger.Exception(new Exception(), xLogger.Vars(one, two), xLogger.Names("one", "two"));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(LogLevel, System.Exception, object[], string[], string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionLevelVariablesNames()
        {
            int one = 1;
            int two = 2;
            logger.Exception(LogLevel.Debug, new Exception(), xLogger.Vars(one, two), xLogger.Names("one", "two"));
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(System.Exception, object[], string[], Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionVariablesNamesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Exception(new Exception(), xLogger.Vars(one, two), xLogger.Names("one", "two"), guid);
        }

        /// <summary>
        ///     Tests <see cref="xLogger.Exception(LogLevel, System.Exception, object[], string[], Guid, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void ExceptionLevelVariablesNamesPersistent()
        {
            Guid guid = logger.EnterMethod(true);
            int one = 1;
            int two = 2;
            logger.Exception(LogLevel.Debug, new Exception(), xLogger.Vars(one, two), xLogger.Names("one", "two"), guid);
        }

        #endregion Exception

        #region Stack Trace

        /// <summary>
        ///     Tests <see cref="xLogger.StackTrace(string, string, int)"/>.
        /// </summary>
        [Fact]
        public void StackTrace()
        {
            logger.StackTrace();
        }

        /// <summary>
        ///     Tests <see cref="xLogger.StackTrace(LogLevel, string, string, int)"/>.
        /// </summary>
        [Fact]
        public void StackTraceLevel()
        {
            logger.StackTrace(LogLevel.Debug);
        }

        #endregion Stack Trace

        /// <summary>
        ///     Tests each method to ensure no exceptions are thrown if the Trace logging level is disabled.
        /// </summary>
        [Fact]
        public void TraceLevelDisabled()
        {
            // disable tracing
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                rule.DisableLoggingForLevel(LogLevel.Trace);
            }

            LogManager.ReconfigExistingLoggers();

            Assert.Equal(false, logger.IsEnabled(LogLevel.Trace));

            logger.EnterMethod();
            logger.Checkpoint();
            logger.Exception(LogLevel.Trace, new Exception());
            logger.StackTrace();
            logger.ExitMethod();

            // re-enable tracing
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                rule.EnableLoggingForLevel(LogLevel.Trace);
            }

            LogManager.ReconfigExistingLoggers();

            Assert.Equal(true, logger.IsEnabled(LogLevel.Trace));
        }

        #endregion Instance Methods

        #endregion Method Tests

        #endregion Public Instance Methods

        #endregion Public Methods

        #region Private Methods

        #region Private Instance Methods

        /// <summary>
        ///     Sets the specified variable in the NLog configuration to the specified value.
        /// </summary>
        /// <param name="variable">The variable to set.</param>
        /// <param name="value">The value to which to set the variable.</param>
        private void SetVariable(string variable, string value)
        {
            if (LogManager.Configuration.Variables.ContainsKey(variable))
            {
                LogManager.Configuration.Variables[variable] = new Layouts.SimpleLayout(value);
            }
            else
            {
                LogManager.Configuration.Variables.Add(variable, new Layouts.SimpleLayout(value));
            }
        }

        #endregion Private Instance Methods

        #endregion Private Methods

        #endregion Methods
    }
}