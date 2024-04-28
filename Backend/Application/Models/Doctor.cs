﻿using Data.enums;

namespace Data.Models
{
    public class Doctor : Person 
    {
        public string Specialization { get; set; }
        public DoctorWorkType type { get; set; }
        public ICollection<Credential> credential { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public ICollection<Callender> Callender { get; set; }
        public ICollection<Avaliability> Avalible {get; set; }
        public ICollection<Patient> Patients { get; set; }  
    }
}