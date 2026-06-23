namespace TestDome.Library
{
    public class MathExpression
    {
        public static bool IsBalanced(string parentheses)
        {
            Stack<char> stack = new Stack<char>();
            HashSet<char> opening = new HashSet<char> { '(', '[', '{' };
            HashSet<char> closing = new HashSet<char> { ')', ']', '}' };
            Dictionary<char, char> pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            foreach (char c in parentheses)
            {
                if (opening.Contains(c))
                    stack.Push(c);
                else if (closing.Contains(c))
                {
                    if (stack.Count == 0 || stack.Peek() != pairs[c])
                        return false;
                    stack.Pop();
                }
            }

            return stack.Count == 0;
        }

        public static bool IsBalancedII(string parentheses)
        {
            Stack<char> stack = new Stack<char>();
            Dictionary<char, char> pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            foreach (char c in parentheses) // 5 + (4*8)
            {
                if(pairs.ContainsValue(c))
                {
                    stack.Push(c);
                }
                else if (pairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Peek() != pairs[c])
                        return false;
                    stack.Pop();
                }
            }

            return stack.Count == 0;
        }
    }
}
