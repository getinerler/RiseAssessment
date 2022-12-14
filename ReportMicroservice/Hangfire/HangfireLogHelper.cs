using Hangfire.Console;
using Hangfire.Server;

namespace ReportMicroservice.Hangfire
{
    public class HangfireLogHelper
    {
        private readonly PerformContext _context;

        public HangfireLogHelper(PerformContext context)
        {
            _context = context;
        }

        public void ErrorLog(string message)
        {
            _context.SetTextColor(ConsoleTextColor.Red);
            _context.WriteLine(message);
            _context.ResetTextColor();
        }

        public void WarningLog(string message)
        {
            _context.SetTextColor(ConsoleTextColor.Yellow);
            _context.WriteLine(message);
            _context.ResetTextColor();
        }

        public void InfoLog(string message)
        {
            _context.SetTextColor(ConsoleTextColor.Gray);
            _context.WriteLine(message);
            _context.ResetTextColor();
        }

        public void MakeProgressBar(int percent)
        {
            var progress = _context.WriteProgressBar();
            progress.SetValue(percent);
        }
    }
}
