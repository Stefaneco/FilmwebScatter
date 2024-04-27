using Microsoft.Playwright;
using FilmwebScatter.Base;
using System.IO;


namespace FilmwebScatter.Pages;
internal class CastPage : BasePage
{
    public CastPage(IPage page) : base(page) { }

    public async Task<string> GetActorsString()
    {
        var actorsList = await page.Locator("//div[@class=\"castRoleListElement__info\"]/a").AllTextContentsAsync();
        return string.Join(", ", actorsList);
    }

    public async Task<List<string>> GetActorsList()
    {
        var actorsList = await page.Locator("//div[@class=\"castRoleListElement__info\"]/a").AllTextContentsAsync();
        return actorsList.ToList();
    }
}
