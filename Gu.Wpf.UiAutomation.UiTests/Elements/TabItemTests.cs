// ReSharper disable AssignmentIsFullyDiscarded
namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class TabItemTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "TabControlWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tabItem = window.FindTabControl().Items[0];
            Assert.IsInstanceOf<TabItem>(UiElement.FromAutomationElement(tabItem.AutomationElement));
        }

        [TestCase(0, "x:Name", "1")]
        [TestCase(1, "Header", "2")]
        [TestCase(2, "AutomationProperties.AutomationId", "3")]
        public void Header(int index, string header, string content)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            var tabItem = tab.Select(index);
            Assert.Multiple(() =>
            {
                Assert.That(tabItem.HeaderText, Is.EqualTo(header));
                Assert.That(((TextBlock)tabItem.Header).Text, Is.EqualTo(header));
                Assert.That(((TextBlock)tabItem.Content).Text, Is.EqualTo(content));
            });
        }

        [TestCase(0, "x:Name", "1")]
        [TestCase(1, "Header", "2")]
        [TestCase(2, "AutomationProperties.AutomationId", "3")]
        public void Content(int index, string header, string content)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            var tabItem = tab.Select(index);
            Assert.Multiple(() =>
            {
                Assert.That(tabItem.HeaderText, Is.EqualTo(header));
                Assert.That(((TextBlock)tabItem.Header).Text, Is.EqualTo(header));
                Assert.That(((TextBlock)tabItem.Content).Text, Is.EqualTo(content));
                Assert.That(((TextBlock)tabItem.ContentCollection[0]).Text, Is.EqualTo(content));
            });
        }

        [Test]
        public void ItemsControlContent()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            var tabItem = tab.Select("WithItemsControl");
            Assert.Multiple(() =>
            {
                Assert.That(tabItem.HeaderText, Is.EqualTo("WithItemsControl"));
                Assert.That(((TextBlock)tabItem.Header).Text, Is.EqualTo("WithItemsControl"));
            });
            Assert.Throws<InvalidOperationException>(() => _ = tabItem.Content);
            var content = tabItem.ContentCollection;
            Assert.Multiple(() =>
            {
                Assert.That(content.Count, Is.EqualTo(2));
                Assert.That(((TextBlock)content[0]).Text, Is.EqualTo("1"));
                Assert.That(((TextBlock)content[1]).Text, Is.EqualTo("2"));
            });
        }

        [Test]
        public void ContentThrowsWhenNotSelected()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            tab.SelectedIndex = 0;
            var exception = Assert.Throws<InvalidOperationException>(() => _ = tab.Items[1].Content);
            Assert.That(exception.Message, Is.EqualTo("TabItem must have be selected to get Content."));
        }
    }
}
