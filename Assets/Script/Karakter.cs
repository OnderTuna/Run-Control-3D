using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public Kamera _Kamera;
    public bool SonaGeldikmi;
    public GameObject Gidecegiyer;
    public Slider _Slider;
    public GameObject GecisNoktasi;

    private void Start()
    {
        float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
        _Slider.maxValue = fark;
    }

    private void FixedUpdate()
    {
        //transform.Translate(.5f * Time.deltaTime * Vector3.forward);

        if (!SonaGeldikmi)
        {
            transform.Translate(.5f * Time.deltaTime * Vector3.forward);
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0)) /*Mouseun ekranýn saðýnda mý solunda mý olduðunu kontrol et.*/
        //{
        //    if (Input.GetAxis("Mouse X") < 0)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f); /*New vector kýsmý mouse pos. Karakteri saða sola çekcez.*/
        //    }
        //    if (Input.GetAxis("Mouse X") > 0)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
        //    }
        //}

        if (SonaGeldikmi)
        {
            transform.position = Vector3.Lerp(transform.position, Gidecegiyer.transform.position, .005f);
            if(_Slider.value != 0)
            {
                _Slider.value -= 0.01f;
            }
        }
        else
        {
            float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
            _Slider.value = fark;

            if(Time.timeScale != 0)
            {
                if (Input.GetKey(KeyCode.Mouse0)) /*Mouseun ekranýn saðýnda mý solunda mý olduðunu kontrol et.*/
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f); /*New vector kýsmý mouse pos. Karakteri saða sola çekcez.*/
                    }
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                    }
                }
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.name == "x2" || other.name == "+3" || other.name == "-4" || other.name == "/2") /*Etkileþimi kontrol et.*/
        //{
        //    _GameManager.AdamYonetimi(other.name, other.transform); /*Çarptýðýmýz objenin konumunda alt karakterleri oluþtur.*/
        //}

        if (other.gameObject.CompareTag("Toplama") || other.gameObject.CompareTag("Cikarma") || other.gameObject.CompareTag("Carpma") || other.gameObject.CompareTag("Bolme")) /*Etkileþimi kontrol et.*/
        {
            int sayi = int.Parse(other.name);
            _GameManager.AdamYonetimi(other.tag, sayi, other.transform); /*Çarptýðýmýz objenin konumunda alt karakterleri oluþtur.*/
        }
        else if (other.gameObject.CompareTag("Sontetikleyici"))
        {
            _Kamera.SonaGeldikmi = true;
            _GameManager.DusmanTetikle();
            SonaGeldikmi = true;
        }
        else if (other.gameObject.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject); /*Çarptýðýmýz nesneyi listeye ekle.*/
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk")) /*Karakter takýldýðý an onu saða sola yolla.*/
        {
            if(transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
            }
        }
    }
}
