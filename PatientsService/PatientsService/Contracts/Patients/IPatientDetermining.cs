using System.ServiceModel;
using PatientsService.Models;

namespace PatientsService.Contracts.Patients
{
    [ServiceContract]
    public interface IPatientDetermining
    {
        [OperationContract]
        bool IsAlreadyRecorded(Doctor doctor, Patient patient);
    }
}