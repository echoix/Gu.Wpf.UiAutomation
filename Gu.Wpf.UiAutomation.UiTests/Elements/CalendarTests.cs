namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class CalendarTests
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
            using var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow");
            var window = app.MainWindow;
            var calendar = window.FindCalendar();
            Assert.That(UiElement.FromAutomationElement(calendar.AutomationElement), Is.InstanceOf<Calendar>());
            Assert.That(UiElement.FromAutomationElement(calendar.Items[0].AutomationElement), Is.InstanceOf<CalendarDayButton>());
        }

        [Test]
        public void SelectDay()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow");
            var window = app.MainWindow;
            var calendar = window.FindCalendar();
            Assert.That(calendar.Items, Is.All.InstanceOf(typeof(CalendarDayButton)));
            calendar.Items[3].Select();
        }

        [Test]
        public void SelectDate()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "CalendarWindow");
            var window = app.MainWindow;
            var calendar = window.FindCalendar();
            var date = DateTime.Today.AddDays(1);
            Assert.Null(calendar.SelectedItem);
            Assert.That(calendar.Select(date), Is.Not.Null);

            // Can't figure out a nice way to assert here
            // Tricky with culture
            Assert.That(calendar.SelectedItem, Is.Not.Null);
        }
    }
}
