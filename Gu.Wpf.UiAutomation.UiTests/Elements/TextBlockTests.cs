namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TextBlockTests
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
        public void FindTextBlock(string key, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "TextBlockWindow");
            var window = app.MainWindow;
            var textBlock = window.FindTextBlock(key);
            Assert.Multiple(() =>
            {
                Assert.That(textBlock.Text, Is.EqualTo(expected));
                Assert.That(UiElement.FromAutomationElement(textBlock.AutomationElement), Is.InstanceOf<TextBlock>());
            });
        }
    }
}
