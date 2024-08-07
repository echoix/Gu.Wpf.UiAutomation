namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using NUnit.Framework;

    public class ComboBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("EditableComboBox")]
        public void Find(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(key);
            Assert.That(UiElement.FromAutomationElement(comboBox.AutomationElement), Is.InstanceOf<ComboBox>());
        }

        [TestCase("EditableComboBox", true)]
        [TestCase("NonEditableComboBox", false)]
        [TestCase("ReadOnlyComboBox", false)]
        [TestCase("DisabledComboBox", false)]
        public void IsEditable(string comboBoxId, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            Assert.That(comboBox.IsEditable, Is.EqualTo(expected));
        }

        [TestCase("EditableComboBox", false)]
        [TestCase("NonEditableComboBox", false)]
        [TestCase("ReadOnlyComboBox", false)]
        [TestCase("ReadOnlyEditableComboBox", true)]
        public void IsReadOnly(string comboBoxId, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            Assert.That(comboBox.IsReadOnly, Is.EqualTo(expected));
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        [TestCase("ReadOnlyComboBox")]
        [TestCase("ReadOnlyEditableComboBox")]
        ////[TestCase("DisabledComboBox")]
        public void Items(string id)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(id);
            Assert.That(comboBox.Items, Is.All.InstanceOf(typeof(ComboBoxItem)));
            Assert.That(comboBox.Items.Select(x => x.Text), Is.EqualTo(new[] { "Item 1", "Item 2", "Item 3" }));
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectedItemTest(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            comboBox.Items[1].Select();
            var selectedItem = comboBox.SelectedItem;
            Assert.That(selectedItem.Text, Is.EqualTo("Item 2"));
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectByIndex(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            Assert.That(comboBox.Select(1).Text, Is.EqualTo("Item 2"));
            var selectedItem = comboBox.SelectedItem;
            Assert.That(selectedItem.Text, Is.EqualTo("Item 2"));
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectByText(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            Assert.That(comboBox.Select("Item 2").Text, Is.EqualTo("Item 2"));
            var selectedItem = comboBox.SelectedItem;
            Assert.That(selectedItem.Text, Is.EqualTo("Item 2"));
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void ExpandCollapseTest(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);

            comboBox.Expand();
            Assert.That(comboBox.IsDropDownOpen, Is.EqualTo(true));

            comboBox.Collapse();
            Assert.That(comboBox.IsDropDownOpen, Is.EqualTo(false));
        }

        [Test]
        public void EditableTextTest()
        {
            using var app = Application.Launch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox("EditableComboBox");
            comboBox.EditableText = "Item 3";
            Assert.That(comboBox.SelectedItem.Text, Is.EqualTo("Item 3"));
        }

        [Test]
        public void Enter()
        {
            using var app = Application.Launch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox("EditableComboBox");
            comboBox.Enter("Item 3");
            Assert.That(comboBox.SelectedItem.Text, Is.EqualTo("Item 3"));
        }
    }
}
