public enum StatusPaket
{
    Disiapkan,
    Dikirim,
    Sampai,
    Diterima
}

public abstract class Paket
{
    private string NomorResi { get; }
    private string Penerima { get; }
    private string Pengirim { get; }

    private float BeratKg { get; }
    private float JarakKm { get; }

    private bool Asuransi { get; }

    private StatusPaket Status { get; set; }

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
}
