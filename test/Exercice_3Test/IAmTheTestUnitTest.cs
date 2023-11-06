
using Exercice_3.Repository;

namespace Exercice_3Test
{
    public class IAmTheTestUnitTest
    {
        private Mock<IWebBrowser> _browser;
        private AmTheTest _emailCrawler;

        public IAmTheTestUnitTest()
        {
            _browser = new Mock<IWebBrowser>();
            _emailCrawler = new AmTheTest();

            _browser.Setup(b => b.GetHtml("C:/TestHtml/index.html"))
            .Returns(
                "<h1>INDEX</h1>" +
                    "<a href=\"./child1.html\">child1</a>" +
                    "<a href=\"mailto:nullepart@mozilla.org\">Envoyer l'email nulle part</a>" +
                    "</html>"
                );

            _browser.Setup(b => b.GetHtml("C:/TestHtml/child1.html"))
                .Returns(
                    "<h1>CHILD1</h1>" +
                    "<a href=\"./index.html\">index</a>" +
                    "<a href=\"./child2.html\">child2</a>" +
                    "<a href=\"mailto:ailleurs@mozilla.org\">Envoyer l'email ailleurs</a>" +
                    "<a href=\"mailto:nullepart@mozilla.org\">Envoyer l'email nulle part</a>" +
                    "</html>"
                );

            _browser.Setup(b => b.GetHtml("C:/TestHtml/child2.html"))
                .Returns(
                    "<html>" +
                    "<h1>CHILD2</h1>" +
                    "<a href=\"./index.html\">index</a>" +
                    "<a href=\"mailto:loin@mozilla.org\">Envoyer l'email loin</a>" +
                    "<a href=\"mailto:nullepart@mozilla.org\">Envoyer l'email nulle part</a>" +
                    "</html>"
                );
        }

        [Fact]
        public void GetEmailsInPageAndChildPages_WithDepth0_ReturnsCorrectEmails()
        {
            List<string> result = _emailCrawler.GetEmailsInPageAndChildPages(_browser.Object, "C:/TestHtml/index.html", 0);

            Assert.Collection(result,
                email => Assert.Equal("nullepart@mozilla.org", email));
        }

        [Fact]
        public void GetEmailsInPageAndChildPages_WithDepth1_ReturnsCorrectEmails()
        {
            List<string> result = _emailCrawler.GetEmailsInPageAndChildPages(_browser.Object, "C:/TestHtml/index.html", 1);

            Assert.Collection(result,
                email => Assert.Equal("nullepart@mozilla.org", email),
                email => Assert.Equal("ailleurs@mozilla.org", email));
        }

        [Fact]
        public void GetEmailsInPageAndChildPages_WithDepth2_ReturnsCorrectEmails()
        {
            List<string> result = _emailCrawler.GetEmailsInPageAndChildPages(_browser.Object, "C:/TestHtml/index.html", 2);

            Assert.Collection(result,
                email => Assert.Equal("nullepart@mozilla.org", email),
                email => Assert.Equal("ailleurs@mozilla.org", email),
                email => Assert.Equal("loin@mozilla.org", email));
        }

        [Fact]
        public void GetEmailsInPageAndChildPages_WithInvalidURL_ReturnsEmptyList()
        {
            List<string> result = _emailCrawler.GetEmailsInPageAndChildPages(_browser.Object, "InvalidURL.html", 1);

            Assert.Empty(result);
        }
    }
}