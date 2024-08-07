namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TreeViewTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.Launch(ExeFileName, "TreeViewWindow");
            var window = app.MainWindow;
            var treeView = window.FindTreeView();
            Assert.IsInstanceOf<TreeView>(UiElement.FromAutomationElement(treeView.AutomationElement));
        }

        [Test]
        public void SelectionTest()
        {
            using var app = Application.Launch(ExeFileName, "TreeViewWindow");
            var window = app.MainWindow;
            var tree = window.FindTreeView();
            Assert.IsNull(tree.SelectedTreeViewItem);
            Assert.That(tree.Items.Count, Is.EqualTo(2));
            var treeItem = tree.Items[0];
            treeItem.Expand();
            var item = treeItem.Items[1];
            item.Expand();
            item.Items[0].Select();
            Assert.That(item.Items[0].IsSelected, Is.EqualTo(true));
            Assert.NotNull(tree.SelectedTreeViewItem);
            Assert.That(tree.SelectedTreeViewItem.Text, Is.EqualTo("Lvl3 a"));
        }

        [Test]
        public void IsExpanded()
        {
            using var app = Application.Launch(ExeFileName, "TreeViewWindow");
            var window = app.MainWindow;
            var tree = window.FindTreeView();
            var item = tree.Items[0];
            Assert.That(item.IsExpanded, Is.EqualTo(false));

            item.IsExpanded = true;
            Assert.That(item.IsExpanded, Is.EqualTo(true));

            item.IsExpanded = false;
            Assert.That(item.IsExpanded, Is.EqualTo(false));
        }
    }
}
