let soruIndex = 0;

function sonrakiSoruyaGit() {
    const cevaplar = document.getElementsByName("cevap");
    const seciliCevap = Array.from(cevaplar).find((cevap) => cevap.checked);

    if (seciliCevap) {
        soruIndex++;

        if (soruIndex < 10) {
            guncelSoruGoster();
        } else {
            alert("Anket tamamlandý. Teþekkür ederiz!");
            // Ýsterseniz anket tamamlandýðýnda baþka bir iþlem yapabilirsiniz.
        }
    } else {
        alert("Lütfen bir cevap seçin.");
    }
}

function oncekiSoruyaGit() {
    if (soruIndex > 0) {
        soruIndex--;
        guncelSoruGoster();
    } else {
        alert("Bu ilk soru, önceki soru yok.");
    }
}
function gonderButonu() {
    // Add any necessary logic to handle the submission of the survey data
    alert("Anket baþarýyla gönderildi!"); // This is just an example, replace it with your actual logic
}
function guncelSoruGoster() {
    // Sorularý dinamik olarak deðiþtir
    document.getElementById("soru-sayisi").innerText = `Soru ${soruIndex + 1}:`;
    document.getElementById("soru-metni").innerText = `Bu bir örnek sorudur?`; // Sorularý burada güncelleyin

    // Seçili cevabý temizle
    const cevaplar = document.getElementsByName("cevap");
    cevaplar.forEach((cevap) => (cevap.checked = false));
}
