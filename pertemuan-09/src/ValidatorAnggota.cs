namespace Pertemuan08;

// Bonus (Level 10): mengumpulkan SEMUA kesalahan sebelum melapor, alih-alih
// berhenti di kesalahan pertama.
public static class ValidatorAnggota
{
    private static List<string> Periksa(string nama, string nrp, int usia)
    {
        var galat = new List<string>();
        if (string.IsNullOrWhiteSpace(nama))
            galat.Add("Nama wajib diisi");
        if (string.IsNullOrWhiteSpace(nrp) || nrp.Length != 10 || !nrp.All(char.IsDigit))
            galat.Add("NRP harus 10 digit");
        if (usia < 17 || usia > 80)
            galat.Add("Usia harus 17-80");
        return galat;
    }

    public static void Validasi(string nama, string nrp, int usia)
    {
        List<string> error = Periksa(nama, nrp, usia);

        if (error.Count > 0)
            throw new ValidasiException(error);
    }

    public static bool TryValidasi(string nama, string nrp, int usia, out IReadOnlyList<string> daftarError)
    {
        daftarError = Periksa(nama, nrp, usia);
        return daftarError.Count == 0;
    }
}
