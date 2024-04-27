using Microsoft.Playwright;
using FilmwebScatter.Base;

namespace FilmwebScatter.Pages;
internal class ListPage : BasePage
{
    //private string filmLocator = "//a[starts-with(@href, '/film/')][@class=\"sc-kFWlue krKfHD\"]";
    private string filmLocator = "//div[@type=\"noPadding\"]/div/div/a[starts-with(@href, '/film/')]"; // no dynamic class name

    public ListPage(IPage page, string category) : base(page)
    {
        Url = $"{BaseUrl}/user/{FilmwebVariables.Username}#/votes/{category}";
    }

    public async Task ChangeViewToList()
    {
        await page.GetByLabel("List view").ClickAsync();
    }

    public async Task ChangeViewToGridAsync()
    {
        await page.GetByLabel("Grid view").ClickAsync();
    }

    public async Task ScrollFromStartToElement(int elementIndex)
    {
        for (int i = 0; i < elementIndex; i++)
        {
            var target = page.Locator(filmLocator).Nth(i);
            await target.ScrollIntoViewIfNeededAsync();
            await Task.Delay(100);
        }
    }

    public async Task<IPage> OpenDetailsPage(int elementIndex)
    {
        var target = page.Locator(filmLocator).Nth(elementIndex);
        await page.EvaluateAsync("window.scrollBy(0, 300)");
        Task.Delay(2000).Wait();
        await target.ScrollIntoViewIfNeededAsync();
        Task.Delay(2000).Wait();
        var href = await target.GetAttributeAsync("href");
        var newPage = await page.Context.NewPageAsync();
        await newPage.GotoAsync(BaseUrl + href);
        return newPage;
    }

    public async Task<int> GetListLength()
    {
        var counter = await page.Locator("//a[@href=\"#/votes/film\"]").GetAttributeAsync("data-counter");
        return int.Parse(counter);
    }

    public async Task ScrollToBottom()
    {
        await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
        await page.WaitForTimeoutAsync(2000);
    }
}
