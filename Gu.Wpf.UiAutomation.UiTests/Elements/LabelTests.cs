namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class LabelTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Content", "Content")]
        public void FindLabel(string key, string header)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "LabelWindow");
            var window = app.MainWindow;
            var label = window.FindLabel(key);
            Assert.Multiple(() =>
            {
                Assert.That(label.Text, Is.EqualTo(header));
                Assert.That(label.FindTextBlock(), Is.Not.Null);
                Assert.That(UiElement.FromAutomationElement(label.AutomationElement), Is.InstanceOf<Label>());
            });
        }
    }
}
