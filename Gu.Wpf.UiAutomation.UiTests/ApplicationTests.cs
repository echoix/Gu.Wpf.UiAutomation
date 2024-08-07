namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using NUnit.Framework;

    public class ApplicationTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void DisposeWhenClosed()
        {
            using var app = Application.Launch("notepad.exe");
            Assert.That(app.Close(), Is.EqualTo(false));
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            Assert.That(app.MainWindowHandle, Is.EqualTo(app.MainWindow.NativeWindowHandle));
            Assert.AreNotEqual(IntPtr.Zero, app.MainWindowHandle);
            Assert.NotZero(app.ProcessId);
            Assert.Multiple(() =>
            {
                Assert.That(app.Name, Is.EqualTo("WpfApplication"));
                Assert.That(app.HasExited, Is.EqualTo(false));
                Assert.That(app.Close(), Is.EqualTo(true));
            });
            Assert.That(app.HasExited, Is.EqualTo(true));
            Assert.That(app.ExitCode, Is.EqualTo(0));
        }

        [Test]
        public void FindExe()
        {
            Assert.That(Path.GetFileName(Application.FindExe(ExeFileName)), Is.EqualTo(ExeFileName));
        }

        [Test]
        public void KillLaunched()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Application.KillLaunched();
            Assert.Throws<InvalidOperationException>(() => app.WaitForMainWindow());
        }

        [Test]
        public void KillLaunchedExeFileName()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Application.KillLaunched(ExeFileName);
            Assert.Throws<InvalidOperationException>(() => app.WaitForMainWindow());
        }

        [Test]
        public void KillLaunchedExeFileNameAndArgs()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Application.KillLaunched(ExeFileName, "EmptyWindow");
            Assert.Throws<InvalidOperationException>(() => app.WaitForMainWindow());
        }

        [Test]
        public void Kill()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            app.Kill();
            Assert.That(app.ExitCode, Is.EqualTo(-1));
        }

        [Test]
        public void GetAllTopLevelWindows()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Assert.That(app.GetAllTopLevelWindows().Count, Is.EqualTo(1));
        }

        [Test]
        public void GetMainWindowThrowsWithTimeOut()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SlowWindow");
            var exception = Assert.Throws<TimeoutException>(() => app.GetMainWindow(TimeSpan.FromMilliseconds(10)));
            Assert.That(exception.Message, Is.EqualTo("Did not find Process.MainWindowHandle, if startup is slow try with a longer timeout."));
        }

        [Test]
        public void StartWaitForMainWindowAndClose()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            var id = app.ProcessId;
            using var process = Process.GetProcessById(id);
            Assert.NotNull(process);
            Application.WaitForMainWindow(process);

            Application.KillLaunched(ExeFileName);
            Assert.Throws<ArgumentException>(() => Process.GetProcessById(id));
        }

        [Test]
        public void TryAttach()
        {
            using (Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                if (Application.TryAttach(ExeFileName, "EmptyWindow", out var app1))
                {
                    using (app1)
                    {
                        Assert.NotNull(app1.MainWindow);
                    }
                }
                else
                {
                    // Wrote it like this to see what the api looks like.
                    Assert.Fail("Failed to attach");
                }

                Assert.Multiple(() =>
                {
                    Assert.That(Application.TryAttach(new ProcessStartInfo(Application.FindExe(ExeFileName)) { Arguments = "EmptyWindow" }, out _), Is.EqualTo(true));
                    Assert.That(Application.TryAttach(new ProcessStartInfo(Application.FindExe(ExeFileName)) { Arguments = "EmptyWindow" }, OnDispose.LeaveOpen, out _), Is.EqualTo(true));
                    Assert.That(Application.TryAttach(ExeFileName, "EmptyWindow", OnDispose.LeaveOpen, out _), Is.EqualTo(true));
                    Assert.That(Application.TryAttach(ExeFileName, out _), Is.EqualTo(true));
                    Assert.That(Application.TryAttach(ExeFileName, OnDispose.LeaveOpen, out _), Is.EqualTo(true));
                    Assert.That(Application.TryAttach(new ProcessStartInfo(Application.FindExe(ExeFileName)) { Arguments = "MehWindow" }, out _), Is.EqualTo(false));
                });
            }
        }

        [Test]
        public void TryWithAttached()
        {
            using (Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(Application.TryWithAttached(ExeFileName, "EmptyWindow", app =>
                                {
                                    Assert.NotNull(app.MainWindow);
                                }), Is.EqualTo(true));

                    Assert.That(Application.TryWithAttached(ExeFileName, app =>
                    {
                        Assert.NotNull(app.MainWindow);
                    }), Is.EqualTo(true));

                    Assert.That(Application.TryWithAttached(new ProcessStartInfo(ExeFileName), app =>
                   {
                       Assert.NotNull(app.MainWindow);
                   }), Is.EqualTo(true));
                });
            }
        }
    }
}
