namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class CheckBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindCheckBox(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox(key);
            Assert.That(UiElement.FromAutomationElement(checkBox.AutomationElement), Is.InstanceOf<CheckBox>());
        }

        [TestCase(null)]
        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindCheckBoxThrowsWhenNotFound(string key)
        {
            Retry.Time = TimeSpan.FromMilliseconds(10);
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            var window = app.MainWindow;
            var exception = Assert.Throws<InvalidOperationException>(() => window.FindCheckBox(key));
            var expected = key is null
                ? $"Did not find a CheckBox matching ControlType == CheckBox."
                : $"Did not find a CheckBox matching (ControlType == CheckBox && (Name == {key} || AutomationId == {key})).";
            Assert.That(exception.Message, Is.EqualTo(expected));
        }

        [Test]
        public void IsChecked()
        {
            using var app = Application.Launch(ExeFileName, "CheckBoxWindow");
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("Test Checkbox");
            checkBox.IsChecked = true;
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));

            checkBox.IsChecked = false;
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));

            checkBox.IsChecked = true;
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));

            var exception = Assert.Throws<UiAutomationException>(() => checkBox.IsChecked = null);
            Assert.That(exception.Message, Is.EqualTo($"Setting ToggleButton Test Checkbox.IsChecked to null failed."));
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            using var app = Application.Launch(ExeFileName, "CheckBoxWindow");
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("3-Way Test Checkbox");
            checkBox.IsChecked = true;
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));

            checkBox.IsChecked = false;
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));

            checkBox.IsChecked = null;
            Assert.That(checkBox.IsChecked, Is.EqualTo(null));

            checkBox.IsChecked = true;
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));
        }

        [Test]
        public void Click()
        {
            using var app = Application.Launch(ExeFileName, "CheckBoxWindow");
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("Test Checkbox");
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));

            checkBox.Click();
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));

            checkBox.Click();
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));

            checkBox.Click();
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));
        }

        [Test]
        public void ThreeStateClick()
        {
            using var app = Application.Launch(ExeFileName, "CheckBoxWindow");
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("3-Way Test Checkbox");
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));

            checkBox.Click();
            Assert.That(checkBox.IsChecked, Is.EqualTo(true));

            checkBox.Click();
            Assert.That(checkBox.IsChecked, Is.EqualTo(null));

            checkBox.Click();
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));
        }
    }
}
