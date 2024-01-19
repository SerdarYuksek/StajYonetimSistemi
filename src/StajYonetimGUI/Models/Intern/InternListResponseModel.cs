namespace StajYonetimGUI.Models.Intern
{
    //Intern Servicesinde staj listelemek için gerekli olan Entityler
    public class InternListResponseModel
    {
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TotalInternDay { get; set; }
        public int AcceptDay { get; set; }
        public bool ConfirmStatus { get; set; }
        public bool AcceptStatus { get; set; }
        public bool ContributStatus { get; set; }
    }
}
