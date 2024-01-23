const URL = "https://localhost:"
const PORT = "7095"


const getAllArtikli = async () => {
    const response = await fetch(URL + PORT + '/api/Artikals');
    const data = await response.json();
    console.log('GET All Artikli:', data);
}

const getArtikliById = async () => {
    let id = document.getElementById("inputID").value;
    const response = await fetch(URL + PORT + `/api/Artikals/${id}`);
    const data = await response.json();
    console.log(`GET Artikal by ID (${id}):`, data);
}

const createArtikal = async () => {
    let artikal = {
        "Naziv": String(document.getElementById("Naziv").value),
        "Cena": parseFloat(document.getElementById("Cena").value),
        "BojaId": parseInt(document.getElementById("BojaId").value)
    }
    const response = await fetch(URL + PORT + '/api/Artikals', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(artikal),
    });
    const data = await response.json();
    console.log('POST Artikal:', data);
}

const updateArtikal = async () => {
    let id = parseInt(document.getElementById("ArtiakId").value)
    let artikal = {
        "Naziv": String(document.getElementById("Naziv").value),
        "Cena": parseFloat(document.getElementById("Cena").value),
        "BojaId": parseInt(document.getElementById("BojaId").value)
    }
    const response = await fetch(URL + PORT + `/api/Artikals/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(artikal),
    });
    console.log('PUT Artikal:', response.status);
}

const deleteArtikal = async () => {
    let id = document.getElementById("inputID").value;
    const response = await fetch(URL + PORT + `/api/Artikals/${id}`, {
        method: 'DELETE',
    });
    console.log('DELETE Artikal:', response.status);
}