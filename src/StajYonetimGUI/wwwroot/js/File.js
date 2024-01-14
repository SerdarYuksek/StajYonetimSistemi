// Örnek dosya verileri
const dosyalar = [
    { ad: 'dosya1.txt', icerik: 'Bu dosya1.txt içeriðidir.' },
    { ad: 'dosya2.txt', icerik: 'Bu dosya2.txt içeriðidir.' },
    // Daha fazla örnek dosya ekleyebilirsiniz
];

// Dosya listesini görüntüleme fonksiyonu
function dosyaListesiniGoster() {
    const dosyaListesi = document.getElementById('fileList');
    dosyaListesi.innerHTML = '';

    dosyalar.forEach(dosya => {
        const listeElemani = document.createElement('li');
        listeElemani.textContent = dosya.ad;
        listeElemani.onclick = () => dosyaIceriginiGoster(dosya);
        dosyaListesi.appendChild(listeElemani);
    });
}

// Dosya içeriðini görüntüleme fonksiyonu
function dosyaIceriginiGoster(dosya) {
    const dosyaGoruntuleyici = document.getElementById('fileViewer');
    dosyaGoruntuleyici.textContent = dosya.icerik;
}

// Dosya yükleme fonksiyonu
function dosyaYukle() {
    const dosyaInput = document.getElementById('fileInput');
    const yeniDosya = {
        ad: dosyaInput.files[0].name,
        icerik: 'Bu yeni bir dosyadýr.'
        // FileReader kullanarak dosyanýn içeriðini okuma iþlemi de ekleyebilirsiniz
    };
    dosyalar.push(yeniDosya);
    dosyaListesiniGoster();
}

// Seçili dosyayý silme fonksiyonu
function dosyaSil() {
    const dosyaListesi = document.getElementById('fileList');
    const seciliDosyaIndex = dosyaListesi.selectedIndex;

    if (seciliDosyaIndex !== -1) {
        dosyalar.splice(seciliDosyaIndex, 1);
        dosyaListesiniGoster();
    }
}

// Ýlk görüntüleme
dosyaListesiniGoster();
