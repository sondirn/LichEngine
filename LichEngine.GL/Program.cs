﻿using LichEngine.GameCode;
using LichEngine.Portable;
using System;

namespace LichEngine.GL
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new LichEngineMain())
                game.Run();
        }
    }
}
