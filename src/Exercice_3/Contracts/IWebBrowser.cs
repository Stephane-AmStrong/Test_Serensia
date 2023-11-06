namespace Exercice_3.Contracts
{
    // Provided interface. Just use it.
    public interface IWebBrowser
    {
        // Returns null if the url could not be visited.
        string? GetHtml(string url);
    }
}
