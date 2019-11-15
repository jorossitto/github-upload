using ACM.BL;
using NUnit.Framework;
using System.Threading;
using Common;
using System.Diagnostics;

namespace CommonTests.CustomClasses
{
    [TestFixture]
    public class InstrumentationTests
    {
        [Test]
        public void GetTotalSeconds()
        {
            // Arrange
            var instrumentation = new Instrumentation();

            // Act
            instrumentation.Start();
            Thread.Sleep(750);

            // Assert
            Assert.AreEqual(1, instrumentation.GetElapsedTime());
        }

        [Test]
        //[Ignore("code changed")]
        public void GetPreciseElapsedTime()
        {
            // Arrange
            var instrumentation = new Instrumentation();

            // Act
            instrumentation.Start();
            Thread.Sleep(750);
            var elapsed = instrumentation.GetPreciseElapsedTime();
            // Assert
            Debug.WriteLine(elapsed);
            Assert.IsTrue(elapsed >= 0.75 && elapsed < 0.76);

        }

        [Test]
        public void GetReallyPreciseElapsedTime()
        {
            // Arrange
            var instrumentation = new Instrumentation();

            // Act
            instrumentation.StartWithPrecision();
            Thread.Sleep(750);
            var elapsed = instrumentation.GetReallyPreciseElapsedTime();
            Debug.WriteLine("has taken " + elapsed);
            // Assert
            Assert.IsTrue(elapsed >= 750 && elapsed < 752);
        }
    }
}
