using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreLogging;
using System;

namespace NetCoreLoggingTest
{
    [TestClass]
    public class LayoutRenderTests
    {
        [TestMethod]
        public void FormatRenderDate()
        {
            var data = new LoggingData();
            data.CurrentDateTime = DateTime.Parse("7/15/2017 1:23 PM");

            var render = new ContentRenderer();
            render.LayoutData = data;

            var output = render.RenderDate("Testing {date:format=yyyy-MM-dd} or {date:format=MM/dd/yy HH:mm}.log");

            Assert.AreEqual("Testing 2017-07-15 or 07/15/17 13:23.log", output);
        }

        [TestMethod]
        public void FormatRenderMessage()
        {
            var data = new LoggingData();
            data.Message = "TestMessage";

            var render = new ContentRenderer();
            render.LayoutData = data;

            var output = render.RenderMessage("123{message}456{message}");

            Assert.AreEqual("123TestMessage456TestMessage", output);
        }

        [TestMethod]
        public void FormatRenderLevel()
        {
            var data = new LoggingData();
            data.LogLevel = LogLevel.Fatal;

            var renderer = new ContentRenderer();
            renderer.LayoutData = data;

            var output = renderer.Render("-->{level}<--");
            var assert = "-->Fatal<--";

            Assert.AreEqual(assert, output);
        }


        [TestMethod]
        public void FormatRender()
        {
            var data = new LoggingData();
            data.CurrentDateTime = DateTime.Parse("7/15/2017 1:23:15 PM");
            data.Message = "Test log entry";
            data.LogLevel = LogLevel.Error;

            var renderer = new ContentRenderer();
            renderer.LayoutData = data;

            var output = renderer.Render("{date:format=yyyy-MM-dd HH:mm:ss}\t{level}\t{message}");
            var assert = "2017-07-15 13:23:15\tError\tTest log entry";

            Assert.AreEqual(assert, output);


        }

    }
}
