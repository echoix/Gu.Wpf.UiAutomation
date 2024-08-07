namespace Gu.Wpf.UiAutomation.UiTests
{
    using System.Globalization;
    using NUnit.Framework;

    [TestFixture]
    public class XPathTests
    {
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched("notepad.exe");
        }

        [Test]
        public void NotepadFindFirst()
        {
            using var app = Application.AttachOrLaunch("notepad.exe");
            var window = app.MainWindow;
            var item = window.FindFirstByXPath($"/MenuBar/MenuItem[@Name='{GetFileMenuText()}']");
            Assert.That(item, Is.Not.Null);
        }

        [Test]
        public void NotePadFindAll()
        {
            using var app = Application.AttachOrLaunch("notepad.exe");
            var window = app.MainWindow;
            var items = window.FindAllByXPath("//MenuItem");
            Assert.That(items.Count, Is.EqualTo(6));
        }

        [Test]
        public void NotePadFindAllIndexed()
        {
            using var app = Application.AttachOrLaunch("notepad.exe");
            var window = app.MainWindow;
            var items = window.FindAllByXPath("(//MenuBar)[1]/MenuItem");
            Assert.That(items.Count, Is.EqualTo(1));
            items = window.FindAllByXPath("(//MenuBar)[2]/MenuItem");
            Assert.That(items.Count, Is.EqualTo(5));
        }

        private static string GetFileMenuText()
        {
            return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName switch
            {
                "de" => "Datei",
                _ => "File",
            };
        }
    }
}
