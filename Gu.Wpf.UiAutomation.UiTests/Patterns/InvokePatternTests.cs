﻿namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using System;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class InvokePatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void InvokeWithEventTest()
        {
            using var app = Application.Launch(ExeFileName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            var tabItem = tab.Items[0];
            var button = tabItem.FindButton("InvokableButton");
            Assert.NotNull(button);
            var invokePattern = button.AutomationElement.InvokePattern();
            Assert.NotNull(invokePattern);
            var invokeFired = false;

            using (button.SubscribeToEvent(
                InvokePatternIdentifiers.InvokedEvent,
                TreeScope.Element,
                (element, id) => invokeFired = true))
            {
                invokePattern.Invoke();
                Wait.For(TimeSpan.FromMilliseconds(50));
                Assert.That(button.Text, Is.EqualTo("Invoked!"));
                Assert.That(invokeFired, Is.EqualTo(true));
            }
        }
    }
}
