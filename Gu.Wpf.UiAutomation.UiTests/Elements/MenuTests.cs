namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class MenuTests
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
            using var app = Application.AttachOrLaunch(ExeFileName, "MenuWindow");
            var window = app.MainWindow;
            var menu = window.FindMenu();
            Assert.IsInstanceOf<Menu>(UiElement.FromAutomationElement(menu.AutomationElement));
        }

        [Test]
        public void TestMenuWithSubMenus()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "MenuWindow");
            var window = app.MainWindow;
            var menu = window.FindMenu();
            Assert.That(menu, Is.Not.Null);
            var items = menu.Items;
            Assert.That(items.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(items[0].Text, Is.EqualTo("File"));
                Assert.That(items[1].Text, Is.EqualTo("Edit"));
            });

            var subitems1 = items[0].Items;
            Assert.That(subitems1.Count, Is.EqualTo(1));
            Assert.That(subitems1[0].Text, Is.EqualTo("Exit"));

            var subitems2 = items[1].Items;
            Assert.That(subitems2.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(subitems2[0].Text, Is.EqualTo("Copy"));
                Assert.That(subitems2[1].Text, Is.EqualTo("Paste"));
            });

            var subsubitems1 = subitems2[0].Items;
            Assert.That(subsubitems1.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(subsubitems1[0].Text, Is.EqualTo("Plain"));
                Assert.That(subsubitems1[1].Text, Is.EqualTo("Fancy"));
            });
        }

        [Test]
        public void TestMenuWithSubMenusByName()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "MenuWindow");
            var window = app.MainWindow;
            var menu = window.FindMenu();
            var edit = menu.Items["Edit"];
            Assert.That(edit.Text, Is.EqualTo("Edit"));

            var copy = edit.Items["Copy"];
            Assert.That(copy.Text, Is.EqualTo("Copy"));

            var fancy = copy.Items["Fancy"];
            Assert.That(fancy.Text, Is.EqualTo("Fancy"));
        }
    }
}
