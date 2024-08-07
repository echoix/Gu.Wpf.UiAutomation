namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Windows.Automation;
    using NUnit.Framework;

    public class TitleBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var titleBar = window.FindTitleBar();
            Assert.IsInstanceOf<TitleBar>(UiElement.FromAutomationElement(titleBar.AutomationElement));
        }

        [Test]
        public void CloseButton()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var titleBar = window.FindTitleBar();
            titleBar.CloseButton.Invoke();
        }

        [Test]
        public void MinimizeButton()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var titleBar = window.FindTitleBar();
            titleBar.MinimizeButton.Invoke();
            Assert.That(window.WindowPattern.Current.WindowVisualState, Is.EqualTo(WindowVisualState.Minimized));
        }

        [Test]
        public void MaximizeButton()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var titleBar = window.FindTitleBar();
            titleBar.MaximizeButton.Invoke();
            Assert.That(window.WindowPattern.Current.WindowVisualState, Is.EqualTo(WindowVisualState.Maximized));
        }

        [Test]
        public void RestoreButton()
        {
            using var app = Application.Launch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var titleBar = window.FindTitleBar();
            titleBar.MaximizeButton.Invoke();
            Assert.That(window.WindowPattern.Current.WindowVisualState, Is.EqualTo(WindowVisualState.Maximized));
            titleBar.RestoreButton.Invoke();
            Assert.That(window.WindowPattern.Current.WindowVisualState, Is.EqualTo(WindowVisualState.Normal));
        }
    }
}
