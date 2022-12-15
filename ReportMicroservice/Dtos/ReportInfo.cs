namespace ReportMicroservice.Dtos
{
    public class ReportInfo
    {
        public string Status { get; set; }
        public string Path { get; set; }
    }

    public enum ReportStatus
    {
        NotQueued,
        Processing,
        Completed
    }
}
