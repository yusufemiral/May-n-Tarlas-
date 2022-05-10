using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;//www.gorselprogramlama.com
using System.Threading.Tasks;

//KENAN EKİNEKER
//BİLİŞİM TEKNOLOJİLERİ TEKNİK ÖĞRETMENİ
//iletisim : kenan_ekineker@hotmail.com

namespace ConsoleApplication9
{
    class Program
    {

        protected static int origRow;
        protected static int origCol;
        protected static int Gecikme = 0;
        protected static int Satir = 15;
        protected static int Sutun = 15;
        protected static int XXIndex = 0, YYIndex = 0;
        protected static bool birincicalisma = true; //ekranyaz fonksiyonunda ilk çalışmada yazdırılacakalar için
        protected static int artimX = 4;
        protected static int artimY = 3;
        protected static int aktifkutuX, aktifkutuY;
        protected static int[,] MayinKutu = new int[Satir, Sutun];
        protected static bool[,] MayinMask = new bool[Satir, Sutun];

        protected static void WriteAt(int s, int x, int y, int YazmaislemTuru = 0)
        {
            //YazmaislemTuru == 0 ise Kutuyu kapalı haliyle çiz
            //YazmaislemTuru == 1 ise Kutuyu Açılmış haliyle çiz
            //YazmaislemTuru == 2 ise Kutuyu Bayrak koy
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write("┌──┐");
                Console.SetCursorPosition(origCol + x, origRow + y + 1);
                if ((x == 0 || y == 0))
                {
                    Console.Write("│{0,2}│", s);//www.gorselprogramlama.com
                }
                else
                {
                    if (YazmaislemTuru == 0) Console.Write("│{0,2}│", "");//İçi boş bir kutu çiz
                    if (YazmaislemTuru == 1)//içine sayısal değeri yaz
                    {
                        if (s == 0) Console.Write("│{0,2}│", "");//0 değerini yazdırma boş kalsın
                        else Console.Write("|{0,2}|", s);
                    }
                    if (YazmaislemTuru == 2) Console.Write("│{0,2}│", "*");
                }
                Console.SetCursorPosition(origCol + x, origRow + y + 2);
                Console.Write("└──┘");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }//www.gorselprogramlama.com
        }

        static bool MayinDegilse(int xx, int yy, int[,] MayinKutu)
        { return (MayinKutu[xx, yy] >= 0) && (MayinKutu[xx, yy] < 10); }

        static bool EtrafiniKontrol(int xx, int yy, ref int[,] MayinKutu, ref bool[,] MayinMask)
        {   //Bu Metood yada fonksiyon sadece Mayın tarlası üzerinde kutunun içerisindeki değer 0(sıfır) ise çalışacaktır.
            //Bu nedenle öncelikle aktif olan kutu açık duruma getirilir (Mayın Mask= true) ve ekrana aaçık olrak çizilir.
            //Sonrasında etrafındaki 8 kutuya bakılır.
            //Bu işlem sırasında eğer bakılan kutu (kuzeybatı,kuzey,kuzeydogu,batı,dogu,güneybatı,güney,güneydogu) 0 dan büyükse açılırarak sırayla tüm kutulara bakılır 
            //ÖNEMLİ NOKTA: Eğer etrafındaki kutulara bakma işlemi sırasında herhangi bir kutuda 0 (sıfır) değeri varsa FONKSİYON recursive olalrak kendi kensini tekrar çağırarak bütün işlemleri tekrardan yapar

            //Bir kutu ya 0 dır, ya mayındır (10) , yada (1.2.....8) değerini içeren rakam kutusudur.
            //bu fonksiyonda tamamı kontrol edilerek işlemler yapılır.

            bool Sonuc = true;
            bool Kuzey = (xx - 1) >= 0; //üst satır =1,2,..........
            bool Guney = (xx + 1) <= (Satir - 1); //alt koşul =....3,(5-1)
            bool Dogu = (yy + 1) <= (Sutun - 1); //sağ koşul =....3,(5-1)
            bool Bati = (yy - 1) >= 0; //sol koşul =1,2,....

            //MayınKutusunun etrafındaki 8 kutuyu kontrol
            if (MayinDegilse(xx, yy, MayinKutu)) //etrafına bak ve değer hesapla <10 mayın değerini pas geçme
            {
                MayinMask[xx, yy] = true; // kendisini aç
                kutuciz(MayinKutu[xx, yy], xx, yy, 1); System.Threading.Thread.Sleep(Gecikme);
                //sonra etrafına bak
                if (MayinKutu[xx, yy] == 0) // aktif kutu değeri 0 (sıfır) ise etrafındaki kutuların tamamına bakılacak.
                {
                    if (Kuzey && Bati) //üstsol
                    {
                        XXIndex = xx - 1; YYIndex = yy - 1;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);
                        }
                        //Console.WriteLine("XXIndex="+XXIndex+ " YYIndex="+YYIndex);
                    }
                    if (Kuzey)//www.gorselprogramlama.com
                    {
                        XXIndex = xx - 1; YYIndex = yy;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);
                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }
                    if (Kuzey && Dogu)
                    {
                        XXIndex = xx - 1; YYIndex = yy + 1;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;//www.gorselprogramlama.com
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            //else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);
                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }
                    if (Bati)
                    {
                        XXIndex = xx; YYIndex = yy - 1;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);
                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }
                    if (Dogu)
                    {
                        XXIndex = xx; YYIndex = yy + 1;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;//www.gorselprogramlama.com
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);
                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }
                    if (Guney && Bati)
                    {
                        XXIndex = xx + 1; YYIndex = yy - 1;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            //else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);
                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }
                    if (Guney)
                    {
                        XXIndex = xx + 1; YYIndex = yy;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)//www.gorselprogramlama.com
                            {
                                MayinMask[XXIndex, YYIndex] = true;
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);

                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }

                    if (Guney && Dogu)
                    {
                        XXIndex = xx + 1; YYIndex = yy + 1;
                        if (MayinMask[XXIndex, YYIndex] == false)
                        {
                            if (MayinKutu[XXIndex, YYIndex] != 0)
                            {
                                MayinMask[XXIndex, YYIndex] = true;
                                kutuciz(MayinKutu[XXIndex, YYIndex], XXIndex, YYIndex, 1); System.Threading.Thread.Sleep(Gecikme);
                            }
                            //else EtrafiniKontrol(XXIndex, YYIndex, ref MayinKutu, ref MayinMask);

                        }
                        //Console.WriteLine("XXIndex=" + XXIndex + " YYIndex=" +YYIndex);
                    }

                } imlecCiz(aktifkutuY, aktifkutuX);
            }
            else Sonuc = false;
            return Sonuc;
        }

        static void Main(string[] args)
        {
        tekrar:
            origRow = 0;
            origCol = 0;
            Console.SetWindowSize(100, 55);
            int MayinSayisi = 30;

            MayinTarlasiUretim(Satir, Sutun, MayinSayisi);


            //Mayın Kuutularını yazdırma bölümü

            ekranayaz(MayinKutu, MayinMask);

            imlecCiz(0, 0);//www.gorselprogramlama.com
            ConsoleKeyInfo keyInfo;
            aktifkutuY = 0;
            aktifkutuX = 0;
            int BirOncekiY;
            int BirOncekiX;
            int deger = 0;



        bidaha:

            //Yön tuşlarının kullanımı konusu
            //Enter tuşuna ya da space tuşuna basıldığı zamanlarda döngüden çıkar ve işlem yapar

            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                BirOncekiY = aktifkutuY;
                BirOncekiX = aktifkutuX;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:

                        if (aktifkutuY - 1 >= 0) aktifkutuY--; else aktifkutuY = Satir - 1;

                        break;
                    //www.gorselprogramlama.com
                    case ConsoleKey.RightArrow:
                        if (aktifkutuX + 1 <= Sutun - 1) aktifkutuX++; else aktifkutuX = 0;

                        break;

                    case ConsoleKey.DownArrow:
                        if (aktifkutuY + 1 <= Satir - 1) aktifkutuY++; else aktifkutuY = 0;

                        break;

                    case ConsoleKey.LeftArrow:
                        if (aktifkutuX - 1 >= 0) aktifkutuX--; else aktifkutuX = Sutun - 1;

                        break;
                }
                if (MayinMask[BirOncekiY, BirOncekiX] == false) deger = 0;
                else deger = 1;
                kutuciz(MayinKutu[BirOncekiY, BirOncekiX], BirOncekiY, BirOncekiX, deger);
                imlecCiz(aktifkutuY, aktifkutuX);

            }
            //www.gorselprogramlama.com
            if (EtrafiniKontrol(aktifkutuY, aktifkutuX, ref MayinKutu, ref MayinMask))
            {
                //ekranayaz(MayinKutu, MayinMask);
                //kutuciz(MayinKutu[xx - 1, yy - 1], xx - 1, yy - 1);

                //aktifkutuX = Console.CursorLeft/3+1;
                //aktifkutuY = Console.CursorTop/3+1 ;
                goto bidaha;
            }
            else
            { //mayına basıldığı durumda

                Console.ForegroundColor = ConsoleColor.Magenta;
                for (int ii = 0; ii < Satir; ii++)
                    for (int jj = 0; jj < Sutun; jj++)
                    {
                        if (MayinKutu[ii, jj] == 10)
                        {
                            MayinMask[ii, jj] = true;//mayınları açık duruma getit
                            kutuciz(MayinKutu[ii, jj], ii, jj, 2);
                        }
                    }


                Console.SetCursorPosition(origCol + Sutun * artimX + 10, origRow + 3);
                Console.Write("Mayına basıldı");
                Console.SetCursorPosition(origCol + Sutun * artimX + 10, origRow + 8);
                Console.Write("Devam Etmek için (ENT/ESC): ");

                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
                {
                    if (keyInfo.Key == ConsoleKey.Enter) goto tekrar;//www.gorselprogramlama.com
                    if (keyInfo.Key == ConsoleKey.Escape) break;
                }


            }


        }



        private static void MayinTarlasiUretim(int Satir, int Sutun, int MayinSayisi)
        {
            bool a, b, c, d;
            int GeciciMayinNumara;
            int[] Mayinlar = new int[MayinSayisi - 1];
            int ToplamKutuSayisi = Satir * Sutun;
            Random Rastgele = new Random();
            decimal tamKisim;
            int MayinSatir, MayinSutun;

            //MayınKutularını ve Mayın Maskelerini sıfırlama işlmei----
            //----------------------------------------------------------
            for (int ii = 0; ii < Satir; ii++) //satır sayısı kadar tekrarlama
                for (int jj = 0; jj < Sutun; jj++)//Sütun sayısı kadar tekrarlama
                {
                    MayinMask[ii, jj] = false;
                    MayinKutu[ii, jj] = 0;
                }
            //---------------------------------------------------
            //-----------------------------------------------------

            for (int k = 0; k < Mayinlar.Length; k++)//Mayınlar Dizine Mayın sayısı kadar değer aktarılır.
            {
                if (k == 0)
                //Dizinin ilk değeri için başka dizi değeri olmadığından kıyaslama yapılmadan değer birinci mayın değeri olarak alınır
                {
                    GeciciMayinNumara = Rastgele.Next(1, ToplamKutuSayisi);//www.gorselprogramlama.com
                    Mayinlar[0] = GeciciMayinNumara;

                }
                else //Eğer Daha önce değer mayın belirlendi ise üretilen değerin farklı olması sağlanır
                {
                x:
                    GeciciMayinNumara = Rastgele.Next(1, ToplamKutuSayisi);
                    for (int l = 0; l < k; l++) //Mevcut üretilmiş mayın sayısı kadar kıyaslama yapılır l<k
                    {
                        if (Mayinlar[l] == GeciciMayinNumara)
                        {
                            //System.Threading.Thread.Sleep(Gecikme);
                            //Console.WriteLine(" Gecici Mayın Değeri :" +GeciciMayinNumara);

                            goto x;

                        }
                        Mayinlar[k] = GeciciMayinNumara;

                    };

                }

                //Mayınlar[] dizisi içerisine Mayın sayısı kadar değer aktarılır
                //Bu değer toplam kutu sayısı içerisinde herhangi bir değere denk gelmektedir. Örneğin 55
                //bu değer daha sonraki aşamada satır ve sutun olarak hesaplanmalıdır.
                //sutun sayısına bölümü ile (tam kısım) satır sayısı olarak hesaplanır
                //Örneğin sutun sayısı 10 olsun burda 55/10=5(tam kısım) 5. satır
                //ancak bu 5 değeri dizide 4 olarak belirlenir (çünkü dizi değerleri 0 dan başlar
                //aynı işlem sutun değeri içinde yapılır.

                //----------Satır belirleme-----------------

                tamKisim = Mayinlar[k] / Sutun;
                tamKisim = Math.Truncate(tamKisim);
                MayinSatir = (int)(tamKisim - 1);
                if (MayinSatir < 0) MayinSatir = 0;

                //----------Sütun belirleme-----------------
                MayinSutun = (Mayinlar[k] % Sutun) - 1;
                if (MayinSutun < 0) MayinSutun = 0;//www.gorselprogramlama.com

                //hesaplanna satır ve sutun değerlerini mayın olarak atama
                MayinKutu[MayinSatir, MayinSutun] = 10;
                //10 Mayın olduğunu gösterir.Burada kutular maksimum 8 değerini alabildiği
                //billinmektedir. bu nedenle farklı olarak mayın değerleri için 10 sayısı seçilmiştir. Dizideki 10 değerlerinin tamamı
                //mayın olarak tanımlanmıştır
            }



            //Mayınlar haricinde kalan kutulardaki sayısal değerleri belirleme bölümü

            for (int ii = 0; ii < Satir; ii++) //satır sayısı kadar tekrarlama
                for (int jj = 0; jj < Sutun; jj++)//Sütun sayısı kadar tekrarlama
                {
                    // MayınKutu dizinde yer alan her kutu değerinin hesaplanması için kullanılan döngü
                    // bu döngüde mayının etrafıdaki tüm kutulara bakılarak mayın varsa sayı arttırılarak kutu değeri hesaplanır

                    if (MayinKutu[ii, jj] < 10) //Şuan hesaplama yapılacak kutu Mayın olarak belirlenmişse hiç hesaplama yapma
                    {
                        //----***
                        //----*X*
                        //----***

                        a = (ii - 1) >= 0; //üst satır =1,2,..........
                        b = (ii + 1) <= (Satir - 1); //alt koşul =....3,(5-1)
                        c = (jj + 1) <= (Sutun - 1); //sağ koşul =....3,(5-1)
                        d = (jj - 1) >= 0; //sol koşul =1,2,....

                        if (a && d && (MayinKutu[ii - 1, jj - 1] == 10)) MayinKutu[ii, jj]++; //üstsol X**
                        if (a && (MayinKutu[ii - 1, jj] == 10)) MayinKutu[ii, jj]++; //üst *X*
                        if (a && c && (MayinKutu[ii - 1, jj + 1] == 10)) MayinKutu[ii, jj]++; //üst sağ **X
                        if (d && (MayinKutu[ii, jj - 1] == 10)) MayinKutu[ii, jj]++; //sol X**
                        if (c && (MayinKutu[ii, jj + 1] == 10)) MayinKutu[ii, jj]++; //sağ **X
                        if (b && d && (MayinKutu[ii + 1, jj - 1] == 10)) MayinKutu[ii, jj]++; //altsol
                        if (b && (MayinKutu[ii + 1, jj] == 10)) MayinKutu[ii, jj]++; //alt
                        if (b && c && (MayinKutu[ii + 1, jj + 1] == 10)) MayinKutu[ii, jj]++; //altsağ

                    }

                }

        }
        private static void kutuciz(int deger, int SatirAc, int SutunAc, int KutuIslemTuru = 0)
        {
            //KutuIslemTuru == 0 ise Kutuyu kapalı haliyle çiz
            //KutuIslemTuru == 1 ise Kutuyu Açılmış haliyle çiz

            if (KutuIslemTuru == 1)
            {
                if (MayinKutu[SatirAc, SutunAc] != 0 && MayinKutu[SatirAc, SutunAc] != 10)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;//www.gorselprogramlama.com
                }
            }
            if (KutuIslemTuru == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            if (KutuIslemTuru == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
            }

            WriteAt(deger, origCol + artimX * (SutunAc + 1), origRow + artimY * (SatirAc + 1), KutuIslemTuru);
        }
        private static void imlecCiz(int SatirAc, int SutunAc)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Red;
            int xxx = origCol + artimX * (SutunAc + 1);
            int yyy = origRow + artimY * (SatirAc + 1);
            try
            {
                Console.SetCursorPosition(xxx, yyy);
                Console.Write("┌──┐");

                Console.SetCursorPosition(xxx, yyy + 1);
                if (MayinMask[SatirAc, SutunAc] && MayinKutu[SatirAc, SutunAc] != 0) Console.Write("│{0,2}│", MayinKutu[SatirAc, SutunAc]);
                else Console.Write("│{0,2}│", "");


                Console.SetCursorPosition(xxx, yyy + 2);
                Console.Write("└──┘");

            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        private static void ekranayaz(int[,] MayinKutu, bool[,] MayinMask)
        {
            Console.OutputEncoding = Encoding.GetEncoding(866);


            int KoordinatY = 0;
            int KoordinatX = 0;


            //sütun başlıklarını yazdırma-----------------------
            //----------------------------------------------------
            if (birincicalisma)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                for (int aa = 0; aa <= Satir; aa++)
                {

                    WriteAt(aa, KoordinatY, KoordinatX);//www.gorselprogramlama.com
                    KoordinatY += artimX;
                }
                KoordinatY = 0;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            //------------------------------------------------
            //------------------------------------------------

            for (int iii = 0; iii < Satir; iii++)
            {

                KoordinatX += artimY;
                if (birincicalisma) WriteAt(iii + 1, 0, KoordinatX);


                for (int jjj = 0; jjj < Sutun; jjj++)
                {
                    if (MayinMask[iii, jjj])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    }
                    KoordinatY += artimX;
                    //if (MayinKutu[iii, jjj] != 10 && MayinMask[iii, jjj])
                    WriteAt(MayinKutu[iii, jjj], KoordinatY, KoordinatX);
                    //else WriteAt(10, KoordinatY, KoordinatX);//10 değeri mayınlar içi geçerlidir.
                }
                KoordinatY = 0;
                Console.ForegroundColor = ConsoleColor.White;
            } birincicalisma = false;

        }
    }//www.gorselprogramlama.com
}
