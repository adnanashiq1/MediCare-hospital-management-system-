# Hospital Management System (C# WinForms)
<b> How to Access and Run the Program</b>
To get the system running on your local machine, follow these steps:
1.Open the Project Folder: Navigate to the main project directory.
2.Open the Solution File: Look for the file named login.sln (the Solution file) and double-click it to open it in Visual Studio.
3.Run the Program: Press F5 or click the Start button at the top of Visual Studio.
<b>Login Credentials:</b>
Username: "admin"
Password: "adnan"
<b>Project Overview:</b>
This is a desktop-based Hospital Management System built using C# and Windows Forms. The application is designed to help hospital staff manage patient records, doctor information, and schedule appointments efficiently.
Unlike traditional database-driven apps, this system uses File-Based Storage, saving all data into .txt files via a custom HospitalService logic layer.
<b>Key Features</b>
<b> Patient Management:</b>
Register Patients: Assigns unique IDs starting from 1001.
Update/Delete: Modify patient details or remove them from the system.
Symptom Tracking: Records specific symptoms for each patient.
<b> Doctor Management: </b>
Specialization Tracking: Stores doctor names alongside their specific fields (e.g., Surgical, Cardiology).
Unique ID System: Assigns IDs starting from 9001.
<b>Appointment Scheduling:</b>
Smart Selection: Dropdowns pull live data from your Patient and Doctor files.
Validation: Prevents booking appointments for past dates.
Display Logic: Shows doctor names with their specialization in the selection menu for better clarity.
<b>Data Persistence:</b>
Automatically saves and loads data from:
Patients.txt
Doctors.txt
Appointments.txt
<b> Technology Stack:</b>
Language: C#
Framework: .NET Framework (Windows Forms)
Storage: Flat-file system (StreamWriter / StreamReader)
IDE: Visual Studio
System Logic (Flowchart)
The application follows a structured flow from authentication to data management:
<b>Project Structure:</b>
loginpage.cs: The entry point for authentication.
dashboard.cs: The central hub for navigation.
HospitalService.cs: The logic "brain" of the app that handles arrays and file I/O.
PatientsForm.cs: UI for managing patient records.
appointments.cs: UI for scheduling and viewing appointments.
<b>Contributing:</b>
This is a student project created for learning purposes. If you'd like to suggest improvements or add new features (like a Search bar or SQL integration), feel free to fork the repo!
