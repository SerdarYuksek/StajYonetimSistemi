document.addEventListener("DOMContentLoaded", function () {
    const stajyerEkleForm = document.getElementById("stajyerForm");
    const stajyerListesiTable = document.getElementById("stajyerListesi");

    stajyerEkleForm.addEventListener("submit", function (event) {
        event.preventDefault();
        stajyerEkle();
    });

    function stajyerEkle() {
        const ad = document.getElementById("stajyerAdi").value;
        const soyad = document.getElementById("stajyerSoyadi").value;
        const numara = document.getElementById("stajyerNumarasi").value;
        const bolum = document.getElementById("stajyerBolum").value;
        const firma = document.getElementById("stajyerFirma").value;
        const baslamaTarihi = document.getElementById("stajBaslamaTarihi").value;
        const bitisTarihi = document.getElementById("stajBitisTarihi").value;

        // Burada kabul gün sayýsýný hesaplayabilirsiniz, gereksinimlere baðlý olarak
        const kabulGunSayisi = hesaplaKabulGunSayisi(baslamaTarihi, bitisTarihi);

        if (ad && soyad && numara && bolum && firma && baslamaTarihi && bitisTarihi && kabulGunSayisi) {
            const yeniStajyer = {
                ad: ad,
                soyad: soyad,
                numara: numara,
                bolum: bolum,
                firma: firma,
                baslamaTarihi: baslamaTarihi,
                bitisTarihi: bitisTarihi,
                kabulGunSayisi: kabulGunSayisi
            };

            stajyerTabloyaEkle(yeniStajyer);
            stajyerEkleForm.reset();
        } else {
            alert("Lütfen tüm bilgileri doldurun.");
        }
    }

    function hesaplaKabulGunSayisi(baslamaTarihi, bitisTarihi) {
        // Burada kabul gün sayýsýný hesaplamak için gerekli iþlemleri yapabilirsiniz
        // Örneðin, tarih farkýný alarak gün sayýsýný hesaplamak gibi
        return 0; // Bu sadece örnek bir deðer, gereksinimlere uygun þekilde deðiþtirin
    }

    function stajyerTabloyaEkle(stajyer) {
        const tableBody = stajyerListesiTable.querySelector(".stajyerlist");
        if (!tableBody) {
            alert("Tablo bulunamadý!");
            return;
        }

        const yeniStajyerTr = document.createElement("tr");
        yeniStajyerTr.innerHTML = `<td>${stajyer.ad}</td>
                                   <td>${stajyer.soyad}</td>
                                   <td>${stajyer.numara}</td>
                                   <td>${stajyer.bolum}</td>
                                   <td>${stajyer.firma}</td>
                                   <td>${stajyer.baslamaTarihi}</td>
                                   <td>${stajyer.bitisTarihi}</td>
                                   <td>${stajyer.kabulGunSayisi}</td>
                                   <td><button onclick='stajyerGuncelle("${stajyer.ad}")'>Güncelle</button>
                                       <button onclick='stajyerSil("${stajyer.ad}")'>Sil</button></td>`;
        tableBody.appendChild(yeniStajyerTr);
    }

    function stajyerGuncelle(adi) {
        // Seçilen stajyeri güncelleme iþlemlerini gerçekleþtirin
        alert("Stajyer Güncelle: " + ad)
    }
})
