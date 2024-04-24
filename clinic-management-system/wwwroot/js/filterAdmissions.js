async function filterAdmissions() {
    const fromDate = document.getElementById("fromDate");
    const toDate = document.getElementById("toDate");

    if (fromDate.value === "" || toDate.value === "") {
        console.log("nema datuma");
        return;
    }

    const response = await fetch(`Admissions/Index?fromDate=${fromDate.value}&toDate=${toDate.value}`);
    if (!response.ok) {
        throw new Error('Došlo je do greške prilikom filtriranja prijema.');
    }

    const data = await response.json();

    let tbody = document.getElementById("filterTable");
    tbody.innerHTML = "";

    data.forEach(admission => {
        const date = admission.admissionDateTime;

        tbody.innerHTML += `<tr><td>${admission.patient.name} ${admission.patient.surname}</td>
        <td>${date}</td>
        <td>${admission.doctor.name} ${admission.doctor.surname} - ${admission.doctor.code}</td>
        <td>${admission.urgency}</td>
        <td>
        <a href="/Admissions/Edit/${admission.id}">Edit</a> |
        <a href="/Admissions/Delete/${admission.id}">Delete</a> |
        <a href="/MedicalReports/Index?admissionId=${admission.id}&medicalReportId=${admission.medicalReportId}">Medical Report</a> |
        <a href="/Admissions/DownloadPDF?admissionId=${admission.id}">Download PDF</a>
        </td></tr>`
    });
}