namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class MessageBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show MessageBox OKCancel").Click();
            var messageBox = window.FindMessageBox();
            Assert.IsInstanceOf<MessageBox>(UiElement.FromAutomationElement(messageBox.AutomationElement));
            messageBox.Close();
        }

        [Test]
        public void MessageBoxOkCancel()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show MessageBox OKCancel").Click();
            var messageBox = window.FindMessageBox();
            Assert.That(messageBox.Caption, Is.EqualTo("Caption text"));
            Assert.That(messageBox.Message, Is.EqualTo("Message text"));
            Assert.That(messageBox.FindLabel().Text, Is.EqualTo("Message text"));

            Assert.NotNull(messageBox.FindButton("OK"));
            Assert.NotNull(messageBox.FindButton("Cancel"));

            messageBox.Close();
        }

        [Test]
        public void MessageBoxYesNoCancel()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show MessageBox YesNoCancel").Click();
            var messageBox = window.FindMessageBox();
            Assert.That(messageBox.Caption, Is.EqualTo("Caption text"));
            Assert.That(messageBox.Message, Is.EqualTo("Message text"));
            Assert.That(messageBox.FindLabel().Text, Is.EqualTo("Message text"));

            Assert.NotNull(messageBox.FindButton("Yes"));
            Assert.NotNull(messageBox.FindButton("No"));
            Assert.NotNull(messageBox.FindButton("Cancel"));

            messageBox.Close();
        }
    }
}
