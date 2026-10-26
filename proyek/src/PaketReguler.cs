namespace Proyek;

public class PaketReguler : Paket
{
    protected override float HargaPerKgKm => 1000;
    public PaketReguler(string nomorResi, string pengirim, string penerima, float beratKg, float jarakKm, bool asuransi = false) :
      base(nomorResi, pengirim, penerima, beratKg, jarakKm)
    {
        Asuransi = asuransi;
    }

    public override float HitungOngkir()
    {
        float ongkir = HargaPerKgKm * BeratKg * JarakKm;
        if (Asuransi)
            ongkir *= 1.1f;

        return ongkir;
    }
}
