namespace Gu.Wpf.UiAutomation.Tests.Extensions
{
    using System.Windows;
    using NUnit.Framework;

    [TestFixture]
    public class RectangleExtTests
    {
        [Test]
        public void EmptyTest()
        {
            var rectangle = new Rect(0, 0, 0, 0);
            var rectangle2 = new Rect(0, 0, 1, 0);
            var rectangle3 = new Rect(0, 0, 0, 1);
            Assert.Multiple(() =>
            {
                Assert.That(rectangle.IsZeroes(), Is.EqualTo(true));
                Assert.That(rectangle2.IsZeroes(), Is.EqualTo(false));
                Assert.That(rectangle3.IsZeroes(), Is.EqualTo(false));
            });
        }

        [Test]
        public void CenterTest()
        {
            var rectangle = new Rect(10, 20, 30, 40);
            Assert.That(new Point(25, 40), Is.EqualTo(rectangle.Center()));
        }
    }
}
