using System;

namespace BowlingApi
{
    public class Game
    {
        private int _currentFrame = 0;
        public Frame[] frames = new Frame[10];

        public void Roll(int firstRoll, int secondRoll)
        {
            Frame f = new Frame(firstRoll, secondRoll);
            frames[_currentFrame] = f;
            _currentFrame++;
        }

        public void RollTenthFrame(int firstRoll, int secondRoll, int bonusRoll = 0)
        {
            Frame f = new Frame(firstRoll, secondRoll, bonusRoll);
            frames[_currentFrame] = f;
            _currentFrame++;
        }

        public int CalculateTotalScore()
        {
            //bool strikeFlag = false;
            //bool spareFlag = false;
            int score = 0;
            int thisFrame = 0;
            for (int i = 0; i < _currentFrame; i++)
            {
                bool strikeFlag = false;
                bool spareFlag = false;
                if (thisFrame < 9)
                {
                    if (frames[thisFrame].IsStrike())
                    {
                        strikeFlag = true;
                        score += frames[thisFrame].CalculateScore(strikeFlag, spareFlag, frames[thisFrame + 1]);
                        thisFrame++;
                    }
                    else if (frames[thisFrame].IsSpare())
                    {
                        spareFlag = true;
                        score += frames[thisFrame].CalculateScore(strikeFlag, spareFlag, frames[thisFrame + 1]);
                        thisFrame++;
                    }
                    else
                    {
                        score += frames[thisFrame].CalculateScore(strikeFlag, spareFlag);
                        thisFrame++;
                    }
                }
                else if (thisFrame >= 9)
                {
                    if (frames[thisFrame].IsStrike())
                    {
                        strikeFlag = true;
                        score += frames[thisFrame].CalculateTotalScore(strikeFlag, spareFlag);
                        thisFrame++;
                    }
                    else if (frames[thisFrame].IsSpare())
                    {
                        spareFlag = true;
                        score += frames[thisFrame].CalculateTotalScore(strikeFlag, spareFlag);
                        thisFrame++;
                    }
                    else
                    {
                        score += frames[thisFrame].CalculateTotalScore(strikeFlag, spareFlag);
                        thisFrame++;
                    }
                }
            }
            return score;
        }
    }
}