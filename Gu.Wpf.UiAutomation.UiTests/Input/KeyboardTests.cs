namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.Diagnostics;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    public class KeyboardTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "SingleTextBoxWindow";

        [Test]
        public void TypeKeysThenBackspace()
        {
            using var app = Application.Launch(ExeFileName, WindowName);
            var window = app.MainWindow;
            Keyboard.Type("abc");
            Keyboard.Type(Key.BACK);
            var textBox = window.FindTextBox();
            window.WaitUntilResponsive();
            Assert.That(textBox.Text, Is.EqualTo("ab"));
        }

        [TestCase(Key.KEY_Z, "z")]
        public void TypeKey(Key key, string expected)
        {
            using var app = Application.Launch(ExeFileName, WindowName);
            var mainWindow = app.MainWindow;
            Keyboard.Type(key);
            var textBox = mainWindow.FindTextBox();
            Assert.That(textBox.Text, Is.EqualTo(expected));
        }

        [TestCase(new[] { Key.KEY_Z }, "z")]
        [TestCase(new[] { Key.KEY_Z, Key.KEY_Z, Key.KEY_Z }, "zzz")]
        public void TypeKeys(Key[] keys, string expected)
        {
            using var app = Application.Launch(ExeFileName, WindowName);
            var mainWindow = app.MainWindow;
            Keyboard.Type(keys);
            var textBox = mainWindow.FindTextBox();
            Assert.That(textBox.Text, Is.EqualTo(expected));
        }

        [TestCase("abc")]
        [TestCase("ééééééööööö aaa | ")]
        [TestCase("ঋ ঌ এ ঐ ও ঔ ক খ গ ঘ ঙ চ ছ জ ঝ ঞ ট ঠ ড ঢ")]
        public void TypeString(string text)
        {
            using var app = Application.Launch(ExeFileName, WindowName);
            var mainWindow = app.MainWindow;
            Keyboard.Type(text);
            var textBox = mainWindow.FindTextBox();
            Wait.UntilInputIsProcessed();
            Assert.That(textBox.Text, Is.EqualTo(text));
        }

        [TestCase(Key.KEY_Z, "Z")]
        public void TypeKeyWhileHoldingShift(Key key, string expected)
        {
            using var app = Application.Launch(ExeFileName, WindowName);
            var mainWindow = app.MainWindow;
            using (Keyboard.Hold(Key.SHIFT))
            {
                Keyboard.Type(key);
            }

            var textBox = mainWindow.FindTextBox();
            Assert.That(textBox.Text, Is.EqualTo(expected));
        }

        [TestCase(ScanCodeShort.KEY_A, false, "a")]
        public void PressScanCode(ScanCodeShort scanCode, bool isExtendedKey, string expected)
        {
            using var app = Application.Launch(ExeFileName, WindowName);
            var mainWindow = app.MainWindow;
#pragma warning disable CS0618 // Type or member is obsolete
            Keyboard.PressScanCode((ushort)scanCode, isExtendedKey);
#pragma warning restore CS0618 // Type or member is obsolete
            Keyboard.ReleaseScanCode((ushort)scanCode, isExtendedKey);
            Wait.UntilInputIsProcessed();
            var textBox = mainWindow.FindTextBox();
            Wait.For(TimeSpan.FromMilliseconds(20));
            Assert.That(textBox.Text, Is.EqualTo(expected));
        }
    }
}
