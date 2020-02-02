using LichEngine.GameCode;

using System;

namespace LichEngine.DX
{
#if WINDOWS || LINUX
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
#endif
}
