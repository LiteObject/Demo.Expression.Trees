# "Code is Data" vs "Data is Code"

"Code is Data" and "Data is Code" are two concepts that highlight the relationship between code and data in programming. While they may seem similar, they have distinct meanings and implications in different programming paradigms. Let's explore these concepts and their applications in various programming languages.

## Code is Data (Homoiconicity)
"Code is Data" is a concept that treats code as data, which can be manipulated and transformed programmatically. This concept is often used in metaprogramming, where code is generated, analyzed, or transformed at runtime. By treating code as data, developers can create more flexible and dynamic applications.

### Example in Lisp:
```lisp
(define myCode '(+ 2 3))
(eval myCode)  ; Output: 5

```
Here, `myCode` is just a list `((+ 2 3))`, but when passed to `eval`, it is treated as executable code.

### Example in C# (Expression Trees):
```csharp
// Define an expression tree for (x + y)
Expression<Func<int, int, int>> expr = (x, y) => x + y;

// Convert expression into executable code
var func = expr.Compile();

// Execute dynamically created function
Console.WriteLine(func(3, 4)); // Output: 7
```
Here, `expr` is just data, but `Compile()` turns it into executable code.

## Data is Code (Eval & Runtime Code Execution)
On the other hand, "Data is Code" refers to the idea that data can be interpreted as code and executed. This concept is commonly used in languages or systems that support runtime evaluation of code from data sources. For example, in Lisp, data structures can be interpreted as code and executed dynamically.

### Example in Python:
```python
code = "print('Hello from generated code!')"
exec(code)  # Output: Hello from generated code!
```

### Example in C# (Roslyn):
```csharp
static async System.Threading.Tasks.Task Main()
{
    string code = "Console.WriteLine(\"Hello from dynamic code!\");";
    await CSharpScript.EvaluateAsync(code);
}
```

While both concepts have their place, it's important to understand the differences between them. "Code is Data" emphasizes the programmatic manipulation of code, while "Data is Code" focuses on the interpretation and execution of data as code.

## Key Differences Between "Code is Data" & "Data is Code"
| Concept | Meaning | Example |
| --- | --- |---|
| Code is Data | Code can be represented and manipulated as data | Lisp (treating code as lists), C# Expression Trees |
| Data is Code | Data (e.g., strings) can be executed as code | JavaScript `eval()`, C# Roslyn, Python `exec()` |

---
To summarize, "Code is Data" is about treating code as a manipulable entity, while "Data is Code" is about interpreting data as executable code. Both concepts are powerful tools in the hands of skilled developers and can be used to create dynamic and flexible applications.
