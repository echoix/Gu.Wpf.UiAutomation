namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TextBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [TestCase("AutomationId")]
        [TestCase("XName")]
        public void FindTextBox(string key)
        {
            using var app = Application.Launch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox(key);
            Assert.Multiple(() =>
            {
                Assert.That(textBox.IsEnabled, Is.EqualTo(true));
                Assert.That(UiElement.FromAutomationElement(textBox.AutomationElement), Is.InstanceOf<TextBox>());
            });
        }

        [TestCase("AutomationId", false)]
        [TestCase("XName", false)]
        [TestCase("ReadOnlyTextBox", true)]
        public void IsReadOnly(string key, bool expected)
        {
            using var app = Application.Launch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox(key);
            Assert.That(textBox.IsReadOnly, Is.EqualTo(expected));
        }

        [Test]
        public void DirectSetText()
        {
            using var app = Application.Launch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox("TestTextBox");
            Assert.That(textBox.Text, Is.EqualTo("Test TextBox"));

            textBox.Text = "Hello World";
            Assert.That(textBox.Text, Is.EqualTo("Hello World"));

            textBox.Text = string.Empty;
            Assert.That(textBox.Text, Is.EqualTo(string.Empty));

            textBox.Text = null;
            Assert.That(textBox.Text, Is.EqualTo(string.Empty));
        }

        [TestCase("Hello World")]
        public void EnterTest(string text)
        {
            using var app = Application.Launch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox("TestTextBox");
            textBox.Enter(text);
            Assert.That(textBox.Text, Is.EqualTo(text));
        }

        [Test]
        public void Focus()
        {
            using var app = Application.Launch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox("TestTextBox");
            textBox.Focus();
            Assert.That(textBox.HasKeyboardFocus, Is.EqualTo(true));

            textBox = window.FindTextBox("TestTextBox1");
            textBox.Focus();
            Assert.That(textBox.HasKeyboardFocus, Is.EqualTo(true));
        }
    }
}
