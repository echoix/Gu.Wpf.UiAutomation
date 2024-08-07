namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Windows.Automation;
    using NUnit.Framework;

    public class DataGridColumnHeaderTests
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
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var header = (DataGridColumnHeader)window.FindFirst(TreeScope.Descendants, Conditions.DataGridColumnHeader);
            Assert.That(UiElement.FromAutomationElement(header.AutomationElement), Is.InstanceOf<DataGridColumnHeader>());
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var header = (DataGridColumnHeader)window.FindFirst(TreeScope.Descendants, Conditions.DataGridColumnHeader);
            Assert.That(header.Text, Is.EqualTo("IntValue"));
            Assert.NotNull(header.LeftHeaderGripper);
            Assert.NotNull(header.RightHeaderGripper);
        }
    }
}
