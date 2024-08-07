namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class UserControlTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [TestCase("UserControl1", "1")]
        [TestCase("UserControl2", "2")]
        public void Find(string key, string expected)
        {
            using var app = Application.Launch(ExeFileName, "UserControlWindow");
            var window = app.MainWindow;
            var userControl = window.FindUserControl(key);
            Assert.Multiple(() =>
            {
                Assert.That(userControl.FindTextBlock().Text, Is.EqualTo(expected));
                Assert.That(((TextBlock)userControl.Content).Text, Is.EqualTo(expected));
            });
            Assert.That(UiElement.FromAutomationElement(userControl.AutomationElement), Is.InstanceOf<UserControl>());
        }
    }
}
