// �rnek dosya verileri
const dosyalar = [
    { ad: 'dosya1.txt', icerik: 'Bu dosya1.txt i�eri�idir.' },
    { ad: 'dosya2.txt', icerik: 'Bu dosya2.txt i�eri�idir.' },
    // Daha fazla �rnek dosya ekleyebilirsiniz
];

// Dosya listesini g�r�nt�leme fonksiyonu
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

// Dosya i�eri�ini g�r�nt�leme fonksiyonu
function dosyaIceriginiGoster(dosya) {
    const dosyaGoruntuleyici = document.getElementById('fileViewer');
    dosyaGoruntuleyici.textContent = dosya.icerik;
}

// Dosya y�kleme fonksiyonu
function dosyaYukle() {
    const dosyaInput = document.getElementById('fileInput');
    const yeniDosya = {
        ad: dosyaInput.files[0].name,
        icerik: 'Bu yeni bir dosyad�r.'
        // FileReader kullanarak dosyan�n i�eri�ini okuma i�lemi de ekleyebilirsiniz
    };
    dosyalar.push(yeniDosya);
    dosyaListesiniGoster();
}

// Se�ili dosyay� silme fonksiyonu
function dosyaSil() {
    const dosyaListesi = document.getElementById('fileList');
    const seciliDosyaIndex = dosyaListesi.selectedIndex;

    if (seciliDosyaIndex !== -1) {
        dosyalar.splice(seciliDosyaIndex, 1);
        dosyaListesiniGoster();
    }
}

// �lk g�r�nt�leme
dosyaListesiniGoster();
