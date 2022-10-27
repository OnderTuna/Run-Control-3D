using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AltKarakter : MonoBehaviour
{
    //GameObject Target;
    NavMeshAgent _NavMesh;
    //GameManager _Gamemanager;
    public GameManager _Gamemanager;
    public GameObject Target;

    void Start()
    {
        _NavMesh = GetComponent<NavMeshAgent>();
        //Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().VarisNoktasi;
        //_Gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void LateUpdate()
    {
        _NavMesh.SetDestination(Target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IgneliKutu"))
        {
            _Gamemanager.YokOlmaEfektiOlustur(transform);
            //GameManager.AnlikKarakterSayisi--;
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Testere"))
        {
            _Gamemanager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("PervaneIgneler"))
        {
            _Gamemanager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Balyoz"))
        {
            _Gamemanager.AdamLekesiEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Dusman"))
        {
            _Gamemanager.YokOlmaEfektiOlustur(transform, false);
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("BosKarakter"))
        {
            _Gamemanager.Karakterler.Add(other.gameObject); /*Çarptýðýmýz nesneyi listeye ekle.*/
        }
    }
}
