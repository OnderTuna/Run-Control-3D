using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Onder;
using UnityEngine.UI;

public class AnaMenu_Manager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    public GameObject CikisPaneli;
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    public AudioSource ButonSes;
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    public Text[] TextObjeleri; /*Diller arasi gecis yapilacak metin textleri listelendi.*/

    public List<DilVerileriAnaObje> _DilVerileriYanObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public GameObject YuklemePaneli;
    public Slider YuklemeSlider;

    void Start()
    {
        _BellekYonetim.KontrolEtveTanýmla(); /*Oyun ilk bu sahnede acildigi icin bu sahnede calistir.*/
        _VeriYonetimi.IlkKurulumDosyaOlusturma(_ItemBilgileri, _DilVerileriAnaObje);
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");

        //Debug.Log(_DilVerileriAnaObje[0]._DilVerileri_TR[3].Metin);

        //if (_BellekYonetim.VeriOku_s("Dil") == "TR") /*Hangi dil seceneginin o an aktif olduguna bakip, ona gore dil ayarlarini yap.*/
        //{
        //    for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
        //    {
        //        TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < TextObjeleri.Length; i++) /*Text objelerimizi iceren metinlere, degerleri tasiyacagiz.*/
        //    {
        //        TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
        //    }
        //}

        _VeriYonetimi.DilLoad(); //Veri oku.
        //_DilVerileriYanObje = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilVerileriYanObje.Add(_DilOkunanVeriler[0]); //Ana menu oldugu icin sifirinci indisi ekle.
        DilTercihiYonetim();
    }

    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }

    public void Oyna()
    {
        ButonSes.Play();
        //SceneManager.LoadScene(_BellekYonetim.VeriOku_i("SonLevel"));
        StartCoroutine(LoadAsync(_BellekYonetim.VeriOku_i("SonLevel")));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        YuklemePaneli.SetActive(true);
        while (!op.isDone)
        {
            YuklemeSlider.value = op.progress;
            yield return null;
        }
    }

    public void Cikis()
    {
        ButonSes.Play();
        CikisPaneli.SetActive(true);
    }

    public void CikisButonIslem(string durum)
    {
        ButonSes.Play();
        if (durum == "Evet")
        {
            Application.Quit();
        }
        else
        {
            CikisPaneli.SetActive(false);
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
