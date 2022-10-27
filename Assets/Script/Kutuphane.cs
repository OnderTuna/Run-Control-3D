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
            int sayi = 0; /*Dongu say�s�.*/
            foreach (var item in Karakterler)
            {
                if (sayi < DonguSayisi) /*Bu d�ng�y� anl�k karakter say�s� kadar d�nd�rmek istiyoruz. ��nk� sahnemizde o an 4 obje olabilir. Her bir obje i�in bu i�lem yap�ls�n istiyoruz.*/
                {
                    if (!item.activeInHierarchy) /*Bu nesne hiyera�ide aktif de�ilse.*/
                    {
                        foreach (var item2 in OlusturmaEfektleri) /*Olusturma efekti.*/
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position; /*Nesnenin olu�aca�� pozisyon.*/
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
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
                if (sayi1 < GelenSayi) /*Toplama i�leminde d�ng� say�s� sabit. De�eri al.*/
                {
                    if (!item.activeInHierarchy) /*Bu nesne hiyera�ide aktif de�ilse.*/
                    {
                        foreach (var item2 in OlusturmaEfektleri) /*Olusturma efekti.*/
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position; /*Nesnenin olu�aca�� pozisyon.*/
                        item.SetActive(true);
                        sayi1++;
                    }
                }
                else
                {
                    sayi1 = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi += GelenSayi;
        }

        public void Cikarma(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi < GelenSayi) /*��lem anl�k karakter say�s�ndan d���k ise alt karakterleri yok edip sadece ana karakteri b�rak.*/
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                    {
                        if (!item2.activeInHierarchy)
                        {
                            item2.SetActive(true);
                            item2.transform.position = item.transform.position;
                            item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
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
                        if (item.activeInHierarchy) /*Bu nesne hiyera�ide aktif.*/
                        {
                            foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero; /*Nesnenin olu�aca�� pozisyon.*/
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= GelenSayi;
            }
        }

        public void Bolme(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi <= GelenSayi) /*��lem anl�k karakter say�s�ndan d���k ise alt karakterleri yok edip sadece ana karakteri b�rak.*/
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                    {
                        if (!item2.activeInHierarchy)
                        {
                            item2.SetActive(true);
                            item2.transform.position = item.transform.position;
                            item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
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
                int bolen = GameManager.AnlikKarakterSayisi / GelenSayi; /*D�ng� ka� kez tekrarlas�n.*/
                int sayi4 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi4 != bolen)
                    {
                        if (item.activeInHierarchy) /*Bu nesne hiyera�ide aktif.*/
                        {
                            foreach (var item2 in YokOlmaEfektleri) /*Yok olma efekti.*/
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero; /*Nesnenin olu�aca�� pozisyon.*/
                            item.SetActive(false);
                            sayi4++;
                        }
                    }
                    else
                    {
                        sayi4 = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
                        break;
                    }
                }

                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0) /*Gelen say�n�n 2 veya 3 olma durumuna g�re mod i�lemi.*/
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

        public void KontrolEtveTan�mla()
        {
            if(!PlayerPrefs.HasKey("SonLevel")) /*Oyun yeni a��ld�.*/
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

    [Serializable] /*Bu sayede class� serilestirip, bir liste halinde kullan.*/
    public class ItemBilgileri /*Liste vari �al��arak veri tutcaz.*/
    {
        public int GrupIndex; /*Sapka, sopa, materyal vs. kategori ay�r.*/
        public int Item_Index; /*�lgili modelin listedeki s�ras�.*/
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
            //bf.Serialize(file, Verilerimiz.puan); /*Puan verilerimizi dosyaya yazd�k.*/
            //file.Close();

            //_ItemBilgileri.Add(new ItemBilgileri()); /*Bu olusturdugumuz class� listenin icersine ekle. Verilerimizi liste halinde ekledik. */
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _ItemBilgileri); /*Puan verilerimizi dosyaya yazd�k.*/
            file.Close();
        }

        List<ItemBilgileri> _Itemicliste; /*Listeyi d�sar� aktaracagiz. Kullan at.*/
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
        public List<ItemBilgileri> ListeAktar() /*Save ve load islemleri sonras� bu fonksiyon ile disari aktar.*/
        { 
            return _Itemicliste;
        }

        public void IlkKurulumDosyaOlusturma(List<ItemBilgileri> _ItemBilgileri, List<DilVerileriAnaObje> _DilVerileriAnaObje) /*Oyun ilk acildiginda olsuturulacak kayit dosyasi.*/
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
            //bf.Serialize(file, Verilerimiz.puan); /*Puan verilerimizi dosyaya yazd�k.*/
            //file.Close();

            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd")) /*Ilk olusturma dosyasi oldugu icin bu dosya yoksa seklinde yaz.*/
            {
                //_ItemBilgileri.Add(new ItemBilgileri()); /*Bu olusturdugumuz class� listenin icersine ekle. Verilerimizi liste halinde ekledik. */
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri); /*Item verilerimizi dosyaya yazd�k.*/
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd")) /*Ilk olusturma dosyasi oldugu icin bu dosya yoksa seklinde yaz.*/
            {
                //_ItemBilgileri.Add(new ItemBilgileri()); /*Bu olusturdugumuz class� listenin icersine ekle. Verilerimizi liste halinde ekledik. */
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, _DilVerileriAnaObje); /*Dil verilerimizi dosyaya yazd�k.*/
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

    [Serializable] /*Bu sayede class� serilestirip, bir liste halinde kullan.*/
    public class DilVerileriAnaObje /*Liste vari �al��arak veri tutcaz.*/
    {
        //public int BolumIndex; /*Bu sayede ana menude mi, ozellestirmede vs anla.*/
        public List<DilVerileri_TR> _DilVerileri_TR = new List<DilVerileri_TR>(); /*Alt classimizi liste olarak aldik.*/
        public List<DilVerileri_TR> _DilVerileri_EN = new List<DilVerileri_TR>();
    }

    [Serializable] /*Bu sayede class� serilestirip, bir liste halinde kullan.*/
    public class DilVerileri_TR /*Liste vari �al��arak veri tutcaz. Bu liste dilverilerianaobje classinin alt cocugu gibi davrancak.*/
    {
        public string Metin;
    }
}
