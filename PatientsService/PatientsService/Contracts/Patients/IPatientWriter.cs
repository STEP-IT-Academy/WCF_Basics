using PatientsService.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace PatientsService.Contracts.Patients
{
    [ServiceContract(CallbackContract = typeof(ICallbackClient))]
    public interface IPatientWriter
    {
        [OperationContract(IsOneWay = true)]
        void AddPatient(Doctor doctor, Patient patient);
    }

    public interface ICallbackClient
    {
        [OperationContract]
        void GetAllDoctorPatients(string doctorSurname, IEnumerable<Patient> patients);
    }
}