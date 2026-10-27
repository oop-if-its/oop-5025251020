namespace Pertemuan08;

public static class Konfigurasi
{
    public static int JumlahInisialisasi { get; private set; }
    public static int DendaPerHari { get; }
    public static int MaksPinjam { get; }

    static Konfigurasi()
    {
        JumlahInisialisasi++;
        DendaPerHari = 1000;
        MaksPinjam = 3;
    }
}
