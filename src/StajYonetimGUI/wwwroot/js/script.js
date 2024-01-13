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
            // Stajyer ekleme i�lemleri
            // Burada ger�ek stajyer ekleme i�lemleri yap�lmal�d�r.

            // �rnek: Eklendikten sonra listeyi g�ncelle
            stajyerListesiniGuncelle(ad, soyad, bolum, baslangicTarihi, bitisTarihi);
        } else {
            alert("L�tfen t�m bilgileri doldurun.");
        }
    }

    function stajyerListesiniGuncelle(ad, soyad, bolum, baslangicTarihi, bitisTarihi) {
        // Stajyer listesini g�ncelleme i�lemleri
        // Burada ger�ek stajyer listesi g�ncelleme i�lemleri yap�lmal�d�r.

        // �rnek: Yeni stajyerleri tabloya ekle
        const yeniStajyerTr = document.createElement("tr");
        yeniStajyerTr.innerHTML = `<td>${ad}</td><td>${soyad}</td><td>${bolum}</td><td>${firma}</td><td>${baslangicTarihi}</td><td>${bitisTarihi}</td><td><button data-id='1'>Detaylar� G�r�nt�le</button></td>`;
        stajyerTabloGovdesi.appendChild(yeniStajyerTr);

        // Formu temizle
        stajyerEkleForm.reset();
    }

    function stajyerDetaylariniGoster(stajyerId) {
        // Se�ilen stajyerin detaylar�n� g�sterme i�lemleri
        // Burada ger�ek detay g�sterme i�lemleri yap�lmal�d�r.

        // �rnek: Detaylar� ekrana yazd�r
        stajyerDetaylariText.innerHTML = `Se�ilen stajyerin detaylar� burada g�sterilecek.`;
    }
});
