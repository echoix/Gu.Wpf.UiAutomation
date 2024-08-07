namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class GridPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void DataGrid()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid();
            Assert.That(dataGrid, Is.Not.Null);
            var pattern = dataGrid.AutomationElement.GridPattern();
            Assert.Multiple(() =>
            {
                Assert.That(pattern.Current.ColumnCount, Is.EqualTo(2));
                Assert.That(pattern.Current.RowCount, Is.EqualTo(4));
            });

            var item = pattern.GetItem(1, 1);
            Assert.That(item.Name(), Is.EqualTo("Item 2"));
        }
    }
}
