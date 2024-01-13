document.addEventListener("DOMContentLoaded", function () {
    const stajyerEkleForm = document.getElementById("stajyerEkleForm");
    const stajyerTabloGovdesi = document.getElementById("stajyerTabloGovdesi");
    const stajyerDetaylariText = document.getElementById("stajyerDetaylariText");

    stajyerEkleForm.addEventListener("submit", function (event) {
        event.preventDefault();
        stajyerEkle();
    });

    stajyerTabloGovdesi.addEventListener("click", function (event) {
        if (event.target.tagName === "BUTTON") {
            const stajyerId = event.target.getAttribute("data-id");
            stajyerDetaylariniGoster(stajyerId);
        }
    });

    function stajyerEkle() {
        const ad = document.getElementById("ad").value;
        const soyad = document.getElementById("soyad").value;
        const bolum = document.getElementById("bolum").value;
        const bolum = document.getElementById("firma").value;
        const baslangicTarihi = document.getElementById("baslangicTarihi").value;
        const bitisTarihi = document.getElementById("bitisTarihi").value;

        if (ad && soyad && bolum && baslangicTarihi && bitisTarihi) {
            // Stajyer ekleme iþlemleri
            // Burada gerçek stajyer ekleme iþlemleri yapýlmalýdýr.

            // Örnek: Eklendikten sonra listeyi güncelle
            stajyerListesiniGuncelle(ad, soyad, bolum, baslangicTarihi, bitisTarihi);
        } else {
            alert("Lütfen tüm bilgileri doldurun.");
        }
    }

    function stajyerListesiniGuncelle(ad, soyad, bolum, baslangicTarihi, bitisTarihi) {
        // Stajyer listesini güncelleme iþlemleri
        // Burada gerçek stajyer listesi güncelleme iþlemleri yapýlmalýdýr.

        // Örnek: Yeni stajyerleri tabloya ekle
        const yeniStajyerTr = document.createElement("tr");
        yeniStajyerTr.innerHTML = `<td>${ad}</td><td>${soyad}</td><td>${bolum}</td><td>${firma}</td><td>${baslangicTarihi}</td><td>${bitisTarihi}</td><td><button data-id='1'>Detaylarý Görüntüle</button></td>`;
        stajyerTabloGovdesi.appendChild(yeniStajyerTr);

        // Formu temizle
        stajyerEkleForm.reset();
    }

    function stajyerDetaylariniGoster(stajyerId) {
        // Seçilen stajyerin detaylarýný gösterme iþlemleri
        // Burada gerçek detay gösterme iþlemleri yapýlmalýdýr.

        // Örnek: Detaylarý ekrana yazdýr
        stajyerDetaylariText.innerHTML = `Seçilen stajyerin detaylarý burada gösterilecek.`;
    }
});
