
# ✅ C# 12 — Constructors, DI, Properties & Records (Ultimate Cheat Sheet)

---

# 🧱 1. Constructors (Regla base)

## ✅ Constructor Injection (BEST PRACTICE)

```csharp
public class OrderService
{
    private readonly IRepository repository;

    public OrderService(IRepository repository)
    {
        this.repository = repository;
    }
}
````

✔ Dependencias obligatorias  
✔ Inmutable después de crear el objeto  
✔ Testable

***

# 🧠 2. Dependency Injection (DI)

## ❌ Nunca hagas esto

```csharp
private readonly Repository repository = new Repository(); // BAD
```

👉 Problemas:

* Acoplamiento fuerte
* No se puede mockear
* Difícil de testear

***

## ✅ Haz esto (DIP — SOLID)

```csharp
public interface IRepository { }

public class Repository : IRepository { }

public class Service
{
    private readonly IRepository repository;

    public Service(IRepository repository)
    {
        this.repository = repository;
    }
}
```

***

## 🧪 Regla mental DI

> ❌ "Create dependencies"  
> ✅ "Receive dependencies"

***

# 🆕 3. C# 12 Primary Constructors

```csharp
public class OrderService(IRepository repository)
{
    public void Process()
    {
        repository.Save();
    }
}
```

✔ Menos boilerplate  
✔ Igual de seguro  
✔ Perfecto para servicios simples

***

# 🧠 4. `this` keyword

## ✅ Uso principal: desambiguar

```csharp
public Service(IRepository repository)
{
    this.repository = repository;
}
```

## ✅ Opcional: claridad

```csharp
this.repository.Save();
```

***

## 🧩 Regla

* Usa `this` **solo cuando haga falta**
* O siempre (si tu equipo lo exige)
* ❌ No mezclar estilos

***

# 🧱 5. Properties (`get`, `set`, `init`)

## ✅ Básico

```csharp
public string Name { get; set; }
```

***

## ✅ Buenas prácticas

### 🔒 1Here is a **clean, senior-level, production-grade cheatsheet** that unifies everything (constructors, DI, `this`, properties, records, best practices). Ready to copy into `.md`:

***

````md
# ✅ C# 12 Cheat Sheet — Constructors, DI, Properties & Records

---

# 🧱 1. Constructors

## Basic Constructor
```csharp
public class Service
{
    private readonly IRepository repository;

    public Service(IRepository repository)
    {
        this.repository = repository;
    }
}
````

## ✅ Rules

* Initialize dependencies here
* Keep constructors **simple**
* Do not put business logic inside

***

# 🧠 2. `this` keyword

## ✅ Purpose

Refers to the current instance

```csharp
this.repository = repository;
```

## ✅ When to use

* Disambiguation (MOST IMPORTANT)
* Optional for readability

## ❌ Avoid

* Using it everywhere without a rule

👉 Best practice:

> Use `this` only when needed OR follow a consistent team convention.

***

# 🔌 3. Dependency Injection (DI)

## ✅ Correct Pattern

```csharp
public class Service
{
    private readonly IRepository repository;

    public Service(IRepository repository)
    {
        this.repository = repository;
    }
}
```

## ❌ Anti-pattern

```csharp
private readonly IRepository repository = new Repository(); // BAD
```

***

## ✅ DI Rules

* ✅ Use constructor injection
* ✅ Depend on interfaces
* ✅ Mark dependencies as `readonly`
* ❌ Never `new` dependencies inside
* ❌ Do not use mutable dependencies

***

## 🧩 Mental Model

> "Classes should RECEIVE dependencies, not CREATE them"

***

# 🆕 4. C# 12 Primary Constructors

```csharp
public class Service(IRepository repository)
{
    public void Do()
    {
        repository.Save();
    }
}
```

## ✅ Benefits

* Less boilerplate
* Cleaner code

## ⚠️ Use when

* Simple services
* No complex constructor logic

***

# 🔒 5. Properties — Best Practices

## ✅ Basic

```csharp
public string Name { get; set; }
```

***

## ✅ Preferred Patterns

### 1. Encapsulation

```csharp
public string Name { get; private set; }
```

***

### 2. Immutability (modern)

```csharp
public string Name { get; init; }
```

***

### 3. Readonly collections

```csharp
public IReadOnlyList<string> Items { get; }
```

***

## ❌ Avoid

```csharp
public IRepository Repository { get; set; } // BAD
```

👉 Mutable dependencies = bugs

***

# 🧪 6. Fields vs Properties

## ✅ Dependencies → Fields

```csharp
private readonly IRepository repository;
```

✔️ Immutable  
✔️ Safe

***

## ✅ Data → Properties

```csharp
public string Name { get; init; }
```

***

# 📦 7. Records

## ✅ Use for DATA (not behavior)

```csharp
public record User(string Name, int Age);
```

***

## ✅ Features

### Value equality

```csharp
new User("A", 1) == new User("A", 1); // true
```

***

### Immutability

```csharp
user.Name = "X"; // ❌
```

***

### Copy (non-destructive mutation)

```csharp
var updated = user with { Name = "New" };
```

***

## ❌ Don’t use record for services

```csharp
public record AlertService(...) // ❌ WRONG
```

***

# ⚖️ 8. Class vs Record

| Use Case          | Use      |
| ----------------- | -------- |
| Business logic    | ✅ class  |
| Services / DI     | ✅ class  |
| DTOs / API models | ✅ record |
| Immutable data    | ✅ record |

***

# 🧠 9. Clean Patterns (Golden Rules)

## ✅ Dependencies

```csharp
private readonly IRepository repository;
```

***

## ✅ Constructor DI

```csharp
public Service(IRepository repository)
{
    this.repository = repository;
}
```

***

## ✅ Immutable data

```csharp
public record Config(string Url);
```

***

## ❌ Avoid

* `new` inside services
* public setters on dependencies
* mixing DI styles
* mutable global state

***

# 🔥 10. One-Pager Summary

## ✅ DO

* Use constructor DI
* Use interfaces
* Use readonly fields
* Use `record` for data
* Use `init` for immutability

## ❌ DON'T

* `new` dependencies inside classes
* Use `object` as return type
* Expose mutable properties
* Use record for behavior

***

# 🧠 Final Mental Model

```
Class  = Behavior + DI
Record = Data + Immutability
```

***

# 🚀 Senior Insight

> "If you can replace an implementation without modifying the class → your DI is correct."

```

---

If you want, I can generate a **real architecture example (API + Service + Repository + DTO with record + DI container)** — exactly what appears in senior interviews.
```
