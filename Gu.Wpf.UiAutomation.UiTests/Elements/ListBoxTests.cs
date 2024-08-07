namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using NUnit.Framework;

    public class ListBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("ListBox")]
        [TestCase("AutomationId")]
        public void Find(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox(key);
            Assert.That(UiElement.FromAutomationElement(listBox.AutomationElement), Is.InstanceOf<ListBox>());
        }

        [TestCase("BoundListBox", new[] { "Johan", "Erik" })]
        [TestCase("ListBox", new[] { "1", "2" })]
        [TestCase("ListBox10", new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", })]
        public void Items(string name, string[] expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox(name);
            Assert.That(listBox.Items.Select(x => x.Text), Is.EqualTo(expected).AsCollection);
        }

        [Test]
        public void ItemsWhenVirtual()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("ListBox10");
            var expected = new[]
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
            };
            Assert.That(listBox.Items.Select(x => x.Text), Is.EqualTo(expected).AsCollection);
        }

        [Test]
        public void SelectByIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("BoundListBox");
            Assert.That(listBox.Items.Count, Is.EqualTo(2));
            Assert.That(listBox.Items[0], Is.InstanceOf<ListBoxItem>());
            Assert.That(listBox.Items[1], Is.InstanceOf<ListBoxItem>());
            Assert.That(listBox.SelectedItem, Is.Null);
            Assert.That(listBox.SelectedIndex, Is.EqualTo(-1));

            var item = listBox.Select(0);
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("Johan"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("Johan"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
                Assert.That(listBox.SelectedIndex, Is.EqualTo(0));
            });

            item = listBox.Select(1);
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("Erik"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("Erik"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
                Assert.That(listBox.SelectedIndex, Is.EqualTo(1));
            });
        }

        [Test]
        public void SelectByIndexOutside()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("ListBox10");
            Assert.That(listBox.SelectedItem, Is.Null);

            var item = listBox.Select(9);
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("10"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("10"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });

            item = listBox.Select(0);
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("1"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("1"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });
        }

        [Test]
        public void SelectByText()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("BoundListBox");
            var item = listBox.Select("Johan");
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("Johan"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("Johan"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });

            item = listBox.Select("Erik");
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("Erik"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("Erik"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });

            item = listBox.Select("Johan");
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("Johan"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("Johan"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });
        }

        [Test]
        public void SelectByTextOutside()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("ListBox10");
            var item = listBox.Select("10");
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("10"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("10"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });

            item = listBox.Select("1");
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("1"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("1"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });

            item = listBox.Select("5");
            Assert.Multiple(() =>
            {
                Assert.That(item.FindTextBlock().Text, Is.EqualTo("5"));
                Assert.That(listBox.SelectedItem.FindTextBlock().Text, Is.EqualTo("5"));
                Assert.That(listBox.SelectedItem, Is.EqualTo(item));
            });
        }
    }
}
