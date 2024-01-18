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

        // Burada kabul g�n say�s�n� hesaplayabilirsiniz, gereksinimlere ba�l� olarak
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
            alert("L�tfen t�m bilgileri doldurun.");
        }
    }

    function hesaplaKabulGunSayisi(baslamaTarihi, bitisTarihi) {
        // Burada kabul g�n say�s�n� hesaplamak i�in gerekli i�lemleri yapabilirsiniz
        // �rne�in, tarih fark�n� alarak g�n say�s�n� hesaplamak gibi
        return 0; // Bu sadece �rnek bir de�er, gereksinimlere uygun �ekilde de�i�tirin
    }

    function stajyerTabloyaEkle(stajyer) {
        const tableBody = stajyerListesiTable.querySelector(".stajyerlist");
        if (!tableBody) {
            alert("Tablo bulunamad�!");
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
                                   <td><button onclick='stajyerGuncelle("${stajyer.ad}")'>G�ncelle</button>
                                       <button onclick='stajyerSil("${stajyer.ad}")'>Sil</button></td>`;
        tableBody.appendChild(yeniStajyerTr);
    }

    function stajyerGuncelle(adi) {
        // Se�ilen stajyeri g�ncelleme i�lemlerini ger�ekle�tirin
        alert("Stajyer G�ncelle: " + ad)
    }
})
