using Microsoft.Playwright;
using FilmwebScatter.Base;

namespace FilmwebScatter.Pages;
internal class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page) 
    {
        Url = $"{BaseUrl}/login";    
    }

    public async Task LoginAsync(string username, string password)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Zaloguj się kontem Filmweb" }).ClickAsync();
        await page.Locator("input[name=\"login\"]").ClickAsync();
        await page.Locator("input[name=\"login\"]").FillAsync(username);
        await page.Locator("input[name=\"login\"]").PressAsync("Tab");
        await page.Locator("input[name=\"password\"]").FillAsync(password);
        await page.GetByRole(AriaRole.Button, new() { Name = "Zaloguj się", Exact = true }).ClickAsync();

        Task.Delay(4000).Wait();
        var failedLoginMessage = await page.GetByText("Nieprawidłowy login lub hasło!").CountAsync();
        if (failedLoginMessage != 0)
        {
            Console.WriteLine("Failed to login. Check your credentials. Press any key to close the app.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

}
