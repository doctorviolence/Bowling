using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace BowlingApi
{
    [DataContract]
    public class Frame
    {
        [JsonProperty("first")] public int FirstRoll { get; private set; }
        [JsonProperty("second")] public int SecondRoll { get; private set; }
        [JsonProperty("bonus")] public int BonusRoll { get; private set; }

        public Frame()
        {
        }

        public Frame(int firstRoll, int secondRoll)
        {
            FirstRoll = firstRoll;
            SecondRoll = secondRoll;
        }

        public Frame(int firstRoll, int secondRoll, int bonusRoll = 0)
        {
            FirstRoll = firstRoll;
            SecondRoll = secondRoll;
            BonusRoll = bonusRoll;
        }

        public bool IsStrike()
        {
            return FirstRoll == 10;
        }

        public bool IsSpare()
        {
            return FirstRoll + SecondRoll == 10 && !IsStrike();
        }

        public int CalculateScore(bool strikeFlag, bool spareFlag, Frame nextFrame = null)
        {
            int score = 0;
            if (strikeFlag && !spareFlag)
                if (nextFrame.IsStrike())
                    return score += FirstRoll + GetFrameScore() + nextFrame.GetFrameScore();
                else
                    return score += GetFrameScore() + nextFrame.GetFrameScore();
            else if (spareFlag && !strikeFlag)
                return score += GetFrameScore() + nextFrame.FirstRoll;
            else
                return score += GetFrameScore();
        }

        public int CalculateTotalScore(bool strikeFlag, bool spareFlag)
        {
            int score = 0;
            if (strikeFlag && !spareFlag)
                return score += BonusRoll + BonusRoll;
            else if (spareFlag && !strikeFlag)
                return score += FirstRoll + SecondRoll + BonusRoll;
            else
                return score += FirstRoll + SecondRoll;
        }

        public int GetFrameScore()
        {
            return FirstRoll + SecondRoll;
        }
    }
}