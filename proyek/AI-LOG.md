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

## Pemeriksaan kriteria K2
- Prompt: "Sekarang cek pemenuhan kriteria K2, apakah ada yang kurang?"
- Dipakai: Pemeriksaan komposisi dan pewarisan berdasarkan K2 pada `SOAL.md`.
- Diubah: Tidak ada perubahan kode. Secara desain, hierarki pewarisan sudah ada melalui abstract class `Paket` dengan tiga turunan (`PaketReguler`, `PaketKilat`, dan `PaketKargo`) yang menggunakan `: base(...)`. Komposisi juga sudah lebih dari dua: `Paket` memiliki daftar `RiwayatPelacakan`, `Kontainer` memiliki daftar `Paket`, dan `Ekspedisi` memiliki daftar `Paket` serta `Kontainer`. Namun proyek saat ini belum dapat dibangun karena ketiga turunan memanggil konstruktor `Paket` dengan lima argumen, sedangkan konstruktor `Paket` hanya menerima empat argumen. Jadi K2 terpenuhi secara rancangan, tetapi belum terpenuhi secara operasional sampai ketidaksesuaian konstruktor diperbaiki.

## Pemeriksaan kriteria K3
- Prompt: "sekarang lanjut ke K3, apakah sudah ada dua method virtual/abstract yang di override? dan apakah harus mengganti pengurutan List<Paket> karena ada pattern matching?"
- Dipakai: Pemeriksaan polimorfisme, override, dan pattern matching pada pengurutan paket berdasarkan K3 di `SOAL.md`.
- Diubah: Tidak ada perubahan kode. Saat ini baru ada satu method abstrak yang dioverride, yaitu `HitungOngkir()` pada `PaketReguler`, `PaketKilat`, dan `PaketKargo`. Properti abstrak `HargaPerKgKm` juga dioverride, tetapi bukan method. `RiwayatPelacakan.ToString()` memang dioverride, namun bukan bagian dari hierarki `Paket` dan tidak menunjukkan variasi perilaku paket. Karena itu syarat minimal dua method virtual/abstract yang dioverride dan dipanggil melalui referensi induk belum terpenuhi. Pattern matching pada `Kontainer.DaftarPaketTerurut` tidak wajib diganti hanya karena menggunakan `switch`, tetapi perlu alasan domain yang jelas. Karena ia memeriksa tipe konkret untuk menentukan prioritas, alternatif yang lebih polimorfik adalah menambahkan properti atau method abstrak/virtual pada `Paket` untuk prioritas pengurutan dan dioverride oleh setiap turunan.

## Penerapan IComparable pada pengurutan paket
- Prompt: "bagaimana cara agar ireadonlylist di kontainer.cs dapat di urutkan dengan interface IComparable di Paket"
- Dipakai: Penjelasan cara menggunakan `IComparable<Paket>` untuk mengurutkan koleksi paket dalam `Kontainer`.
- Diubah: Tidak ada perubahan kode. `Paket` saat ini sudah mengimplementasikan `IComparable<Paket>` melalui `CompareTo`, tetapi `Kontainer.DaftarPaketTerurut` masih mengurutkan berdasarkan `DapatkanPrioritas()` sehingga belum memakai `CompareTo`. Agar interface digunakan, koleksi dapat diurutkan dengan `_daftarPaket.Order().ToList()` atau `_daftarPaket.OrderBy(paket => paket).ToList()`. Ditemukan pula kondisi kedua pada `CompareTo` tidak pernah tercapai karena kondisi pertama sudah mengembalikan hasil saat prioritas sama; perbandingan harus disusun berurutan menggunakan hasil `CompareTo` sementara. `IReadOnlyList` tetap dapat dikembalikan karena hasil sorting dibuat sebagai list baru lalu diekspos sebagai read-only.

## Storyline Program
- Prompt: "Sekarang tolong buatkan kode untuk storyline di Program.cs untuk membuktikan bahwa semua class lain dapat berjalan"
- Dipakai: Demo konsol berurutan yang menjalankan ekspedisi, paket reguler/kilat/kargo, kontainer, pelacakan, riwayat, polimorfisme ongkir, `IComparable`, pencarian, dan exception domain.
- Diubah: `Program.cs` diisi storyline beserta penanganan exception. `Paket.CompareTo`, `Equals`, dan `GetHashCode` dirapikan agar kontrak perbandingan dan kesetaraan konsisten. `Kontainer.DaftarPaketTerurut` menggunakan `IComparable<Paket>` melalui `Order()`.

## Storyline tiga kontainer
- Prompt: "jadikan agar ada 3 kontainer dengan masing-masing berisi 9-15 paket yang bermacam-macam dari kilat, reguler, kargo"
- Dipakai: Memperluas demo agar menguji komposisi `Ekspedisi` dan `Kontainer` dengan data beberapa jenis paket.
- Diubah: `Program.cs` sekarang membuat 3 kontainer. Setiap kontainer berisi 9 paket, yaitu 3 `PaketKilat`, 3 `PaketReguler`, dan 3 `PaketKargo`, lalu menampilkan ringkasan jumlah dan variasi paket per kontainer.

## Nama orang pada storyline
- Prompt: "Gunakan nama orang seperti Budi, Dimas, dll, jangan 'Pengirim kilat ...' dan 'Penerima ...'"
- Dipakai: Membuat data demo pengiriman lebih mudah dibaca dan dipresentasikan.
- Diubah: `Program.cs` menggunakan nama pengirim dan penerima seperti Budi, Dimas, Citra, Wahyu, Nadia, dan Bagas, bukan label berdasarkan jenis paket.

## Menu interaktif Program
- Prompt: "Ubag agar Program menjalankan aplikasi looping dengan Menu dimmana user mengakses fitur dengan pilihan seperti 1, 2, 3. Fitur yang ada seperti: menampilkan kargo yang tersedia, menampilkan paket dalam sebuah kargo beserta data2nya jika diberikan nomorid kargo, menambahkan kontainer dan paket, dan fitur2 lain yang dirasa perlu untuk membuktikan fitur-fitur class berjalan. Tapi pertahankan dummy data."
- Dipakai: Mengubah demo satu kali menjadi aplikasi konsol interaktif dengan menu berulang dan data dummy tetap tersedia.
- Diubah: `Program.cs` sekarang memiliki menu untuk menampilkan kontainer, menampilkan detail paket berdasarkan ID kontainer, menambah kontainer, menambah paket, mencari paket, mengurutkan paket, menghitung ongkir, dan memperbarui status paket.

## Riwayat pada fitur pelacakan paket
- Prompt: "Untuk pilihan 5. Cari dan Lacak paket, tamplkan seluruh riwayatpelacakan"
- Dipakai: Menampilkan seluruh riwayat status dan lokasi setelah paket ditemukan melalui fitur pencarian dan pelacakan.
- Diubah: `Program.cs` pada pilihan 5 sekarang menampilkan hasil `Lacak()` dan setiap item dari `Paket.RiwayatPelacakan`.

## Riwayat dan status kontainer
- Prompt: "Tambahkan juga RiwayatPelacakan dan perbarui status kontainer"
- Dipakai: Menambahkan pelacakan lifecycle kontainer dan akses menu untuk memperbarui statusnya.
- Diubah: `Kontainer` sekarang menyimpan riwayat awal, mencatat perpindahan lokasi, dan menyediakan `UpdateStatus` dengan validasi status berurutan. Detail kontainer menampilkan seluruh riwayatnya, dan menu 8 digunakan untuk memperbarui status kontainer.

## Dummy data dengan riwayat bervariasi
- Prompt: "buat agar dummy data juga memiliki status bervariasi dengan riwayatpelacakan yang terisi bervariasi juga dan lokasi juga bervariasi"
- Dipakai: Membuat demo awal memperlihatkan lifecycle paket dan kontainer pada tahap pengiriman yang berbeda.
- Diubah: `Program.cs` mengisi riwayat dummy dengan jumlah tahap status yang bervariasi dari `Disiapkan` sampai `Diterima`. Setiap tahap menggunakan lokasi berbeda, dan status kontainer juga diisi secara berurutan agar tetap sesuai aturan domain.

## Menampilkan DaftarPaketTerurut
- Prompt: "Ubah agar pilihan nomer 6 menampilkan Paket di DaftarPaketTerurut"
- Dipakai: Menegaskan bahwa menu pilihan 6 membaca dan menampilkan koleksi `Kontainer.DaftarPaketTerurut`.
- Diubah: `Program.cs` menyimpan `DaftarPaketTerurut` ke `IReadOnlyList<Paket>`, menampilkan jumlah paket, lalu menampilkan tipe, resi, prioritas, ongkir, dan hasil pelacakan setiap paket.

## Menampilkan daftar paket ekspedisi
- Prompt: "maksutku buat agar menampilkan DaftarPaket dari Ekspedisi"
- Dipakai: Memperjelas cakupan menu pilihan 6 agar menampilkan seluruh paket yang terdaftar pada ekspedisi.
- Diubah: Pilihan 6 sekarang membaca `ekspedisi.DaftarPaket`, menampilkan seluruh paket ekspedisi, serta mencari dan menampilkan nomor kontainer masing-masing paket.

## Menghindari duplikasi riwayat paket
- Prompt: "kenapa ada duplikat riwayat pelacakan?"
- Dipakai: Menelusuri penyebab dua catatan awal dengan lokasi dan status yang sama pada fitur pencarian dan pelacakan.
- Diubah: Duplikasi terjadi karena konstruktor `Paket` membuat riwayat awal `Gudang - Disiapkan`, kemudian `MasukkanKontainer` menambahkan catatan identik. `Paket` sekarang memusatkan penambahan riwayat pada `TambahRiwayat` dan mengabaikan catatan baru jika lokasi serta statusnya sama dengan catatan terakhir.

## Pemeriksaan K1-K6
- Prompt: "Cek K1-K6 mana kriteria yang belum terpenuhi dan dimana bagian yang belum memenuhi?"
- Dipakai: Audit kebutuhan wajib K1 sampai K6 pada `SOAL.md` terhadap seluruh kode di `src/` dan hasil `dotnet build`.
- Diubah: Tidak ada perubahan kode. Hasilnya: K1 belum sepenuhnya terpenuhi karena masih ada field `protected`, beberapa input belum divalidasi, dan `TambahPaket` dapat mengubah state sebagian saat kontainer tidak ditemukan. K2 terpenuhi melalui hierarki `Paket` dan beberapa komposisi. K3 terpenuhi melalui `HitungOngkir` dan `DapatkanPrioritas` yang abstrak serta dioverride, ditambah override `ToString`. K4 belum terpenuhi karena belum ada dependency injection interface melalui konstruktor. K5 belum terpenuhi karena belum ada `IDisposable` yang dipakai dengan `using`, konstruktor berantai `: this(...)`, dan `finally`; dua exception kustom serta beberapa `try/catch` sudah ada. K6 build dan menu berjalan, tetapi pembuktian seluruh aturan serta skenario gagal masih bergantung pada pilihan manual pengguna, bukan demo otomatis saat startup.

## Cara melengkapi K4 dengan dependency injection
- Prompt: "Bagaimana cara melengkapi K4"
- Dipakai: Menjelaskan cara memenuhi syarat abstract class, interface, dua implementasi interface, dan dependency injection melalui konstruktor.
- Diubah: Tidak ada perubahan kode. Solusi yang disarankan adalah membuat kelas layanan, misalnya `LayananPelacakan`, yang menerima `IBisaDilacak` pada konstruktor dan menyimpan dependency tersebut sebagai field private. Layanan dapat dibuat dengan `new LayananPelacakan(paket)` atau `new LayananPelacakan(kontainer)`, sehingga satu layanan dapat bekerja dengan dua implementasi interface tanpa memeriksa tipe konkret.

## Pemeriksaan penerapan IDisposable
- Prompt: "apakah disposable yang diterapkan di ekspedisi sudah benar?"
- Dipakai: Menilai implementasi `IDisposable` pada `Ekspedisi` berdasarkan kontrak .NET dan kebutuhan K5.
- Diubah: Tidak ada perubahan kode. Implementasi dinilai benar secara mekanisme dasar karena `Ekspedisi` mengimplementasikan `IDisposable`, menyediakan `Dispose()`, aman dipanggil lebih dari satu kali melalui `_sudahDispose`, mencegah penggunaan objek setelah dispose melalui `ThrowIfDisposed()`, dan digunakan dengan `using` pada `Program.cs`. Namun, `Ekspedisi` tidak mengelola resource eksternal, sehingga `IDisposable` belum diperlukan secara teknis untuk aplikasi ini. Pengosongan daftar paket dan kontainer saat dispose sesuai untuk demonstrasi lifecycle tugas, tetapi bukan pengganti pelepasan resource eksternal.

## Pemeriksaan pemenuhan K1-K5
- Prompt: "Apakah K1-K5 sudah terpenuhi di proyek ini?"
- Dipakai: Mengaudit implementasi proyek terhadap kriteria K1 sampai K5 pada `SOAL.md`, termasuk enkapsulasi, komposisi, pewarisan, polimorfisme, interface, dependency injection, exception, dan lifecycle.
- Diubah: Tidak ada perubahan kode. K1 belum sepenuhnya terpenuhi karena masih ada anggota data `protected` pada `Paket`, validasi konstruktor `Ekspedisi` belum memeriksa nama kosong, dan `CariKontainer` masih memakai `ArgumentNullException` untuk kontainer yang tidak ditemukan. K2 terpenuhi melalui hierarki `Paket` dengan tiga turunan dan komposisi paket, riwayat, kontainer, serta ekspedisi. K3 terpenuhi melalui dua method abstrak `HitungOngkir()` dan `DapatkanPrioritas()` yang dioverride dan dipanggil melalui koleksi `Paket`, serta override `ToString()` pada `RiwayatPelacakan`. K4 terpenuhi melalui abstract class `Paket`, interface `IBisaDilacak` dengan implementasi `Paket` dan `Kontainer`, serta dependency injection pada konstruktor `LayananPelacakan`. K5 belum sepenuhnya terpenuhi karena exception kustom, `try/catch`, `IDisposable`, dan `using` sudah ada, tetapi `finally` dan konstruktor berantai `: this(...)` belum ada. Build proyek berhasil tanpa error.
