namespace Isu.Extra.Tools
{
    public class OgnpNameException : Exception
    {
        public OgnpNameException(string message, int value)
                : base(message)
        {
            Value = value;
        }

        public OgnpNameException(string message)
            : base(message)
        { }

        public int Value { get; }
    }
}
