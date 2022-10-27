using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Onder
{
    public class Matematiksel_islemler 
    {
        public void Carpma(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int DonguSayisi = (GameManager.AnlikKarakterSayisi * GelenSayi) - GameManager.AnlikKarakterSayisi;
            //                                5                     7       -           5
            int sayi = 0; /*Dongu sayýsý.*/
            foreach (var item in Karakterler)
            {
                if (sayi < DonguSayisi) /*Bu döngüyü anlýk karakter sayýsý kadar döndürmek istiyoruz. Çünkü sahnemizde o an 4 obje olabilir. Her bir obje için bu iþlem yapýlsýn istiyoruz.*/
                {
                    if (!item.activeInHierarchy) /*Bu nesne hiyeraþide aktif deðilse.*/
                    {
                        foreach (var item2 in OlusturmaEfektleri) /*Olusturma efekti.*/
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position; /*Nesnenin oluþacaðý pozisyon.*/
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi *= GelenSayi;
        }

        public void Toplama(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int sayi1 = 0;
            foreach (var item in Karakterler)
            {
                if (sayi1 < GelenSayi) /*Toplama iþleminde döngü sayýsý sabit. Deðeri al.*/
                {
                    if (!item.activeInHierarchy) /*Bu nesne hiyeraþide aktif deðilse.*/
                    {
                        foreach (var item2 in OlusturmaEfektleri) /*Olusturma efekti.*/
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position; /*Nesnenin oluþacaðý pozisyon.*/
                        item.SetActive(true);
                        sayi1++;
                    }
                }
                else
                {
                    sayi1 = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi += GelenSayi;
        }

        public void Cikarma(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi < GelenSayi) /*Ýþlem anlýk karakter sayýsýndan düþük ise alt karakterleri yok edip sadece ana karakteri býrak.*/
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                    {
                        if (!item2.activeInHierarchy)
                        {
                            item2.SetActive(true);
                            item2.transform.position = item.transform.position;
                            item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != GelenSayi)
                    {
                        if (item.activeInHierarchy) /*Bu nesne hiyeraþide aktif.*/
                        {
                            foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero; /*Nesnenin oluþacaðý pozisyon.*/
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= GelenSayi;
            }
        }

        public void Bolme(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi <= GelenSayi) /*Ýþlem anlýk karakter sayýsýndan düþük ise alt karakterleri yok edip sadece ana karakteri býrak.*/
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                    {
                        if (!item2.activeInHierarchy)
                        {
                            item2.SetActive(true);
                            item2.transform.position = item.transform.position;
                            item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int bolen = GameManager.AnlikKarakterSayisi / GelenSayi; /*Döngü kaç kez tekrarlasýn.*/
                int sayi4 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi4 != bolen)
                    {
                        if (item.activeInHierarchy) /*Bu nesne hiyeraþide aktif.*/
                        {
                            foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero; /*Nesnenin oluþacaðý pozisyon.*/
                            item.SetActive(false);
                            sayi4++;
                        }
                    }
                    else
                    {
                        sayi4 = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
                        break;
                    }
                }

                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0) /*Gelen sayýnýn 2 veya 3 olma durumuna göre mod iþlemi.*/
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 2)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi+=2;
                }
            }
        }
    }

    public class BellekYonetim
    {
        public void VeriKaydet_String(string Key, string Value)
        {
            PlayerPrefs.SetString(Key, Value);
            PlayerPrefs.Save();
        }

        public void VeriKaydet_Int(string Key, int Value)
        {
            PlayerPrefs.SetInt(Key, Value);
            PlayerPrefs.Save();
        }

        public void VeriKaydet_Float(string Key, float Value)
        {
            PlayerPrefs.SetFloat(Key, Value);
            PlayerPrefs.Save();
        }

        public string VeriOku_s(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }

        public int VeriOku_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }

        public float VeriOku_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void KontrolEtveTanýmla()
        {
            if(!PlayerPrefs.HasKey("SonLevel")) /*Oyun yeni açýldý.*/
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 0);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);
                PlayerPrefs.SetFloat("MenuSes", 1);
                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetString("Dil", "TR");
            }
        }
    }

    //public class Verilerimiz
    //{
    //    public static List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    //}

    [Serializable] /*Bu sayede classý serilestirip, bir liste halinde kullan.*/
    public class ItemBilgileri /*Liste vari çalýþarak veri tutcaz.*/
    {
        public int GrupIndex; /*Sapka, sopa, materyal vs. kategori ayýr.*/
        public int Item_Index; /*Ýlgili modelin listedeki sýrasý.*/
        public string Item_Ad;
        public string Puan; /*Bu item kac puan ile alinir.*/
        public bool SatinAlmaDurumu;
    }

    public class VeriYonetimi
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
            //bf.Serialize(file, Verilerimiz.puan); /*Puan verilerimizi dosyaya yazdýk.*/
            //file.Close();

            //_ItemBilgileri.Add(new ItemBilgileri()); /*Bu olusturdugumuz classý listenin icersine ekle. Verilerimizi liste halinde ekledik. */
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _ItemBilgileri); /*Puan verilerimizi dosyaya yazdýk.*/
            file.Close();
        }

        List<ItemBilgileri> _Itemicliste; /*Listeyi dýsarý aktaracagiz. Kullan at.*/
        public void Load()
        {
            //if(File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
            //    Verilerimiz.puan = (int)bf.Deserialize(file);
            //    file.Close();
            //}

            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _Itemicliste = (List<ItemBilgileri>)bf.Deserialize(file); /*Degerleri okur ve listeye yazar.*/
                file.Close();
            }
        }
        public List<ItemBilgileri> ListeAktar() /*Save ve load islemleri sonrasý bu fonksiyon ile disari aktar.*/
        { 
            return _Itemicliste;
        }

        public void IlkKurulumDosyaOlusturma(List<ItemBilgileri> _ItemBilgileri, List<DilVerileriAnaObje> _DilVerileriAnaObje) /*Oyun ilk acildiginda olsuturulacak kayit dosyasi.*/
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
            //bf.Serialize(file, Verilerimiz.puan); /*Puan verilerimizi dosyaya yazdýk.*/
            //file.Close();

            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd")) /*Ilk olusturma dosyasi oldugu icin bu dosya yoksa seklinde yaz.*/
            {
                //_ItemBilgileri.Add(new ItemBilgileri()); /*Bu olusturdugumuz classý listenin icersine ekle. Verilerimizi liste halinde ekledik. */
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri); /*Item verilerimizi dosyaya yazdýk.*/
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd")) /*Ilk olusturma dosyasi oldugu icin bu dosya yoksa seklinde yaz.*/
            {
                //_ItemBilgileri.Add(new ItemBilgileri()); /*Bu olusturdugumuz classý listenin icersine ekle. Verilerimizi liste halinde ekledik. */
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, _DilVerileriAnaObje); /*Dil verilerimizi dosyaya yazdýk.*/
                file.Close();
            }
        }

        List<DilVerileriAnaObje> _Dilverileriicliste;
        public void DilLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                _Dilverileriicliste = (List<DilVerileriAnaObje>)bf.Deserialize(file); /*Degerleri okur ve listeye yazar.*/
                file.Close();
            }
        }
        public List<DilVerileriAnaObje> DilVerileriListeAktar() 
        {
            return _Dilverileriicliste;
        }
    }

    [Serializable] /*Bu sayede classý serilestirip, bir liste halinde kullan.*/
    public class DilVerileriAnaObje /*Liste vari çalýþarak veri tutcaz.*/
    {
        //public int BolumIndex; /*Bu sayede ana menude mi, ozellestirmede vs anla.*/
        public List<DilVerileri_TR> _DilVerileri_TR = new List<DilVerileri_TR>(); /*Alt classimizi liste olarak aldik.*/
        public List<DilVerileri_TR> _DilVerileri_EN = new List<DilVerileri_TR>();
    }

    [Serializable] /*Bu sayede classý serilestirip, bir liste halinde kullan.*/
    public class DilVerileri_TR /*Liste vari çalýþarak veri tutcaz. Bu liste dilverilerianaobje classinin alt cocugu gibi davrancak.*/
    {
        public string Metin;
    }
}
