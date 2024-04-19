console.log("connected");

const url = "https://localhost:44332";

async function fetchData() {

    try {
        let response = await fetch(`${url}/Admissions/Fetch`);

        let list = await response.json();
        console.log(list);  
        
    } catch (error) {
        console.error("Failed to fetch data: ", error);
    }
}

fetchData();



