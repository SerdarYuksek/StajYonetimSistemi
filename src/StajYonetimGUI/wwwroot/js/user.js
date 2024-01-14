let personelListesi = [];
let ogrenciListesi = [];

function eklePersonel() {
    const ad = document.getElementById('ad').value;
    const soyad = document.getElementById('soyad').value;
    const tcno = document.getElementById('tcno').value;
    const email = document.getElementById('email').value;
    const gender = document.getElementById('gender').value;
    const personalNo = document.getElementById('personalNo').value;
    const tittle = document.getElementById('title').value;

    const yeniPersonel = {
        ad: ad,
        soyad: soyad,
        tcno: tcno,
        email: email,
        gender: gender,
        personalNo: personalNo,
        tittle: tittle
    };

    personelListesi.push(yeniPersonel);
    resetForm();
    listele();
}

function ekleOgrenci() {
    const ad = document.getElementById('ad').value;
    const soyad = document.getElementById('soyad').value;
    const tcno = document.getElementById('tcno').value;
    const email = document.getElementById('email').value;
    const gender = document.getElementById('gender').value;
    const studentNo = document.getElementById('studentNo').value;
    const classValue = document.getElementById('class').value;

    const yeniOgrenci = {
        ad: ad,
        soyad: soyad,
        tcno: tcno,
        email: email,
        gender: gender,
        studentNo: studentNo,
        class: classValue
    };

    ogrenciListesi.push(yeniOgrenci);
    resetForm();
    listele();
}

function resetForm() {
    document.getElementById('ad').value = '';
    document.getElementById('soyad').value = '';
    document.getElementById('tcno').value = '';
    document.getElementById('email').value = '';
    document.getElementById('gender').value = '';
    document.getElementById('personalNo').value = '';
    document.getElementById('title').value = '';
    document.getElementById('studentNo').value = '';
    document.getElementById('class').value = '';
}

function listele() {
    const listeElementi = document.getElementById('liste');
    listeElementi.innerHTML = '';

    personelListesi.forEach(function (personel, index) {
        const li = document.createElement('li');
        li.appendChild(document.createTextNode(`${personel.ad} ${personel.soyad} - TC: ${personel.tcno} - Email: ${personel.email} - Personel No: ${personel.personalNo} - Ünvan: ${personel.tittle}`));

        const silButton = document.createElement('button');
        silButton.appendChild(document.createTextNode('Sil'));
        silButton.onclick = function () {
            personelListesi.splice(index, 1);
            listele();
        };
        li.appendChild(silButton);

        listeElementi.appendChild(li);
    });

    ogrenciListesi.forEach(function (ogrenci, index) {
        const li = document.createElement('li');
        li.appendChild(document.createTextNode(`${ogrenci.ad} ${ogrenci.soyad} - TC: ${ogrenci.tcno} - Email: ${ogrenci.email} - Öðrenci No: ${ogrenci.studentNo} - Sýnýf: ${ogrenci.class}`));

        const silButton = document.createElement('button');
        silButton.appendChild(document.createTextNode('Sil'));
        silButton.onclick = function () {
            ogrenciListesi.splice(index, 1);
            listele();
        };
        li.appendChild(silButton);

        listeElementi.appendChild(li);
    });
}

function listelePersonel() {
    const listeElementi = document.getElementById('liste');
    listeElementi.innerHTML = '';

    personelListesi.forEach(function (personel, index) {
        const li = document.createElement('li');
        li.appendChild(document.createTextNode(`${personel.ad} ${personel.soyad} - TC: ${personel.tcno} - Email: ${personel.email} - Personel No: ${personel.personalNo} - Ünvan: ${personel.tittle}`));

        const silButton = document.createElement('button');
        silButton.appendChild(document.createTextNode('Sil'));
        silButton.onclick = function () {
            personelListesi.splice(index, 1);
            listelePersonel();
        };
        li.appendChild(silButton);

        listeElementi.appendChild(li);
    });
}

function listeleOgrenci() {
    const listeElementi = document.getElementById('liste');
    listeElementi.innerHTML = '';

    ogrenciListesi.forEach(function (ogrenci, index) {
        const li = document.createElement('li');
        li.appendChild(document.createTextNode(`${ogrenci.ad} ${ogrenci.soyad} - TC: ${ogrenci.tcno} - Email: ${ogrenci.email} - Öðrenci No: ${ogrenci.studentNo} - Sýnýf: ${ogrenci.class}`));

        const silButton = document.createElement('button');
        silButton.appendChild(document.createTextNode('Sil'));
        silButton.onclick = function () {
            ogrenciListesi.splice(index, 1);
            listeleOgrenci();
        };
        li.appendChild(silButton);

        listeElementi.appendChild(li);
    });
}