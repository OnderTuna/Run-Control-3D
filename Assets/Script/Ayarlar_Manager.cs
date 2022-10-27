using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Onder;
using TMPro;

public class Ayarlar_Manager : MonoBehaviour
{
    public AudioSource ButonSes;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    [Header("Dil Panel")]
    public TextMeshProUGUI DilText;
    public Button[] DilButton;
#pragma warning disable IDE0052 // Remove unread private members
    int AktifDilIndex;
#pragma warning restore IDE0052 // Remove unread private members

    void Start()
    {
        MenuSes.value = _BellekYonetim.VeriOku_f("MenuSes"); /*Sistemden en son kaydedilen degerleri oku.*/
        MenuFx.value = _BellekYonetim.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_f("OyunSes");
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");

        _VeriYonetimi.DilLoad(); //Veri oku.
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[4]);
        DilTercihiYonetim();
        DilDurumuKontrol();
    }

    void Update()
    {

    }

    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }

    void DilDurumuKontrol()
    {
        if(_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            AktifDilIndex = 0;
            DilText.text = "Turkce";
            DilButton[0].interactable = false;
        }
        else
        {
            AktifDilIndex = 1;
            DilText.text = "English";
            DilButton[1].interactable = false;
        }
    }

    public void DilDegistir(string durum)
    {
        ButonSes.Play();

        if (durum == "ileri")
        {
            AktifDilIndex = 1;
            DilText.text = "English";
            DilButton[1].interactable = false;
            DilButton[0].interactable = true;
            _BellekYonetim.VeriKaydet_String("Dil", "EN");
            DilTercihiYonetim();

        }
        else
        {
            AktifDilIndex = 0;
            DilText.text = "Turkce";
            DilButton[0].interactable = false;
            DilButton[1].interactable = true;
            _BellekYonetim.VeriKaydet_String("Dil", "TR");
            DilTercihiYonetim();
        }
    }

    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "MenuSes":
                //Debug.Log("Menu ses degeri:" + MenuSes.value);
                _BellekYonetim.VeriKaydet_Float("MenuSes", MenuSes.value);
                break;
            case "MenuFx":
                _BellekYonetim.VeriKaydet_Float("MenuFx", MenuFx.value);
                break;
            case "OyunSes":
                _BellekYonetim.VeriKaydet_Float("OyunSes", OyunSes.value);
                break;
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
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }
}
