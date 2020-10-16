using PatientsService.Contracts.Doctors;
using PatientsService.Contracts.Patients;
using PatientsService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PatientsService.Services
{
    public class PatientServices : IPatientWriter, IDoctorsReader, IPatientDetermining
    {
        private const string PathToDoctorsFile = @"..\..\Doctors\";
        private ICallbackClient _currentUser;

        public async void AddPatient(Doctor doctor, Patient patient)
        {
            File.AppendAllText(Path.Combine(PathToDoctorsFile, $"{doctor.Surname}.txt"), $"{patient.Surname};{patient.Age}{Environment.NewLine}");
            _currentUser = OperationContext.Current.GetCallbackChannel<ICallbackClient>();
            _currentUser.GetAllDoctorPatients(doctor.Surname, await GetAllDoctorPatientsAsync(doctor));
        }

        private async Task<IEnumerable<Patient>> GetAllDoctorPatientsAsync(Doctor doctor)
        {
            List<Patient> result = new List<Patient>();
            using StreamReader reader = new StreamReader(Path.Combine(PathToDoctorsFile, $"{doctor.Surname}.txt"));

            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                string[] tmp = line.Split(';');
                result.Add(new Patient { Surname = tmp[0], Age = Convert.ToInt32(tmp[1]) });
            }
            return result;
        }

        public IEnumerable<string> ReadAllDoctorNames() => Directory.GetFiles(PathToDoctorsFile, "*.txt").Select(Path.GetFileNameWithoutExtension);

        public bool IsAlreadyRecorded(Doctor doctor, Patient patient)
        {
            List<string> patients = File.ReadAllLines(Path.Combine(PathToDoctorsFile, $"{doctor.Surname}.txt")).ToList();
            return patients.Contains($"{patient.Surname};{patient.Age}");
        }
    }
}