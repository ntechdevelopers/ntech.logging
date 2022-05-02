namespace Ntech.Logging.Serilog
{
    public class ELKOptions
    {
        public bool Enabled { get; set; }
        public string Url { get; set; } = default!;
    }
}
