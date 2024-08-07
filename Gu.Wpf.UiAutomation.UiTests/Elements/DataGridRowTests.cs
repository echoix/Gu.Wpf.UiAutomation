namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class DataGridRowTests
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
            var header = (DataGridRow)window.FindFirst(TreeScope.Descendants, Conditions.DataGridRow);
            Assert.That(UiElement.FromAutomationElement(header.AutomationElement), Is.InstanceOf<DataGridRow>());
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var row = (DataGridRow)window.FindFirst(TreeScope.Descendants, Conditions.DataGridRow);
            Assert.That(row.Header.Text, Is.EqualTo("Row 1"));
            Assert.That(row.Header.TopHeaderGripper, Is.Not.Null);
            Assert.That(row.Header.BottomHeaderGripper, Is.Not.Null);
        }

        [Test]
        public void Cells()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var row = (DataGridRow)window.FindFirst(TreeScope.Descendants, Conditions.DataGridRow);
            Assert.That(row.Cells.Count, Is.EqualTo(2));
            Assert.That(row.Cells, Is.All.InstanceOf(typeof(DataGridCell)));
            Assert.That(row.Cells.Select(x => x.Value), Is.EqualTo(new[] { "1", "Item 1" }).AsCollection);
        }
    }
}
