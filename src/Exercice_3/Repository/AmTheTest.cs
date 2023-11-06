namespace Exercice_3.Repository
{
    public class AmTheTest : IAmTheTest
    {
        public List<string> GetEmailsInPageAndChildPages(IWebBrowser browser, string url, int maximumDepth)
        {
            var visitedUrls = new HashSet<string>();
            var resultEmails = new HashSet<string>();
            GetEmailsAndChildPages(browser, url, maximumDepth, visitedUrls, resultEmails);
            return resultEmails.ToList();
        }

        private void GetEmailsAndChildPages(IWebBrowser browser, string url, int depth, HashSet<string> visitedUrls, HashSet<string> resultEmails)
        {
            if (visitedUrls.Contains(url) || depth < 0) return;

            visitedUrls.Add(url);

            string? html = browser.GetHtml(url);
            if (html != null)
            {
                resultEmails.UnionWith(GetEmailsFromHtml(html));

                var links = GetLinksFromHtml(html);
                foreach (var link in links)
                {
                    GetEmailsAndChildPages(browser, link, depth - 1, visitedUrls, resultEmails);
                }
            }
        }

        private IEnumerable<string> GetEmailsFromHtml(string html)
        {
            var emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}";
            var matches = Regex.Matches(html, emailPattern);
            return matches.Cast<Match>().Select(match => match.Value);
        }

        private IEnumerable<string> GetLinksFromHtml(string html)
        {
            var links = new List<string>();
            var matches = Regex.Matches(html, "<a\\s+href\\s*=\\s*\"([^\"]+)\"");
            foreach (Match match in matches)
            {
                var href = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(href) && Uri.IsWellFormedUriString(href, UriKind.Relative))
                {
                    links.Add(href.Replace("./", "C:/TestHtml/"));
                }
            }
            return links;
        }
    }
}
