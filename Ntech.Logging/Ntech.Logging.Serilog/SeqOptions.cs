﻿namespace Ntech.Logging.Serilog
{
    public class SeqOptions
    {
        public bool Enabled { get; set; }
        public string Url { get; set; } = default!;
        public string ApiKey { get; set; } = default!;
    }
}
