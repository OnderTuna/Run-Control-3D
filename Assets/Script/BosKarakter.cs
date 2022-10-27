using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer; /*�arp��ma an�nda rengi de�i�sin istiyoruz.*/
    public Material AtananOlacakMateryal;
    public NavMeshAgent _NavMesh;
    public Animator _Animator;
    public GameObject Target;
    bool TemasVar;
    public GameManager _Gamemanager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(TemasVar)
        {
            _NavMesh.SetDestination(Target.transform.position);
        }
    }

    void MateryalDegistirveAnimasyonTetikle()
    {
        Material[] mats = _Renderer.materials; /*Karakterde birden fazla materyal olabilir. Bu y�zden dizi.*/
        mats[0] = AtananOlacakMateryal;
        _Renderer.materials = mats;
        _Animator.SetBool("Saldir", true);
        gameObject.tag = "AltKarakterler";
        GameManager.AnlikKarakterSayisi++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("AltKarakterler") || other.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("BosKarakter")) /*Taglarda çarpışma oluyor. Bu yüzden sorgula. Yoksa hata. Objenin kendi ile etkileşime girmesini engelle.*/
            {
                MateryalDegistirveAnimasyonTetikle();
                TemasVar = true;
            }
        }
        else if (other.gameObject.CompareTag("Dusman"))
        {
            _Gamemanager.YokOlmaEfektiOlustur(transform, false);
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("IgneliKutu"))
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
        else if (other.gameObject.CompareTag("PervaneIgneler"))
        {
            _Gamemanager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Balyoz"))
        {
            _Gamemanager.AdamLekesiEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
    }
}
