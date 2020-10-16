using System;
using System.Runtime.Serialization;

namespace ChatService.Models.UserEntities
{
    [DataContract]
    public class UserMessage
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string MessageText { get; set; }

        [DataMember]
        public DateTime DepartureTime { get; set; }
    }
}