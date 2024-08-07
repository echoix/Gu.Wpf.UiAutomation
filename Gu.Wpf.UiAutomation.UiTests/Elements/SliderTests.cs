namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Globalization;
    using NUnit.Framework;

    public class SliderTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Find()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            Assert.That(UiElement.FromAutomationElement(slider.AutomationElement), Is.InstanceOf<Slider>());
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;

            Assert.Multiple(() =>
            {
                Assert.That(slider.Minimum, Is.EqualTo(0));
                Assert.That(slider.Maximum, Is.EqualTo(10));
                Assert.That(slider.Value, Is.EqualTo(5));
                Assert.That(slider.SmallChange, Is.EqualTo(1));
                Assert.That(slider.LargeChange, Is.EqualTo(4));
                Assert.That(slider.IsOnlyValue, Is.EqualTo(false));
            });
        }

        [Test]
        public void SlideHorizontally()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;

            var thumb = slider.Thumb;
            Assert.That(thumb.Bounds.Center().ToString(CultureInfo.InvariantCulture), Is.EqualTo("350.5,240"));
            thumb.SlideHorizontally(50);
            Assert.That(thumb.Bounds.Center().ToString(CultureInfo.InvariantCulture), Is.EqualTo("397.5,240"));
        }

        [Test]
        public void Value()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 6;
            Assert.That(slider.Value, Is.EqualTo(6));

            slider.Value = 4;
            Assert.That(slider.Value, Is.EqualTo(4));
        }

        [Test]
        public void SmallIncrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.That(slider.Value, Is.EqualTo(5));

            slider.SmallIncrement();
            Assert.That(slider.Value, Is.EqualTo(6));

            slider.SmallIncrement();
            Assert.That(slider.Value, Is.EqualTo(7));

            slider.SmallIncrement();
            Assert.That(slider.Value, Is.EqualTo(8));
        }

        [Test]
        public void SmallDecrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.That(slider.Value, Is.EqualTo(5));

            slider.SmallDecrement();
            Assert.That(slider.Value, Is.EqualTo(4));

            slider.SmallDecrement();
            Assert.That(slider.Value, Is.EqualTo(3));
        }

        [Test]
        public void LargeIncrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.That(slider.Value, Is.EqualTo(5));

            slider.LargeIncrement();
            Assert.That(slider.Value, Is.EqualTo(9));

            slider.LargeIncrement();
            Assert.That(slider.Value, Is.EqualTo(10));
        }

        [Test]
        public void LargeDecrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.That(slider.Value, Is.EqualTo(5));

            slider.LargeDecrement();
            Assert.That(slider.Value, Is.EqualTo(1));

            slider.LargeDecrement();
            Assert.That(slider.Value, Is.EqualTo(0));
        }
    }
}
