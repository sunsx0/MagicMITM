using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM
{
    public class Logger
    {
        public static bool DebugMode = false;
        static Logger()
        {
#if DEBUG
            DebugMode = true;
#endif
        }

        public static void Process(string text)
        {
            if (DebugMode)
                Console.Error.WriteLine("[{0}] {1}", DateTime.Now.ToString("u"), text);
        }
        public static void Process(string text, params object[] args)
        {
            Process(string.Format(text, args));
        }
        public static void Process(Exception exception)
        {
            Process(exception.Message);
            Process(exception.Source);
            Process(exception.StackTrace);
        }
        public static void Process(string sender, Exception exception)
        {
            Process("Exception from {0}", sender);
            Process(exception);
        }
    }
}
