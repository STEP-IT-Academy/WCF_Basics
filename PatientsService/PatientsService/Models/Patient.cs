using System.Runtime.Serialization;

namespace PatientsService.Models
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}