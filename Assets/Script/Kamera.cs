using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target; /*Takip edilecek nesne.*/
    public Vector3 targetOffset;
    public bool SonaGeldikmi;
    public GameObject Gidecegiyer;

    void Start()
    {
        targetOffset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, .125f);

        if (!SonaGeldikmi)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, .125f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Gidecegiyer.transform.position, .015f);
        }
    }
}
