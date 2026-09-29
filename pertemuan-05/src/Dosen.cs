namespace Pertemuan05;

public class Dosen : Anggota
{
    public string Nip { get; }
    public Dosen(string id, string nama, Alamat alamat, string nip)
        : base(id, nama, alamat)
    {
        if (string.IsNullOrWhiteSpace(nip))
            throw new ArgumentException();
        Nip = nip;
        BatasPinjam = 10;
    }

    public string InfoLengkap()
    {
        return $"{Info()} | NIP: {Nip} | Alamat: {Alamat.ToString()}";
        throw new NotImplementedException("Level 7 belum diimplementasikan");
    }
}
