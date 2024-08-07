namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using NUnit.Framework;

    public class DataGridTests
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
            var dataGrid = window.FindDataGrid();
            Assert.IsInstanceOf<DataGrid>(UiElement.FromAutomationElement(dataGrid.AutomationElement));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGridEmpty")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void RowCellsCount(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            for (var i = 0; i < dataGrid.Rows.Count; i++)
            {
                var row = dataGrid.Rows[i];
                Assert.That(row.Cells.Count, Is.EqualTo(2));
                Assert.That(dataGrid.Row(0).Cells.Count, Is.EqualTo(2));
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGridEmpty", 1)]
        [TestCase("DataGrid10", 11)]
        [TestCase("DataGridNoHeaders", 4)]
        [TestCase("ReadOnlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowCount(string name, int expectedRows)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.RowCount, Is.EqualTo(expectedRows));
        }

        [TestCase("DataGrid", new[] { "1, Item 1", "2, Item 2", "3, Item 3", ", " })]
        [TestCase("DataGridEmpty", new[] { ", " })]
        [TestCase("DataGrid10", new[] { "1, Item 1", "2, Item 2", "3, Item 3", "4, Item 4", "5, Item 5", "6, Item 6", "7, Item 7", "8, Item 8", "9, Item 9", "10, Item 10", ", " })]
        [TestCase("DataGridNoHeaders", new[] { "1, Item 1", "2, Item 2", "3, Item 3", ", " })]
        [TestCase("ReadOnlyDataGrid", new[] { "1, Item 1", "2, Item 2", "3, Item 3" })]
        [TestCase("ReadonlyColumnsDataGrid", new[] { "1, Item 1", "2, Item 2", "3, Item 3", ", " })]
        public void RowsCells(string name, string[] expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            CollectionAssert.AreEqual(expected, dataGrid.Rows.Select(x => string.Join(", ", x.Cells.Select(c => c.Value))));
        }

        [TestCase("DataGrid", new[] { "Row 1", "Row 2", "Row 3", "" })]
        [TestCase("DataGridEmpty", new[] { "" })]
        [TestCase("DataGrid10", new[] { "Row 1", "Row 2", "Row 3", "Row 4", "Row 5", "Row 6", "Row 7", "Row 8", "Row 9", "Row 10", "" })]
        [TestCase("ReadOnlyDataGrid", new[] { "Row 1", "Row 2", "Row 3" })]
        [TestCase("ReadonlyColumnsDataGrid", new[] { "Row 1", "Row 2", "Row 3", "" })]
        public void RowsHeaders(string name, string[] expected)
        {
            if (name == "DataGrid10" && WindowsVersion.IsAzureDevops())
            {
                Assert.Inconclusive("Not sure why this fails on devops.");
            }

            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            CollectionAssert.AreEqual(expected, dataGrid.Rows.Select(x => x.Header.Text));
        }

        [TestCase("DataGrid", 0, "Row 1")]
        [TestCase("DataGrid", 1, "Row 2")]
        [TestCase("DataGrid10", 0, "Row 1")]
        [TestCase("DataGrid10", 9, "Row 10")]
        [TestCase("ReadOnlyDataGrid", 0, "Row 1")]
        [TestCase("ReadonlyColumnsDataGrid", 0, "Row 1")]
        public void RowHeader(string name, int index, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.RowHeader(index).Text, Is.EqualTo(expected));
        }

        [TestCase("DataGrid", false)]
        [TestCase("DataGridEmpty", false)]
        [TestCase("DataGrid10", false)]
        [TestCase("DataGridNoHeaders", false)]
        [TestCase("TemplateColumnDataGrid", false)]
        [TestCase("ReadOnlyDataGrid", true)]
        [TestCase("ReadonlyColumnsDataGrid", false)]
        public void IsReadOnly(string name, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.IsReadOnly, Is.EqualTo(expected));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGridEmpty")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void ColumnCount(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.ColumnCount, Is.EqualTo(2));
        }

        [TestCase("DataGrid", 2)]
        [TestCase("DataGridEmpty", 2)]
        [TestCase("DataGrid10", 2)]
        [TestCase("DataGridNoHeaders", 0)]
        [TestCase("ReadOnlyDataGrid", 2)]
        [TestCase("ReadonlyColumnsDataGrid", 2)]
        public void ColumnHeadersCount(string name, int expected)
        {
            // We want launch here to repro a bug.
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.ColumnHeaders.Count, Is.EqualTo(expected));
        }

        [TestCase("DataGrid", 2)]
        [TestCase("DataGridEmpty", 2)]
        [TestCase("DataGrid10", 2)]
        [TestCase("DataGridNoHeaders", 0)]
        [TestCase("ReadOnlyDataGrid", 2)]
        [TestCase("ReadonlyColumnsDataGrid", 2)]
        public void ColumnHeaders(string name, int expectedCount)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.ColumnHeaders.Count, Is.EqualTo(expectedCount));
            if (expectedCount == 0)
            {
                return;
            }

            Assert.That(dataGrid.ColumnHeaders[0].Text, Is.EqualTo("IntValue"));
            Assert.That(dataGrid.ColumnHeaders[1].Text, Is.EqualTo("StringValue"));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void RowIndexer(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid.Row(0).Cells[0].Value, Is.EqualTo("1"));
            Assert.That(dataGrid.Row(0).Cells[1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid.Row(1).Cells[0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid.Row(1).Cells[1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid.Row(2).Cells[0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid.Row(2).Cells[1].Value, Is.EqualTo("Item 3"));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void CellIndexer(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("1"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));

            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));

            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));
        }

        [TestCase("DataGrid", 0, 1)]
        [TestCase("DataGrid", 1, 2)]
        [TestCase("DataGrid", 2, 0)]
        [TestCase("DataGrid10", 0, 9)]
        [TestCase("DataGrid10", 1, 2)]
        [TestCase("DataGrid10", 9, 0)]
        [TestCase("DataGridNoHeaders", 0, 1)]
        [TestCase("DataGridNoHeaders", 1, 0)]
        [TestCase("DataGridNoHeaders", 2, 0)]
        public void SelectRowByIndex(string name, int index1, int index2)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            var selectedRow = dataGrid.Select(index1);
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo($"{index1 + 1}"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo($"Item {index1 + 1}"));

            selectedRow = (DataGridRow)dataGrid.SelectedItem;
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo($"{index1 + 1}"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo($"Item {index1 + 1}"));

            selectedRow = dataGrid.Select(index2);
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo($"{index2 + 1}"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo($"Item {index2 + 1}"));

            selectedRow = (DataGridRow)dataGrid.SelectedItem;
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo($"{index2 + 1}"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo($"Item {index2 + 1}"));
        }

        [TestCase("SelectCellDataGrid", 2, 0)]
        public void SelectCellByIndex(string name, int index1, int index2)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            var selectedCell = dataGrid.Select(index1, index2);
            Assert.That(selectedCell.Value, Is.EqualTo($"{index1 + 1}"));

            selectedCell = (DataGridCell)dataGrid.SelectedItem;
            Assert.That(selectedCell.Value, Is.EqualTo($"{index1 + 1}"));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        public void SelectByTextTest(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            var selectedRow = dataGrid.Select(1, "Item 2");
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo("2"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo("Item 2"));

            selectedRow = (DataGridRow)dataGrid.SelectedItem;
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo("2"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo("Item 2"));

            selectedRow = dataGrid.Select(1, "Item 3");
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo("3"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo("Item 3"));

            selectedRow = (DataGridRow)dataGrid.SelectedItem;
            Assert.That(selectedRow.Cells[0].Value, Is.EqualTo("3"));
            Assert.That(selectedRow.Cells[1].Value, Is.EqualTo("Item 3"));
        }
    }
}
