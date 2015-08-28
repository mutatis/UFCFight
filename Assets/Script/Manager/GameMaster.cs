using UnityEngine;
using System.Collections;

/* Codigo q toma conta do estado do jogo, dinheiro etc */

public class GameMaster : MonoBehaviour 
{
	public static GameMaster master;

	[HideInInspector]
	public int vitorias;

	void Awake()
	{
		master = this;
	}
}
