namespace InternService.Api.Dtos
{
    public class InternInfoDto
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string address { get; set; }
        public string area { get; set; }
        public string OwnerName { get; set; }
        public string TelNo { get; set; }
        public string Mail { get; set; }
        public string FaxNo { get; set; }
        public string WebAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string İnternType { get; set; }
        public int İnternNumber { get; set; }
        public bool Holliday { get; set; }
        public bool SaturdayInc { get; set; }
        public bool Education { get; set; }
    }
}
