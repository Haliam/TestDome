using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System.Linq;

namespace TestDome.Library
{
    /// <summary>
    /// Collection examples: one method per collection type that returns small, deterministic outputs.
    /// Use these from the console app or unit tests to learn common operations and patterns.
    /// </summary>
    public static class CollectionsDemo
    {
        /// <summary>
        /// Run all demo methods and return aggregated outputs.
        /// </summary>
        public static IEnumerable<string> RunAll()
        {
            foreach (var s in ArrayExample()) yield return $"Array => {s}";
            foreach (var s in ListExample()) yield return $"List => {s}";
            foreach (var s in DictionaryExample()) yield return $"Dictionary => {s}";
            foreach (var s in HashSetExample()) yield return $"HashSet => {s}";
            foreach (var s in QueueExample()) yield return $"Queue => {s}";
            foreach (var s in StackExample()) yield return $"Stack => {s}";
            foreach (var s in LinkedListExample()) yield return $"LinkedList => {s}";
            foreach (var s in SpanExample()) yield return $"Span => {s}";
            foreach (var s in ReadOnlyCollectionExample()) yield return $"ReadOnlyCollection => {s}";
            foreach (var s in ImmutableListExample()) yield return $"ImmutableList => {s}";
            foreach (var s in ConcatenateExample()) yield return $"Concatenate => {s}";
            foreach (var s in LinqExamples()) yield return $"LINQ => {s}";
            foreach (var s in ConcurrentExamples()) yield return $"Concurrent => {s}";
            foreach (var s in BfsExample()) yield return $"BFS => {s}";
            foreach (var s in DfsExample()) yield return $"DFS => {s}";
            foreach (var s in MemorySpanExample()) yield return $"Memory/Span => {s}";
        }

        /// <summary>
        /// Simple array usage: instantiate, modify an element and return the snapshot.
        /// </summary>
        public static IEnumerable<string> ArrayExample()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };
            // Arrays are fixed-size; you can update elements by index.
            arr[0] = 10;
            // common operations: Length, IndexOf (via Array.IndexOf), copy, clear
            yield return "Length: " + arr.Length;
            yield return "IndexOf(3): " + Array.IndexOf(arr, 3);
            var copy = new int[arr.Length];
            Array.Copy(arr, copy, arr.Length);
            yield return "Copy: " + string.Join(", ", copy);
            Array.Clear(copy, 1, 2); // clear a range
            yield return "After Clear range: " + string.Join(", ", copy);
        }

        /// <summary>
        /// List example: add/remove/sort operations.
        /// </summary>
        public static IEnumerable<string> ListExample()
        {
            var list = new List<string> { "Ana", "Luis", "Carlos" };
            list.Add("Haliam");        // append
            list.Insert(1, "Bea");    // insert at index
            list.Remove("Luis");      // remove by value
            list.RemoveAt(2);          // remove at index (after insert)
            var contains = list.Contains("Ana");
            list.Sort();               // in-place sort
            yield return "Count: " + list.Count;
            yield return "Contains(Ana): " + contains;
            yield return "IndexOf(Haliam): " + list.IndexOf("Haliam");
            yield return string.Join(", ", list);
            list.Clear();
            yield return "After Clear Count: " + list.Count;
        }

        /// <summary>
        /// Dictionary example: indexer, TryGetValue and ordered output for determinism.
        /// </summary>
        public static IEnumerable<string> DictionaryExample()
        {
            var dict = new Dictionary<int, string> { [1] = "Admin", [2] = "User", [3] = "Guest" };
            dict[4] = "SuperUser";    // add/update via indexer
            // TryGetValue and ContainsKey
            if (dict.TryGetValue(2, out var role)) yield return $"Key 2 => {role}";
            yield return "ContainsKey(5): " + dict.ContainsKey(5);

            // Keys / Values / Remove
            yield return "Keys: " + string.Join(", ", dict.Keys.OrderBy(k => k));
            dict.Remove(3);
            yield return "After Remove(3): " + string.Join(" | ", dict.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}:{kvp.Value}"));
        }

        /// <summary>
        /// HashSet example: uniqueness and ordering when presenting results.
        /// </summary>
        public static IEnumerable<string> HashSetExample()
        {
            var set = new HashSet<int> { 1, 2, 3, 3, 4 };
            // duplicate '3' is ignored
            set.Add(5);
            yield return "Count: " + set.Count;
            yield return "Contains(3): " + set.Contains(3);
            // set operations
            var other = new HashSet<int> { 3, 4, 6 };
            var union = new HashSet<int>(set);
            union.UnionWith(other);
            yield return "Union: " + string.Join(", ", union.OrderBy(x => x));
            var intersect = new HashSet<int>(set);
            intersect.IntersectWith(other);
            yield return "Intersect: " + string.Join(", ", intersect.OrderBy(x => x));
            set.Remove(2);
            yield return "After Remove(2): " + string.Join(", ", set.OrderBy(x => x));
        }

        /// <summary>
        /// Queue example: FIFO semantics demonstrated with Enqueue/Dequeue.
        /// </summary>
        public static IEnumerable<string> QueueExample()
        {
            var q = new Queue<string>(new[] { "first", "second", "third" });
            q.Enqueue("fourth");
            // Dequeue removes and returns items in FIFO order
            yield return "Peek: " + q.Peek();
            yield return "Count before dequeue: " + q.Count;
            yield return "Dequeue1: " + q.Dequeue();
            yield return "Dequeue2: " + q.Dequeue();
            q.Enqueue("fifth");
            yield return "After Enqueue fifth: " + string.Join(", ", q);
            q.Clear();
            yield return "After Clear Count: " + q.Count;
        }

        /// <summary>
        /// Stack example: LIFO semantics demonstrated with Push/Pop.
        /// </summary>
        public static IEnumerable<string> StackExample()
        {
            var s = new Stack<string>(new[] { "one", "two", "three" });
            s.Push("four");
            // Pop returns most recently pushed item first
            yield return "Peek: " + s.Peek();
            yield return "Pop1: " + s.Pop();
            yield return "Pop2: " + s.Pop();
            s.Push("zero");
            yield return "Contains(two): " + s.Contains("two");
            yield return "ToArray: " + string.Join(", ", s.ToArray());
        }

        /// <summary>
        /// LinkedList example: insert at head and tail, then enumerate.
        /// </summary>
        public static IEnumerable<string> LinkedListExample()
        {
            var ll = new LinkedList<int>(new[] { 10, 20, 30 });
            ll.AddFirst(5);
            ll.AddLast(40);
            yield return "First: " + ll.First.Value;
            yield return "Last: " + ll.Last.Value;
            var node20 = ll.Find(20);
            if (node20 != null) ll.AddAfter(node20, 25);
            if (node20 != null) ll.AddBefore(node20, 15);
            yield return "After Insertions: " + string.Join(", ", ll);
            ll.Remove(30);
            yield return "After Remove(30): " + string.Join(", ", ll);
        }

        /// <summary>
        /// Span example: stack-friendly view over an array; cannot return Span<T> from a method.
        /// We return a snapshot (array) instead.
        /// </summary>
        public static IEnumerable<string> SpanExample()
        {
            var arr = new int[] { 1, 2, 3, 4 };
            var span = arr.AsSpan();
            span[0] = 99; // modifies the backing array

            // Materialize snapshots before yielding to avoid keeping Span<T> across yields
            var originalSnapshot = "Original array after span modify: " + string.Join(", ", arr);
            var slice = span.Slice(1, 2);
            var sliceArr = slice.ToArray();
            var dest = new int[sliceArr.Length];
            Array.Copy(sliceArr, dest, sliceArr.Length);

            yield return originalSnapshot;
            yield return "Slice: " + string.Join(", ", sliceArr);
            yield return "Copied slice: " + string.Join(", ", dest);
        }

        /// <summary>
        /// ReadOnlyCollection example: wrapper that prevents callers from mutating the collection API.
        /// Note that the underlying list can still be changed by its owner.
        /// </summary>
        public static IEnumerable<string> ReadOnlyCollectionExample()
        {
            var baseList = new List<int> { 1, 2, 3 };
            ReadOnlyCollection<int> ro = baseList.AsReadOnly();
            // mutate underlying list to show the wrapper reflects changes
            baseList.Add(4);
            yield return "Count: " + ro.Count;
            yield return "Contains(2): " + ro.Contains(2);
            yield return string.Join(", ", ro);
        }

        /// <summary>
        /// ImmutableList example: persistent immutable collection that returns new instances on changes.
        /// </summary>
        public static IEnumerable<string> ImmutableListExample()
        {
            var imm = ImmutableList.Create(1, 2, 3);
            var newList = imm.Add(4); // original remains unchanged
            var removed = newList.Remove(2);
            var addedRange = removed.AddRange(new[] { 7, 8 });
            yield return "Original: " + string.Join(", ", imm);
            yield return "After Add(4): " + string.Join(", ", newList);
            yield return "After Remove(2) and AddRange(7,8): " + string.Join(", ", addedRange);
        }

        /// <summary>
        /// Array concatenation using LINQ `Concat` to emulate a spread operator.
        /// </summary>
        public static IEnumerable<string> ConcatenateExample()
        {
            int[] a = { 1, 2 };
            int[] b = { 3, 4 };
            int[] combined = a.Concat(b).Concat(new[] { 5 }).ToArray();
            yield return string.Join(", ", combined);
        }

        /// <summary>
        /// Examples of common LINQ patterns: filtering, projection, grouping and aggregation.
        /// </summary>
        public static IEnumerable<string> LinqExamples()
        {
            var nums = Enumerable.Range(1, 10);

            // Filtering & projection
            var evens = nums.Where(n => n % 2 == 0).Select(n => $"{n}");
            yield return $"Evens: {string.Join(", ", evens)}";

            // Grouping
            var groups = nums.GroupBy(n => n % 3).Select(g => $"Rem{g.Key}:{g.Count()}");
            yield return $"Groups: {string.Join(" | ", groups)}";

            // Aggregation
            yield return $"Sum: {nums.Sum()}, Avg: {nums.Average():F2}";
        }

        /// <summary>
        /// Breadth-first search (BFS) example on a small adjacency list graph.
        /// Returns the visitation order starting from node 0.
        /// </summary>
        public static IEnumerable<string> BfsExample()
        {
            // Simple graph (undirected representation for demo)
            var graph = new List<int>[]
            {
                new List<int>{1,2},    // 0
                new List<int>{0,3,4},  // 1
                new List<int>{0,4},    // 2
                new List<int>{1,5},    // 3
                new List<int>{1,2,5},  // 4
                new List<int>{3,4}     // 5
            };

            var visited = new bool[graph.Length];
            var q = new Queue<int>();
            var order = new List<int>();

            q.Enqueue(0); visited[0] = true;
            while (q.Count > 0)
            {
                var u = q.Dequeue();
                order.Add(u);
                foreach (var v in graph[u])
                {
                    if (!visited[v]) { visited[v] = true; q.Enqueue(v); }
                }
            }

            yield return string.Join(", ", order);
        }

        /// <summary>
        /// Depth-first search (DFS) example (recursive) on the same graph.
        /// Returns the visitation order starting from node 0.
        /// </summary>
        public static IEnumerable<string> DfsExample()
        {
            var graph = new List<int>[]
            {
                new List<int>{1,2},    // 0
                new List<int>{0,3,4},  // 1
                new List<int>{0,4},    // 2
                new List<int>{1,5},    // 3
                new List<int>{1,2,5},  // 4
                new List<int>{3,4}     // 5
            };

            var visited = new bool[graph.Length];
            var order = new List<int>();

            void Dfs(int u)
            {
                visited[u] = true;
                order.Add(u);
                foreach (var v in graph[u])
                {
                    if (!visited[v]) Dfs(v);
                }
            }

            Dfs(0);
            yield return string.Join(", ", order);
        }

        /// <summary>
        /// Demonstrates differences between Span<T> and Memory<T>, and shows stackalloc usage.
        /// All operations are synchronous and return snapshots as strings.
        /// </summary>
        public static IEnumerable<string> MemorySpanExample()
        {
            var arr = new int[] { 1, 2, 3, 4, 5 };

            // Memory<T> can be stored and passed around; get Span when needed
            Memory<int> mem = arr.AsMemory(1, 3); // view over [2,3,4]
            yield return "Memory.Span snapshot: " + string.Join(", ", mem.Span.ToArray());

            // Span<T> is stack-only and very efficient for local operations
            Span<int> span = arr.AsSpan(2, 2); // view over [3,4]
            span[0] = 99; // modifies backing array
            yield return "After Span modify (arr): " + string.Join(", ", arr);

            // stackalloc creates a stack-allocated span (no heap allocation)
            Span<byte> buffer = stackalloc byte[4];
            buffer[0] = 10; buffer[1] = 20;
            yield return "stackalloc buffer length: " + buffer.Length;
        }

        /// <summary>
        /// Demonstrates usage of concurrency-safe collections from System.Collections.Concurrent.
        /// Shows basic thread-safe operations and how to present results deterministically.
        /// </summary>
        public static IEnumerable<string> ConcurrentExamples()
        {
            // ConcurrentDictionary: thread-safe add/update
            var cd = new ConcurrentDictionary<int, string>();
            cd.TryAdd(1, "one");
            cd[2] = "two"; // thread-safe indexer
            cd.AddOrUpdate(1, "one", (k, v) => v + "+");
            yield return "ConcurrentDictionary: " + string.Join(", ", cd.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}:{kvp.Value}"));

            // ConcurrentQueue: thread-safe FIFO
            var cq = new ConcurrentQueue<string>();
            cq.Enqueue("a");
            cq.Enqueue("b");
            if (cq.TryDequeue(out var first)) yield return $"ConcurrentQueue.Dequeued: {first}";

            // ConcurrentBag: unordered collection optimized for producer/consumer
            var cb = new ConcurrentBag<int>();
            cb.Add(10);
            cb.Add(20);
            // present results ordered for determinism
            yield return "ConcurrentBag: " + string.Join(", ", cb.OrderBy(x => x));

            // BlockingCollection: can be used as a bounded producer/consumer queue
            using (var bc = new BlockingCollection<int>())
            {
                bc.Add(100);
                bc.Add(200);
                bc.CompleteAdding();
                yield return "BlockingCollection: " + string.Join(", ", bc);
            }
        }
    }

}

