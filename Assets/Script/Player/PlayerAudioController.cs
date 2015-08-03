using UnityEngine;
using System.Collections;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip soco;
    public AudioClip socoF;
    public AudioClip carrega;

    bool isCarrega;

    //som de soco
    public void Soco()
    {
        AudioSource.PlayClipAtPoint(soco, transform.position, 1f);
        isCarrega = false;
    }

    //som de socoForte
    public void SocoForte()
    {
        AudioSource.PlayClipAtPoint(socoF, transform.position, 1f);
        isCarrega = false;
    }
    
    //som quandoc arrega soco forte
    public void Carrega()
    {
        if(!isCarrega)
        {
            AudioSource.PlayClipAtPoint(carrega, transform.position, 1f);
            isCarrega = true;
        }
    }
}
