namespace Proyek;

public enum StatusPaket
{
    Disiapkan,
    Dikirim,
    Sampai,
    Diterima
}

public abstract class Paket : IBisaDilacak
{
    protected string NomorResi { get; }
    protected string Penerima { get; }
    protected string Pengirim { get; }

    protected float BeratKg { get; }
    protected float JarakKm { get; }

    protected bool Asuransi { get; set; }

    protected StatusPaket Status { get; set; }
    protected string? NomorKontainer { get; set; }

    protected abstract float HargaPerKgKm { get; }

    public Paket(string nomorResi, string pengirim, string penerima, float beratKg, float jarakKm)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(pengirim);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(penerima);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(nomorResi);
        NomorResi = nomorResi;
        Penerima = penerima;
        Pengirim = pengirim;
        BeratKg = beratKg;
        JarakKm = jarakKm;

        Status = StatusPaket.Disiapkan;
    }

    public void UpdateStatus(StatusPaket newStatus)
    {
        if (newStatus - Status != 1)
            throw new InvalidOperationException();
        Status = newStatus;
    }

    public abstract float HitungOngkir();
    public void MasukkanKontainer(string nomorKontainer)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(nomorKontainer);
        NomorKontainer = nomorKontainer;
    }
    public string Lacak()
    {
        string? kontainer = NomorKontainer;
        string lacak = $"Paket {NomorResi} dari {Pengirim} ke {Penerima} berstatus {Status}"; 

        if (kontainer is not null)
            lacak += $" dan berada di kontainer {kontainer}";

        return lacak;
    }
}
