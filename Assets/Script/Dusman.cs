using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public GameObject Saldiri_Hedefi;
    //NavMeshAgent _NavMesh;
    public NavMeshAgent _NavMesh;
    public Animator _Animator;
    public GameManager _Gamemanager;
    bool Saldiri_Basladimi;

    void Start()
    {
        //_NavMesh = GetComponent<NavMeshAgent>();
    }

    public void AnimasyonTetikle()
    {
        _Animator.SetBool("Saldir", true);
        Saldiri_Basladimi = true;
    }

    void LateUpdate()
    {
        if(Saldiri_Basladimi)
        _NavMesh.SetDestination(Saldiri_Hedefi.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AltKarakterler"))
        {
            //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfektiOlustur(transform, true);
            _Gamemanager.YokOlmaEfektiOlustur(transform, true);
            gameObject.SetActive(false);
        }
    }
}
