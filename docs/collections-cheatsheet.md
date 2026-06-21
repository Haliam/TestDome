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

## Small examples (summary)

- Create a list and add items:

```csharp
var list = new List<string> { "A", "B" };
list.Add("C");
```

- Dictionary lookup with TryGetValue:

```csharp
var dict = new Dictionary<int, string>();
dict[1] = "one";
if (dict.TryGetValue(1, out var value)) Console.WriteLine(value);
```

- HashSet membership:

```csharp
var set = new HashSet<int> { 1, 2, 3 };
if (!set.Add(2)) Console.WriteLine("already present");
```

- Concurrent queue consumption:

```csharp
var cq = new ConcurrentQueue<string>();
cq.Enqueue("work1");
if (cq.TryDequeue(out var item)) Process(item);
```

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

