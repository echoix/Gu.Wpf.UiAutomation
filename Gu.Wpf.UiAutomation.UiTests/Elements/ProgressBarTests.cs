namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ProgressBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.Launch(ExeFileName, "ProgressBarWindow");
            var window = app.MainWindow;
            var progressBar = window.FindProgressBar();
            Assert.That(UiElement.FromAutomationElement(progressBar.AutomationElement), Is.InstanceOf<ProgressBar>());
        }

        [Test]
        public void MinMaxAndValue()
        {
            using var app = Application.Launch(ExeFileName, "ProgressBarWindow");
            var window = app.MainWindow;
            var progressBar = window.FindProgressBar();
            Assert.Multiple(() =>
            {
                Assert.That(progressBar.Minimum, Is.EqualTo(0));
                Assert.That(progressBar.Maximum, Is.EqualTo(100));
                Assert.That(progressBar.Value, Is.EqualTo(50));
            });
        }
    }
}
