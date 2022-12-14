namespace ReportMicroservice.Dtos
{
    public class ReportInfo
    {
        public ReportStatus Status { get; set; }
        public string Path { get; set; }
    }

    public enum ReportStatus
    {
        NotQueued,
        Processing,
        Completed
    }
}
