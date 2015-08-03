using UnityEngine;
using System.Collections;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip soco;
    public AudioClip socoF;
    public AudioClip carrega;
    public AudioClip sprawl;

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
    
    //som quando carrega soco forte
    public void Carrega()
    {
        if(!isCarrega)
        {
            AudioSource.PlayClipAtPoint(carrega, transform.position, 1f);
            isCarrega = true;
        }
    }

    //som do sprawl
    public void Sprawl()
    {
        if (MovmentPlayer.player.esquiva)
        {
            AudioSource.PlayClipAtPoint(sprawl, transform.position, 1f);
        }
    }
}
