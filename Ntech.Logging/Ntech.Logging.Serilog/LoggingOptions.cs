namespace Ntech.Logging.Serilog
{
    public class LoggingOptions
    {
        public bool ConsoleEnabled { get; set; }
        public SeqOptions Seq { get; set; } = default;
        public ELKOptions Elk { get; set; }
        public FileOptions File { get; set; }
    }
}
