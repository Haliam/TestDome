
namespace TestDome.Library;


public class Song
{
    private string name;

    public Song NextSong { get; set; }

    public Song(string name)
    {
        this.name = name;
    }

    public bool IsInRepeatingPlaylist()
    {
        var listenedSong = new HashSet<Song>();
        Song current = this;

        while (current != null)
        {
            if (!listenedSong.Add(current))
            {
                // Ya hemos visto esta canción → hay ciclo
                return true;
            }

            current = current.NextSong;
        }

        return false;
    }

    // Second version
    public bool IsInRepeatingPlaylistII()
    {
        Song slow = this;
        Song fast = this;

        while (fast != null && fast.NextSong != null)
        {
            slow = slow.NextSong;
            fast = fast.NextSong.NextSong;

            if (slow == fast)
            {
                return true; // ciclo detectado
            }
        }

        return false;
    }


    public static void Main(string[] args)
    {
        Song first = new Song("Hello");
        Song second = new Song("Eye of the tiger");

        first.NextSong = second;
        second.NextSong = first;

        Console.WriteLine(first.IsInRepeatingPlaylist());
    }
}

