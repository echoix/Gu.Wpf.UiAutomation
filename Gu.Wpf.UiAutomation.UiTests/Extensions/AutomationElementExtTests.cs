namespace Gu.Wpf.UiAutomation.UiTests.Extensions
{
    using System.Linq;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class AutomationElementExtTests
    {
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched("WpfApplication.exe");
        }

        [Test]
        public void Parent()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var parent = window.AutomationElement.Parent();
            Assert.Multiple(() =>
            {
                Assert.That(parent.ClassName(), Is.EqualTo("#32769"));
                Assert.That(parent, Is.EqualTo(Desktop.AutomationElement));
            });

            var checkbox = window.AutomationElement.FindFirst(TreeScope.Descendants, Conditions.CheckBox);
            Assert.That(checkbox.Parent(), Is.EqualTo(window.AutomationElement));
        }

        [Test]
        public void TryFindFirstAncestorWindow()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var checkBox = window.FindTextBlock("TextBlock1");
            Assert.Multiple(() =>
            {
                Assert.That(checkBox.AutomationElement.TryFindFirst(TreeScope.Ancestors, Conditions.Window, out var element), Is.EqualTo(true));
                Assert.That(element, Is.EqualTo(window.AutomationElement));
            });
        }

        [Test]
        public void TryFindFirstCheckBox()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            Assert.Multiple(() =>
            {
                Assert.That(window.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.CheckBox, out var element), Is.EqualTo(true));
                Assert.That(element.Name(), Is.EqualTo("CheckBox1Content"));
            });
        }

        [Test]
        public void TryFindFirstTextBlock()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            Assert.Multiple(() =>
            {
                Assert.That(window.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBlock, out var element), Is.EqualTo(true));
                Assert.That(element.Name(), Is.EqualTo("TextBlock1"));
            });
        }

        [Test]
        public void TryFindFirstTextBlockAndNameWhenMissing()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            Assert.That(window.AutomationElement.TryFindFirst(TreeScope.Children, new AndCondition(Conditions.TextBlock, Conditions.ByName("missing")), out _), Is.EqualTo(false));
        }

        [Test]
        public void FindAllTextBlockChildren()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var children = window.AutomationElement.FindAllChildren(Conditions.TextBlock);
            CollectionAssert.AreEqual(new[] { "TextBlock1", "TextBlock2" }, children.OfType<AutomationElement>().Select(x => x.Name()));
        }

        [Test]
        public void FindAllLabelChildren()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var children = window.AutomationElement.FindAllChildren(Conditions.Label);
            CollectionAssert.AreEqual(new[] { "Label1", "Label2" }, children.OfType<AutomationElement>().Select(x => x.Name()));
        }

        [Test]
        public void FindAllTextBoxChildren()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var children = window.AutomationElement.FindAllChildren(Conditions.TextBox);
            CollectionAssert.AreEqual(new[] { "TextBox1", "TextBox2" }, children.OfType<AutomationElement>().Select(x => x.ValuePattern().Current.Value));
        }
    }
}
