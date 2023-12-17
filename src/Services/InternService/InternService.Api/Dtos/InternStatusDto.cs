namespace InternService.Api.Dtos
{
    public class InternStatusDto
    {
        public int ID { get; set; }
        public int AcceptDay { get; set; }
        public bool InternConfırm { get; set; }
        public bool InternAccept { get; set; }
        public bool ContributConfirm { get; set; }
        public string? RejectReason { get; set; }
    }
}
