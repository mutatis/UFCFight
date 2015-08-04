using UnityEngine;
using System.Collections;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource audios;

    public AudioClip[] socoVento;
    public AudioClip[] soco;

    public AudioClip socoF;
    public AudioClip carrega;
    public AudioClip sprawl;
    public AudioClip esquiva;

    bool isCarrega;

    void Update()
    {
        if (MovmentPlayer.player.velX > 0 && !audios.isPlaying)
        {
            audios.Play();
        }
        else if (MovmentPlayer.player.velX == 0)
        {
            audios.Stop();
        }

        if(MovmentPlayer.player.isEsquiva)
        {
            AudioSource.PlayClipAtPoint(esquiva, transform.position, 1f);
            MovmentPlayer.player.isEsquiva = false;
        }
    }

    //som de soco
    public void Soco()
    {
        print(MovmentPlayer.player.obj);
        if (MovmentPlayer.player.obj != null)
        {
            AudioSource.PlayClipAtPoint(soco[Random.Range(0, soco.Length)], transform.position, 1f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(socoVento[Random.Range(0, socoVento.Length)], transform.position, 1f);
        }
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
