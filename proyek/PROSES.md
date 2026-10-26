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

## 3. Keputusan desain dan alasannya

Membuat `StatusPaket` menjadi enum agar pilihan status bersifat tetap dan dapat dibandingkan secara berurutan. Dengan nilai enum yang berurutan, `UpdateStatus` dapat memastikan status hanya maju satu tahap dan tidak dapat melompati tahap.

Menerapkan `IComparable<Paket>` pada `Paket` agar aturan pengurutan berada pada objek paket, bukan pada `Kontainer`. `CompareTo` membandingkan prioritas pengiriman, kemudian ongkir, sehingga `Kontainer` cukup memanggil `Order()` tanpa memeriksa tipe konkret paket.

Menggunakan menu looping di `Program.cs` dengan data dummy yang tetap dibuat saat aplikasi dimulai. Menu membuktikan operasi utama kelas domain secara langsung, sedangkan data dummy membuat fitur pencarian, pengurutan, status, dan riwayat.

## 4. Bug atau kendala (minimal dua)

## Riwayat pelacakan paket tercatat dua kali
Saat paket baru dimasukkan ke kontainer, riwayat `Gudang - Disiapkan` muncul dua kali.
Fix: memusatkan penambahan riwayat pada method `TambahRiwayat`. Method tersebut membandingkan lokasi dan status dengan catatan terakhir, sehingga catatan identik tidak ditambahkan lagi.

## Pengurutan Paket pada kontainer tidak sesuai dengan aturan pada fungsi CompareTo
Paket hanya diurutkan berdasarkan prioritas karena masih menggunakan method OrderBy
Fix: menggantinya dengan method .Order saja sehingga menggunakan komparator default yaitu CompareTo