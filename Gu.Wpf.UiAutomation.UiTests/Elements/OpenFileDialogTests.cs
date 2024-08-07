namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class OpenFileDialogTests
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
            window.FindButton("Show OpenFileDialog").Click();
            var dialog = window.FindOpenFileDialog();
            dialog.Close();
        }

        [Test]
        public void FileTextBox()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show OpenFileDialog").Click();
            var dialog = window.FindOpenFileDialog();
            Assert.That(dialog.FileTextBox, Is.Not.Null);
            dialog.SetFileName("C:\\Temp\\Foo.txt");
            dialog.Close();
        }

        [Test]
        public void OpenButton()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow");
            var window = app.MainWindow;
            window.FindButton("Show OpenFileDialog").Click();
            var dialog = window.FindOpenFileDialog();
            Assert.That(dialog.OpenButton, Is.Not.Null);
            dialog.Close();
        }
    }
}
