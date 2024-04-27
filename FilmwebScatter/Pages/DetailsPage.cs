using Microsoft.Playwright;
using FilmwebScatter.Base;
using System.Text.RegularExpressions;

namespace FilmwebScatter.Pages;
internal class DetailsPage : BasePage
{
    public DetailsPage(IPage page) : base(page) { }

    public async Task<FilmData> GetFilmData()
    {
        var filmData = new FilmData();

        filmData.Title = await GetTitle();
        filmData.OriginalTitle = await GetOriginalTitle();
        filmData.PremiereYear = await GetPremiereYear();
        filmData.RateDate = await GetRateDate();
        filmData.Rate = await GetRate();
        filmData.AvgRate = await GetAvgRate();
        filmData.RateCount = await GetRateCount();
        filmData.CriticsRate = await GetCriticsRate();
        filmData.CriticsRateCount = await GetCriticsRateCount();
        filmData.Director = await GetDirector();
        filmData.Duration = await GetDuration();
        filmData.Genre = await GetGenre();
        filmData.Country = await GetCountry();
        filmData.BoxOffice = await GetBoxOffice();
        filmData.BoxOfficeUsa = await GetBoxOfficeUsa();
        filmData.BoxOfficeWorld = await GetBoxOfficeWorld();
        filmData.Budget = await GetBudget();
        filmData.IsFavorite = await GetIsFavorite();
        await Task.Delay(1000);
        filmData.Actors = await GetActors();

        return filmData;
    }

    private async Task<string> GetBudget()
    {
        var budgetElementLocator = page.Locator("//div[contains(text(),'budżet')]/following-sibling::div[@class=\"filmInfo__info\"]");
        if (await budgetElementLocator.CountAsync() == 0)
        {
            return "0";
        }
        var budgetText = await budgetElementLocator.TextContentAsync();
        var numberOnly = Regex.Replace(budgetText, @"[^\d]", "");
        return numberOnly;
    }

    private async Task<string> GetIsFavorite()
    {
        var dataRate = await page.Locator("//div[@class=\"iconicRate IconicRate iconicRate--fav\"]").GetAttributeAsync("data-rate");
        return dataRate == "1" ? "Yes" : "No";
    }

    private async Task<string> GetBoxOfficeWorld()
    {
        var regex = new Regex(@"\$\d{1,3}( \d{3})* poza USA");
        var textElementCount = await page.GetByText(regex).CountAsync();
        if (textElementCount == 0)
        {
            return "0";
        }
        var text = await page.GetByText(regex).TextContentAsync();
        var numberOnly = Regex.Replace(text, @"[^\d]", "");
        return numberOnly;
    }

    private async Task<string> GetBoxOfficeUsa()
    {
        var regex = new Regex(@"\$\d{1,3}( \d{3})* w USA");
        var textElementCount = await page.GetByText(regex).CountAsync();
        if (textElementCount == 0)
        {
            return "0";
        }
        var text = await page.GetByText(regex).TextContentAsync();
        var numberOnly = Regex.Replace(text, @"[^\d]", "");
        return numberOnly;
    }

    private async Task<string> GetBoxOffice()
    {
        var regex = new Regex(@"\$\d{1,3}( \d{3})* na świecie");
        var textElementCount = await page.GetByText(regex).CountAsync();
        if (textElementCount == 0)
        {
            return "0";
        }
        var text = await page.GetByText(regex).TextContentAsync();
        var numberOnly = Regex.Replace(text, @"[^\d]", "");
        return numberOnly;
    }

    private async Task<List<string>> GetActors()
    {
        try
        {
            var castPage = await OpenCastPage();
            await Task.Delay(1000);
            var actors = await castPage.GetActorsList();
            await castPage.ClosePage();
            return actors;
        }
        catch (ElementNotFoundException)
        {
            return new List<string>();
        }
    }

    private async Task<string> GetCountry()
    {
        var countryLocator = page.Locator("//a[starts-with(@href, '/ranking/film/country/')]/span");
        if (await countryLocator.CountAsync() == 0)
        {
            return "";
        }
        var countryElements = await countryLocator.AllInnerTextsAsync();
        return string.Join(", ", countryElements);
    }

    private async Task<string> GetGenre()
    {
        var genreLocator = page.Locator("//a[starts-with(@href, '/ranking/film/genre/')]/span");
        if (await genreLocator.CountAsync() == 0)
        {
            return "";
        }
        var genreElements = await genreLocator.AllInnerTextsAsync();
        return string.Join(", ", genreElements);
    }

    private async Task<string> GetDuration()
    {
        var durationLocator = page.Locator("//div[@class=\"filmCoverSection__duration\"]");
        if (await durationLocator.CountAsync() == 0)
        {
            return "";
        }
        var duration = await page.Locator("//div[@class=\"filmCoverSection__duration\"]").GetAttributeAsync("data-duration");
        return duration;
    }

    private async Task<string> GetDirector()
    {
        var directorLocator = page.Locator("//div[@class=\"filmPosterSection__info filmInfo\"]/div[@data-type=\"directing-info\"]/a[@itemprop=\"director\"]/span[@itemprop=\"name\"]");
        if (await directorLocator.CountAsync() == 0)
        {
            return "";
        }
        var directors = await directorLocator.AllInnerTextsAsync();
        return string.Join(", ", directors);
    }

    private async Task<string> GetCriticsRateCount()
    {
        var rateCountLocator = page.Locator("//div[@class=\"filmRating filmRating--filmCritic\"]//span[@class=\"filmRating__count\"]");
        if (await rateCountLocator.CountAsync() == 0)
        {
            return "0";
        }
        var rateCountText = await rateCountLocator.TextContentAsync();
        var rateCount = Regex.Match(rateCountText, @"\d+").Value;
        return rateCount;
    }

    private async Task<string> GetCriticsRate()
    {
        var criticsRateLocator = page.Locator("//div[@class=\"filmRating filmRating--filmCritic\"]//span[@class=\"filmRating__rateValue\"]");
        if (await criticsRateLocator.CountAsync() == 0)
        {
            return "0";
        }
        return await criticsRateLocator.TextContentAsync();
    }

    private async Task<string> GetRateCount()
    {
        return await page.Locator("//div[@class=\"filmRating filmRating--filmRate filmRating--hasPanel\"]").GetAttributeAsync("data-count");
    }

    private async Task<string> GetAvgRate()
    {
        return await page.Locator("//div[@class=\"filmRating filmRating--filmRate filmRating--hasPanel\"]").GetAttributeAsync("data-rate");
    }

    private async Task<string> GetRate()
    {
        var rate = await page.Locator("//div[@class=\"iconicRate IconicRate iconicRate--star\"]").GetAttributeAsync("data-rate");
        var startTime = DateTime.Now;
        while (Convert.ToInt16(rate) == -1 && (DateTime.Now - startTime).TotalSeconds < 10)
        {
            await Task.Delay(1000);
            rate = await page.Locator("//div[@class=\"iconicRate IconicRate iconicRate--star\"]").GetAttributeAsync("data-rate");
        }
        return rate;
    }

    private async Task<string> GetRateDate()
    {
        var date = await page.Locator("//button[@class=\"filmRatingBox__date\"]").GetAttributeAsync("title");
        var startTime = DateTime.Now;
        while (date == "Zmień datę" && (DateTime.Now - startTime).TotalSeconds < 10)
        {
            await Task.Delay(1000);
            date = await page.Locator("//div[@class=\"iconicRate IconicRate iconicRate--star\"]").GetAttributeAsync("data-rate");
        }
        return date;
    }

    private async Task<string> GetPremiereYear()
    {
        return await page.Locator("//div[@class=\"filmCoverSection__year\"]").TextContentAsync();
    }

    private async Task<string> GetOriginalTitle()
    {
        var originalTitleLocator = page.Locator("//div[@class=\"filmCoverSection__originalTitle\"]");
        if (await originalTitleLocator.CountAsync() == 0)
        {
            return "";
        }
        return await originalTitleLocator.TextContentAsync();
    }

    private async Task<string> GetTitle()
    {
        return await page.Locator("//div[@class=\"filmCoverSection__titleDetails\"]/h1").TextContentAsync();
    }

    private async Task<CastPage> OpenCastPage()
    {
        var castButtonLocator = page.Locator("//span[@data-i18n=\"film:cast.action.see-all.label\"]/parent::a");
        if (await castButtonLocator.CountAsync() == 0)
        {
            throw new ElementNotFoundException("Cast button not found");
        }
        var href = await castButtonLocator.GetAttributeAsync("href");
        var newPage = await page.Context.NewPageAsync();
        await newPage.GotoAsync(BaseUrl + href);
        return new CastPage(newPage);
    }
}
