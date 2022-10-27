using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Onder;

public class Level_Manager : MonoBehaviour
{
    public Button[] Butonlar;
    //public int Level;
    public Sprite KilitButon;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    public AudioSource ButonSes;
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public Text TextObjeleri;
    public GameObject YuklemePaneli;
    public Slider YuklemeSlider;

    void Start()
    {
        //_BellekYonetim.VeriKaydet_Int("SonLevel", Level);
        int mevcutLevel = _BellekYonetim.VeriOku_i("SonLevel") - 4; /*Mevcut levele ulaþmak için baþlangýç levelinin indexini kullandýk. Yani 5 olan indexten 4 çýkararak ilk level için mevcut level deðerini 1 yaptýk.*/
        int Index = 1;

        for (int i = 0; i < Butonlar.Length; i++) /*Butonlara eriþ.*/
        {
            if (Index <= mevcutLevel) /*Hangi levellar kapalý hangileri açýk olsun.*/
            {
                Butonlar[i].GetComponentInChildren<Text>().text = Index.ToString();
                //int Index = i + 1;
                int SahneIndex = Index + 4;
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(SahneIndex); }); /*Listener ile butonlar id taþýyor mu.*/
            }
            else
            {
                Butonlar[i].GetComponentInChildren<Image>().sprite = KilitButon;
                //Butonlar[i].interactable = false;
                Butonlar[i].enabled = false;
            }
            Index++;
        }

        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");

        _VeriYonetimi.DilLoad(); //Veri oku.
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeAktar(); //Okudugumuz verileri aktar.
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[2]);
        DilTercihiYonetim();
    }

    public void GeriDon(int index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(index);
    }

    public void SahneYukle(int index)
    {
        ButonSes.Play();
        //SceneManager.LoadScene(index);
        StartCoroutine(LoadAsync(index));
    }

    public void DilTercihiYonetim()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR") /*Hangi dil seceneginin o an aktif olduguna bakip, ona gore dil ayarlarini yap.*/
        {
            TextObjeleri.text = _DilVerileriAnaObje[0]._DilVerileri_TR[0].Metin;
        }
        else
        {
            TextObjeleri.text = _DilVerileriAnaObje[0]._DilVerileri_EN[0].Metin;
        }
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
}
