using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onder; /*Eriþim için namespace ekle.*/
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public GameObject PrefabKarakter;
    //public GameObject DogmaNoktasi;
    public static int AnlikKarakterSayisi = 1; /*Matematiksel iþlemler için sahnedeki o anki toplam karakter sayisi.*/
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektleri;

    [Header("LEVEL VERILERI")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi;

    [Header("SAPKALAR")]
    public GameObject[] Sapkalar;

    [Header("SOPALAR")]
    public GameObject[] Sopalar;

    [Header("Materyal")]
    public Material[] Materyaler;
    public Material VarsayilanTema;
    public SkinnedMeshRenderer _Renderer;

    Matematiksel_islemler _Matematiksel_Islemler = new Matematiksel_islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    Scene _Scene;

    [Header("Genel Veriler")]
    public AudioSource[] Ses;
    public GameObject[] islemPanelleri;
    public Slider OyunSes;

    private void Awake()
    {
        Destroy(GameObject.FindWithTag("MenuSes"));
        ItemlariKontrolEt();
        Ses[0].volume = _BellekYonetim.VeriOku_f("OyunSes");
        Ses[1].volume = _BellekYonetim.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_f("OyunSes");

        _VeriYonetimi.DilLoad(); //Veri oku.
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[5]);
        DilTercihiYonetim();
    }

    void Start()
    {
        DusmanOlustur();
        //_BellekYonetim.VeriKaydet_String("Ad", "Blue");
        //Debug.Log(_BellekYonetim.VeriOku_s("Ad"));
        _Scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A)) /*Bir tuþa basýldýðýnda karakter oluþturma iþlemi.*/
        //{
        //    //Instantiate(PrefabKarakter, DogmaNoktasi.transform.position, Quaternion.identity);

        //    foreach (var item in Karakterler)
        //    {
        //        if(!item.activeInHierarchy) /*Bu nesne hiyeraþide aktif deðilse.*/
        //        {
        //            item.transform.position = DogmaNoktasi.transform.position; /*Nesnenin oluþacaðý pozisyon.*/
        //            item.SetActive(true);
        //            AnlikKarakterSayisi++;
        //            break; /*Tüm pasif karakterleri aktif yapmasýn diye döngüyü bitirdik.*/
        //        }
        //    }
        //}
    }

    //public void AdamYonetimi(string veri, Transform Pozisyon) /*Hangi objeye çarptý ve o objenin konumu.*/
    //{
    //    switch (veri)
    //    {
    //        case "x2":
    //            int sayi = 0; /*Dongu sayýsý.*/
    //            foreach (var item in Karakterler)
    //            {
    //                if (sayi < AnlikKarakterSayisi) /*Bu döngüyü anlýk karakter sayýsý kadar döndürmek istiyoruz. Çünkü sahnemizde o an 4 obje olabilir. Her bir obje için bu iþlem yapýlsýn istiyoruz.*/
    //                {
    //                    if (!item.activeInHierarchy) /*Bu nesne hiyeraþide aktif deðilse.*/
    //                    {
    //                        item.transform.position = Pozisyon.position; /*Nesnenin oluþacaðý pozisyon.*/
    //                        item.SetActive(true);
    //                        sayi++;
    //                    }
    //                }
    //                else
    //                {
    //                    sayi = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
    //                    break;
    //                }
    //            }
    //            AnlikKarakterSayisi *= 2;
    //            break;

    //        case "+3":
    //            int sayi1 = 0;
    //            foreach (var item in Karakterler)
    //            {
    //                if (sayi1 < 3) /*Toplama iþleminde döngü sayýsý sabit. Deðeri al.*/
    //                {
    //                    if (!item.activeInHierarchy) /*Bu nesne hiyeraþide aktif deðilse.*/
    //                    {
    //                        item.transform.position = Pozisyon.position; /*Nesnenin oluþacaðý pozisyon.*/
    //                        item.SetActive(true);
    //                        sayi1++;
    //                    }
    //                }
    //                else
    //                {
    //                    sayi1 = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
    //                    break;
    //                }
    //            }
    //            AnlikKarakterSayisi += 3;
    //            break;

    //        case "-4":
    //            if(AnlikKarakterSayisi < 4) /*Ýþlem anlýk karakter sayýsýndan düþük ise alt karakterleri yok edip sadece ana karakteri býrak.*/
    //            {
    //                foreach (var item in Karakterler)
    //                {
    //                    item.transform.position = Vector3.zero;
    //                    item.SetActive(false);
    //                }
    //                AnlikKarakterSayisi = 1;
    //            }
    //            else
    //            {
    //                int sayi3 = 0;
    //                foreach (var item in Karakterler)
    //                {
    //                    if (sayi3 != 4)
    //                    {
    //                        if (item.activeInHierarchy) /*Bu nesne hiyeraþide aktif.*/
    //                        {
    //                            item.transform.position = Vector3.zero; /*Nesnenin oluþacaðý pozisyon.*/
    //                            item.SetActive(false);
    //                            sayi3++;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        sayi3 = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
    //                        break;
    //                    }
    //                }
    //                AnlikKarakterSayisi -= 4;
    //            }
    //            break;

    //        case "/2":
    //            if (AnlikKarakterSayisi <= 2) /*Ýþlem anlýk karakter sayýsýndan düþük ise alt karakterleri yok edip sadece ana karakteri býrak.*/
    //            {
    //                foreach (var item in Karakterler)
    //                {
    //                    item.transform.position = Vector3.zero;
    //                    item.SetActive(false);
    //                }
    //                AnlikKarakterSayisi = 1;
    //            }
    //            else
    //            {
    //                int bolen = AnlikKarakterSayisi / 2; /*Döngü kaç kez tekrarlasýn.*/
    //                int sayi4 = 0;
    //                foreach (var item in Karakterler)
    //                {
    //                    if (sayi4 != bolen)
    //                    {
    //                        if (item.activeInHierarchy) /*Bu nesne hiyeraþide aktif.*/
    //                        {
    //                            item.transform.position = Vector3.zero; /*Nesnenin oluþacaðý pozisyon.*/
    //                            item.SetActive(false);
    //                            sayi4++;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        sayi4 = 0; /*Dongu sonu sayýsý sýfýra eþitle.*/
    //                        break;
    //                    }
    //                }
    //                if(AnlikKarakterSayisi %2 == 0)
    //                {
    //                    AnlikKarakterSayisi /= 2;
    //                }
    //                else
    //                {
    //                    AnlikKarakterSayisi /= 2;
    //                    AnlikKarakterSayisi++;
    //                }
    //            }
    //            break; 
    //    }
    //}

    public void AdamYonetimi(string islemturu, int GelenSayi, Transform Pozisyon) /*Hangi objeye çarptý , hangi iþlem, o objenin konumu.*/
    {
        switch (islemturu)
        {
            case "Carpma":
                _Matematiksel_Islemler.Carpma(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;

            case "Toplama":
                _Matematiksel_Islemler.Toplama(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;

            case "Cikarma":
                _Matematiksel_Islemler.Cikarma(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;

            case "Bolme":
                _Matematiksel_Islemler.Bolme(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;
        }
    }

    public void YokOlmaEfektiOlustur(Transform Pozisyon, bool Durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon.position;
                item.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yaptýðýmýz için otomatik pasif olacak.*/
                //AnlikKarakterSayisi--;
                item.GetComponent<AudioSource>().Play();
                if (!Durum) /*Çarpýþma sonrasý toplam alt karakter ve toplam düþman karakter sayýsýný tutmak için.*/
                {
                    AnlikKarakterSayisi--;
                }
                else
                {
                    KacDusmanOlsun--;
                }
                break;
            }
        }

        if (!OyunBittimi)
            SavasDurumu();
    }

    public void AdamLekesiEfektiOlustur(Transform Pozisyon)
    {
        foreach (var item in AdamLekesiEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon.position;
                item.GetComponent<AudioSource>().Play();
                AnlikKarakterSayisi--;
                break;
            }
        }
    }

    public void DusmanOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }

    public void DusmanTetikle()
    {
        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikmi = true;
        SavasDurumu();
    }

    void SavasDurumu() /*Çarpýþmalardan sonraki durumu kontrol etcez.*/
    {
        if (SonaGeldikmi)
        {
            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittimi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {
                    islemPanelleri[3].SetActive(true);
                }
                else
                {
                    if (AnlikKarakterSayisi > 5)
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") + 600);
                            _BellekYonetim.VeriKaydet_Int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    else
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") + 200);
                            _BellekYonetim.VeriKaydet_Int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    islemPanelleri[2].SetActive(true);
                }
            }
        }
    }

    public void ItemlariKontrolEt() /*Bu fonksiyon ile ozellestir kisminda satin alinan esyalar karaktere eklenir.*/
    {
        if (_BellekYonetim.VeriOku_i("AktifSapka") != -1)
            Sapkalar[_BellekYonetim.VeriOku_i("AktifSapka")].SetActive(true);
        if (_BellekYonetim.VeriOku_i("AktifSopa") != -1)
            Sopalar[_BellekYonetim.VeriOku_i("AktifSopa")].SetActive(true);
        if (_BellekYonetim.VeriOku_i("AktifTema") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materyaler[_BellekYonetim.VeriOku_i("AktifTema")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = VarsayilanTema;
            _Renderer.materials = mats;
        }
    }

    public void CikisButonIslem(string durum)
    {
        Ses[1].Play();
        Time.timeScale = 0f;
        if (durum == "durdur")
        {
            islemPanelleri[0].SetActive(true);
        }
        else if (durum == "devamet")
        {
            islemPanelleri[0].SetActive(false);
            Time.timeScale = 1f;
        }
        else if (durum == "tekrar")
        {
            SceneManager.LoadScene(_Scene.buildIndex);
            Time.timeScale = 1f;
        }
        else if (durum == "anasayfa")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }
    }

    public void Ayarlar(string durum)
    {
        if (durum == "ayarla")
        {
            islemPanelleri[1].SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            islemPanelleri[1].SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SesAyarla()
    {
        _BellekYonetim.VeriKaydet_Float("OyunSes", OyunSes.value);
        Ses[0].volume = _BellekYonetim.VeriOku_f("OyunSes");
        //Ses[0].volume = OyunSes.value;
    }

    public void DilTercihiYonetim()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR") /*Hangi dil seceneginin o an aktif olduguna bakip, ona gore dil ayarlarini yap.*/
        {
            for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }

    public void SonrakiLevel()
    {
        SceneManager.LoadScene(_Scene.buildIndex + 1);
    }
}
