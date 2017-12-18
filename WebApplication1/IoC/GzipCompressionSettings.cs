namespace WebApplication1
{
    using System.Collections.Generic;

    public class GzipCompressionSettings
    {
        public int MinimumBytes { get; set; } = 4096;

        public IList<string> MimeTypes { get; set; } = new List<string>
        {
            "text/plain",
            "text/html",
            "text/xml",
            "text/css",
            "application/json",
            "application/x-javascript",
            "application/atom+xml",
            "application/xml",
            "application/x-protobuf"//google的
        };
    }
}
