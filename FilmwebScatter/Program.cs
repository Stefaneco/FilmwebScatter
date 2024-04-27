using FilmwebScatter.Base;
using FilmwebScatter.Pages;
using Microsoft.Playwright;

FilmwebVariables.ReadConfig();

Logger.StartLog();

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = FilmwebVariables.Headless
});
var context = await browser.NewContextAsync();

var mainTab = await context.NewPageAsync();

var loginPage = new LoginPage(mainTab);
var listPage = new ListPage(mainTab, "films");

await loginPage.OpenPage();
await loginPage.AcceptEULA();
await loginPage.LoginAsync(FilmwebVariables.Username, FilmwebVariables.Password);
await Task.Delay(new Random().Next(500, 5000));
await listPage.OpenPage();
await listPage.ChangeViewToList();

await Task.Delay(new Random().Next(500, 5000));
var listLength = await listPage.GetListLength();
listLength = FilmwebVariables.EndAtFilmIndex.HasValue ? Math.Min(listLength, FilmwebVariables.EndAtFilmIndex.Value) : listLength;

using (var writerFilm = new StreamWriter(FilmwebVariables.AttachToExistingCsv ? "filmData.csv" : $"newFilmData{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss")}.csv", FilmwebVariables.AttachToExistingCsv))
using (var writerActors = new StreamWriter(FilmwebVariables.AttachToExistingCsv ? "actorsData.csv" : $"newActorsData{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss")}.csv", FilmwebVariables.AttachToExistingCsv))
{
    if (!FilmwebVariables.AttachToExistingCsv)
    {
        writerFilm.WriteLine(FilmData.GetCsvHeader());
        writerActors.WriteLine(FilmData.GetActorsCsvHeader());
    }

    await listPage.ScrollFromStartToElement(FilmwebVariables.StartAtFilmIndex);

    for (int i = FilmwebVariables.StartAtFilmIndex; i < listLength; i++)
    {
        Logger.Log($"Processing film {i + 1}/{listLength}...");
        await Task.Delay(new Random().Next(500, 5000));

        try
        {
            var detailsTab = await listPage.OpenDetailsPage(i);
            var detailsPage = new DetailsPage(detailsTab);

            var filmData = await detailsPage.GetFilmData();

            await detailsPage.ClosePage();

            var filmDataCsv = filmData.ToCsv();
            writerFilm.WriteLine(filmDataCsv);
            writerFilm.Flush();
            foreach (var actor in filmData.ActorsToCsv())
            {
                writerActors.WriteLine(actor);
                writerActors.Flush();
            }
            Logger.Log($"Film {i + 1}/{listLength} {filmData.Title} processed.");
        }
        catch (Exception ex)
        {
            Logger.Log($"Error processing film {i + 1}/{listLength}: {ex.Message}", Logger.LogLevel.Error);
            Logger.Log(ex);
            Console.ReadKey();
            break;
        }
    }
    writerActors.Close();
    writerFilm.Close();
}

Logger.Close();
