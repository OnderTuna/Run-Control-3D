using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onder; /*Eri�im i�in namespace ekle.*/
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public GameObject PrefabKarakter;
    //public GameObject DogmaNoktasi;
    public static int AnlikKarakterSayisi = 1; /*Matematiksel i�lemler i�in sahnedeki o anki toplam karakter sayisi.*/
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
        //if(Input.GetKeyDown(KeyCode.A)) /*Bir tu�a bas�ld���nda karakter olu�turma i�lemi.*/
        //{
        //    //Instantiate(PrefabKarakter, DogmaNoktasi.transform.position, Quaternion.identity);

        //    foreach (var item in Karakterler)
        //    {
        //        if(!item.activeInHierarchy) /*Bu nesne hiyera�ide aktif de�ilse.*/
        //        {
        //            item.transform.position = DogmaNoktasi.transform.position; /*Nesnenin olu�aca�� pozisyon.*/
        //            item.SetActive(true);
        //            AnlikKarakterSayisi++;
        //            break; /*T�m pasif karakterleri aktif yapmas�n diye d�ng�y� bitirdik.*/
        //        }
        //    }
        //}
    }

    //public void AdamYonetimi(string veri, Transform Pozisyon) /*Hangi objeye �arpt� ve o objenin konumu.*/
    //{
    //    switch (veri)
    //    {
    //        case "x2":
    //            int sayi = 0; /*Dongu say�s�.*/
    //            foreach (var item in Karakterler)
    //            {
    //                if (sayi < AnlikKarakterSayisi) /*Bu d�ng�y� anl�k karakter say�s� kadar d�nd�rmek istiyoruz. ��nk� sahnemizde o an 4 obje olabilir. Her bir obje i�in bu i�lem yap�ls�n istiyoruz.*/
    //                {
    //                    if (!item.activeInHierarchy) /*Bu nesne hiyera�ide aktif de�ilse.*/
    //                    {
    //                        item.transform.position = Pozisyon.position; /*Nesnenin olu�aca�� pozisyon.*/
    //                        item.SetActive(true);
    //                        sayi++;
    //                    }
    //                }
    //                else
    //                {
    //                    sayi = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
    //                    break;
    //                }
    //            }
    //            AnlikKarakterSayisi *= 2;
    //            break;

    //        case "+3":
    //            int sayi1 = 0;
    //            foreach (var item in Karakterler)
    //            {
    //                if (sayi1 < 3) /*Toplama i�leminde d�ng� say�s� sabit. De�eri al.*/
    //                {
    //                    if (!item.activeInHierarchy) /*Bu nesne hiyera�ide aktif de�ilse.*/
    //                    {
    //                        item.transform.position = Pozisyon.position; /*Nesnenin olu�aca�� pozisyon.*/
    //                        item.SetActive(true);
    //                        sayi1++;
    //                    }
    //                }
    //                else
    //                {
    //                    sayi1 = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
    //                    break;
    //                }
    //            }
    //            AnlikKarakterSayisi += 3;
    //            break;

    //        case "-4":
    //            if(AnlikKarakterSayisi < 4) /*��lem anl�k karakter say�s�ndan d���k ise alt karakterleri yok edip sadece ana karakteri b�rak.*/
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
    //                        if (item.activeInHierarchy) /*Bu nesne hiyera�ide aktif.*/
    //                        {
    //                            item.transform.position = Vector3.zero; /*Nesnenin olu�aca�� pozisyon.*/
    //                            item.SetActive(false);
    //                            sayi3++;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        sayi3 = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
    //                        break;
    //                    }
    //                }
    //                AnlikKarakterSayisi -= 4;
    //            }
    //            break;

    //        case "/2":
    //            if (AnlikKarakterSayisi <= 2) /*��lem anl�k karakter say�s�ndan d���k ise alt karakterleri yok edip sadece ana karakteri b�rak.*/
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
    //                int bolen = AnlikKarakterSayisi / 2; /*D�ng� ka� kez tekrarlas�n.*/
    //                int sayi4 = 0;
    //                foreach (var item in Karakterler)
    //                {
    //                    if (sayi4 != bolen)
    //                    {
    //                        if (item.activeInHierarchy) /*Bu nesne hiyera�ide aktif.*/
    //                        {
    //                            item.transform.position = Vector3.zero; /*Nesnenin olu�aca�� pozisyon.*/
    //                            item.SetActive(false);
    //                            sayi4++;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        sayi4 = 0; /*Dongu sonu say�s� s�f�ra e�itle.*/
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

    public void AdamYonetimi(string islemturu, int GelenSayi, Transform Pozisyon) /*Hangi objeye �arpt� , hangi i�lem, o objenin konumu.*/
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
                item.GetComponent<ParticleSystem>().Play(); /*Stop Action > Disable yapt���m�z i�in otomatik pasif olacak.*/
                //AnlikKarakterSayisi--;
                item.GetComponent<AudioSource>().Play();
                if (!Durum) /*�arp��ma sonras� toplam alt karakter ve toplam d��man karakter say�s�n� tutmak i�in.*/
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

    void SavasDurumu() /*�arp��malardan sonraki durumu kontrol etcez.*/
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
