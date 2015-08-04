using UnityEngine;
using System.Collections;

public class SomBotao : MonoBehaviour
{
    public void Click(AudioClip click)
    {
        AudioSource.PlayClipAtPoint(click, transform.position, 1f);
    }
}