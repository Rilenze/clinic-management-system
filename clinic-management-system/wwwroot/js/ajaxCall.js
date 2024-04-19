console.log("connected");

const url = "https://localhost:44332";

async function fetchData() {
    try {
        let doctorsResponse = await fetch(`${url}/Admissions/Doctors`);
        let patientsResponse = await fetch(`${url}/Admissions/Patients`);

        let doctors = await doctorsResponse.json();
        let patients = await patientsResponse.json();
        
        const doctorsList = document.getElementById("DoctorId");

        doctors.forEach(doctor => {
            doctorsList.innerHTML += `<option value="${doctor.id}">${doctor.name} ${doctor.surname}</option>`
        });

        const patientList = document.getElementById("PatientId");

        patients.forEach(patient => {
            patientList.innerHTML += `<option value="${patient.id}">${patient.name} ${patient.surname}</option>`
        });

        console.log(list);  
        
    } catch (error) {
        console.error("Failed to fetch data: ", error);
    }
}

fetchData();



