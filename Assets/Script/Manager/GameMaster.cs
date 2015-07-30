using UnityEngine;
using System.Collections;

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
