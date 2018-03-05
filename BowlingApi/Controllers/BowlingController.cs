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
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;

namespace BowlingApi.Controllers
{
    [Route("api/bowling")]
    //[EnableCors("bowling")]
    public class BowlingController : Controller
    {
        private Game _game;

        public BowlingController()
        {
            _game = new Game();
        }

        // WORKS
        [Route("score")]
        [HttpPost]
        public JsonResult Score(string json)
        {
            Frame[] _frames = JsonConvert.DeserializeObject<List<Frame>>(json).ToArray();
            int score = 0;
            foreach (Frame f in _frames)
            {
                int first = f.FirstRoll;
                int second = f.SecondRoll;
                int bonus = f.BonusRoll;
                _game.Roll(first, second, bonus);
            }

            score += _game.CalculateTotalScore();
            return Json(new {score});
        }

        // WORKS
        [Route("submit")]
        [HttpPost]
        public JsonResult SubmitScore(string json)
        {
            ListOfFrames _frames = JsonConvert.DeserializeObject<ListOfFrames>(json);
            int score = 0;
            foreach (Frame f in _frames.Frames)
            {
                int first = f.FirstRoll;
                int second = f.SecondRoll;
                int bonus = f.BonusRoll;
                _game.Roll(first, second, bonus);
            }

            score += _game.CalculateTotalScore();
            return Json(new {score});
        }
    }
}