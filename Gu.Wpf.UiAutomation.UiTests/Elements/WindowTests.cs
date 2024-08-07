namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Globalization;
    using System.Linq;
    using NUnit.Framework;

    public class WindowTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Close()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            window.WaitUntilResponsive();
            window.Close();
        }

        [Test]
        public void Dialog()
        {
            using var app = Application.Launch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show Dialog").Click();
            var dialog = window.ModalWindows.Single();
            Assert.That(dialog.FindTextBlock().Text, Is.EqualTo("Message"));
            dialog.Close();
        }

        [Test]
        public void Resize()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            Assert.Multiple(() =>
            {
                Assert.That(window.CanResize, Is.EqualTo(true));
                Assert.That(window.Bounds.Size.ToString(CultureInfo.InvariantCulture), Is.EqualTo("300,300"));
            });

            window.Resize(270, 280);
            Assert.Multiple(() =>
            {
                Assert.That(window.CanResize, Is.EqualTo(true));
                Assert.That(window.Bounds.Size.ToString(CultureInfo.InvariantCulture), Is.EqualTo("270,280"));
            });
        }

        [Test]
        public void Move()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            Assert.That(window.CanMove, Is.EqualTo(true));

            window.MoveTo(10, 20);
            Assert.Multiple(() =>
            {
                Assert.That(window.CanMove, Is.EqualTo(true));
                Assert.That(window.Bounds.ToString(CultureInfo.InvariantCulture), Is.EqualTo("10,20,300,300"));
            });

            window.MoveTo(30, 40);
            Assert.Multiple(() =>
            {
                Assert.That(window.CanMove, Is.EqualTo(true));
                Assert.That(window.Bounds.ToString(CultureInfo.InvariantCulture), Is.EqualTo("30,40,300,300"));
            });
        }

        [Test]
        public void Netcore3App()
        {
            using var app = Application.Launch("Netcore3App.exe");
            var window = app.MainWindow;
            Assert.That(window.FindTextBlock("TextBlock").Text, Is.EqualTo("Test"));
        }
    }
}
