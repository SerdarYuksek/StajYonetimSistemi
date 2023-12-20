﻿namespace InternService.Api.Model
{
    //Intern Servicesindeki InternStatus Tablosunun Entityleri
    public class InternStatus
    {
        public int ID { get; set; }
        public int AcceptDay { get; set; }
        public bool InternConfirm { get; set; }
        public bool InternAccept { get; set; }
        public bool ContributConfirm { get; set; }
        public string? RejectReason { get; set; }
    }
}