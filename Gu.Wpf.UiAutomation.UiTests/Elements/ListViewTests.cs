namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ListViewTests
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
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            Assert.IsInstanceOf<ListView>(UiElement.FromAutomationElement(listView.AutomationElement));
        }

        [Test]
        public void ColumnHeaders()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            Assert.That(listView.ColumnHeaders.Count, Is.EqualTo(2));

            Assert.That(listView.ColumnHeaders[0].Text, Is.EqualTo("Key"));
            Assert.That(listView.ColumnHeaders[1].Text, Is.EqualTo("Value"));
        }

        [Test]
        public void ColumnHeadersPresenter()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            var presenter = listView.ColumnHeadersPresenter;
            Assert.IsInstanceOf<GridViewHeaderRowPresenter>(UiElement.FromAutomationElement(presenter.AutomationElement));
            Assert.That(presenter.Headers.Count, Is.EqualTo(2));

            Assert.That(presenter.Headers[0].Text, Is.EqualTo("Key"));
            Assert.That(presenter.Headers[1].Text, Is.EqualTo("Value"));
        }

        [Test]
        public void RowAndColumnCount()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            Assert.That(listView.RowCount, Is.EqualTo(3));
            Assert.That(listView.ColumnCount, Is.EqualTo(2));
        }

        [Test]
        public void HeaderAndColumns()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            var columns = listView.ColumnHeaders;
            Assert.That(columns.Count, Is.EqualTo(2));
            Assert.That(columns[0].Text, Is.EqualTo("Key"));
            Assert.That(columns[1].Text, Is.EqualTo("Value"));
        }

        [TestCase(0, 0, "1")]
        [TestCase(1, 0, "2")]
        [TestCase(2, 0, "3")]
        [TestCase(0, 1, "10")]
        [TestCase(1, 1, "20")]
        [TestCase(2, 1, "30")]
        public void Cell(int row, int column, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            var cell = listView[row, column];
            Assert.IsInstanceOf<GridViewCell>(cell);
            Assert.That(cell.Text, Is.EqualTo(expected));
            Assert.IsInstanceOf<GridViewCell>(UiElement.FromAutomationElement(cell.AutomationElement));
        }

        [Test]
        public void RowsAndCells()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            Assert.That(listView.RowCount, Is.EqualTo(3));
            var items = listView.Items;
            Assert.That(items.Count, Is.EqualTo(3));
            Assert.That(items[0].Cells.Count, Is.EqualTo(2));
            Assert.That(items[0].Cells[0].Text, Is.EqualTo("1"));
            Assert.That(items[0].Cells[1].Text, Is.EqualTo("10"));
            Assert.That(items[1].Cells.Count, Is.EqualTo(2));
            Assert.That(items[1].Cells[0].Text, Is.EqualTo("2"));
            Assert.That(items[1].Cells[1].Text, Is.EqualTo("20"));
            Assert.That(items[2].Cells.Count, Is.EqualTo(2));
            Assert.That(items[2].Cells[0].Text, Is.EqualTo("3"));
            Assert.That(items[2].Cells[1].Text, Is.EqualTo("30"));
        }

        [Test]
        public void Indexer()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            Assert.That(((TextBlock)listView[0, 0].Content).Text, Is.EqualTo("1"));
            Assert.That(((TextBlock)listView[0, 1].Content).Text, Is.EqualTo("10"));
            Assert.That(((TextBlock)listView[1, 0].Content).Text, Is.EqualTo("2"));
            Assert.That(((TextBlock)listView[1, 1].Content).Text, Is.EqualTo("20"));
            Assert.That(((TextBlock)listView[2, 0].Content).Text, Is.EqualTo("3"));
            Assert.That(((TextBlock)listView[2, 1].Content).Text, Is.EqualTo("30"));
        }

        [Test]
        public void ContainingGridView()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            Assert.That(listView.Items[0].ContainingListView, Is.EqualTo(listView));
            Assert.That(listView.Items[1].ContainingListView, Is.EqualTo(listView));
            Assert.That(listView.Items[2].ContainingListView, Is.EqualTo(listView));
        }

        [Test]
        public void SelectByIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            var selectedRow = listView.Select(1);
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("2"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("20"));

            selectedRow = listView.SelectedItem;
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("2"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("20"));

            selectedRow = listView.Select(2);
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("3"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("30"));

            selectedRow = listView.SelectedItem;
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("3"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("30"));
        }

        [Test]
        public void SelectByTextTest()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            var selectedRow = listView.Select(1, "20");
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("2"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("20"));

            selectedRow = listView.SelectedItem;
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("2"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("20"));

            selectedRow = listView.Select(1, "30");
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("3"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("30"));

            selectedRow = listView.SelectedItem;
            Assert.That(selectedRow.Cells.Count, Is.EqualTo(2));
            Assert.That(selectedRow.Cells[0].Text, Is.EqualTo("3"));
            Assert.That(selectedRow.Cells[1].Text, Is.EqualTo("30"));
        }
    }
}
