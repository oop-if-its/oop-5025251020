namespace Pertemuan05;

public class Alamat
{
    public string Jalan { get; }
    public string Kota { get; }

    public Alamat(string jalan, string kota)
    {
        if (string.IsNullOrWhiteSpace(jalan))
            throw new ArgumentException("Jalan Kosong");
        if (string.IsNullOrWhiteSpace(kota))
            throw new ArgumentException("Kota Kosong");
        Jalan = jalan;
        Kota = kota;
    }

    public override string ToString()
    {
        return Jalan + ", " + Kota;
    }
}
