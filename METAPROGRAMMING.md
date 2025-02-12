# What is Metaprogramming?

Metaprogramming is a programming technique where programs have the ability to treat other programs as their data. This means a program can generate, modify, or analyze code dynamically at runtime. Metaprogramming is useful for scenarios like code generation, dynamic method invocation, or aspect-oriented programming.

## Types of Metaprogramming
Metaprogramming can be broadly classified into two types:
- **Compile-Time Metaprogramming**: This involves generating code during the compilation phase. Examples include C++ templates, Java annotations, and C# generics.
- **Runtime Metaprogramming**: This involves generating code during the execution phase. Examples include reflection, dynamic proxies, and code generation libraries.

## Benefits of Metaprogramming
Metaprogramming offers several benefits, such as:
- **Code Reusability**: Metaprogramming allows generating code dynamically, which can reduce redundancy and improve maintainability.
- **Dynamic Behavior**: Metaprogramming allows modifying the behavior of programs at runtime, which can be useful for dynamic configuration, aspect-oriented programming, or runtime optimization.
- **Domain-Specific Languages (DSLs)**: Metaprogramming can be used to create domain-specific languages tailored to specific problem domains, improving expressiveness and productivity.

## Example: Using Reflection in C#
In C#, one way to achieve metaprogramming is through reflection, which allows inspecting and modifying metadata of types at runtime.

### Example: Invoking a Method Dynamically Using Reflection

```csharp
using System;
using System.Reflection;

class Program
{
    public void SayHello()
    {
        Console.WriteLine("Hello from Metaprogramming!");
    }

    static void Main()
    {
        // Get the type of the Program class
        Type type = typeof(Program);

        // Create an instance of the Program class
        object instance = Activator.CreateInstance(type);

        // Get the method info for SayHello
        MethodInfo method = type.GetMethod("SayHello");

        // Invoke the SayHello method dynamically
        method.Invoke(instance, null);
    }
}

```

### Explanation:
1. Reflection is used to inspect the `Program` class.
2. An instance of `Program` is created dynamically using `Activator.CreateInstance()`.
3. The method `SayHello` is fetched using `GetMethod()`.
4. It is then invoked dynamically using `Invoke()`.

This is a simple example, but metaprogramming in C# can go much deeper with features like expression trees, code generation, and dynamic types (e.g., using `System.Reflection.Emit` or `Roslyn` for runtime compilation).

## Advanced Example: Runtime Code Generation with Roslyn
This example demonstrates how to compile and execute C# code at runtime using the Microsoft.CodeAnalysis (Roslyn) library.

### Steps:
1. Define C# code as a string.
2. Use Roslyn to compile it dynamically.
3. Execute the compiled code.

```csharp
using System;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System.Runtime.Loader;

class Program
{
    static void Main()
    {
        // Define C# code as a string
        string code = @"
        using System;

        public class DynamicClass
        {
            public void Execute()
            {
                Console.WriteLine(""Hello from dynamically generated code!"");
            }
        }";

        // Compile the code
        Assembly assembly = CompileCode(code);

        if (assembly != null)
        {
            // Get the dynamically created type
            Type dynamicType = assembly.GetType("DynamicClass");

            // Create an instance of the class
            object instance = Activator.CreateInstance(dynamicType);

            // Invoke the Execute method
            MethodInfo method = dynamicType.GetMethod("Execute");
            method.Invoke(instance, null);
        }
    }

    static Assembly CompileCode(string code)
    {
        // Create a syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        // Define references (include necessary assemblies)
        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
        };

        // Compile the code
        CSharpCompilation compilation = CSharpCompilation.Create(
            "DynamicAssembly",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        using (var ms = new System.IO.MemoryStream())
        {
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                Console.WriteLine("Compilation failed.");
                foreach (var diagnostic in result.Diagnostics)
                {
                    Console.WriteLine(diagnostic.ToString());
                }
                return null;
            }

            ms.Seek(0, System.IO.SeekOrigin.Begin);
            return AssemblyLoadContext.Default.LoadFromStream(ms);
        }
    }
}

```

### How This Works:
1. The C# code is stored as a string inside the program.
2. The Roslyn compiler (`Microsoft.CodeAnalysis`) dynamically compiles this code into an in-memory assembly.
3. The compiled assembly is loaded, and we use reflection to create an instance of the dynamically generated class (`DynamicClass`).
4. The `Execute` method is called dynamically, printing `"Hello from dynamically generated code!"`.

## Metaprogramming with Expression Trees in C#
Expression Trees allow dynamic code generation by creating and compiling code at runtime. Unlike Roslyn (which compiles entire C# code snippets), Expression Trees generate and execute lambda expressions dynamically.

### Example: Generating a Dynamic Function Using Expression Trees
We’ll generate a method at runtime that takes two numbers and returns their sum.

```csharp
using System;
using System.Linq.Expressions;

class Program
{
    static void Main()
    {
        // Define parameters (x, y)
        ParameterExpression paramX = Expression.Parameter(typeof(int), "x");
        ParameterExpression paramY = Expression.Parameter(typeof(int), "y");

        // Create the expression for (x + y)
        BinaryExpression body = Expression.Add(paramX, paramY);

        // Compile the expression into a delegate (Func<int, int, int>)
        var lambda = Expression.Lambda<Func<int, int, int>>(body, paramX, paramY).Compile();

        // Invoke the dynamically generated function
        int result = lambda(5, 7);
        Console.WriteLine($"5 + 7 = {result}");
    }
}

```
### How It Works:
1. Create Parameters → Define `x` and `y` as inputs using `Expression.Parameter()`.
2. Build an Expression → Construct the operation `x + y` using `Expression.Add()`.
3. Compile to a Delegate → Convert the expression into an executable function with `Expression.Lambda().Compile()`.
4. Invoke the Function → Call `lambda(5, 7)` dynamically, returning `12`.

### Why Use Expression Trees?
- **Performance Optimization** → Faster than Reflection because it compiles expressions into executable code.
- **Dynamic Code Generation** → Modify logic at runtime without recompiling.
- **Dynamic Query Builders** → Used in LINQ providers to translate queries into SQL.
- **Rule Engines** → Dynamically modify business logic.

## Use Cases for Runtime Code Generation
- Scripting Engines (e.g., executing user-defined scripts in applications)
- Dynamic Business Rules (changing logic without recompiling the main app)
- Plugin Systems (loading external code modules at runtime)
- Performance Optimizations (generating specialized code dynamically)

