namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ContextMenuTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void ContextMenuTest()
        {
            using var app = Application.Launch(ExeFileName);
            var window = app.MainWindow;
            var btn = window.FindButton("With ContextMenu");
            btn.RightClick();
            var ctxMenu = window.ContextMenu;
            var subMenuLevel1 = ctxMenu.Items;
            Assert.That(subMenuLevel1, Has.Count.EqualTo(2));
            var subMenuLevel2 = subMenuLevel1[1].Items;
            Assert.That(subMenuLevel2, Has.Count.EqualTo(1));
            var innerItem = subMenuLevel2[0];
            Assert.That(innerItem.Text, Is.EqualTo("Inner Context"));
            Assert.That(UiElement.FromAutomationElement(ctxMenu.AutomationElement), Is.InstanceOf<ContextMenu>());
        }
    }
}
