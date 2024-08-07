namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ToggleButtonTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindToggleButton(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow");
            var window = app.MainWindow;
            var toggleButton = window.FindToggleButton(key);
            Assert.IsInstanceOf<ToggleButton>(UiElement.FromAutomationElement(toggleButton.AutomationElement));
        }

        [Test]
        public void IsChecked()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow");
            var window = app.MainWindow;
            var toggleButton = window.FindToggleButton("Test ToggleButton");
            toggleButton.IsChecked = true;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));

            toggleButton.IsChecked = false;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(false));

            toggleButton.IsChecked = true;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));

            var exception = Assert.Throws<UiAutomationException>(() => toggleButton.IsChecked = null);
            Assert.That(exception.Message, Is.EqualTo($"Setting ToggleButton Test ToggleButton.IsChecked to null failed."));
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow");
            var window = app.MainWindow;
            var toggleButton = window.FindToggleButton("3-Way Test ToggleButton");
            toggleButton.IsChecked = true;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));

            toggleButton.IsChecked = false;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(false));

            toggleButton.IsChecked = null;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(null));

            toggleButton.IsChecked = true;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));
        }

        [Test]
        public void Click()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow");
            var window = app.MainWindow;
            var toggleButton = window.FindToggleButton("Test ToggleButton");
            toggleButton.IsChecked = false;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(false));

            toggleButton.Click();
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));

            toggleButton.Click();
            Assert.That(toggleButton.IsChecked, Is.EqualTo(false));

            toggleButton.Click();
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));
        }

        [Test]
        public void ThreeStateClick()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow");
            var window = app.MainWindow;
            var toggleButton = window.FindToggleButton("3-Way Test ToggleButton");
            toggleButton.IsChecked = false;
            Assert.That(toggleButton.IsChecked, Is.EqualTo(false));

            toggleButton.Click();
            Assert.That(toggleButton.IsChecked, Is.EqualTo(true));

            toggleButton.Click();
            Assert.That(toggleButton.IsChecked, Is.EqualTo(null));

            toggleButton.Click();
            Assert.That(toggleButton.IsChecked, Is.EqualTo(false));
        }
    }
}
