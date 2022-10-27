using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance; /*Sitemde dolasacak bu sebeple static. Bu sayede olusturulup olusturulmad�g�n� anla.*/
    AudioSource Ses;

    void Start()
    {
        Ses = GetComponent<AudioSource>();
        Ses.volume = PlayerPrefs.GetFloat("MenuSes");
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        Ses.volume = PlayerPrefs.GetFloat("MenuSes");
    }
}
