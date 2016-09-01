using Xunit;

namespace NLog.xLogger.Tests
{
    /// <summary>
    ///     Tests for the <see cref="xLogManager"/> class.
    /// </summary>
    public class xLogManagerTests
    {
        #region Methods

        #region Public Methods

        #region Public Instance Methods

        /// <summary>
        ///     Tests the <see cref="xLogManager.GetxLogger(string)"/>.
        /// </summary>
        [Fact]
        public void GetxLogger()
        {
            xLogger test = xLogManager.GetxLogger("test");

            Assert.IsType(typeof(xLogger), test);
            Assert.Equal("test", test.Name);            
        }

        /// <summary>
        ///     Tests the <see cref="xLogManager.GetCurrentClassxLogger"/>.
        /// </summary>
        [Fact]
        public void GetCurrentClassxLogger()
        {
            xLogger test = xLogManager.GetCurrentClassxLogger();

            Assert.IsType(typeof(xLogger), test);
            Assert.Equal(GetType().FullName, test.Name);
        }

        #endregion

        #endregion

        #endregion
    }
}
