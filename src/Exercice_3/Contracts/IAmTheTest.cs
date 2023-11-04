namespace Exercice_3.Contracts
{
    public interface IAmTheTest
    {
        List<string> GetEmailsInPageAndChildPages(IWebBrowser browser, string url, int maximumDepth);
    }
}
