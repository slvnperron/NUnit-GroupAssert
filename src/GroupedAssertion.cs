namespace Nunit_GroupAssert
{
    #region

    using System;

    #endregion

    public class GroupedAssertion
    {
        private readonly Action _action;

        public GroupedAssertion(Action action, int lineNumber, string caller, string filePath)
        {
            this._action = action;
            this.LineNumber = lineNumber;
            this.FilePath = filePath;
            this.Caller = caller;
        }

        public string Caller { get; private set; }

        public int LineNumber { get; private set; }

        public string FilePath { get; private set; }

        public void Execute()
        {
            this._action();
        }
    }
}
