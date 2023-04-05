using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Exercise1_DatabasePerpustakaanUmum
{
    class Program
    {
        static void Main(string[] args)
        {
             Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = ALDAYANDAY\\ALDAYANDAY; " +
                                    "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data Perpustakaan");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.Write("\nEnter your choice (1-3):");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Perpustakaan");
                                                    pr.baca(conn);
                                                }
                                                break;

                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Masukkan Id :");
                                                    string Id = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Anggota Peminjam : ");
                                                    string Anggota = Console.ReadLine();
                                                    Console.WriteLine("Nama Buku yang dipinjam : ");
                                                    string Jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Kelamin (L/P) : ");
                                                    string Alamat = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon : ");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(Id, Anggota, Jk, Alamat, notlpn, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }

                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid Option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid Option");
                            }
                            break;
                    }

                   
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.Anggota", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }
        public void insert(string Id, string Anggota, string Jk, string Alamat, string notlpn, SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.Anggota (Id, Anggota, Buku, Alamat, notlpn)"
                + "values(@nim,@nma,@alamat,@JK,@Phn)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("Id", Id));
            cmd.Parameters.Add(new SqlParameter("Anggota", Anggota));
            cmd.Parameters.Add(new SqlParameter("Jk", Jk));
            cmd.Parameters.Add(new SqlParameter("Alamat", Alamat));
            cmd.Parameters.Add(new SqlParameter("notlpn", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
