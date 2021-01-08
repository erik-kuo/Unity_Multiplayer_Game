﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
	class Constants
	{
		public const int TICKS_PER_SEC = 50; // How many ticks per second
		public const float MS_PER_TICK = 1000f / TICKS_PER_SEC; // How many milliseconds per tick
		public const int PROGRESS_STEP = 10;
	}
}