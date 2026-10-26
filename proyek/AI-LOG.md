# AI-LOG

Catat setiap penggunaan AI yang menghasilkan kode. Satu entri per penggunaan. Tulis "tidak memakai AI" jika memang tidak.
Log yang jujur tidak mengurangi nilai; log yang tidak sesuai dengan kode, atau isi log yang tidak bisa dijelaskan, yang mengurangi nilai.

## Entri saya

## Saran fitur IBisaDilacak
- Prompt: "Menurut requirement, apa saja fitur yang seharusnya dipunya oleh IBisaDilacak? berikan saran saja. Masukkan prompt ini ke AI-LOG.md sesuai format"
- Dipakai: Saran desain untuk `IBisaDilacak` berdasarkan requirement di `SOAL.md` dan `PROSES.md`
- Diubah: Tidak ada perubahan kode. Saran yang diberikan adalah kontrak sebaiknya menyediakan `AmbilResi()`, `AmbilStatus()`, dan `AmbilRiwayat()`, serta diimplementasikan oleh `Paket` dan `Kontainer`. `Kontainer` sebaiknya mengembalikan informasi pelacakan tingkat kontainer, sedangkan `Paket` mengembalikan riwayat status paketnya.

## Saran lokasi kontainer
- Prompt: "Bagaimana jika Kontainer punya attribute lokasi, bagaimana cara penggunaan kontainer kalau begitu? beri saran saja. Lalu metode Lacak() mengembalikan lokasi sekarang"
- Dipakai: Saran desain penggunaan atribut lokasi pada `Kontainer` dan makna method `Lacak()`
- Diubah: Tidak ada perubahan kode. `Kontainer` disarankan menyimpan lokasi terkini yang berubah ketika kontainer berpindah dalam proses pengiriman. `Lacak()` mengembalikan lokasi terkini tersebut. Paket di dalam kontainer dapat dianggap sedang berada di lokasi kontainer untuk pelacakan tingkat kontainer, tetapi riwayat dan status paket tetap dikelola oleh masing-masing `Paket`. Lokasi sebaiknya tidak diubah langsung dari luar, melainkan melalui method perpindahan seperti `PindahKe(lokasiBaru)` agar nilai kosong dapat ditolak dan perubahan lokasi dapat dikontrol.

## Pemeriksaan kriteria enkapsulasi
- Prompt: "Apakah proyek saat ini sudah memenuhi kriteria enkapsulasi dari @SOAL.md? jika belum point out dimana yang belum memenuhi. Tambahkan prompt ini ke @AI-LOG.md"
- Dipakai: Pemeriksaan K1 Enkapsulasi pada `SOAL.md` terhadap seluruh kelas di `src/`.
- Diubah: Tidak ada perubahan kode. Hasil pemeriksaan menunjukkan K1 belum sepenuhnya terpenuhi karena `_riwayatPelacakan` di `Paket` dideklarasikan `protected`, bukan `private`; konstruktor `Paket` belum memvalidasi `beratKg` dan `jarakKm`; konstruktor `Ekspedisi` belum memvalidasi `nama`; `CariKontainer` belum memvalidasi ID kosong dan menggunakan `ArgumentNullException` untuk kontainer yang tidak ditemukan; serta riwayat paket belum dibuka melalui properti atau method read-only. `Lokasi` pada `Kontainer` sudah memiliki `private set`, dan koleksi paket/kontainer sudah dikembalikan sebagai `IReadOnlyList`.

## Kandidat IComparable atau IEquatable
- Prompt: "Mulai sekarang tambahkan tiap prompt ke @AI-LOG.md. Class mana yang dapat diterapkan IComparable atau IEquatable"
- Dipakai: Saran penerapan bonus kesetaraan atau pengurutan pada kelas domain ekspedisi.
- Diubah: Tidak ada perubahan kode. `Paket` paling tepat menerapkan `IEquatable<Paket>` berdasarkan `NomorResi`, karena nomor resi merupakan identitas unik paket. Alternatifnya, `Kontainer` dapat menerapkan `IComparable<Kontainer>` untuk mengurutkan berdasarkan `NomorId`, tetapi pengurutan nomor ID perlu dijelaskan manfaat domainnya dan benar-benar digunakan.
