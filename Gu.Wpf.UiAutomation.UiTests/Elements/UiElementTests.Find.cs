namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using NUnit.Framework;

    public partial class UiElementTests
    {
        public class Find
        {
            private static readonly IReadOnlyList<TestCaseData> FindAtCases = new[]
            {
                new TestCaseData(0, ControlType.TitleBar),
                new TestCaseData(1, ControlType.MenuBar),
                new TestCaseData(2, ControlType.MenuItem),
            };

            [OneTimeTearDown]
            public void OneTimeTearDown()
            {
                Application.KillLaunched(ExeFileName);
                Retry.ResetTime();
            }

            [Test]
            public void FromPoint()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow");
                var window = app.MainWindow;
                var button = window.FindButton("EmptyButton");
                var fromPoint = (Button)UiElement.FromPoint(button.GetClickablePoint());
                Assert.That(fromPoint.Text, Is.EqualTo(button.Text));
            }

            [Test]
            public void FocusedElement()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow");
                var window = app.MainWindow;
                var textBox = window.FindTextBox();
                textBox.Text = "focused";
                textBox.Focus();
                var fromPoint = (TextBox)window.FocusedElement();
                Assert.That(fromPoint.Text, Is.EqualTo("focused"));
            }

            [Test]
            public void FindCheckBox()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox();
                Assert.That(checkBox, Is.InstanceOf<CheckBox>());
            }

            //// [TestCase("CheckBoxXName")]
            [TestCase("CheckBox1AutomationId")]
            [TestCase("CheckBox1Content")]
            public void FindCheckBoxWithXNameAndAutomationId(string key)
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "FindWindow");
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox(key);
                Assert.That(checkBox, Is.Not.Null);
            }

            [TestCase(null)]
            [TestCase("AutomationId")]
            [TestCase("XName")]
            [TestCase("Content")]
            public void FindCheckBoxThrowsWhenNotFound(string key)
            {
                Retry.Time = TimeSpan.FromMilliseconds(0);
                using (var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
                {
                    var window = app.MainWindow;
                    var exception = Assert.Throws<InvalidOperationException>(() => window.FindCheckBox(key));
                    var expected = key is null
                        ? $"Did not find a CheckBox matching ControlType == CheckBox."
                        : $"Did not find a CheckBox matching (ControlType == CheckBox && (Name == {key} || AutomationId == {key})).";
                    Assert.That(exception.Message, Is.EqualTo(expected));
                }

                Retry.ResetTime();
            }

            [Test]
            public void FindFirstChild()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
                var window = app.MainWindow;
                var child = window.FindFirstChild();
                Assert.That(child.ControlType.ProgrammaticName, Is.EqualTo("ControlType.TitleBar"));
            }

            [Test]
            public void FindFirstChildWithWrap()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
                var window = app.MainWindow;
                var child = window.FindFirstChild(x => new CheckBox(x));
                Assert.That(child.ControlType, Is.EqualTo(ControlType.TitleBar));
            }

            [TestCaseSource(nameof(FindAtCases))]
            public void FindAt(int index, ControlType expected)
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
                var window = app.MainWindow;
                var child = window.FindAt(TreeScope.Descendants, Conditions.TrueCondition, index, TimeSpan.FromMilliseconds(100));
                Assert.That(child.ControlType, Is.EqualTo(expected));
            }

            [Test]
            public void FindAtWithWrap()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
                var window = app.MainWindow;
                var child = window.FindAt(
                    TreeScope.Descendants,
                    Conditions.CheckBox,
                    1,
                    x => new CheckBox(x),
                    TimeSpan.FromMilliseconds(100));
                Assert.That(child, Is.InstanceOf<CheckBox>());
                Assert.That(child.AutomationId, Is.EqualTo("XName"));
            }

            [Test]
            public void TryFindAtWithWrap()
            {
                using var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow");
                var window = app.MainWindow;
                Assert.That(window.TryFindAt(
                    TreeScope.Descendants,
                    Conditions.CheckBox,
                    1,
                    x => new CheckBox(x),
                    TimeSpan.FromMilliseconds(100),
                    out var child), Is.EqualTo(true));
                Assert.That(child, Is.InstanceOf<CheckBox>());
                Assert.Multiple(() =>
                {
                    Assert.That(child.AutomationId, Is.EqualTo("XName"));

                    Assert.That(window.TryFindAt(
                        TreeScope.Descendants,
                        Conditions.CheckBox,
                        100,
                        x => new CheckBox(x),
                        TimeSpan.FromMilliseconds(100),
                        out child), Is.EqualTo(false));
                });
                Assert.That(child, Is.Null);
            }
        }
    }
}
