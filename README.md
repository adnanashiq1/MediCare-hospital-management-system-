# Hospital Management System (C# WinForms)
<b> How to Access and Run the Program</b><br>
To get the system running on your local machine, follow these steps:<br>
1.Open the Project Folder: Navigate to the main project directory.<br>
2.Open the Solution File: Look for the file named login.sln (the Solution file) and double-click it to open it in Visual Studio.<br>
3.Run the Program: Press F5 or click the Start button at the top of Visual Studio.<br>
<b>Login Credentials:</b><br>
Username: "admin"<br>
Password: "adnan"<br>
<b>Project Overview:</b><br>
This is a desktop-based Hospital Management System built using C# and Windows Forms. The application is designed to help hospital staff manage patient records, doctor information, and schedule appointments efficiently.<br>
Unlike traditional database-driven apps, this system uses File-Based Storage, saving all data into .txt files via a custom HospitalService logic layer.<br>
<b>Key Features</b><br>
<b> Patient Management:</b><br>
Register Patients: Assigns unique IDs starting from 1001.<br>
Update/Delete: Modify patient details or remove them from the system.<br>
Symptom Tracking: Records specific symptoms for each patient.<br>
<b> Doctor Management: </b><br>
Specialization Tracking: Stores doctor names alongside their specific fields (e.g., Surgical, Cardiology).<br>
Unique ID System: Assigns IDs starting from 9001.<br>
<b>Appointment Scheduling:</b><br>
Smart Selection: Dropdowns pull live data from your Patient and Doctor files.<br>
Validation: Prevents booking appointments for past dates.<br>
Display Logic: Shows doctor names with their specialization in the selection menu for better clarity.<br>
<b>Data Persistence:</b><br>
Automatically saves and loads data from:<br>
Patients.txt<br>
Doctors.txt<br>
Appointments.txt<br>
<b> Technology Stack:</b><br>
Language: C#<br>
Framework: .NET Framework (Windows Forms)<br>
Storage: Flat-file system (StreamWriter / StreamReader)<br>
IDE: Visual Studio<br>
System Logic (Flowchart)<br>
The application follows a structured flow from authentication to data management:<br>
<b>Project Structure:</b><br>
loginpage.cs: The entry point for authentication.<br>
dashboard.cs: The central hub for navigation.<br>
HospitalService.cs: The logic "brain" of the app that handles arrays and file I/O.<br>
PatientsForm.cs: UI for managing patient records.<br>
appointments.cs: UI for scheduling and viewing appointments.<br>
<b>Contributing:</b><br>
This is a student project created for learning purposes. If you'd like to suggest improvements or add new features (like a Search bar or SQL integration), feel free to fork the repo!
