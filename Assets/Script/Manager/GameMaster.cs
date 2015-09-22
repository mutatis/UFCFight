using UnityEngine;
using System.Collections;

/* Codigo q toma conta do estado do jogo, dinheiro etc */

public class GameMaster : MonoBehaviour 
{
	public static GameMaster master;
    public AudioSource audio;

	[HideInInspector]
	public int vitorias;

	void Awake()
	{
		master = this;
	}

    void Update()
    {
        if (Time.timeScale == 0)
        {
            audio.volume = 0.3f;
        }
        else
        {
            audio.volume = 1;
        }
    }
}
