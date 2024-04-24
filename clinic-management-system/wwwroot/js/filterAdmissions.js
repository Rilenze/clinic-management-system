async function filterAdmissions() {
    const fromDate = document.getElementById("fromDate");
    const toDate = document.getElementById("toDate");

    if (fromDate.value === "" || toDate.value === "") {
        console.log("nema datuma");
        return;
    }

    const response = await fetch(`/Admissions/Index?fromDate=${fromDate.value}&toDate=${toDate.value}`);
    if (!response.ok) {
        throw new Error('Došlo je do greške prilikom filtriranja prijema.');
    }

    const data = await response.json();
    console.log(data);
}