using System;
using System.IO;

namespace login_page
{
    public class HospitalService
    {
        // ===================== DATA STORAGE =====================
        public int[] patientID = new int[100];
        public string[] patientName = new string[100];
        public int[] patientAge = new int[100];
        public string[] patientSymptom = new string[100];
        public int patientCount = 0;

        public int[] doctorID = new int[100];
        public string[] doctorName = new string[100];
        public string[] doctorSpecialization = new string[100];
        public int doctorCount = 0;

        public int[] appointmentID = new int[200];
        public string[] appointmentPatientName = new string[200];
        public string[] appointmentPatientSymptom = new string[200];
        public string[] appointmentDoctorName = new string[200];
        public string[] appointmentDate = new string[200];
        public string[] appointmentTime = new string[200];
        public int appointmentCount = 0;

        public HospitalService()
        {
            patientCount = LoadPatients();
            doctorCount = LoadDoctors();
            appointmentCount = LoadAppointments();
        }

        // ===================== PATIENT METHODS =====================
        public bool RegisterPatient(string name, int age, string symptom, out int newID)
        {
            newID = 0;
            if (patientCount >= 100) return false;
            newID = patientCount == 0 ? 1001 : patientID[patientCount - 1] + 1;
            patientID[patientCount] = newID;
            patientName[patientCount] = name;
            patientAge[patientCount] = age;
            patientSymptom[patientCount] = symptom;
            patientCount++;
            SavePatients();
            return true;
        }

        public bool UpdatePatient(int id, string name, int age, string symptom)
        {
            for (int i = 0; i < patientCount; i++)
            {
                if (patientID[i] == id)
                {
                    patientName[i] = name;
                    patientAge[i] = age;
                    patientSymptom[i] = symptom;
                    SavePatients();
                    return true;
                }
            }
            return false;
        }

        public bool DeletePatient(int id)
        {
            for (int i = 0; i < patientCount; i++)
            {
                if (patientID[i] == id)
                {
                    for (int j = i; j < patientCount - 1; j++)
                    {
                        patientID[j] = patientID[j + 1];
                        patientName[j] = patientName[j + 1];
                        patientAge[j] = patientAge[j + 1];
                        patientSymptom[j] = patientSymptom[j + 1];
                    }
                    patientCount--;
                    SavePatients();
                    return true;
                }
            }
            return false;
        }

        // ===================== DOCTOR METHODS =====================
        public bool AddDoctor(string name, string spec, out int newID)
        {
            newID = 0;
            if (doctorCount >= 100) return false;
            newID = doctorCount == 0 ? 9001 : doctorID[doctorCount - 1] + 1;
            doctorID[doctorCount] = newID;
            doctorName[doctorCount] = name;
            doctorSpecialization[doctorCount] = spec;
            doctorCount++;
            SaveDoctors();
            return true;
        }

        public bool DeleteDoctor(int id)
        {
            for (int i = 0; i < doctorCount; i++)
            {
                if (doctorID[i] == id)
                {
                    for (int j = i; j < doctorCount - 1; j++)
                    {
                        doctorID[j] = doctorID[j + 1];
                        doctorName[j] = doctorName[j + 1];
                        doctorSpecialization[j] = doctorSpecialization[j + 1];
                    }
                    doctorCount--;
                    SaveDoctors();
                    return true;
                }
            }
            return false;
        }

        // ===================== APPOINTMENT METHODS =====================
        public bool AddAppointment(string pName, string dName, string date, string time, out int newID)
        {
            newID = 0;
            if (appointmentCount >= 200) return false;
            int pIndex = Array.IndexOf(patientName, pName);
            if (pIndex == -1) return false;

            newID = appointmentCount == 0 ? 5001 : appointmentID[appointmentCount - 1] + 1;
            appointmentID[appointmentCount] = newID;
            appointmentPatientName[appointmentCount] = pName;
            appointmentPatientSymptom[appointmentCount] = patientSymptom[pIndex];
            appointmentDoctorName[appointmentCount] = dName;
            appointmentDate[appointmentCount] = date;
            appointmentTime[appointmentCount] = time;
            appointmentCount++;
            SaveAppointments();
            return true;
        }

        public bool DeleteAppointment(int id)
        {
            for (int i = 0; i < appointmentCount; i++)
            {
                if (appointmentID[i] == id)
                {
                    for (int j = i; j < appointmentCount - 1; j++)
                    {
                        appointmentID[j] = appointmentID[j + 1];
                        appointmentPatientName[j] = appointmentPatientName[j + 1];
                        appointmentPatientSymptom[j] = appointmentPatientSymptom[j + 1];
                        appointmentDoctorName[j] = appointmentDoctorName[j + 1];
                        appointmentDate[j] = appointmentDate[j + 1];
                        appointmentTime[j] = appointmentTime[j + 1];
                    }
                    appointmentCount--;
                    SaveAppointments();
                    return true;
                }
            }
            return false;
        }

        // ===================== FILE HANDLING =====================
        private void SavePatients()
        {
            using StreamWriter sw = new StreamWriter("Patients.txt");
            for (int i = 0; i < patientCount; i++)
                sw.WriteLine($"{patientID[i]},{patientName[i]},{patientAge[i]},{patientSymptom[i]}");
        }

        private int LoadPatients()
        {
            if (!File.Exists("Patients.txt")) return 0;
            string[] lines = File.ReadAllLines("Patients.txt");
            int count = 0;
            foreach (string line in lines)
            {
                string[] p = line.Split(',');
                if (p.Length >= 4)
                {
                    patientID[count] = int.Parse(p[0]);
                    patientName[count] = p[1];
                    patientAge[count] = int.Parse(p[2]);
                    patientSymptom[count] = p[3];
                    count++;
                }
            }
            return count;
        }

        private void SaveDoctors()
        {
            using StreamWriter sw = new StreamWriter("Doctors.txt");
            for (int i = 0; i < doctorCount; i++)
                sw.WriteLine($"{doctorID[i]},{doctorName[i]},{doctorSpecialization[i]}");
        }

        private int LoadDoctors()
        {
            if (!File.Exists("Doctors.txt")) return 0;
            string[] lines = File.ReadAllLines("Doctors.txt");
            int count = 0;
            foreach (string line in lines)
            {
                string[] p = line.Split(',');
                if (p.Length >= 3)
                {
                    doctorID[count] = int.Parse(p[0]);
                    doctorName[count] = p[1];
                    doctorSpecialization[count] = p[2];
                    count++;
                }
            }
            return count;
        }

        private void SaveAppointments()
        {
            using StreamWriter sw = new StreamWriter("Appointments.txt");
            for (int i = 0; i < appointmentCount; i++)
                sw.WriteLine($"{appointmentID[i]},{appointmentPatientName[i]},{appointmentPatientSymptom[i]},{appointmentDoctorName[i]},{appointmentDate[i]},{appointmentTime[i]}");
        }

        private int LoadAppointments()
        {
            if (!File.Exists("Appointments.txt")) return 0;
            string[] lines = File.ReadAllLines("Appointments.txt");
            int count = 0;
            foreach (string line in lines)
            {
                string[] p = line.Split(',');
                if (p.Length >= 6)
                {
                    appointmentID[count] = int.Parse(p[0]);
                    appointmentPatientName[count] = p[1];
                    appointmentPatientSymptom[count] = p[2];
                    appointmentDoctorName[count] = p[3];
                    appointmentDate[count] = p[4];
                    appointmentTime[count] = p[5];
                    count++;
                }
            }
            return count;
        }
    }
}