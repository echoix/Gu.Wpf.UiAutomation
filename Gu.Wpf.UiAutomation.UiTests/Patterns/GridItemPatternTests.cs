namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    [TestFixture]
    public class GridItemPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void DataGrid()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var cell = window.FindDataGrid()[0, 0];
            Assert.NotNull(cell);
            var pattern = cell.AutomationElement.GridItemPattern();
            Assert.That(pattern.Current.Row, Is.EqualTo(0));
            Assert.That(pattern.Current.RowSpan, Is.EqualTo(1));

            Assert.That(pattern.Current.Column, Is.EqualTo(0));
            Assert.That(pattern.Current.ColumnSpan, Is.EqualTo(1));

            Assert.That(pattern.Current.ContainingGrid.ClassName(), Is.EqualTo("DataGrid"));
        }
    }
}
