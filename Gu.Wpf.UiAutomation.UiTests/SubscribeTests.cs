namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Automation;
    using NUnit.Framework;

    [TestFixture]
    public class SubscribeTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void TextBoxValuePropertyChanges()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "FocusWindow");
            var expected = new List<string>();
            var actual = new List<string>();
            var window = app.MainWindow;
            var textBox = window.FindTextBox("TextBox1");
            textBox.Text = string.Empty;
            using (Subscribe.ToPropertyChangedEvent(
                textBox.AutomationElement,
                TreeScope.Element,
                ValuePattern.ValueProperty,
                (sender, args) => actual.Add($"{((AutomationElement)sender).Current.AutomationId}.{args.Property.ProgrammaticName.Split('.')[1]} = {args.NewValue}")))
            {
                Assert.That(actual, Is.EqualTo(expected).AsCollection);

                textBox.Text = "abc";
                Wait.For(TimeSpan.FromMilliseconds(50));
                expected.Add("TextBox1.ValueProperty = abc");
                Assert.That(actual, Is.EqualTo(expected).AsCollection);

                textBox.Text = string.Empty;
                Wait.For(TimeSpan.FromMilliseconds(50));
                expected.Add("TextBox1.ValueProperty = ");
                Assert.That(actual, Is.EqualTo(expected).AsCollection);

                textBox.Text = "abc";
                Wait.For(TimeSpan.FromMilliseconds(50));
                expected.Add("TextBox1.ValueProperty = abc");
                Assert.That(actual, Is.EqualTo(expected).AsCollection);
            }

            // Checking that we stopped subscribing when disposing
            textBox.Text = string.Empty;
            Assert.That(actual, Is.EqualTo(expected).AsCollection);
        }

        [Test]
        public void TextBoxSubscribeToPropertyChangedEvent()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "FocusWindow");
            var expected = new List<string>();
            var actual = new List<string>();
            var window = app.MainWindow;
            var textBox = window.FindTextBox("TextBox1");
            textBox.Text = string.Empty;
            using (textBox.SubscribeToPropertyChangedEvent(
                TreeScope.Element,
                ValuePattern.ValueProperty,
                (sender, args) => actual.Add($"{sender.AutomationId}.{args.Property.ProgrammaticName.Split('.')[1]} = {args.NewValue}")))
            {
                Assert.That(actual, Is.EqualTo(expected).AsCollection);

                textBox.Text = "abc";
                Wait.UntilInputIsProcessed();
                expected.Add("TextBox1.ValueProperty = abc");
                Assert.That(actual, Is.EqualTo(expected).AsCollection);

                textBox.Text = string.Empty;
                Wait.UntilInputIsProcessed();
                expected.Add("TextBox1.ValueProperty = ");
                Assert.That(actual, Is.EqualTo(expected).AsCollection);

                textBox.Text = "abc";
                Wait.UntilInputIsProcessed();
                expected.Add("TextBox1.ValueProperty = abc");
                Assert.That(actual, Is.EqualTo(expected).AsCollection);
            }

            // Checking that we stopped subscribing when disposing
            textBox.Text = string.Empty;
            Assert.That(actual, Is.EqualTo(expected).AsCollection);
        }

        [Test]
        public void ToFocusChangedEvent()
        {
            if (WindowsVersion.IsWindows10())
            {
                Assert.Inconclusive("This fails on Win 10 for some reason.");
            }

            using var app = Application.AttachOrLaunch(ExeFileName, "FocusWindow");
            var changes = new List<int>();
            var window = app.MainWindow;

            using (Subscribe.ToFocusChangedEvent((sender, args) => changes.Add(args.EventId.Id)))
            {
                var textBox = window.FindTextBox("TextBox2");
                textBox.Focus();
                Wait.For(TimeSpan.FromMilliseconds(200));
                Assert.Multiple(() =>
                {
                    Assert.That(changes, Is.EqualTo(new[] { 20005 }).AsCollection);
                    Assert.That(window.FocusedElement(), Is.EqualTo(textBox));
                });

                var button = window.FindButton("Button1");
                button.Focus();
                Wait.For(TimeSpan.FromMilliseconds(200));
                Assert.Multiple(() =>
                {
                    Assert.That(changes, Is.EqualTo(new[] { 20005, 20005 }).AsCollection);
                    Assert.That(window.FocusedElement(), Is.EqualTo(button));
                });
            }
        }

        [Test]
        public void ToFocusChangedEventPaint()
        {
            using var app = Application.Launch("mspaint");
            var changes = new List<string>();
            var mainWindow = app.MainWindow;
            using (Subscribe.ToFocusChangedEvent((element, _) => changes.Add(element.ToString())))
            {
                Wait.For(TimeSpan.FromMilliseconds(100));
                var button1 = mainWindow.FindButton(GetResizeText());
                button1.Invoke();
                Wait.For(TimeSpan.FromMilliseconds(100));
                var radio2 = mainWindow.FindRadioButton(GetPixelsText());
                Mouse.Click(MouseButton.Left, radio2.GetClickablePoint());
                Wait.For(TimeSpan.FromMilliseconds(100));
                using (Keyboard.Hold(Key.ESCAPE))
                {
                    Wait.For(TimeSpan.FromMilliseconds(100));
                    mainWindow.Close();
                    Assert.That(changes.Count, Is.GreaterThan(0));
                }
            }
        }

        private static string GetResizeText()
        {
            return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName switch
            {
                "de" => "Größe ändern",
                _ => "Resize",
            };
        }

        private static string GetPixelsText()
        {
            return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName switch
            {
                "de" => "Pixel",
                _ => "Pixels",
            };
        }
    }
}
