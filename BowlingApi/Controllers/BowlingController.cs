using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BowlingApi.Controllers
{
    [Route("api/bowling")]
    public class BowlingController : Controller
    {

        private Game _game;
        private int _i = 0;

        public BowlingController()
        {
            _game = new Game();
        }

        [Route("GetScore")]
        [HttpGet]
        public JsonResult GetScore(IEnumerable<Frame> frames)
        {
            int score = 0;
            foreach (Frame f in frames)
            {
                score += _game.CalculateTotalScore();
            }
            return Json(new { Value = "score" + score });
        }

        [Route("SubmitScore")]
        [HttpPost]
        public JsonResult SubmitScore(string frames)
        {
            Frame[] framesInArray = Deserialize<Frame[]>(frames);
            int score = 0;
            foreach (Frame f in framesInArray)
            {
                int first = 0;
                int second = 0;
                first = f.FirstRoll;
                second = f.SecondRoll;
                _game.Roll(first, second);
                score += _game.CalculateTotalScore();
            }
            return Json(new { Value = "score" + score }); // Hmm?
        }

        private Frame Deserialize<Frame>(string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(Frame));
                Frame f = (Frame)serializer.ReadObject(ms);
                return (Frame)serializer.ReadObject(ms);
            }
        }

        /*
        [Route("PostFrame")]
        [HttpPost]
        public ActionResult SubmitFrame(IEnumerable<Frame> f)
        {
            Frame frame;
            int first = f.FirstRoll;
            int second = f.SecondRoll;
            frame = new Frame(first, second);
            game.frames[i] = frame;
            i++;
            return Json(new { msg = "score" + frame.GetFrameScore() });
        }*/

        /* Note: not yet tested
        [Route("frames")]
        [HttpPost]
        public IActionResult HandleRequest(HttpRequest request)
        {
            Frame frame = JsonConvert.DeserializeObject<Frame>(request.ToString()); // toString vs body?
            int firstRoll = frame.FirstRoll;
            int secondRoll = frame.SecondRoll;
            int score = 0;
            foreach (Frame f in game.frames)
            {
                game.Roll(firstRoll, secondRoll);
                //game.frames[i] = new Frame(firstRoll, secondRoll);
                score += game.CalculateTotalScore();
                i++;
            }
            return Json("score" + score);
        }*/
    }
}