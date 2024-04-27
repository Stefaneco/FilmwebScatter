using Microsoft.Playwright;

namespace FilmwebScatter.Base;
internal abstract class BasePage
{
    protected readonly IPage page;
    public static string BaseUrl { get; } = "https://www.filmweb.pl";
    protected string Url { get; set; }

    public BasePage(IPage page)
    {
        this.page = page;
    }

    public async Task OpenPage()
    {
        await page.GotoAsync(Url);
    }

    public async Task AcceptEULA()
    {
        await page.GetByLabel("Zaakceptuj i zamknij: Wyraź").ClickAsync();
    }

    public async Task AcceptFriendsPopup()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "OK, ROZUMIEM" }).ClickAsync();
    }

    public async Task CloseFullscreenAd()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Przejdź do Filmwebu teraz" }).ClickAsync();
    }

    public async Task ClosePage()
    {
        await page.CloseAsync();
    }
}
