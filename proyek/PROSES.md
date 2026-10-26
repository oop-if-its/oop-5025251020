# PROSES

Berkas ini menjadi bahan utama ujian lisan. Tulis dengan kata-kata sendiri.

## 1. Domain dan masalahnya

Domain: Ekspedisi Paket
Masalah:
1. Membuat abstract class Paket yang merupakan blueprint untuk jenis-jenis paket yang lain (Reguler, Kilat, Kargo)
2. Implementasi class PaketReguler, PaketKilat, dan PaketKargo dengan karakteristik berbeda tapi punya kesamaan dari class Paket
3. Membuat class Kontainer yang berisi kumpulan Paket
4. Membuat Interface IBisaDilacak yang akan diterapkan di Kontainer dan Paket
5. Membuat class RiwayatPelacakan yang akan dipunyai oleh Paket dan menerapkan sistem riwayat
6. Menerapkan sistem Status dimana hanya bisa maju 1 langkah per operasi, tidak bisa mundur dan tidak bisa skip langkah tertentu
7. Membuat class Ekspedisi yang keep track semua Kargo dan Paket, menerapkan sistem pencarian resi
8. Membuat Custom Exception class dan menggunaknnya saat operasi Status tidak valid dan paket tidak ditemukan

## 2. Rancangan kelas

| Kelas / tipe | Jenis | Tanggung jawab | Hubungan |
|---|---|---|---|
| `StatusPaket` | enum | Empat status berurutan: `Disiapkan`, `Dikirim`, `Sampai`, `Diterima` | Dipakai oleh `Paket` (atribut `Status`) dan `RiwayatPelacakan` (atribut `Status`) |
| `IBisaDilacak` | interface | Kontrak pelacakan: `AmbilResi()`, `AmbilStatus()`, `AmbilRiwayat()` | Diimplementasikan oleh `Paket` dan `Kontainer` |
| `Paket` | abstract class | Blueprint semua paket: data dasar, aturan status, kerangka `HitungOngkir()` | Mengimplementasikan `IBisaDilacak`; diwarisi `PaketReguler`, `PaketKilat`, `PaketKargo`. Memiliki `RiwayatPelacakan`, dapat melempar `StatusTidakValidException` |
| `PaketReguler` | class | Paket biasa | Mewarisi `Paket` (inheritance) |
| `PaketKilat` | class | Paket cepat | Mewarisi `Paket` (inheritance) |
| `PaketKargo` | class | Paket besar | Mewarisi `Paket` (inheritance) |
| `RiwayatPelacakan` | record | Satu catatan perubahan status: status, waktu, keterangan | Dimiliki oleh `Paket` (komposisi) |
| `Kontainer` | class | Kumpulan `Paket` yang dilacak sebagai satu kesatuan | Mengimplementasikan `IBisaDilacak`, Memiliki daftar `Paket`, dimiliki oleh `Ekspedisi` |
| `Ekspedisi` | class | Menyimpan semua paket dan kontainer, mencari berdasarkan resi | Memiliki `Paket` dan `Kontainer`, dapat melempar `PaketTidakDitemukanException` |
| `StatusTidakValidException` | exception | Dilempar saat status loncat atau mundur | Mewarisi `Exception`; dilempar oleh `Paket` |
| `PaketTidakDitemukanException` | exception | Dilempar saat resi tidak ada | Mewarisi `Exception`; dilempar oleh `Ekspedisi` |

## 3. Satu keputusan desain dan alasannya

Membuat StatusPaket menjadi enum agar memiliki pilihan yang fixed, tidak seperti string. Juga mempermudah validitas pengubahan status.

## 4. Bug atau kendala (minimal dua)

Untuk tiap kendala: apa yang terjadi, bagaimana menemukan penyebabnya, bagaimana memperbaikinya.
