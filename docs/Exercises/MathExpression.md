`C# | Data Structures | Stack | Easy | 7 min | mca | Public`

# Math Expression

Perfecto, Haliam. Aquí tienes **todo el contenido del ejercicio**, limpio y listo para **copiar/pegar directamente en un archivo `.md`**.  
No incluyo nada fuera del texto visible en la captura.

---

# Math Expression

You are working on a calculator application where users can input a mathematical expression such as `5 * (3 + (2 - 4))`.

To correctly evaluate this expression, the application needs to validate the expression and ensure that the parentheses are balanced. The balanced parentheses are crucial for determining the order of operations and evaluating the expression correctly.

You have used an AI assistant to generate the following static method to check if the parentheses in an expression are balanced:

```csharp
static bool IsBalanced(string parentheses)
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
```

## What are correct statements for the given method?  
(Select all acceptable answers.)

- ☐ The method will validate an expression but not report the reason for any errors encountered.  
- ☐ If any mathematical operators with different precedence are present, the method will return.  
- ☐ We can remove HashSet(s) and only use the pairs Dictionary to check if the current character exists in pairs, achieving the same functionality and performance.  
- ☐ Using if-else instead of HashSet and Dictionary to check for brackets in the loop reduces the number of code changes needed to handle new bracket types.  
- ☐ The method will work correctly if the expression is made of numbers and mathematical operators.  
- ☐ The method will iterate through the entire string for expressions like `13 = 5 × (4 / [–3])`.

---

**Tags:** All, Code Review, Data Structures, Stack  
**Difficulty:** Easy  
**Duration:** 7 min  
**Type:** MCA  
**Score Distribution:** Not enough data for chart.

---

