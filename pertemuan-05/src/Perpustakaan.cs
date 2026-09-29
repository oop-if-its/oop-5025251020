// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan05;

// Komposisi: Perpustakaan MEMILIKI kumpulan Anggota. Karena Mahasiswa dan Dosen
// adalah Anggota, satu daftar bertipe Anggota bisa menampung keduanya.
public class Perpustakaan
{
    private readonly List<Anggota> _anggota = new();

    public int JumlahAnggota => _anggota.Count;

    public void Daftarkan(Anggota anggota)
    {
        if (anggota == null)
            throw new ArgumentNullException();
        if (_anggota.Any(a => a.Id == anggota.Id))
            throw new InvalidOperationException();
        _anggota.Add(anggota);
    }

    public Anggota? Cari(string id)
    {
        return _anggota.Find(a => a.Id == id) ?? null;
    }

    public int JumlahMahasiswa()
    {
        return _anggota.Count(a => a is Mahasiswa);
    }

    public int JumlahDosen()
    {
        return _anggota.Count(a => a is Dosen);
    }
}
