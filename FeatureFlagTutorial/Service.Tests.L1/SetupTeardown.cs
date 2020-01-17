using System;
using System.Diagnostics;
using System.IO;
using Glasswall.Common.Testing.L1.Helpers.AppSettingConfiguration;
using NUnit.Framework;

namespace Glasswall.FileTrust.RepoName.Service.Tests.L1
{
    [SetUpFixture]
    public class SetupTeardown
    {
        public static AppSettingJsonConfiguration JsonConfiguration { get; private set; }

        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            // Setup tracelog
            var traceFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, "trace.txt");
            Trace.Listeners.Add(new TextWriterTraceListener(traceFileName));
            Trace.AutoFlush = true;

            JsonConfiguration = new AppSettingJsonConfiguration(TestContext.CurrentContext.TestDirectory);
        }

        [OneTimeTearDown]
        public static void AssemblyCleanup()
        {
            Trace.WriteLine("Starting AssemblyCleanup..");

            // Wait for finalisers (avoids cleanup errors at end of tests - important for all test projects)
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}