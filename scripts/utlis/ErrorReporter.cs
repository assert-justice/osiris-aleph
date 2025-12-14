
using System;

namespace Osiris
{
    public static class ErrorReporter
    {
        static Action<string> Reporter = s => {};
        public static void Report(string message)
        {
            Reporter(message);
        }
        public static void SetReporter(Action<string> reporter)
        {
            Reporter = reporter;
        }
    }
}
