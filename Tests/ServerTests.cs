using BowlingApi;
using BowlingApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests
{
    [TestClass]
    public class ServerTests : BowlingController
    {
        private BowlingController _bowlingController;
        private Game _game;

        [TestInitialize]
        public void SetUp()
        {
            _bowlingController = new BowlingController();
            _game = new Game();
        }

        public ActionResult JsonOne()
        {
            string test = "{'first':3, 'second': 4}";
            return Json(test);
        }

        public ActionResult JsonTwo()
        {
            string test = "{'first':4, 'second': 5}";
            return Json(test);
        }

        [TestMethod]
        public void JsonTestOne()
        {
            JsonResult actual = this.JsonOne() as JsonResult;
            Assert.AreEqual(7, actual);
        }

        [TestMethod]
        public void JsonTestTwo()
        {
            JsonResult actual = this.JsonTwo() as JsonResult;
            Assert.AreEqual(9, actual);
        }

        [TestCleanup]
        public void CleanUpServerTest()
        {
            _game = null;
            _bowlingController.Dispose();
        }
    }
}