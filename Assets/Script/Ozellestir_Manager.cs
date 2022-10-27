using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Onder;
using TMPro;
using UnityEngine.SceneManagement;

public class Ozellestir_Manager : MonoBehaviour
{
    public Text PuanText;
    public GameObject[] islemPanelleri;
    public GameObject islemCanvasi;
    public GameObject[] GenelPaneller;
    public Button[] islemButtonlari;
    public TextMeshProUGUI SatinAlmaText;
    int AktifislemPaneliIndex; /*Bu sayede index gerektiren birden fazla fonksiyonda ortak kullanim sagla.*/

    [Header("SAPKALAR")]
    public GameObject[] Sapkalar;
    public Button[] SapkaButonlari;
    public Text SapkaText;

    [Header("SOPALAR")]
    public GameObject[] Sopalar;
    public Button[] SopaButonlari;
    public Text SopaText;

    [Header("Materyal")]
    public Material[] Materyaler;
    public Material VarsayilanTema;
    public Button[] MateryalButonlari;
    public Text MateryalText;
    public SkinnedMeshRenderer _Renderer;

    int SapkaIndex = -1; /*Bu sayede ok tuþuna bastýðýmýzda yeni bir þapkaya geçiþ. Baþlangýcta default olarak -1, her bir arttýrmada index numarasýný yakalar.*/
    int SopaIndex = -1;
    int MateryalIndex = -1;

    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    [Header("GENEL VERILER")]
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public Text[] TextObjeleri;
    public Animator _Animator;
    public AudioSource[] Sesler;

    string SatinAlmaTextDY; 

    void Start()
    {
        //_BellekYonetim.VeriKaydet_Int("AktifSapka", -1);
        //_BellekYonetim.VeriKaydet_Int("AktifSopa", -1);
        //_BellekYonetim.VeriKaydet_Int("AktifTema", -1);
        //_BellekYonetim.VeriKaydet_Int("Puan", 1500);
        PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();

        //if (_BellekYonetim.VeriOku_i("AktifSapka") == -1) /*Aktif bir sapka yok demek.*/
        //{
        //    foreach (var item in Sapkalar)
        //    {
        //        item.SetActive(false); /*Tüm þapkalarý deaktif et.*/
        //    }
        //    SapkaIndex = -1;
        //    SapkaText.text = "Sapka Yok!";
        //}
        //else
        //{
        //    SapkaIndex = _BellekYonetim.VeriOku_i("AktifSapka"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
        //    Sapkalar[SapkaIndex].SetActive(true);
        //}

        //if (_BellekYonetim.VeriOku_i("AktifSopa") == -1) /*Aktif bir sapka yok demek.*/
        //{
        //    foreach (var item in Sopalar)
        //    {
        //        item.SetActive(false); /*Tüm þapkalarý deaktif et.*/
        //    }
        //    SopaIndex = -1;
        //    SopaText.text = "Sopa Yok!";
        //}
        //else
        //{
        //    SopaIndex = _BellekYonetim.VeriOku_i("AktifSopa"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
        //    Sopalar[SopaIndex].SetActive(true);
        //}

        //if (_BellekYonetim.VeriOku_i("AktifTema") == -1) /*Aktif bir sapka yok demek.*/
        //{
        //    MateryalIndex = -1;
        //    MateryalText.text = "Tema Yok!";
        //}
        //else
        //{
        //    MateryalIndex = _BellekYonetim.VeriOku_i("AktifTema"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
        //    Material[] mats = _Renderer.materials;
        //    mats[0] = Materyaler[MateryalIndex];
        //    _Renderer.materials = mats;
        //}

        //_VeriYonetimi.Save(_ItemBilgileri);
        _VeriYonetimi.Load();
        _ItemBilgileri = _VeriYonetimi.ListeAktar(); /*Okunan veriler listeye aktarilir.*/

        DurumuKontrolEt(0, true); /*Kaydettigimiz itemlar karakter uzerinde oyun baslar baslamaz gozuksun.*/
        DurumuKontrolEt(1, true);
        DurumuKontrolEt(2, true);        

        foreach (var item in Sesler)
        {
            item.volume = _BellekYonetim.VeriOku_f("MenuFx");
        }

        _VeriYonetimi.DilLoad(); //Veri oku.
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[1]);
        DilTercihiYonetim();
    }

    void Update()
    {

    }

    public void Sapka_Yonbutonlari(string durum)
    {
        Sesler[0].Play();
        if (durum == "ileri")
        {
            if (SapkaIndex == -1)
            {
                SapkaIndex = 0; /*Birkez ileri tuþuna bastýk ve 0. indisteki þapka geldi.*/
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;

                if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    SatinAlmaText.text = _ItemBilgileri[SapkaIndex].Puan + " - Satin Al";
                    islemButtonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[SapkaIndex].Puan))
                    {
                        islemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        islemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = true;
                }
            }
            else
            {
                Sapkalar[SapkaIndex].SetActive(false); /*Mevcut sapkayý false etmeli cunkü ileri tusu ile yeni sapkaya ulascaz.*/
                SapkaIndex++;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;

                if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    SatinAlmaText.text = _ItemBilgileri[SapkaIndex].Puan + " - Satin Al";
                    islemButtonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[SapkaIndex].Puan))
                    {
                        islemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        islemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = true;
                }
            }

            if (SapkaIndex == Sapkalar.Length - 1) /*Index numarasýna esitligi kontrol ediyoruz. Bu nedenle -1.*/
            {
                SapkaButonlari[1].interactable = false; /*Son elemana gelince ileri butonunu pasif et.*/
            }
            else
            {
                SapkaButonlari[1].interactable = true;
            }

            if (SapkaIndex != -1) /*Eger sapka var ise geri butonu aktif.*/
            {
                SapkaButonlari[0].interactable = true;
            }
        }
        else
        {
            if (SapkaIndex != -1)
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex--;
                if (SapkaIndex != -1)
                {
                    Sapkalar[SapkaIndex].SetActive(true);
                    SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;
                    SapkaButonlari[0].interactable = true;

                    if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                    {
                        SatinAlmaText.text = _ItemBilgileri[SapkaIndex].Puan + " - Satin Al";
                        islemButtonlari[1].interactable = false;

                        if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[SapkaIndex].Puan))
                        {
                            islemButtonlari[0].interactable = false;
                        }
                        else
                        {
                            islemButtonlari[0].interactable = true;
                        }
                    }
                    else
                    {
                        SatinAlmaText.text = "Satin Al";
                        islemButtonlari[0].interactable = false;
                        islemButtonlari[1].interactable = true;
                    }
                }
                else
                {
                    SapkaButonlari[0].interactable = false;
                    SapkaText.text = "Sapka Yok!";
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                }
            }
            else
            {
                SapkaButonlari[0].interactable = false;
                SapkaText.text = "Sapka Yok!";
                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
            }

            if (SapkaIndex != Sapkalar.Length - 1)
            {
                SapkaButonlari[1].interactable = true;
            }
        }
    }

    public void Sopa_Yonbutonlari(string durum)
    {
        Sesler[0].Play();
        if (durum == "ileri")
        {
            if (SopaIndex == -1)
            {
                SopaIndex = 0; /*Birkez ileri tuþuna bastýk ve 0. indisteki þapka geldi.*/
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;

                if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                {
                    SatinAlmaText.text = _ItemBilgileri[SopaIndex + 3].Puan + " - Satin Al";
                    islemButtonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[SopaIndex + 3].Puan))
                    {
                        islemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        islemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = true;
                }
            }
            else
            {
                Sopalar[SopaIndex].SetActive(false); /*Mevcut sapkayý false etmeli cunkü ileri tusu ile yeni sapkaya ulascaz.*/
                SopaIndex++;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;

                if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                {
                    SatinAlmaText.text = _ItemBilgileri[SopaIndex + 3].Puan + " - Satin Al";
                    islemButtonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[SopaIndex + 3].Puan))
                    {
                        islemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        islemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = true;
                }
            }

            if (SopaIndex == Sopalar.Length - 1) /*Index numarasýna esitligi kontrol ediyoruz. Bu nedenle -1.*/
            {
                SopaButonlari[1].interactable = false; /*Son elemana gelince ileri butonunu pasif et.*/
            }
            else
            {
                SopaButonlari[1].interactable = true;
            }

            if (SopaIndex != -1) /*Eger sapka var ise geri butonu aktif.*/
            {
                SopaButonlari[0].interactable = true;
            }
        }
        else
        {
            if (SopaIndex != -1)
            {
                Sopalar[SopaIndex].SetActive(false);
                SopaIndex--;
                if (SopaIndex != -1)
                {
                    Sopalar[SopaIndex].SetActive(true);
                    SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;
                    SopaButonlari[0].interactable = true;

                    if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                    {
                        SatinAlmaText.text = _ItemBilgileri[SopaIndex + 3].Puan + " - Satin Al";
                        islemButtonlari[1].interactable = false;

                        if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[SopaIndex + 3].Puan))
                        {
                            islemButtonlari[0].interactable = false;
                        }
                        else
                        {
                            islemButtonlari[0].interactable = true;
                        }
                    }
                    else
                    {
                        SatinAlmaText.text = "Satin Al";
                        islemButtonlari[0].interactable = false;
                        islemButtonlari[1].interactable = true;
                    }
                }
                else
                {
                    SopaButonlari[0].interactable = false;
                    SopaText.text = "Sopa Yok!";
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                }
            }
            else
            {
                SopaButonlari[0].interactable = false;
                SopaText.text = "Sopa Yok!";
                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
            }

            if (SopaIndex != Sopalar.Length - 1)
            {
                SopaButonlari[1].interactable = true;
            }
        }
    }

    public void Tema_Yonbutonlari(string durum)
    {
        Sesler[0].Play();
        if (durum == "ileri")
        {
            if (MateryalIndex == -1)
            {
                MateryalIndex = 0; /*Birkez ileri tuþuna bastýk ve 0. indisteki þapka geldi.*/
                Material[] mats = _Renderer.materials;
                mats[0] = Materyaler[MateryalIndex];
                _Renderer.materials = mats;
                MateryalText.text = _ItemBilgileri[MateryalIndex + 6].Item_Ad;

                if (!_ItemBilgileri[MateryalIndex + 6].SatinAlmaDurumu)
                {
                    SatinAlmaText.text = _ItemBilgileri[MateryalIndex + 6].Puan + " - Satin Al";
                    islemButtonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[MateryalIndex + 6].Puan))
                    {
                        islemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        islemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = true;
                }
            }
            else
            {
                MateryalIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materyaler[MateryalIndex];
                _Renderer.materials = mats;
                MateryalText.text = _ItemBilgileri[MateryalIndex + 6].Item_Ad;

                if (!_ItemBilgileri[MateryalIndex + 6].SatinAlmaDurumu)
                {
                    SatinAlmaText.text = _ItemBilgileri[MateryalIndex + 6].Puan + " - Satin Al";
                    islemButtonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[MateryalIndex + 6].Puan))
                    {
                        islemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        islemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = true;
                }
            }

            if (MateryalIndex == Materyaler.Length - 1) /*Index numarasýna esitligi kontrol ediyoruz. Bu nedenle -1.*/
            {
                MateryalButonlari[1].interactable = false; /*Son elemana gelince ileri butonunu pasif et.*/
            }
            else
            {
                MateryalButonlari[1].interactable = true;
            }

            if (MateryalIndex != -1) /*Eger sapka var ise geri butonu aktif.*/
            {
                MateryalButonlari[0].interactable = true;
            }
        }
        else
        {
            if (MateryalIndex != -1)
            {
                MateryalIndex--;
                if (MateryalIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materyaler[MateryalIndex];
                    _Renderer.materials = mats;
                    MateryalText.text = _ItemBilgileri[MateryalIndex + 6].Item_Ad;
                    MateryalButonlari[0].interactable = true;

                    if (!_ItemBilgileri[MateryalIndex + 6].SatinAlmaDurumu)
                    {
                        SatinAlmaText.text = _ItemBilgileri[MateryalIndex + 6].Puan + " - Satin Al";
                        islemButtonlari[1].interactable = false;

                        if (_BellekYonetim.VeriOku_i("Puan") < int.Parse(_ItemBilgileri[MateryalIndex + 6].Puan))
                        {
                            islemButtonlari[0].interactable = false;
                        }
                        else
                        {
                            islemButtonlari[0].interactable = true;
                        }
                    }
                    else
                    {
                        SatinAlmaText.text = "Satin Al";
                        islemButtonlari[0].interactable = false;
                        islemButtonlari[1].interactable = true;
                    }
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = VarsayilanTema;
                    _Renderer.materials = mats;
                    MateryalButonlari[0].interactable = false;
                    MateryalText.text = "Tema Yok!";
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                }
            }
            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = VarsayilanTema;
                _Renderer.materials = mats;
                MateryalButonlari[0].interactable = false;
                MateryalText.text = "Tema Yok!";
                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
            }

            if (MateryalIndex != Materyaler.Length - 1)
            {
                MateryalButonlari[1].interactable = true;
            }
        }
    }

    public void islemPaneliCikart(int index)
    {
        Sesler[0].Play();
        DurumuKontrolEt(index);
        GenelPaneller[0].SetActive(true);
        AktifislemPaneliIndex = index;
        islemPanelleri[index].SetActive(true);
        GenelPaneller[1].SetActive(true);
        islemCanvasi.SetActive(false);
    }

    public void GeriDon()
    {
        Sesler[0].Play();
        GenelPaneller[0].SetActive(false);
        islemCanvasi.SetActive(true);
        GenelPaneller[1].SetActive(false);

        //foreach (var item in islemPanelleri) /*Bu sayede spesifik olmadan tum islem panellerini deaktif et.*/
        //{
        //    item.SetActive(false);
        //}

        islemPanelleri[AktifislemPaneliIndex].SetActive(false);
        //islemSonucuKontrolEt(AktifislemPaneliIndex);
        DurumuKontrolEt(AktifislemPaneliIndex, true);
        AktifislemPaneliIndex = -1; /*Bu sayede aktif hiçbir item satin alma panelinin olmadigini anlariz. Cunku bunu satin al ve kaydet fonksiyonlarinda kullan.*/
    }

    public void SatinAl()
    {
        Sesler[1].Play();
        if (AktifislemPaneliIndex != -1) /*Aktifislempaneliindexine ulasarak hangi panelin aktif oldugunu index uzerinden yakalayip ona gore islem yap.*/
        {
            switch (AktifislemPaneliIndex)
            {
                case 0:
                    Debug.Log("Bolum no: " + AktifislemPaneliIndex + " Item Index " + SapkaIndex + " Item Ad " + _ItemBilgileri[SapkaIndex].Item_Ad);
                    //_ItemBilgileri[SapkaIndex].SatinAlmaDurumu = true;
                    //_BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") - int.Parse(_ItemBilgileri[SapkaIndex].Puan));
                    //SatinAlmaText.text = "Satin Al";
                    //islemButtonlari[0].interactable = false;
                    //islemButtonlari[1].interactable = true;
                    //PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();

                    SatinAlmaSonuc(SapkaIndex);
                    break;
                case 1:
                    Debug.Log("Bolum no: " + AktifislemPaneliIndex + " Item Index " + SopaIndex + " Item Ad " + _ItemBilgileri[SopaIndex + 3].Item_Ad);
                    //_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu = true;
                    //_BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") - int.Parse(_ItemBilgileri[SopaIndex + 3].Puan));
                    //SatinAlmaText.text = "Satin Al";
                    //islemButtonlari[0].interactable = false;
                    //islemButtonlari[1].interactable = true;
                    //PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();

                    SatinAlmaSonuc(SopaIndex + 3);
                    break;
                case 2:
                    Debug.Log("Bolum no: " + AktifislemPaneliIndex + " Item Index " + MateryalIndex + " Item Ad " + _ItemBilgileri[MateryalIndex + 6].Item_Ad);
                    //_ItemBilgileri[MateryalIndex + 6].SatinAlmaDurumu = true;
                    //_BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") - int.Parse(_ItemBilgileri[MateryalIndex + 6].Puan));
                    //SatinAlmaText.text = "Satin Al";
                    //islemButtonlari[0].interactable = false;
                    //islemButtonlari[1].interactable = true;
                    //PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();

                    SatinAlmaSonuc(MateryalIndex + 6);
                    break;
            }
        }
    }

    public void Kaydet()
    {
        Sesler[2].Play();
        if (AktifislemPaneliIndex != -1) /*Aktifislempaneliindexine ulasarak hangi panelin aktif oldugunu index uzerinden yakalayip ona gore islem yap.*/
        {
            switch (AktifislemPaneliIndex)
            {
                case 0:
                    Debug.Log("Bolum no: " + AktifislemPaneliIndex + " Item Index " + SapkaIndex + " Item Ad " + _ItemBilgileri[SapkaIndex].Item_Ad);
                    //_BellekYonetim.VeriKaydet_Int("AktifSapka", SapkaIndex);
                    //islemButtonlari[1].interactable = false;
                    //if(!_Animator.GetBool("ok")) /*Bool durumunu sorgula.*/
                    //{
                    //    _Animator.SetBool("ok", true);
                    //}
                    KaydetmeSonuc("AktifSapka", SapkaIndex);
                    break;
                case 1:
                    Debug.Log("Bolum no: " + AktifislemPaneliIndex + " Item Index " + SopaIndex + " Item Ad " + _ItemBilgileri[SopaIndex + 3].Item_Ad);
                    //_BellekYonetim.VeriKaydet_Int("AktifSopa", SopaIndex);
                    //islemButtonlari[1].interactable = false;
                    //if (!_Animator.GetBool("ok")) /*Bool durumunu sorgula.*/
                    //{
                    //    _Animator.SetBool("ok", true);
                    //}
                    KaydetmeSonuc("AktifSopa", SopaIndex);
                    break;
                case 2:
                    Debug.Log("Bolum no: " + AktifislemPaneliIndex + " Item Index " + MateryalIndex + " Item Ad " + _ItemBilgileri[MateryalIndex + 6].Item_Ad);
                    //_BellekYonetim.VeriKaydet_Int("AktifTema", MateryalIndex);
                    //islemButtonlari[1].interactable = false;
                    //if (!_Animator.GetBool("ok")) /*Bool durumunu sorgula.*/
                    //{
                    //    _Animator.SetBool("ok", true);
                    //}
                    KaydetmeSonuc("AktifTema", MateryalIndex);
                    break;
            }
        }
        
    }

    void DurumuKontrolEt(int Bolum, bool islem = false)
    {
        if (Bolum == 0)
        {
            if (_BellekYonetim.VeriOku_i("AktifSapka") == -1) /*Aktif bir sapka yok demek.*/
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false); /*Tüm þapkalarý deaktif et.*/
                }

                SatinAlmaText.text = "Satin Al";
                //TextObjeleri[5].text = SatinAlmaTextDY; //Dil secenegi icin.
                islemButtonlari[0].interactable = false;
                islemButtonlari[1].interactable = false;

                if (!islem)
                {
                    SapkaIndex = -1;
                    SapkaText.text = "Sapka Yok!";
                }
            }
            else
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false); /*Tüm þapkalarý deaktif et.*/
                }

                SapkaIndex = _BellekYonetim.VeriOku_i("AktifSapka"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;
                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
                islemButtonlari[1].interactable = true;
            }
        }
        else if (Bolum == 1)
        {
            if (_BellekYonetim.VeriOku_i("AktifSopa") == -1) /*Aktif bir sapka yok demek.*/
            {
                foreach (var item in Sopalar)
                {
                    item.SetActive(false); 
                }

                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
                islemButtonlari[1].interactable = false;

                if (!islem)
                {
                    SopaIndex = -1;
                    SopaText.text = "Sopa Yok!";
                }
            }
            else
            {
                foreach (var item in Sopalar)
                {
                    item.SetActive(false); 
                }

                SopaIndex = _BellekYonetim.VeriOku_i("AktifSopa"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;
                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
                islemButtonlari[1].interactable = true;
            }
        }
        else if (Bolum == 2)
        {
            if (_BellekYonetim.VeriOku_i("AktifTema") == -1) /*Aktif bir sapka yok demek.*/
            {
                if (!islem)
                {
                    MateryalIndex = -1;
                    MateryalText.text = "Tema Yok!";
                    SatinAlmaText.text = "Satin Al";
                    islemButtonlari[0].interactable = false;
                    islemButtonlari[1].interactable = false;
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = VarsayilanTema;
                    _Renderer.materials = mats;
                    SatinAlmaText.text = "Satin Al";
                }
            }
            else
            {
                MateryalIndex = _BellekYonetim.VeriOku_i("AktifTema"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
                Material[] mats = _Renderer.materials;
                mats[0] = Materyaler[MateryalIndex];
                _Renderer.materials = mats;
                MateryalText.text = _ItemBilgileri[MateryalIndex + 6].Item_Ad;
                SatinAlmaText.text = "Satin Al";
                islemButtonlari[0].interactable = false;
                islemButtonlari[1].interactable = true;
            }
        }
    }

    //void islemSonucuKontrolEt(int Bolum)
    //{
    //    if (Bolum == 0)
    //    {
    //        if (_BellekYonetim.VeriOku_i("AktifSapka") == -1) /*Aktif bir sapka yok demek.*/
    //        {
    //            foreach (var item in Sapkalar)
    //            {
    //                item.SetActive(false); /*Tüm þapkalarý deaktif et.*/
    //            }
    //        }
    //        else
    //        {
    //            SapkaIndex = _BellekYonetim.VeriOku_i("AktifSapka"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
    //            Sapkalar[SapkaIndex].SetActive(true);
    //        }
    //    }
    //    else if (Bolum == 1)
    //    {
    //        if (_BellekYonetim.VeriOku_i("AktifSopa") == -1) /*Aktif bir sapka yok demek.*/
    //        {
    //            foreach (var item in Sopalar)
    //            {
    //                item.SetActive(false); /*Tüm þapkalarý deaktif et.*/
    //            }
    //        }
    //        else
    //        {
    //            SopaIndex = _BellekYonetim.VeriOku_i("AktifSopa"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
    //            Sopalar[SopaIndex].SetActive(true);
    //        }
    //    }
    //    else if (Bolum == 2)
    //    {
    //        if (_BellekYonetim.VeriOku_i("AktifTema") == -1) /*Aktif bir sapka yok demek.*/
    //        {
    //            Material[] mats = _Renderer.materials;
    //            mats[0] = VarsayilanTema;
    //            _Renderer.materials = mats;
    //        }
    //        else
    //        {
    //            MateryalIndex = _BellekYonetim.VeriOku_i("AktifTema"); /*O anki aktif þapkama ait indexi verilerden okuyup sapkaindex degerine tanýmla.*/
    //            Material[] mats = _Renderer.materials;
    //            mats[0] = Materyaler[MateryalIndex];
    //            _Renderer.materials = mats;
    //        }
    //    }
    //}

    public void AnaMenuyeDon()
    {
        Sesler[0].Play();
        _VeriYonetimi.Save(_ItemBilgileri);
        SceneManager.LoadScene(0);
    }

    void SatinAlmaSonuc(int Index)
    {
        _ItemBilgileri[Index].SatinAlmaDurumu = true;
        _BellekYonetim.VeriKaydet_Int("Puan", _BellekYonetim.VeriOku_i("Puan") - int.Parse(_ItemBilgileri[Index].Puan));
        SatinAlmaText.text = "Satin Al";
        islemButtonlari[0].interactable = false;
        islemButtonlari[1].interactable = true;
        PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();
    }

    void KaydetmeSonuc(string key, int Index)
    {
        _BellekYonetim.VeriKaydet_Int(key, Index);
        islemButtonlari[1].interactable = false;
        if (!_Animator.GetBool("ok")) /*Bool durumunu sorgula.*/
        {
            _Animator.SetBool("ok", true);
        }
    }

    public void DilTercihiYonetim()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR") /*Hangi dil seceneginin o an aktif olduguna bakip, ona gore dil ayarlarini yap.*/
        {
            for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }

            SatinAlmaTextDY = _DilVerileriAnaObje[0]._DilVerileri_TR[5].Metin;
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }

            SatinAlmaTextDY = _DilVerileriAnaObje[0]._DilVerileri_EN[5].Metin;
        }
    }
}
