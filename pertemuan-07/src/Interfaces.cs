namespace Pertemuan07;

public interface IDapatDipinjam
{
    bool Tersedia { get; }
    void Pinjam();
    void Kembalikan();
}

public interface IDapatDiperpanjang
{
    int JumlahPerpanjangan { get; }
    bool Perpanjang();
}

public interface INotifikasi
{
    void Kirim(string pesan);
}

public interface IDapatDicetak
{
    string Cetak();

    string CetakDenganHeader() => $"=== {Cetak()} ===";
}
