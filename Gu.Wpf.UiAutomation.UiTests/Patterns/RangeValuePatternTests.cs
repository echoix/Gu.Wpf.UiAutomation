namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class RangeValuePatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Slider()
        {
            using var app = Application.Launch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider();
            Assert.NotNull(slider);
            var rvPattern = slider.AutomationElement.RangeValuePattern();
            Assert.That(rvPattern.Current.IsReadOnly, Is.EqualTo(false));
            Assert.That(rvPattern.Current.Value, Is.EqualTo(5));
            Assert.That(rvPattern.Current.LargeChange, Is.EqualTo(4));
            Assert.That(rvPattern.Current.SmallChange, Is.EqualTo(1));
            Assert.That(rvPattern.Current.Minimum, Is.EqualTo(0));
            Assert.That(rvPattern.Current.Maximum, Is.EqualTo(10));

            rvPattern.SetValue(6);
            Assert.That(rvPattern.Current.Value, Is.EqualTo(6));

            rvPattern.SetValue(3);
            Assert.That(rvPattern.Current.Value, Is.EqualTo(3));
        }
    }
}
