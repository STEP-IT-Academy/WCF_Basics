using System.Runtime.Serialization;

namespace PatientsService.Models
{
    [DataContract]
    public class Doctor
    {
        [DataMember]
        public string Surname { get; set; }
    }
}