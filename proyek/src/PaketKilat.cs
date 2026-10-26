namespace Proyek;

public class PaketKilat : Paket
{
    protected override float HargaPerKgKm => 5000;
    public PaketKilat(string nomorResi, string pengirim, string penerima, float beratKg, float jarakKm, bool asuransi = true) : base(nomorResi, pengirim, penerima, beratKg, jarakKm)
    {
        // PaketKilat wajib punya asuransi
    }

    public override float HitungOngkir()
    {
        float ongkir = HargaPerKgKm * BeratKg * JarakKm;

        // Asuransi
        ongkir *= 1.2f;

        return ongkir;
    }
}
