using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trappe : MonoBehaviour
{
    private Animator fermeture;
    // Start is called before the first frame update
    void Start()
    {
        fermeture = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        FadeNActive.Fermeture += FinIntro;
    }

    private void OnDisable()
    {
        FadeNActive.Fermeture -= FinIntro;
    }

    // Update is called once per frame
    private void FinIntro()
    {
        fermeture.SetTrigger("Trappe");
    }
}
