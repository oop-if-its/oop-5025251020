namespace Proyek;

public class PaketKargo : Paket
{
    protected override float HargaPerKgKm => 2000;
    private const float BeratMinimum = 25f;

    public PaketKargo(string pengirim, string penerima, float beratKg, float jarakKm, bool asuransi = false) : base(pengirim, penerima, beratKg, jarakKm)
    {
        if (beratKg < BeratMinimum) throw new ArgumentException();
        Asuransi = asuransi;
    }

    public override int DapatkanPrioritas() => 2;
    public override float HitungOngkir()
    {
        float ongkir = HargaPerKgKm * BeratKg * JarakKm;

        // Diskon untuk pengiriman besar;
        if (HargaPerKgKm >= 200)
            ongkir *= 0.85f;
        else if (HargaPerKgKm >= 100)
            ongkir *= 0.9f;
        else if (HargaPerKgKm >= 50)
            ongkir *= 0.95f;

        if (Asuransi)
            ongkir *= 1.2f;

        return ongkir;
    }
}
