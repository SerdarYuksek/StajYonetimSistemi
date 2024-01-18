let soruIndex = 0;

function sonrakiSoruyaGit() {
    const cevaplar = document.getElementsByName("cevap");
    const seciliCevap = Array.from(cevaplar).find((cevap) => cevap.checked);

    if (seciliCevap) {
        soruIndex++;

        if (soruIndex < 10) {
            guncelSoruGoster();
        } else {
            alert("Anket tamamland�. Te�ekk�r ederiz!");
            // �sterseniz anket tamamland���nda ba�ka bir i�lem yapabilirsiniz.
        }
    } else {
        alert("L�tfen bir cevap se�in.");
    }
}

function oncekiSoruyaGit() {
    if (soruIndex > 0) {
        soruIndex--;
        guncelSoruGoster();
    } else {
        alert("Bu ilk soru, �nceki soru yok.");
    }
}
function gonderButonu() {
    // Add any necessary logic to handle the submission of the survey data
    alert("Anket ba�ar�yla g�nderildi!"); // This is just an example, replace it with your actual logic
}
function guncelSoruGoster() {
    // Sorular� dinamik olarak de�i�tir
    document.getElementById("soru-sayisi").innerText = `Soru ${soruIndex + 1}:`;
    document.getElementById("soru-metni").innerText = `Bu bir �rnek sorudur?`; // Sorular� burada g�ncelleyin

    // Se�ili cevab� temizle
    const cevaplar = document.getElementsByName("cevap");
    cevaplar.forEach((cevap) => (cevap.checked = false));
}
