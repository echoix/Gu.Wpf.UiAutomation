namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class RadioButtonTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindRadioButton(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton = window.FindRadioButton(key);
            Assert.That(radioButton.IsEnabled, Is.EqualTo(true));
            Assert.That(UiElement.FromAutomationElement(radioButton.AutomationElement), Is.InstanceOf<RadioButton>());
        }

        [Test]
        public void IsChecked()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton1 = window.FindRadioButton("Test RadioButton");
            var radioButton2 = window.FindRadioButton("AutomationId");
            radioButton1.IsChecked = true;
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(true));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(false));
            });

            radioButton2.IsChecked = true;
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(false));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(true));
            });

            radioButton1.IsChecked = true;
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(true));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(false));
            });
        }

        [TestCase("Test RadioButton", "Test RadioButton")]
        public void Text(string name, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            Assert.That(window.FindRadioButton(name).Text, Is.EqualTo(expected));
        }

        [Test]
        public void Click()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton1 = window.FindRadioButton("Test RadioButton");
            var radioButton2 = window.FindRadioButton("AutomationId");

            radioButton1.Click();
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(true));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(false));
            });

            radioButton2.Click();
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(false));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(true));
            });

            radioButton1.Click();
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(true));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(false));
            });
        }

        [Test]
        public void SelectSingleRadioButtonTest()
        {
            using var app = Application.Launch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton = window.FindRadioButton("RadioButton1");
            Assert.That(radioButton.IsChecked, Is.EqualTo(false));

            radioButton.IsChecked = true;
            Assert.That(radioButton.IsChecked, Is.EqualTo(true));
        }

        [Test]
        public void SelectRadioButtonGroupTest()
        {
            using var app = Application.Launch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton1 = window.FindRadioButton("RadioButton1");
            var radioButton2 = window.FindRadioButton("RadioButton2");

            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(false));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(false));
            });

            radioButton1.IsChecked = true;
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(true));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(false));
            });

            radioButton2.IsChecked = true;
            Assert.Multiple(() =>
            {
                Assert.That(radioButton1.IsChecked, Is.EqualTo(false));
                Assert.That(radioButton2.IsChecked, Is.EqualTo(true));
            });
        }
    }
}
