using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerTimer
    {
        public float timeRemaining = 300;
        public bool timerIsRunning = false;

        public void Start()
        {
            // Starts the timer automatically
            timerIsRunning = true;
        }

        public void Reset()
        {
            timeRemaining = 300;
            timerIsRunning = false;
        }

        public void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Constants.MS_PER_TICK/1000;
                }
                else
                {
                    Console.WriteLine("Time has run out!");
                    GameLogic.Reset();
                }
            }
        }
    }

}
