namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class ScrollItemPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Test()
        {
            using var app = Application.Launch(ExeFileName, "LargeListViewWindow");
            var window = app.MainWindow;
            var listView = window.FindListView();
            var gridPattern = listView.AutomationElement.GridPattern();
            Assert.Multiple(() =>
            {
                Assert.That(gridPattern.Current.ColumnCount, Is.EqualTo(2));
                Assert.That(gridPattern.Current.RowCount, Is.EqualTo(7));
            });

            ItemRealizer.RealizeItems(listView);
            Assert.That(gridPattern.Current.RowCount, Is.EqualTo(listView.Items.Count));
            var scrollPattern = listView.AutomationElement.ScrollPattern();
            Assert.That(scrollPattern.Current.VerticalScrollPercent, Is.EqualTo(0));
            foreach (var item in listView.Items)
            {
                var scrollItemPattern = item.AutomationElement.ScrollItemPattern();
                Assert.That(scrollItemPattern, Is.Not.Null);
                item.ScrollIntoView();
            }

            Assert.That(scrollPattern.Current.VerticalScrollPercent, Is.EqualTo(100));
        }
    }
}
