namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class DataGridCellTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("DataGrid", false)]
        [TestCase("DataGrid10", false)]
        [TestCase("DataGridNoHeaders", false)]
        [TestCase("ReadOnlyDataGrid", true)]
        [TestCase("ReadonlyColumnsDataGrid", true)]
        [TestCase("TemplateColumnDataGrid", false)]
        public void IsReadOnly(string name, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid[0, 0].IsReadOnly, Is.EqualTo(expected));
            Assert.That(dataGrid[0, 1].IsReadOnly, Is.EqualTo(expected));
            Assert.That(dataGrid[1, 0].IsReadOnly, Is.EqualTo(expected));
            Assert.That(dataGrid[1, 1].IsReadOnly, Is.EqualTo(expected));
            Assert.That(dataGrid[2, 0].IsReadOnly, Is.EqualTo(expected));
            Assert.That(dataGrid[2, 1].IsReadOnly, Is.EqualTo(expected));
            if (name != "ReadOnlyDataGrid")
            {
                Assert.That(dataGrid[3, 0].IsReadOnly, Is.EqualTo(expected));
                Assert.That(dataGrid[3, 1].IsReadOnly, Is.EqualTo(expected));
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void ContainingGrid(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid[0, 0].ContainingDataGrid, Is.EqualTo(dataGrid));
            Assert.That(dataGrid[0, 1].ContainingDataGrid, Is.EqualTo(dataGrid));
            Assert.That(dataGrid[1, 0].ContainingDataGrid, Is.EqualTo(dataGrid));
            Assert.That(dataGrid[1, 1].ContainingDataGrid, Is.EqualTo(dataGrid));
            Assert.That(dataGrid[2, 0].ContainingDataGrid, Is.EqualTo(dataGrid));
            Assert.That(dataGrid[2, 1].ContainingDataGrid, Is.EqualTo(dataGrid));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void NewItemPlaceholder(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid[0, 0].IsNewItemPlaceholder, Is.EqualTo(false));
            Assert.That(dataGrid[dataGrid.RowCount - 1, 0].IsNewItemPlaceholder, Is.EqualTo(true));
            Assert.That(dataGrid[dataGrid.RowCount - 1, 0].Value, Is.EqualTo(string.Empty));
            Assert.That(dataGrid[dataGrid.RowCount - 1, 1].IsNewItemPlaceholder, Is.EqualTo(true));
            Assert.That(dataGrid[dataGrid.RowCount - 1, 1].Value, Is.EqualTo(string.Empty));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        public void Enter(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("1"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[0, 0].Enter("11");
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("1"));
            Assert.That(dataGrid[0, 0].FindTextBox().Text, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[1, 1].Click();
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 0].FindTextBlock().Text, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[0, 0].Enter("111");
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));
        }

        [Test]
        public void EnterTemplateColumn()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid("TemplateColumnDataGrid");

            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("1"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[0, 0].Enter("11");
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 0].FindTextBox().Text, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[0, 0].Enter("111");
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("111"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));
        }

        [TestCase("DataGrid")]
        [TestCase("SelectCellDataGrid")]
        public void EnterInvalidValue(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            Assert.That(cell.Value, Is.EqualTo("1"));

            cell.Enter("a");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.That(cell.Value, Is.EqualTo("1"));
            Assert.That(cell.FindTextBox().Text, Is.EqualTo("a"));

            cell.Enter("11");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.That(cell.Value, Is.EqualTo("11"));
            Assert.That(cell.FindTextBlock().Text, Is.EqualTo("11"));
        }

        [Test]
        public void EnterInvalidValueTemplateColumn()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid("TemplateColumnDataGrid");

            var cell = dataGrid[0, 0];
            Assert.That(cell.Value, Is.EqualTo("1"));

            cell.Enter("a");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.That(cell.Value, Is.EqualTo("a"));
            Assert.That(cell.FindTextBox().Text, Is.EqualTo("a"));

            cell.Enter("11");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.That(cell.Value, Is.EqualTo("11"));
            Assert.That(cell.FindTextBlock().Text, Is.EqualTo("11"));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void SetValue(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("1"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[0, 0].Value = "11";
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 3"));

            dataGrid[2, 1].Value = "Item 5";
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 0].FindTextBlock().Text, Is.EqualTo("11"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 5"));

            dataGrid[0, 0].Value = "111";
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("111"));
            Assert.That(dataGrid[0, 1].Value, Is.EqualTo("Item 1"));
            Assert.That(dataGrid[1, 0].Value, Is.EqualTo("2"));
            Assert.That(dataGrid[1, 1].Value, Is.EqualTo("Item 2"));
            Assert.That(dataGrid[2, 0].Value, Is.EqualTo("3"));
            Assert.That(dataGrid[2, 1].Value, Is.EqualTo("Item 5"));
            Assert.That(dataGrid[2, 1].FindTextBlock().Text, Is.EqualTo("Item 5"));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void SetValueWhenClickedOnce(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            Assert.That(cell.Value, Is.EqualTo("1"));

            cell.Click();
            cell.Value = "11";
            Assert.That(cell.Value, Is.EqualTo("11"));
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void SetValueWhenClickedTwice(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            Assert.That(cell.Value, Is.EqualTo("1"));

            cell.Click();
            cell.Click();
            cell.Value = "11";
            Assert.That(cell.Value, Is.EqualTo("11"));
        }

        [Explicit("Dunno if this is possible.")]
        [TestCase("DataGrid")]
        [TestCase("SelectCellDataGrid")]
        public void SetInvalidValueThrows(string name)
        {
            Assert.Inconclusive("VS test runner does not understand [Explicit].");
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            var exception = Assert.Throws<InvalidOperationException>(() => cell.Value = "a");
            Assert.That(exception.Message, Is.EqualTo("Failed setting value."));
        }

        [Test]
        public void SetValueUpdatesBinding()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid("DataGrid");
            var readOnly = window.FindDataGrid("ReadOnlyDataGrid");

            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("1"));
            Assert.That(readOnly[0, 0].Value, Is.EqualTo("1"));

            dataGrid[0, 0].Value = "11";
            Assert.That(dataGrid[0, 0].Value, Is.EqualTo("11"));
            Assert.Inconclusive("Figure out the least ugly way here.");
            //// ReSharper disable once HeuristicUnreachableCode
            Assert.That(readOnly[0, 0].Value, Is.EqualTo("11"));
        }

        [TestCase("DataGrid10", 9)]
        [TestCase("DataGrid10", 10)]
        public void SetValueWhenOffScreen(string name, int row)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            dataGrid[row, 0].Value = "-1";
            dataGrid[row, 1].Value = "Item -1";
            Assert.That(dataGrid[row, 0].Value, Is.EqualTo("-1"));
            Assert.That(dataGrid[row, 1].Value, Is.EqualTo("Item -1"));
        }

        [TestCase("DataGrid10")]
        public void GetValueWhenOffScreen(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.That(dataGrid[9, 0].Value, Is.EqualTo("10"));
            Assert.That(dataGrid[9, 1].Value, Is.EqualTo("Item 10"));
        }
    }
}
