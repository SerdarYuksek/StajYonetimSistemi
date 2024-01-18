namespace InternService.Api.Model
{
    //Intern Servicesindeki InternStatus Tablosunun Entityleri
    public class InternStatus
    {
        public int ID { get; set; }
        public int AcceptDay { get; set; }
        public bool ConfirmStatus { get; set; }
        public bool AcceptStatus { get; set; }
        public bool ContributStatus { get; set; }
        public string? RejectReason { get; set; }
    }
}
