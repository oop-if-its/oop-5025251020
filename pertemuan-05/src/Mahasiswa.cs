namespace Pertemuan05;

// Pewarisan tunggal: Mahasiswa "adalah sebuah" Anggota.
public class Mahasiswa : Anggota
{
    public string Nrp { get; }
    public string Prodi { get; }
    public Mahasiswa(string id, string nama, Alamat alamat, string nrp, string prodi)
        : base(id, nama, alamat)
    {
        if (
            string.IsNullOrWhiteSpace(nrp) ||
            string.IsNullOrWhiteSpace(prodi)
        )
            throw new ArgumentException();
        Nrp = nrp;
        Prodi = prodi;
        BatasPinjam = 3;
    }

    public string InfoLengkap()
    {
        return $"{Info()} | NRP: {Nrp} | Prodi: {Prodi} | Alamat: {Alamat.ToString()}";
    }
}
