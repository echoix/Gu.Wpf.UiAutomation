namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using System.Drawing;

    using NUnit.Framework;

    public partial class UiElementTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Parent()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            var parent = button.Parent;
            Assert.That(parent.AutomationElement, Is.EqualTo(window.AutomationElement));
        }

        [Test]
        public void Window()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            var buttonWindow = button.Window;
            Assert.Multiple(() =>
            {
                Assert.That(buttonWindow.AutomationElement, Is.EqualTo(window.AutomationElement));
                Assert.That(buttonWindow.IsMainWindow, Is.EqualTo(true));
            });
        }

        [Test]
        public void IsKeyboardFocusable()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox();
            Assert.That(textBox.IsKeyboardFocusable, Is.EqualTo(true));
        }

        [Test]
        public void HasKeyboardFocus()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox();
            Assert.That(textBox.HasKeyboardFocus, Is.EqualTo(false));

            textBox.Click();
            Assert.That(textBox.HasKeyboardFocus, Is.EqualTo(true));

            Keyboard.ClearFocus();
        }

        [Test]
        public void Size()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            Assert.Multiple(() =>
            {
                Assert.That(button.ActualWidth, Is.EqualTo(200));
                Assert.That(button.ActualHeight, Is.EqualTo(100));
            });
        }

        [Test]
        public void RenderBounds()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            window.MoveTo(100, 200);
            if (WindowsVersion.IsWindows7())
            {
                Assert.Multiple(() =>
                {
                    Assert.That(button.Bounds, Is.EqualTo(new System.Windows.Rect(150, 311, 200, 100)));
                    Assert.That(window.Bounds, Is.EqualTo(new System.Windows.Rect(100, 200, 300, 300)));
                    Assert.That(button.RenderBounds, Is.EqualTo(new System.Windows.Rect(50, 111, 200, 100)));
                });
            }
            else
            {
                Assert.Multiple(() =>
                {
                    Assert.That(button.Bounds, Is.EqualTo(new System.Windows.Rect(150, 311, 200, 100)));
                    Assert.That(window.Bounds, Is.EqualTo(new System.Windows.Rect(100, 200, 300, 300)));
                    Assert.That(button.RenderBounds, Is.EqualTo(new System.Windows.Rect(50, 111, 200, 100)));
                });
            }
        }

        [Test]
        public void DrawHighlight()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            _ = button.DrawHighlight();
            var bounds = button.Bounds;
            bounds.Inflate(2, 2);
            using var actual = Capture.Rectangle(bounds);
            ImageAssert.AreEqual(Properties.Resources.HiglightedButton, actual);
        }

        [Test]
        public void DrawHighlightBlocking()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            _ = button.DrawHighlight(blocking: true, color: Color.Blue, duration: TimeSpan.FromMilliseconds(500));
        }
    }
}
