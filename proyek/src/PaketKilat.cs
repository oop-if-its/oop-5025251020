namespace Proyek;

public class PaketKilat : Paket
{
    protected override float HargaPerKgKm => 5000;
    public PaketKilat(string pengirim, string penerima, float beratKg, float jarakKm) : base(pengirim, penerima, beratKg, jarakKm)
    {
        // PaketKilat wajib punya asuransi
        Asuransi = true;
    }

    public override int DapatkanPrioritas() => 1;

    public override float HitungOngkir()
    {
        float ongkir = HargaPerKgKm * BeratKg * JarakKm;

        // Asuransi
        ongkir *= 1.2f;

        return ongkir;
    }
}
