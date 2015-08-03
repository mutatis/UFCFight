using UnityEngine;
using System.Collections;

public class EnemyAudioController : MonoBehaviour
{
    public AudioClip takedown;

    bool tocaT;

    public void Takedown()
    {
        if (!tocaT)
        {
            AudioSource.PlayClipAtPoint(takedown, transform.position, 1f);
            tocaT = true;
        }
    }
}
