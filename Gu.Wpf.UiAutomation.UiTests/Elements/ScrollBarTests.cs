namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ScrollBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FindHorizontalScrollBar()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollBar = window.FindHorizontalScrollBar();
            Assert.IsInstanceOf<HorizontalScrollBar>(scrollBar);
            Assert.That(scrollBar.AutomationId, Is.EqualTo("HorizontalScrollBar"));
            Assert.IsInstanceOf<HorizontalScrollBar>(UiElement.FromAutomationElement(scrollBar.AutomationElement));
        }

        [Test]
        public void FindVerticalScrollBar()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollBar = window.FindVerticalScrollBar();
            Assert.IsInstanceOf<VerticalScrollBar>(scrollBar);
            Assert.That(scrollBar.AutomationId, Is.EqualTo("VerticalScrollBar"));
            Assert.IsInstanceOf<VerticalScrollBar>(UiElement.FromAutomationElement(scrollBar.AutomationElement));
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollBar = window.FindVerticalScrollBar();
            Assert.Multiple(() =>
            {
                Assert.That(scrollBar.Minimum, Is.EqualTo(0));
                //// Using a tolerance as there is a difference on Win7 & Win10
                Assert.That(scrollBar.Maximum, Is.EqualTo(155).Within(1));
                Assert.That(scrollBar.Value, Is.EqualTo(0));
                Assert.That(scrollBar.SmallChange, Is.EqualTo(0.1));
                Assert.That(scrollBar.LargeChange, Is.EqualTo(1));
                Assert.That(scrollBar.IsReadOnly, Is.EqualTo(false));
            });
        }
    }
}
