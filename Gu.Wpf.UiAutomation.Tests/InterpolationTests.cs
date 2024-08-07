namespace Gu.Wpf.UiAutomation.Tests
{
    using System;
    using System.Globalization;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    public class InterpolationTests
    {
        [TestCase("0,0", "100,0", 0, "0,0")]
        [TestCase("0,0", "100,0", 100, "50,0")]
        [TestCase("0,0", "100,0", 195, null)]
        [TestCase("0,0", "100,0", 200, null)]
        [TestCase("0,0", "-100,0", 100, "-50,0")]
        [TestCase("0,0", "-100,0", 195, "-100,0")]
        [TestCase("0,0", "0,100", 100, "0,50")]
        [TestCase("0,0", "0,100", 201, null)]
        public void TryGetPosition(string @from, string to, int elapsed, string expected)
        {
            var interpolation = Interpolation.Start(Parse(from), Parse(to), TimeSpan.FromMilliseconds(200));
            Assert.That(interpolation.TryGetPosition(TimeSpan.FromMilliseconds(0), out var p), Is.EqualTo(true));
            Assert.That($"{p.X},{p.Y}", Is.EqualTo(@from));

            if (expected is null)
            {
                Assert.That(interpolation.TryGetPosition(TimeSpan.FromMilliseconds(elapsed), out p), Is.EqualTo(true));
                Assert.That($"{p.X},{p.Y}", Is.EqualTo(to));
                Assert.That(interpolation.TryGetPosition(TimeSpan.FromMilliseconds(elapsed), out _), Is.EqualTo(false));
            }
            else
            {
                Assert.That(interpolation.TryGetPosition(TimeSpan.FromMilliseconds(elapsed), out p), Is.EqualTo(true));
                Assert.That($"{p.X},{p.Y}", Is.EqualTo(expected));
            }
        }

        private static POINT Parse(string text)
        {
            var texts = text.Split(',');
            Assert.That(texts.Length, Is.EqualTo(2));
            return new POINT(
                int.Parse(texts[0], CultureInfo.InvariantCulture),
                int.Parse(texts[1], CultureInfo.InvariantCulture));
        }
    }
}
