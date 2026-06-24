`C# | Algorithmic Thinking | HashSet Linked List | Easy | 15 min | code | Public`

# Song


A playlist is considered a repeating playlist if any of the songs contain a reference to a previous song in the playlist. Otherwise, the playlist will end with the last song which points to null.

Implement a function `IsInRepeatingPlaylist` that, efficiently with respect to time used, returns true if a playlist is repeating or false if it is not.

## Example

The following code prints "True" as both songs point to each other:

```csharp
Song first = new Song("Hello");
Song second = new Song("Eye of the tiger");

first.NextSong = second;
second.NextSong = first;

Console.WriteLine(first.IsInRepeatingPlaylist());

