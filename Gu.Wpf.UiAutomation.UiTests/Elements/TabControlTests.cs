namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TabControlTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "TabControlWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tabControl = window.FindTabControl();
            Assert.That(UiElement.FromAutomationElement(tabControl.AutomationElement), Is.InstanceOf<TabControl>());
        }

        [Test]
        public void Items()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            Assert.That(tab.Items, Has.Count.EqualTo(4));
            Assert.Multiple(() =>
            {
                Assert.That(tab.Items[0].HeaderText, Is.EqualTo("x:Name"));
                Assert.That(tab.Items[1].HeaderText, Is.EqualTo("Header"));
                Assert.That(tab.Items[2].HeaderText, Is.EqualTo("AutomationProperties.AutomationId"));
                Assert.That(tab.Items[3].HeaderText, Is.EqualTo("WithItemsControl"));
            });

            for (var i = 0; i < tab.Items.Count; i++)
            {
                var tabItem = tab.Items[i];
                tabItem.Click();
                if (tabItem.ContentCollection.Count == 1)
                {
                    Assert.Multiple(() =>
                    {
                        Assert.That(((TextBlock)tabItem.Content).Text, Is.EqualTo($"{i + 1}"));
                        Assert.That(((TextBlock)tab.Content).Text, Is.EqualTo($"{i + 1}"));
                    });
                }
            }
        }

        [Test]
        public void SelectedIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            tab.SelectedIndex = 0;
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(0));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[0]));
            });

            tab.SelectedIndex = 1;
            Wait.UntilInputIsProcessed();
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(1));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[1]));
            });

            tab.SelectedIndex = 0;
            Wait.UntilInputIsProcessed();
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(0));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[0]));
            });
        }

        [Test]
        public void SelectIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            tab.SelectedIndex = 0;
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(0));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[0]));

                Assert.That(tab.Select(1), Is.EqualTo(tab.Items[1]));
            });
            Wait.UntilInputIsProcessed();
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(1));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[1]));

                Assert.That(tab.Select(0), Is.EqualTo(tab.Items[0]));
            });
            Wait.UntilInputIsProcessed();
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(0));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[0]));
            });
        }

        [Test]
        public void SelectText()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            tab.SelectedIndex = 0;
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(0));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[0]));

                Assert.That(tab.Select("Header"), Is.EqualTo(tab.Items[1]));
            });
            Wait.UntilInputIsProcessed();
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(1));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[1]));

                Assert.That(tab.Select("x:Name"), Is.EqualTo(tab.Items[0]));
            });
            Wait.UntilInputIsProcessed();
            Assert.Multiple(() =>
            {
                Assert.That(tab.SelectedIndex, Is.EqualTo(0));
                Assert.That(tab.SelectedItem, Is.EqualTo(tab.Items[0]));
            });
        }
    }
}
