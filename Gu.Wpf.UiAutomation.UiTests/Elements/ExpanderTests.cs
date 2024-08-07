// ReSharper disable AssignmentIsFullyDiscarded
namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class ExpanderTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        private const string WindowName = "ExpanderWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void FindExpander(string key, string header)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander(key);
            Assert.That(expander.HeaderText, Is.EqualTo(header));
            Assert.NotNull(expander.FindTextBlock());
            Assert.IsInstanceOf<Expander>(UiElement.FromAutomationElement(expander.AutomationElement));
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void Header(string key, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander(key);
            Assert.That(expander.HeaderText, Is.EqualTo(expected));
            var header = expander.Header;
            Assert.That(((ToggleButton)header).Text, Is.EqualTo(expected));
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "3")]
        public void Content(string key, string content)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander(key);
            Assert.That(((TextBlock)expander.Content).Text, Is.EqualTo(content));
            Assert.That(((TextBlock)expander.ContentCollection[0]).Text, Is.EqualTo(content));
        }

        [Test]
        public void ContentCollection()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander("WithItemsControl");
            expander.IsExpanded = true;
            Assert.That(expander.HeaderText, Is.EqualTo("WithItemsControl"));
            Assert.That(((ToggleButton)expander.Header).Text, Is.EqualTo("WithItemsControl"));
            Assert.Throws<InvalidOperationException>(() => _ = expander.Content);
            var content = expander.ContentCollection;
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(((TextBlock)content[0]).Text, Is.EqualTo("1"));
            Assert.That(((TextBlock)content[1]).Text, Is.EqualTo("2"));
        }

        [Test]
        public void ContentElements()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander("WithItemsControl");
            expander.IsExpanded = true;
            Assert.That(expander.HeaderText, Is.EqualTo("WithItemsControl"));
            Assert.That(((ToggleButton)expander.Header).Text, Is.EqualTo("WithItemsControl"));
            Assert.Throws<InvalidOperationException>(() => _ = expander.Content);
            var content = expander.ContentElements(x => new TextBlock(x));
            Assert.That(content.Count, Is.EqualTo(2));
            Assert.That(content[0].Text, Is.EqualTo("1"));
            Assert.That(content[1].Text, Is.EqualTo("2"));
        }

        [Test]
        public void IsExpanded()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander("AutomationId");
            expander.IsExpanded = true;
            Assert.That(expander.IsExpanded, Is.EqualTo(true));

            expander.IsExpanded = false;
            Assert.That(expander.IsExpanded, Is.EqualTo(false));

            expander.IsExpanded = true;
            Assert.That(expander.IsExpanded, Is.EqualTo(true));
        }

        [Test]
        public void ExpandCollapse()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var expander = window.FindExpander("AutomationId");
            expander.IsExpanded = true;
            Assert.That(expander.IsExpanded, Is.EqualTo(true));

            expander.Collapse();
            Assert.That(expander.IsExpanded, Is.EqualTo(false));

            expander.Expand();
            Assert.That(expander.IsExpanded, Is.EqualTo(true));
        }
    }
}
