namespace Nunit_GroupAssert
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    using NUnit.Framework;

    #endregion

    public class AssertGroup : IDisposable
    {
        private readonly IList<GroupedAssertion> _assertions;

        private bool _hasVerifiedAlready = false;

        public AssertGroup()
        {
            this._assertions = new List<GroupedAssertion>();
        }

        public void Dispose()
        {
            if (!_hasVerifiedAlready)
            {
                this.Verify();
            } 
        }

        public bool ShowFailingFilePath { get; set; }

        public void Add(
            Action assertion, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null,
            [CallerFilePath] string filePath = null)
        {
            this._assertions.Add(new GroupedAssertion(assertion, lineNumber, caller, filePath));
        }

        public void Verify()
        {
            this._hasVerifiedAlready = true;
            var exceptionCount = 0;
            var exceptionTrace = new StringBuilder();
            var hasThrown = false;

            exceptionTrace.AppendLine("Test failed because one or more assertions failed: ");

            foreach (var assertion in this._assertions)
            {
                try
                {
                    assertion.Execute();
                }
                catch (AssertionException exception)
                {
                    if (exceptionCount > 0) exceptionTrace.AppendLine();

                    exceptionTrace.AppendLine(
                        string.Format("{0})\t{1}", ++exceptionCount, FormatExceptionMessage(exception.Message)));

                    exceptionTrace.AppendLine(
                        string.Format("\tFrom {0} at line {1}", assertion.Caller, assertion.LineNumber));

                    if (this.ShowFailingFilePath)
                    {
                        exceptionTrace.AppendLine("\tIn file: " + assertion.FilePath);
                    }

                    hasThrown = true;
                }
            }

            if (hasThrown)
            {
                throw new AssertionException(exceptionTrace.ToString());
            }
        }

        private static string FormatExceptionMessage(string message)
        {
            message = message.Trim();
            return message.Replace("\r\n", "\r\n\t");
        }
    }
}
