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
            Assert.That(UiElement.FromAutomationElement(messageBox.AutomationElement), Is.InstanceOf<MessageBox>());
            messageBox.Close();
        }

        [Test]
        public void MessageBoxOkCancel()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show MessageBox OKCancel").Click();
            var messageBox = window.FindMessageBox();
            Assert.Multiple(() =>
            {
                Assert.That(messageBox.Caption, Is.EqualTo("Caption text"));
                Assert.That(messageBox.Message, Is.EqualTo("Message text"));
                Assert.That(messageBox.FindLabel().Text, Is.EqualTo("Message text"));
            });

            Assert.Multiple(() =>
            {
                Assert.That(messageBox.FindButton("OK"), Is.Not.Null);
                Assert.That(messageBox.FindButton("Cancel"), Is.Not.Null);
            });

            messageBox.Close();
        }

        [Test]
        public void MessageBoxYesNoCancel()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show MessageBox YesNoCancel").Click();
            var messageBox = window.FindMessageBox();
            Assert.Multiple(() =>
            {
                Assert.That(messageBox.Caption, Is.EqualTo("Caption text"));
                Assert.That(messageBox.Message, Is.EqualTo("Message text"));
                Assert.That(messageBox.FindLabel().Text, Is.EqualTo("Message text"));
            });

            Assert.Multiple(() =>
            {
                Assert.That(messageBox.FindButton("Yes"), Is.Not.Null);
                Assert.That(messageBox.FindButton("No"), Is.Not.Null);
                Assert.That(messageBox.FindButton("Cancel"), Is.Not.Null);
            });

            messageBox.Close();
        }
    }
}
