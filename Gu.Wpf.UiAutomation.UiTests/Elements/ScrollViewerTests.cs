namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ScrollViewerTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Find()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollViewer = window.FindScrollViewer();
            Assert.That(scrollViewer.HorizontalScrollBar.AutomationId, Is.EqualTo("HorizontalScrollBar"));
            Assert.That(scrollViewer.VerticalScrollBar.AutomationId, Is.EqualTo("VerticalScrollBar"));
            Assert.IsInstanceOf<ScrollViewer>(UiElement.FromAutomationElement(scrollViewer.AutomationElement));
        }

        [Test]
        public void ScrollPattern()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollViewer = window.FindScrollViewer();
            var pattern = scrollViewer.ScrollPattern.Current;
            Assert.That(pattern.HorizontalScrollPercent, Is.EqualTo(0));
            Assert.That(pattern.HorizontalViewSize, Is.EqualTo(66.75));
            Assert.That(pattern.HorizontallyScrollable, Is.EqualTo(true));
            Assert.That(pattern.VerticalScrollPercent, Is.EqualTo(0));
            //// Using a tolerance as there is a difference on Win7 & Win10
            Assert.That(pattern.VerticalViewSize, Is.EqualTo(61.25).Within(1));
            Assert.That(pattern.VerticallyScrollable, Is.EqualTo(true));
        }
    }
}
