namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using System.Windows.Automation;
    using NUnit.Framework;

    public class ExpandCollapsePatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void ExpanderTest()
        {
            using var app = Application.Launch(ExeFileName, "ExpanderWindow");
            var window = app.MainWindow;
            var expander = window.FindExpander();
            Assert.That(expander, Is.Not.Null);
            var ecp = expander.AutomationElement.ExpandCollapsePattern();
            Assert.That(ecp.Current.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Expanded));

            ecp.Collapse();
            Assert.That(ecp.Current.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Collapsed));

            ecp.Expand();
            Assert.That(ecp.Current.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Expanded));
        }
    }
}
