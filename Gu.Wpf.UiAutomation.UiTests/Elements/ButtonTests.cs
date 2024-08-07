namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class ButtonTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [TestCase(null)]
        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButton(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton(key);
            Assert.That(UiElement.FromAutomationElement(button.AutomationElement), Is.AssignableFrom<Button>());
        }

        [TestCase("AutomationId", "AutomationProperties.AutomationId")]
        [TestCase("XName", "x:Name")]
        [TestCase("Content", "Content")]
        public void Text(string key, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton(key);
            Assert.That(button.Text, Is.EqualTo(expected));
        }

        [TestCase("AutomationId", "AutomationProperties.AutomationId")]
        [TestCase("XName", "x:Name")]
        [TestCase("Content", "Content")]
        public void Content(string key, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton(key);
            Assert.That(((TextBlock)button.Content).Text, Is.EqualTo(expected));
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButtonThrowsWhenNotFound(string key)
        {
            Retry.Time = TimeSpan.FromMilliseconds(10);
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var exception = Assert.Throws<InvalidOperationException>(() => window.FindButton(key));
            Assert.That(exception.Message, Is.EqualTo($"Did not find a Button matching ((ControlType == Button && IsTogglePatternAvailable == False) && (Name == {key} || AutomationId == {key}))."));
        }

        [Test]
        public void Click()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Test Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("1"));
        }

        [Test]
        public void ClickThrice()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Test Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("1"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("2"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("3"));
        }

        [Test]
        public void Invoke()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Test Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("1"));
        }

        [Test]
        public void InvokeThrice()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Test Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("1"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("2"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("3"));
        }

        [Test]
        public void ClickSleep()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Sleep Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("1"));
        }

        [Test]
        public void ClickSleepThrice()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Sleep Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("1"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("2"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("3"));
        }

        [Test]
        public void InvokeSleep()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Sleep Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("1"));
        }

        [Test]
        public void InvokeSleepThrice()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Sleep Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("1"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("2"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("3"));
        }

        [Test]
        public void ClickThenInvoke()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Test Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            button.Click();
            Assert.That(textBlock.Text, Is.EqualTo("1"));

            button.Invoke();
            Assert.That(textBlock.Text, Is.EqualTo("2"));
        }

        [Test]
        public void LaunchClickCloseLaunchInvoke()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.That(textBlock.Text, Is.EqualTo("0"));

                button.Click();
                Assert.That(textBlock.Text, Is.EqualTo("1"));
            }

            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.That(textBlock.Text, Is.EqualTo("0"));

                button.Invoke();
                Assert.That(textBlock.Text, Is.EqualTo("1"));
            }
        }

        [Test]
        public void GetClickablePoint()
        {
            using var app = Application.Launch(ExeFileName, "ButtonWindow");
            var window = app.MainWindow;
            var button = window.FindButton("Test Button");
            var textBlock = window.FindTextBlock("CountTextBlock");
            Assert.That(textBlock.Text, Is.EqualTo("0"));

            Mouse.LeftClick(button.GetClickablePoint());
#pragma warning disable NUnit2045 // Use Assert.Multiple
            Assert.That(textBlock.Text, Is.EqualTo("1"));
#pragma warning restore NUnit2045 // Use Assert.Multiple

            Assert.That(button.TryGetClickablePoint(out var p), Is.EqualTo(true));
            Mouse.LeftClick(p);
            Assert.That(textBlock.Text, Is.EqualTo("2"));
        }
    }
}
