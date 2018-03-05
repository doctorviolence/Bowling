using System;
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

        [TestInitialize]
        public void SetUp()
        {
            _bowlingController = new BowlingController();
        }

        [TestMethod]
        public void TestIfReturnTypeIsJsonResult()
        {
            Frame[] frames = new Frame[2];
            frames[0] = new Frame(4, 5);
            frames[1] = new Frame(6, 2);
            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            Assert.IsInstanceOfType(score, typeof(JsonResult));
        }

        [TestMethod]
        public void JsonTestScenarioOne()
        {
            string json = "{\"frames\": [{\"first\": 4, \"second\": 5}, {\"first\": 6, \"second\": 2}]}";
            JsonResult score = _bowlingController.SubmitScore(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":17}", scoreString);
        }

        [TestMethod]
        public void JsonTestScenarioTwo()
        {
            string json = "{\"frames\": [{\"first\": 3, \"second\": 4}]}";
            JsonResult score = _bowlingController.SubmitScore(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":7}", scoreString);
        }

        [TestMethod]
        public void TestRollingAllMisses()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 10; i++)
            {
                frames[i] = new Frame(0, 0);
            }

            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":0}", scoreString);
        }

        [TestMethod]
        public void TestRollingAllStrikes()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = new Frame(10, 0);
            }

            frames[9] = new Frame(10, 10, 10);
            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":300}", scoreString);
        }

        [TestMethod]
        public void TestRollingAllSpares()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = new Frame(4, 6);
            }

            frames[9] = new Frame(4, 6, 4);
            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":140}", scoreString);
        }

        [TestMethod]
        public void TestRollingAllFives()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = new Frame(5, 5);
            }

            frames[9] = new Frame(5, 5, 5);
            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":150}", scoreString);
        }

        [TestMethod]
        public void TestRollingFirstStrikeRestTwos()
        {
            Frame[] frames = new Frame[10];
            frames[0] = new Frame(10, 0);
            frames[1] = new Frame(2, 2);
            frames[2] = new Frame(2, 2);
            frames[3] = new Frame(2, 2);
            frames[4] = new Frame(2, 2);
            frames[5] = new Frame(2, 2);
            frames[6] = new Frame(2, 2);
            frames[7] = new Frame(2, 2);
            frames[8] = new Frame(2, 2);
            frames[9] = new Frame(2, 2);

            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":50}", scoreString);
        }

        [TestMethod]
        public void TestRollingAllFours()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = new Frame(4, 4);
            }

            frames[9] = new Frame(4, 4, 0);
            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":80}", scoreString);
        }

        [TestMethod]
        public void TestRollingAllOnes()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 10; i++)
            {
                frames[i] = new Frame(1, 1);
            }

            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":20}", scoreString);
        }

        [TestMethod]
        public void TestRollingAlternateStrikesAndFives()
        {
            Frame[] frames = new Frame[10];
            frames[0] = new Frame(10, 0);
            frames[1] = new Frame(5, 5);
            frames[2] = new Frame(10, 0);
            frames[3] = new Frame(5, 5);
            frames[4] = new Frame(10, 0);
            frames[5] = new Frame(5, 5);
            frames[6] = new Frame(10, 0);
            frames[7] = new Frame(5, 5);
            frames[8] = new Frame(10, 0);
            frames[9] = new Frame(5, 5, 10);

            var json = JsonConvert.SerializeObject(frames);
            JsonResult score = _bowlingController.Score(json);
            string scoreString = JsonConvert.SerializeObject(score.Value);
            Assert.AreEqual("{\"score\":200}", scoreString);
        }

        [TestCleanup]
        public void CleanUpServerTest()
        {
            _bowlingController.Dispose();
        }
    }
}