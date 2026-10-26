using Proyek;

Ekspedisi ekspedisi = new("JNT");
List<Kontainer> kontainer = [];
string[] namaPengirim = ["Budi", "Dimas", "Citra", "Andi", "Sari", "Rina", "Fajar", "Lestari", "Gilang"];
string[] namaPenerima = ["Wahyu", "Nadia", "Bagas", "Intan", "Rizky", "Maya", "Arif", "Putri", "Yoga"];
string[] lokasiPengiriman = ["Gudang Surabaya", "Hub Malang", "Hub Jakarta", "Alamat Penerima"];

IsiDataDummy();
JalankanMenu();

void IsiDataDummy()
{
    for (int nomorKontainer = 1; nomorKontainer <= 3; nomorKontainer++)
    {
        Kontainer kontainerBaru = TambahKontainerBaru();

        for (int nomorPaket = 1; nomorPaket <= 3; nomorPaket++)
        {
            int indeksNama = (nomorKontainer - 1) * 3 + nomorPaket - 1;
            Paket paketKilat = new PaketKilat(
                namaPengirim[indeksNama],
                namaPenerima[indeksNama],
                1 + nomorPaket,
                10 + nomorKontainer);
            Paket paketReguler = new PaketReguler(
                namaPengirim[indeksNama],
                namaPenerima[indeksNama],
                2 + nomorPaket,
                15 + nomorKontainer,
                nomorPaket % 2 == 0);
            Paket paketKargo = new PaketKargo(
                namaPengirim[indeksNama],
                namaPenerima[indeksNama],
                25 + nomorPaket,
                20 + nomorKontainer,
                true);

            ekspedisi.TambahPaket(paketKilat, kontainerBaru.NomorId);
            ekspedisi.TambahPaket(paketReguler, kontainerBaru.NomorId);
            ekspedisi.TambahPaket(paketKargo, kontainerBaru.NomorId);

            IsiRiwayatPaket(paketKilat, (nomorKontainer + nomorPaket) % 4);
            IsiRiwayatPaket(paketReguler, (nomorKontainer + nomorPaket + 1) % 4);
            IsiRiwayatPaket(paketKargo, (nomorKontainer + nomorPaket + 2) % 4);
        }

        IsiRiwayatKontainer(kontainerBaru, nomorKontainer % 4);
    }
}

void IsiRiwayatPaket(Paket paket, int jumlahTahap)
{
    for (int tahap = 1; tahap <= jumlahTahap; tahap++)
    {
        paket.UpdateStatus((StatusPaket)tahap, lokasiPengiriman[tahap - 1]);
    }
}

void IsiRiwayatKontainer(Kontainer kontainer, int jumlahTahap)
{
    for (int tahap = 1; tahap <= jumlahTahap; tahap++)
    {
        kontainer.UpdateStatus((StatusPaket)tahap, lokasiPengiriman[tahap - 1]);
    }
}

void JalankanMenu()
{
    bool berjalan = true;
    while (berjalan)
    {
        Console.WriteLine("\n=== MENU EKSPEDISI ===");
        Console.WriteLine("1. Tampilkan kontainer yang tersedia");
        Console.WriteLine("2. Tampilkan detail kontainer");
        Console.WriteLine("3. Tambahkan kontainer");
        Console.WriteLine("4. Tambahkan paket ke kontainer");
        Console.WriteLine("5. Cari dan lacak paket");
        Console.WriteLine("6. Tampilkan paket terurut dan ongkir");
        Console.WriteLine("7. Perbarui status paket");
        Console.WriteLine("8. Perbarui status kontainer");
        Console.WriteLine("0. Keluar");
        Console.Write("Pilihan: ");

        switch (Console.ReadLine())
        {
            case "1":
                TampilkanKontainer();
                break;
            case "2":
                TampilkanDetailKontainer();
                break;
            case "3":
                TambahkanKontainer();
                break;
            case "4":
                TambahkanPaket();
                break;
            case "5":
                LacakPaket();
                break;
            case "6":
                TampilkanPaketTerurut();
                break;
            case "7":
                PerbaruiStatusPaket();
                break;
            case "8":
                PerbaruiStatusKontainer();
                break;
            case "0":
                berjalan = false;
                Console.WriteLine("Program selesai.");
                break;
            default:
                Console.WriteLine("Pilihan tidak valid.");
                break;
        }
    }
}

Kontainer TambahKontainerBaru()
{
    Kontainer kontainerBaru = new();
    kontainer.Add(kontainerBaru);
    ekspedisi.TambahKontainer(kontainerBaru);
    return kontainerBaru;
}

void TampilkanKontainer()
{
    Console.WriteLine($"\nEkspedisi: {ekspedisi.Nama}");
    Console.WriteLine($"Jumlah kontainer: {ekspedisi.DaftarKontainer.Count}");
    foreach (Kontainer kontainerSaatIni in ekspedisi.DaftarKontainer)
    {
        int jumlahKilat = kontainerSaatIni.DaftarPaketTerurut.Count(paket => paket is PaketKilat);
        int jumlahReguler = kontainerSaatIni.DaftarPaketTerurut.Count(paket => paket is PaketReguler);
        int jumlahKargo = kontainerSaatIni.DaftarPaketTerurut.Count(paket => paket is PaketKargo);
        Console.WriteLine(
            $"ID {kontainerSaatIni.NomorId}: {kontainerSaatIni.DaftarPaketTerurut.Count} paket " +
            $"({jumlahKilat} kilat, {jumlahReguler} reguler, {jumlahKargo} kargo)");
    }
}

void TampilkanDetailKontainer()
{
    Kontainer? kontainerYangDipilih = PilihKontainer();
    if (kontainerYangDipilih is null)
        return;

    Console.WriteLine($"\n{kontainerYangDipilih.Lacak()}");
    Console.WriteLine("Riwayat pelacakan kontainer:");
    foreach (RiwayatPelacakan riwayat in kontainerYangDipilih.RiwayatPelacakan)
    {
        Console.WriteLine($"- {riwayat}");
    }

    Console.WriteLine($"\nNilai kontainer: Rp{kontainerYangDipilih.NilaiKontainer:N0}");
    Console.WriteLine("Daftar paket di kontainer:");
    foreach (Paket paket in kontainerYangDipilih.DaftarPaketTerurut)
    {
        Console.WriteLine(
            $"- {paket.GetType().Name}, resi {paket.NomorResi}, " +
            $"ongkir Rp{paket.HitungOngkir():N0}");
        Console.WriteLine($"  {paket.Lacak()}");
    }
}

void TambahkanKontainer()
{
    Kontainer kontainerBaru = TambahKontainerBaru();
    Console.WriteLine($"Kontainer berhasil ditambahkan dengan ID {kontainerBaru.NomorId}.");
}

void TambahkanPaket()
{
    Kontainer? kontainerTujuan = PilihKontainer();
    if (kontainerTujuan is null)
        return;

    string pengirim = BacaWajib("Nama pengirim: ");
    string penerima = BacaWajib("Nama penerima: ");
    string jenis = BacaWajib("Jenis paket (1=Kilat, 2=Reguler, 3=Kargo): ");
    if (!float.TryParse(BacaWajib("Berat (kg): "), out float berat) ||
        !float.TryParse(BacaWajib("Jarak (km): "), out float jarak))
    {
        Console.WriteLine("Berat dan jarak harus berupa angka.");
        return;
    }

    try
    {
        Paket paket = jenis switch
        {
            "1" => new PaketKilat(pengirim, penerima, berat, jarak),
            "2" => new PaketReguler(pengirim, penerima, berat, jarak),
            "3" => new PaketKargo(pengirim, penerima, berat, jarak),
            _ => throw new ArgumentException("Jenis paket tidak tersedia.")
        };

        ekspedisi.TambahPaket(paket, kontainerTujuan.NomorId);
        Console.WriteLine($"Paket berhasil ditambahkan dengan resi {paket.NomorResi}.");
    }
    catch (ArgumentException exception)
    {
        Console.WriteLine($"Paket gagal dibuat: {exception.Message}");
    }
}

void LacakPaket()
{
    string nomorResi = BacaWajib("Nomor resi: ");
    try
    {
        Paket paket = ekspedisi.CariPaket(nomorResi);
        Console.WriteLine(paket.Lacak());
        Console.WriteLine("Riwayat pelacakan:");
        foreach (RiwayatPelacakan riwayat in paket.RiwayatPelacakan)
        {
            Console.WriteLine($"- {riwayat}");
        }
    }
    catch (PaketTidakDitemukanException exception)
    {
        Console.WriteLine(exception.Message);
    }
}

void TampilkanPaketTerurut()
{
    IReadOnlyList<Paket> daftarPaket = ekspedisi.DaftarPaket;
    Console.WriteLine($"\nDaftarPaket Ekspedisi ({daftarPaket.Count} paket):");
    foreach (Paket paket in daftarPaket)
    {
        Console.WriteLine(
            $"- {paket.GetType().Name}, resi {paket.NomorResi}, " +
            $"prioritas {paket.DapatkanPrioritas()}, ongkir Rp{paket.HitungOngkir():N0}, " +
            $"kontainer {CariNomorKontainer(paket)}");
        Console.WriteLine($"  {paket.Lacak()}");
    }
}

string CariNomorKontainer(Paket paket)
{
    Kontainer? kontainer = ekspedisi.DaftarKontainer
        .FirstOrDefault(k => k.DaftarPaketTerurut.Any(p => p.Equals(paket)));

    return kontainer?.NomorId ?? "Belum ditempatkan";
}

void PerbaruiStatusPaket()
{
    string nomorResi = BacaWajib("Nomor resi: ");
    Paket paket;
    try
    {
        paket = ekspedisi.CariPaket(nomorResi);
    }
    catch (PaketTidakDitemukanException exception)
    {
        Console.WriteLine(exception.Message);
        return;
    }

    Console.WriteLine($"Status saat ini: {paket.StatusSaatIni}");
    Console.WriteLine("Status berikutnya: 1=Dikirim, 2=Sampai, 3=Diterima");
    string pilihanStatus = BacaWajib("Pilih status: ");
    StatusPaket statusBaru = pilihanStatus switch
    {
        "1" => StatusPaket.Dikirim,
        "2" => StatusPaket.Sampai,
        "3" => StatusPaket.Diterima,
        _ => (StatusPaket)(-1)
    };

    if (!Enum.IsDefined(statusBaru))
    {
        Console.WriteLine("Pilihan status tidak valid.");
        return;
    }

    try
    {
        paket.UpdateStatus(statusBaru, BacaWajib("Lokasi terbaru: "));
        Console.WriteLine("Status berhasil diperbarui.");
        Console.WriteLine("Riwayat pelacakan terbaru:");
        foreach (RiwayatPelacakan riwayat in paket.RiwayatPelacakan)
        {
            Console.WriteLine($"- {riwayat}");
        }
    }
    catch (StatusTidakValidException exception)
    {
        Console.WriteLine(exception.Message);
    }
}

void PerbaruiStatusKontainer()
{
    Kontainer? kontainerYangDipilih = PilihKontainer();
    if (kontainerYangDipilih is null)
        return;

    Console.WriteLine("Status berikutnya: 1=Dikirim, 2=Sampai, 3=Diterima");
    string pilihanStatus = BacaWajib("Pilih status: ");
    StatusPaket statusBaru = pilihanStatus switch
    {
        "1" => StatusPaket.Dikirim,
        "2" => StatusPaket.Sampai,
        "3" => StatusPaket.Diterima,
        _ => (StatusPaket)(-1)
    };

    if (!Enum.IsDefined(statusBaru))
    {
        Console.WriteLine("Pilihan status tidak valid.");
        return;
    }

    try
    {
        kontainerYangDipilih.UpdateStatus(statusBaru, BacaWajib("Lokasi terbaru: "));
        Console.WriteLine("Status kontainer berhasil diperbarui.");
    }
    catch (StatusTidakValidException exception)
    {
        Console.WriteLine(exception.Message);
    }
}

Kontainer? PilihKontainer()
{
    string nomorId = BacaWajib("Nomor ID kontainer: ");
    try
    {
        return ekspedisi.CariKontainer(nomorId);
    }
    catch (ArgumentNullException)
    {
        Console.WriteLine("Kontainer tidak ditemukan.");
        return null;
    }
}

string BacaWajib(string pesan)
{
    string? input;
    do
    {
        Console.Write(pesan);
        input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            Console.WriteLine("Input tidak boleh kosong.");
    } while (string.IsNullOrWhiteSpace(input));

    return input;
}
