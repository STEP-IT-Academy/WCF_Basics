using System.Collections.Generic;
using System.ServiceModel;

namespace PatientsService.Contracts.Doctors
{
    [ServiceContract]
    public interface IDoctorsReader
    {
        [OperationContract]
        IEnumerable<string> ReadAllDoctorNames();
    }
}