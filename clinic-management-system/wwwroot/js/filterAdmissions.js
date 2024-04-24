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
    console.log(data);

    let tbody = document.getElementById("filterTable");
    tbody.innerHTML = "";

    data.forEach(admission => {
        const date = new Date(admission.admissionDateTime);

        const formatedDate = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() + ' ' + ('0' + date.getHours()).slice(-2) + ':' + ('0' + date.getMinutes()).slice(-2) + ':' + ('0' + date.getSeconds()).slice(-2);


        tbody.innerHTML += `<tr><td>${admission.patient.name} ${admission.patient.surname}</td>
        <td>${formatedDate}</td>
        <td>${admission.doctor.name} ${admission.doctor.surname} - ${admission.doctor.code}</td>
        <td><input type="checkbox" disabled ${admission.urgency ? 'checked="checked"' : ""}</td>
        <td>
        <a href="/Admissions/Edit/${admission.id}">Edit</a> |
        <a href="/Admissions/Delete/${admission.id}">Delete</a> |
        <a href="/MedicalReports/Index?admissionId=${admission.id}&medicalReportId=${admission.medicalReportId}">Medical Report</a> |
        <a href="/Admissions/DownloadPDF?admissionId=${admission.id}">Download PDF</a>
        </td></tr>`
    });
}