# Diagram Hierarki Kelas (UML) — Perpustakaan

Gambarkan diagram UML yang menunjukkan **hubungan antar kelas** di pertemuan ini, di bagian **bawah** penanda di akhir berkas ini. Format bebas — boleh kotak ASCII/Mermaid (`classDiagram`) atau daftar bertingkat. Yang wajib ada:

- Kelas `Anggota`, `Mahasiswa`, `Dosen`, `Asisten`, `Alamat`, dan `LogAktivitas`.
- Hubungan **pewarisan** (*is-a*): panah segitiga kosong dari kelas turunan ke kelas induk (`<|--` di Mermaid, atau `▲`, atau tulis "extends"/"turunan dari").
- Hubungan **komposisi** (*has-a*): berlian terisi dekat pihak pemilik (`*--` di Mermaid, atau `◆`, atau tulis "komposisi"/"memiliki") — siapa memiliki siapa?
- Anggota `protected` ditandai dengan simbol `#` (mis. `# BatasPinjam`), `public` dengan `+`, `private` dengan `-`.

Contoh format Mermaid (untuk kelas lain, bukan jawaban):

```mermaid
classDiagram
    Kendaraan <|-- Mobil
    Mobil *-- Mesin
    class Kendaraan {
        + Merek : string
        # kecepatan : int
    }
```

Jangan hapus baris penanda di bawah ini — jawaban kalian harus ditulis **setelah** baris itu, bukan sebelumnya.

<!-- TULIS JAWABAN KALIAN DI BAWAH BARIS INI -->

```mermaid
classDiagram
    Anggota <|-- Mahasiswa
    Anggota <|-- Dosen
    Mahasiswa <|-- Asisten
    Anggota "1" *-- "1" Alamat : memilik
    Anggota "1" *-- "1" LogAktivitas : memiliki
    Perpustakaan "1" *-- "0..*" Anggota : memiliki

    class Anggota {
        +string Id
        +string Nama
        +Alamat Alamat
        +int BatasPinjam
        +int JumlahPinjam
        -LogAktivitas _log
        +IReadOnlyList~string~ Riwayat
        +Anggota(string id, string nama, Alamat alamat)
        #set_BatasPinjam(int nilai) void
        -set_JumlahPinjam(int nilai) void
        +Info() string
        +Pinjam(string judul) void
    }

    class Mahasiswa {
        +string Nrp
        +string Prodi
        +Mahasiswa(string id, string nama, Alamat alamat, string nrp, string prodi)
        +InfoLengkap() string
    }

    class Dosen {
        +string Nip
        +Dosen(string id, string nama, Alamat alamat, string nip)
        +InfoLengkap() string
    }

    class Asisten {
        +string MataKuliah
        +Asisten(string id, string nama, Alamat alamat, string nrp, string prodi, string mataKuliah)
        +InfoAsisten() string
    }

    class Alamat {
        +string Jalan
        +string Kota
        +Alamat(string jalan, string kota)
        +ToString() string
    }

    class LogAktivitas {
        -List~string~ _entri
        +IReadOnlyList~string~ Semua
        +Catat(string pesan) void
    }

    class Perpustakaan {
        -List~Anggota~ _anggota
        +int JumlahAnggota
        +Daftarkan(Anggota anggota) void
        +Cari(string id) Anggota?
        +JumlahMahasiswa() int
        +JumlahDosen() int
    }
```
