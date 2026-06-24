`C# | Config | Easy | ? min | code | Public`


# App Settings

The C# method GetAppSettings returns the config settings for the app. The code below shows the Main method using it:

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static object GetAppSettings(bool detailed)
    {
        if (detailed)
        {
            return new Dictionary<string, string>
            {
                ["os"] = Environment.OSVersion.ToString(),
                ["machine"] = Environment.MachineName,
                ["user"] = Environment.UserName
            };
        }

        return Environment.MachineName;
    }

    static void Main()
    {
        var appSettings = GetAppSettings(true);
        Console.WriteLine(appSettings["os"]);
    }
}
````

The code in the Main method causes the following compile time error:

```
Main.cs(24,38): error CS0021: Cannot apply indexing with [] to an expression of type 'object'
```

Explain in short why the compile error occurred in the Main method.

(An answer up to 40 words is sufficient)

````

---

## 🧠 Explanation

### ❌ What is the problem?

`GetAppSettings` is declared as:

```csharp
static object GetAppSettings(bool detailed)
````

That means it **always returns `object`**, even though internally it may return:

* A `Dictionary<string, string>` (when `detailed = true`)
* A `string` (when `detailed = false`)

***

### 💥 Why the error happens

In `Main`:

```csharp
var appSettings = GetAppSettings(true);
Console.WriteLine(appSettings["os"]);
```

* `appSettings` is inferred as **type `object`**
* `object` **does NOT support indexing (`[]`)**
* Only types like `Dictionary<string, string>` support indexing

👉 Therefore:

```
Cannot apply indexing with [] to an expression of type 'object'
```

***

### ✅ Root cause (short answer)

> The method returns `object`, so the result is treated as `object`. Since `object` does not support indexing, accessing `appSettings["os"]` causes a compile-time error.

***

### ✅ How to fix it

#### Option 1: Cast explicitly

```csharp
var appSettings = (Dictionary<string, string>)GetAppSettings(true);
Console.WriteLine(appSettings["os"]);
```

#### Option 2: Change return type (better design)

```csharp
static Dictionary<string, string> GetAppSettings(bool detailed)
```

(or use a proper model / interface instead of `object`)

***

### 🧩 Key takeaway

Using `object` as a return type removes type safety and requires casting before using specific features like indexing.

***
