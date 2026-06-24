namespace TestDome.Library;

public class AppSettings
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
        // var appSettings = GetAppSettings(true);
        // Console.WriteLine(appSettings["os"]); // Error because cannot apply indexing to a object


        //Basic Solution
        var appSettings = (Dictionary<string, string>)GetAppSettings(true);
        Console.WriteLine(appSettings["os"]);
    }

    // Optimal Solution 
    static Dictionary<string, string> GetAppSettingsII(bool detailed)
    {
        var result = new Dictionary<string, string>
        {
            ["machine"] = Environment.MachineName
        };

        if (detailed)
        {
            result["os"] = Environment.OSVersion.ToString();
            result["user"] = Environment.UserName;
        }

        return result;
    }

    static void MainII()
    {
        var appSettings = GetAppSettingsII(true);
        Console.WriteLine(appSettings["os"]);
    }
}

