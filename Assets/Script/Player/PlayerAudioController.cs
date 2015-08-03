using UnityEngine;
using System.Collections;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip soco;
    public AudioClip socoF;

    //som de soco
    public void Soco()
    {
        AudioSource.PlayClipAtPoint(soco, transform.position, 1f);
    }

    //som de socoForte
    public void SocoForte()
    {
        AudioSource.PlayClipAtPoint(socoF, transform.position, 1f);
    }
}
