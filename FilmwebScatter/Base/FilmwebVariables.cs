using System.Configuration;

namespace FilmwebScatter.Base;
internal static class FilmwebVariables
{
    public static string Username = "";
    public static string Password = "";
    public static bool Headless = bool.Parse(ConfigurationManager.AppSettings["Headless"] ?? "True");
    public static int StartAtFilmIndex = int.Parse(ConfigurationManager.AppSettings["StartAtFilmNumber"] ?? "1") - 1;
    public static int? EndAtFilmIndex = int.TryParse(ConfigurationManager.AppSettings["EndAtFilmNumber"], out int endAtFilmNumber) ? endAtFilmNumber : null;
    public static bool AttachToExistingCsv = bool.Parse(ConfigurationManager.AppSettings["AttachToExistingCsv"] ?? "False");

    public static void ReadConfig()
    {
        var appSettings = ConfigurationManager.AppSettings;
        if (appSettings == null)
        {
            Console.WriteLine("AppSettings is null. Check your app.config file. Press any key to close the app.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        else
        {
            Username = appSettings["Username"] ?? "";
            Password = appSettings["Password"] ?? "";
        }

        while (string.IsNullOrEmpty(Username))
        {
            Console.WriteLine("Username not found. Enter your filmweb username: ");
            Username = Console.ReadLine();
        }
        while (string.IsNullOrEmpty(Password))
        {
            Console.WriteLine("Password not found. Enter your filmweb password: ");
            Password = Console.ReadLine();
        }
    }
}
