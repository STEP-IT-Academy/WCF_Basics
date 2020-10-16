# Write an application that implements recording patients in the doctor's queue.
### To do this, create a WCF service as a class library with a duplex contract.
### The service contract must contain:
1. an operation for adding information about the patient (last name, year of birth) to a text file located at the location of the service, and whose name matches the doctor's last name (pass the doctor's last name and the Patient class object to the operation via parameters). if there is no file with this name yet, create a new one;
2. determining whether the specified patient is among those who have made an appointment with the specified doctor;
3. a callback operation that displays a list of patients;
### Create a console project to web hosting service you just created. Add endpoints using the configuration file.
### Create a client WinForms application that allows the user to sign up to a specific doctor using the appropriate operation of the service, to know, signed up already to the doctor, the patient, and also displays a list of enrolled to the chosen doctor to patients each time a new patient. Provide asynchronous callback operations using an asynchronous task-based model.
