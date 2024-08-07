// ReSharper disable AssignmentIsFullyDiscarded
namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class GroupBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        private const string WindowName = "GroupBoxWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void FindGroupBox(string key, string header)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox(key);
            Assert.That(groupBox.HeaderText, Is.EqualTo(header));
            Assert.NotNull(groupBox.FindTextBlock());
            Assert.IsInstanceOf<GroupBox>(UiElement.FromAutomationElement(groupBox.AutomationElement));
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void Header(string key, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox(key);
            Assert.That(groupBox.HeaderText, Is.EqualTo(expected));
            var header = groupBox.Header;
            Assert.That(((TextBlock)header).Text, Is.EqualTo(expected));
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "3")]
        public void Content(string key, string content)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox(key);
            Assert.That(((TextBlock)groupBox.Content).Text, Is.EqualTo(content));
            Assert.That(((TextBlock)groupBox.ContentCollection[0]).Text, Is.EqualTo(content));
        }

        [Test]
        public void ContentCollection()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox("WithItemsControl");
            Assert.That(groupBox.HeaderText, Is.EqualTo("WithItemsControl"));
            Assert.That(((TextBlock)groupBox.Header).Text, Is.EqualTo("WithItemsControl"));
            Assert.Throws<InvalidOperationException>(() => _ = groupBox.Content);
            var content = groupBox.ContentCollection;
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(((TextBlock)content[0]).Text, Is.EqualTo("1"));
            Assert.That(((TextBlock)content[1]).Text, Is.EqualTo("2"));
        }

        [Test]
        public void ContentElements()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox("WithItemsControl");
            Assert.That(groupBox.HeaderText, Is.EqualTo("WithItemsControl"));
            Assert.That(((TextBlock)groupBox.Header).Text, Is.EqualTo("WithItemsControl"));
            Assert.Throws<InvalidOperationException>(() => _ = groupBox.Content);
            var content = groupBox.ContentElements(x => new TextBlock(x));
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(content[0].Text, Is.EqualTo("1"));
            Assert.That(content[1].Text, Is.EqualTo("2"));
        }
    }
}
