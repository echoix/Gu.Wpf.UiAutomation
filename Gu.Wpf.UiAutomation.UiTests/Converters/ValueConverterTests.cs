namespace Gu.Wpf.UiAutomation.UiTests.Converters
{
    using System.Windows.Automation;
    using NUnit.Framework;

    public class ValueConverterTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void CheckBoxControlType()
        {
            using var app = Application.Launch(ExeFileName);
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("Test Checkbox");
            Assert.That(checkBox.ControlType, Is.EqualTo(ControlType.CheckBox));
        }

        [Test]
        public void CheckBoxIsChecked()
        {
            using var app = Application.Launch(ExeFileName);
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("Test Checkbox");
            Assert.That(checkBox.IsChecked, Is.EqualTo(false));
        }

        [Test]
        public void CheckBoxBounds()
        {
            using var app = Application.Launch(ExeFileName);
            var window = app.MainWindow;
            var checkBox = window.FindCheckBox("Test Checkbox");
            Assert.That(checkBox.Bounds, Is.InstanceOf<System.Windows.Rect>());
        }
    }
}
