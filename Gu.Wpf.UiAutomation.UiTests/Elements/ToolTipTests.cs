namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ToolTipTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string Window = "ToolTipWindow";

        [Test]
        public void FindImplicit()
        {
            using var app = Application.Launch(ExeFileName, Window);
            var window = app.MainWindow;
            var button = window.FindButton("With ToolTip");
            Mouse.Position = button.Bounds.Center();
            var toolTip = button.FindToolTip();
            Assert.Multiple(() =>
            {
                Assert.That(toolTip.IsOffscreen, Is.EqualTo(false));
                Assert.That(toolTip.Text, Is.EqualTo("Tool tip text."));
            });
            Assert.IsInstanceOf<ToolTip>(UiElement.FromAutomationElement(toolTip.AutomationElement));

            window.FindButton("Lose focus").Click();
            Assert.That(toolTip.IsOffscreen, Is.EqualTo(true));
        }

        [Test]
        public void FindExplicit()
        {
            using var app = Application.Launch(ExeFileName, Window);
            var window = app.MainWindow;
            var button = window.FindButton("With explicit ToolTip");
            Mouse.Position = button.Bounds.Center();
            var toolTip = button.FindToolTip();
            Assert.Multiple(() =>
            {
                Assert.That(toolTip.IsOffscreen, Is.EqualTo(false));
                Assert.That(toolTip.Text, Is.EqualTo("Explicit tool tip text."));
            });
            Assert.IsInstanceOf<ToolTip>(UiElement.FromAutomationElement(toolTip.AutomationElement));

            window.FindButton("Lose focus").Click();
            Assert.That(toolTip.IsOffscreen, Is.EqualTo(true));
        }
    }
}
