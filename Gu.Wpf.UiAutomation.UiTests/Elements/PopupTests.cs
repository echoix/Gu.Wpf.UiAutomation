namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class PopupTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void CheckBoxInPopupTest()
        {
            using var app = Application.Launch(ExeFileName, "PopupWindow");
            var window = app.MainWindow;
            var btn = window.FindToggleButton("PopupToggleButton1");
            btn.Click();
            Wait.UntilInputIsProcessed();
            var popup = window.FindPopup();
            Assert.NotNull(popup);
            var popupChildren = popup.FindAllChildren();
            Assert.That(popupChildren.Count, Is.EqualTo(1));
            var check = (CheckBox)popupChildren[0];
            Assert.That(check.Text, Is.EqualTo("This is a popup"));
        }

        [Test]
        public void MenuInPopupTest()
        {
            using var app = Application.Launch(ExeFileName, "PopupWindow");
            var window = app.MainWindow;
            var btn = window.FindToggleButton("PopupToggleButton2");
            btn.Click();
            Wait.UntilInputIsProcessed();
            var popup = window.FindPopup();
            var popupChildren = popup.FindAllChildren();
            Assert.That(popupChildren.Count, Is.EqualTo(1));
            var menu = (Menu)popupChildren[0];
            Assert.That(menu.Items.Count, Is.EqualTo(1));
            var menuItem = menu.Items[0];
            Assert.That(menuItem.Text, Is.EqualTo("Some MenuItem"));
        }
    }
}
