# Collections Cheatsheet

This cheatsheet explains the most commonly used .NET collections, their differences, and typical use cases. Use it alongside the `CollectionsDemo` examples in the library for hands-on practice.

---

## Quick Overview

- **Array (`T[]`)**: Fixed-size, contiguous memory. Fast indexing, low overhead.
- **List<T>**: Resizable array wrapper. Best general-purpose collection for ordered items you modify.
- **Dictionary<TKey,TValue>**: Key → value mapping with O(1) average lookup.
- **HashSet<T>**: Unordered set of unique items. Fast membership tests.
- **Queue<T>**: FIFO ordering. Use for work queues or breadth-first traversal.
- **Stack<T>**: LIFO ordering. Use for recursion emulation, undo stacks.
- **LinkedList<T>**: Doubly-linked list. Efficient insert/remove at arbitrary positions when you already have the node.
- **Span<T> / Memory<T>**: Lightweight view over contiguous memory. Use for high-performance, low-allocation scenarios.
- **ReadOnlyCollection<T>**: Wrapper that exposes a read-only view of a mutable list.
- **ImmutableList<T> / Immutable collections**: Persistent, thread-safe immutable collections (System.Collections.Immutable).
- **LINQ**: Declarative query API for projection, filtering, grouping and aggregation.
- **Concurrent collections**: Thread-safe collections in `System.Collections.Concurrent` (ConcurrentDictionary, ConcurrentQueue, ConcurrentBag, BlockingCollection).
- **SortedSet<T>**: Self-balancing BST (Red-Black Tree). Maintains sorted unique elements; supports range queries.

---

## When to choose which collection

- Array (`T[]`)
  - Use when size is fixed or known up-front.
  - Advantages: fastest indexing, best memory locality.
  - Disadvantages: cannot `Add`/`Remove` without allocating new array.

- `List<T>`
  - Use for a general ordered collection with frequent reads and occasional adds/removes at the end.
  - Advantages: random access, amortized O(1) append.
  - Avoid for many mid-list removals — prefer LinkedList when you have nodes.

- `Dictionary<TKey,TValue>`
  - Use when you need fast lookup by a key (e.g., id → object).
  - Choose `Dictionary` over repeated linear searches.
  - Consider `SortedDictionary` or `SortedList` if you need ordered keys.

- `HashSet<T>`
  - Use to enforce uniqueness (e.g., visited nodes, unique IDs).
  - Offers O(1) membership tests.

- `Queue<T>` / `Stack<T>`
  - Use `Queue` for producer/consumer FIFO scenarios or BFS algorithms.
  - Use `Stack` for DFS-like behavior or undo stacks.

- `LinkedList<T>`
  - Use when you need O(1) insert/remove and you already have a reference to the node.
  - Avoid if you need frequent random access — indexing is O(n).

- `SortedSet<T>`
  - Use when you need a set that is always sorted and supports range queries (`GetViewBetween`).
  - O(log n) add/remove/contains — slower than `HashSet` but gives ordered iteration for free.

- `Span<T>` / `Memory<T>`
  - Use for slicing arrays, stack-only temporary views, or parsing without allocations.
  - `Span<T>` cannot be stored on the heap or return from async methods; use `Memory<T>` when necessary.

- `ReadOnlyCollection<T>`
  - Use to publish a collection in an API while preventing callers from modifying it.
  - Note: underlying collection can still change if owner mutates it.

- Immutable collections (`ImmutableList<T>`, etc.)
  - Use when you need thread-safety by design, or when you want persistent data structures that produce new versions on change.

- Concurrent collections
  - Use `ConcurrentDictionary` for thread-safe maps without external locking.
  - Use `ConcurrentQueue` / `ConcurrentBag` for thread-safe queuing/producer-consumer patterns.
  - Use `BlockingCollection<T>` for a higher-level bounded producer/consumer queue with blocking semantics.

---

## Common LINQ patterns (brief)

- Filtering: `source.Where(x => predicate)`
- Projection: `source.Select(x => transform)`
- Grouping: `source.GroupBy(x => key)`
- Aggregation: `source.Sum()`, `source.Average()`, `source.Aggregate(...)`
- Ordering: `source.OrderBy(x => key)` (ordering causes materialization)

LINQ is great for readable queries; prefer `List<T>` or arrays as the underlying collection for performance-sensitive loops.

---

## Performance tips

- Prefer arrays for hot paths where memory locality matters.
- Use `List<T>` default capacity tuning when you know approximate sizes: `new List<T>(capacity)` to avoid repeated reallocation.
- Avoid boxing of value types: use `List<int>` instead of `ArrayList` (legacy).
- When enumerating a `Dictionary`, avoid modifying it during iteration.
- Use `StringBuilder` when concatenating many strings; `string.Join` is fine for small lists.

---

## Concurrency guidance

- Use concurrent collections to reduce the need for external locks in producer/consumer scenarios.
- `ConcurrentDictionary` supports `GetOrAdd` and `AddOrUpdate` atomic helpers.
- `ConcurrentBag` is optimized for scenarios where threads mostly add and occasionally take (unordered).
- `BlockingCollection<T>` wraps an `IProducerConsumerCollection<T>` and adds blocking/consuming semantics — useful for worker queues.

---

## Examples with performance notes

### List\<T\> — dynamic array

```csharp
List<string> fruits = new List<string>();
fruits.Add("Apple");       // O(1) amortized
fruits.Add("Banana");
string first = fruits[0];  // O(1) index access
fruits.Remove("Banana");   // O(n) shift
```

| Operation | Complexity | Why |
|---|---|---|
| Index access | O(1) | Direct array lookup |
| Search | O(n) | Linear scan |
| Insert / Remove (end) | O(1)* | Amortized; may resize |
| Insert / Remove (middle) | O(n) | Elements must shift |

---

### Dictionary\<TKey, TValue\> — hash table

```csharp
var employees = new Dictionary<int, string>();
employees.Add(101, "Alice");
employees[102] = "Bob";                          // O(1)
if (employees.TryGetValue(101, out var name))    // O(1)
    Console.WriteLine(name);
employees.Remove(102);                           // O(1)
```

| Operation | Complexity | Why |
|---|---|---|
| Key lookup | O(1) avg | Hash bucket access |
| Insert / Delete | O(1) avg | Hash + amortized resize |
| Memory | Higher | Buckets + entries + chains |

---

### HashSet\<T\> — unique items

```csharp
var tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
tags.Add("C#");   // O(1)
tags.Add("c#");   // ignored — duplicate
bool exists = tags.Contains("C#"); // O(1)
var other = new HashSet<string> { "Java", "C#" };
tags.IntersectWith(other); // keeps only "C#"
```

| Operation | Complexity | Why |
|---|---|---|
| Add | O(1) avg | Hash-based insertion |
| Contains | O(1) avg | Direct bucket check |
| Remove | O(1) avg | Hash-based deletion |

---

### Queue\<T\> — FIFO

```csharp
var tasks = new Queue<string>();
tasks.Enqueue("Process order"); // O(1)
tasks.Enqueue("Send email");
while (tasks.Count > 0)
    Console.WriteLine(tasks.Dequeue()); // O(1)
```

| Operation | Complexity | Why |
|---|---|---|
| Enqueue | O(1) | Tail pointer update |
| Dequeue | O(1) | Head pointer update |

---

### Stack\<T\> — LIFO

```csharp
var history = new Stack<string>();
history.Push("Homepage");  // O(1)
history.Push("Products");
string last = history.Pop(); // O(1) → "Products"
```

| Operation | Complexity | Why |
|---|---|---|
| Push | O(1) | Top pointer update |
| Pop | O(1) | Top pointer update |

---

### LinkedList\<T\> — node chain

```csharp
var playlist = new LinkedList<string>();
var first = playlist.AddFirst("Bohemian Rhapsody"); // O(1)
playlist.AddLast("Sweet Child O'Mine");             // O(1)
playlist.AddAfter(first, "Hotel California");       // O(1) — node known
for (var node = playlist.First; node != null; node = node.Next)
    Console.WriteLine(node.Value);
```

| Operation | Complexity | Why |
|---|---|---|
| Insert / Remove at node | O(1) | Only pointer updates |
| Index access | O(n) | Traverse from head/tail |

---

### SortedSet\<T\> — auto-sorted unique elements

```csharp
var scores = new SortedSet<int> { 85, 90, 75 }; // auto-sorted
scores.Add(95);
foreach (var s in scores.GetViewBetween(80, 100)) // range query
    Console.WriteLine(s); // 85, 90, 95
```

| Operation | Complexity | Why |
|---|---|---|
| Add / Remove | O(log n) | Red-Black Tree rebalance |
| Contains | O(log n) | Binary tree traversal |
| Range query | O(log n + k) | Tree view + k results |

---

### ConcurrentQueue consumption

```csharp
var cq = new ConcurrentQueue<string>();
cq.Enqueue("work1");
if (cq.TryDequeue(out var item)) Process(item);
```

---

## Collection comparison table

| Collection | Index Access | Search | Insert / Remove | Best For |
|---|---|---|---|---|
| `T[]` | O(1) | O(n) | N/A (fixed) | Fixed-size, performance-critical |
| `List<T>` | O(1) | O(n) | End O(1)\*, mid O(n) | General-purpose ordered data |
| `Dictionary<K,V>` | — | O(1) avg | O(1) avg | Key-value lookups |
| `HashSet<T>` | — | O(1) avg | O(1) avg | Uniqueness, membership tests |
| `Queue<T>` | — | — | O(1) | FIFO, BFS |
| `Stack<T>` | — | — | O(1) | LIFO, DFS, backtracking |
| `LinkedList<T>` | O(n) | O(n) | O(1) at node | Frequent mid-list changes |
| `SortedSet<T>` | — | O(log n) | O(log n) | Sorted unique items, ranges |
| `SortedDictionary<K,V>` | — | O(log n) | O(log n) | Sorted key-value pairs |

\* amortized

---

## Learning path suggestions

1. Start with `Array` and `List<T>` — understand indexing and resizing.
2. Learn `Dictionary` and `HashSet` for key-based lookups and uniqueness.
3. Practice `Queue`/`Stack` with BFS/DFS problems.
4. Try `LinkedList` when you need many insertions/removals in the middle.
5. Explore `Span<T>` for high-performance parsing.
6. Learn LINQ for expressive transformations and aggregations.
7. Study `System.Collections.Concurrent` for thread-safe patterns.

---

If you want, I can also:
- Add short unit tests demonstrating each use case.
- Generate a printable PDF from this cheatsheet.

File: docs/collections-cheatsheet.md

---

## BFS vs DFS (expanded)

- **BFS (Breadth-First Search)**:
  - Visits nodes by increasing distance from the start (level order).
  - Typical implementation uses a `Queue<T>` and a `visited` set/array.
  - Use when you need the shortest path in an unweighted graph or want to explore by levels.

- **DFS (Depth-First Search)**:
  - Explores as far as possible down one path, then backtracks.
  - Can be implemented recursively or with an explicit `Stack<T>`.
  - Use for problems like topological sorting, cycle detection, backtracking (puzzles).

Small BFS/DFS snippets (see `CollectionsDemo` for runnable examples):

```csharp
// BFS
var q = new Queue<int>();
var seen = new bool[n];
q.Enqueue(start); seen[start] = true;
while (q.Count > 0) {
  var u = q.Dequeue();
  foreach (var v in graph[u]) if (!seen[v]) { seen[v] = true; q.Enqueue(v); }
}

// DFS (recursive)
void Dfs(int u) {
  seen[u] = true;
  foreach (var v in graph[u]) if (!seen[v]) Dfs(v);
}
```

---

## Span<T> vs Memory<T> (expanded)

- **Span<T>**
  - A `ref struct` that represents a contiguous region of memory.
  - Stack-only: cannot be stored on the heap, cannot be a field of a class, and cannot cross `await`/`async` boundaries.
  - Best for high-performance, short-lived operations (parsing, slicing, temporary buffers).

- **Memory<T>**
  - Heap-friendly representation of memory that can be stored, captured and returned from methods.
  - Use `Memory<T>.Span` to get a `Span<T>` when you need fast synchronous access.
  - Use when an API needs to return or hold a view into memory.

Examples (see `CollectionsDemo.MemorySpanExample`):

```csharp
int[] arr = {1,2,3,4,5};
Memory<int> m = arr.AsMemory(1,3); // can be stored/returned
Span<int> s = arr.AsSpan(2,2);     // stack-only view
s[0] = 99; // modifies the backing array

// stackalloc for temporary buffers
Span<byte> buf = stackalloc byte[256];
```

Practical rules:
- Use `Span<T>` for tight, synchronous work where allocations matter.
- Use `Memory<T>` when the view must be stored, passed around, or survive async/await.

