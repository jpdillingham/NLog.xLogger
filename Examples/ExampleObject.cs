using System.Collections.Generic;

namespace xLogger.Examples
{
    /// <summary>
    /// Example object.
    /// </summary>
    public class ExampleObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleObject"/> class.
        /// </summary>
        /// <param name="num">An integer.</param>
        /// <param name="str">A string.</param>
        /// <param name="list">A list of doubles.</param>
        public ExampleObject(int num, string str, List<double> list)
        {
            Num = num;
            Str = str;
            List = list;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a number.
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// Gets or sets a string.
        /// </summary>
        public string Str { get; set; }

        /// <summary>
        /// Gets or sets a list of doubles.
        /// </summary>
        public List<double> List { get; set; }

        #endregion
    }
}
