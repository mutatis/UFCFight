using UnityEngine;
using System.Collections;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource audios;

    public GameObject carrega;

    public AudioClip[] socoVento;
    public AudioClip[] soco;
    public AudioClip[] grito;

    public AudioClip chute;
    public AudioClip gritoChute;
    public AudioClip blockEnemy;
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

        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            Destroy(GameObject.FindGameObjectWithTag("Carrega"));
        }
    }

    //inimigo defendeu
    public void BlockEnemy()
    {
        AudioSource.PlayClipAtPoint(blockEnemy, transform.position, 1f);
    }

    //som de soco
    public void Soco()
    {
        if (MovmentPlayer.player.obj != null)
        {
            AudioSource.PlayClipAtPoint(soco[Random.Range(0, soco.Length)], transform.position, 1f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(socoVento[Random.Range(0, socoVento.Length)], transform.position, 1f);
        }
        AudioSource.PlayClipAtPoint(grito[Random.Range(0, grito.Length)], transform.position, 0.3f);
        isCarrega = false;
    }

    //som de socoForte
    public void SocoForte()
    {
        Destroy(GameObject.FindGameObjectWithTag("Carrega"));
        if (MovmentPlayer.player.obj != null)
        {
            AudioSource.PlayClipAtPoint(chute, transform.position, 1f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(socoVento[Random.Range(0, socoVento.Length)], transform.position, 1f);
        }
        AudioSource.PlayClipAtPoint(gritoChute, transform.position, 0.3f);
        isCarrega = false;
    }
    
    //som quando carrega soco forte
    public void Carrega()
    {
        if(!isCarrega)
        {
            Instantiate(carrega);
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
