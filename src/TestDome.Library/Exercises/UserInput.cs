namespace TestDome.Library
{
    public class UserInput
    {
        public class TextInput
        {
            string text = string.Empty;

            public virtual void Add(char c)
            {
                text += c;
            }

            public string GetValue()
            {
                return text;
            }
        }

        public class NumericInput : TextInput
        {
            string text = string.Empty;

            public override void Add(char c)
            {
                if (char.IsDigit(c))
                    text += c;
            }

            public string GetValue()
            {
                return text;
            }
        }
    }
}
