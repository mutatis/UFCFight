using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeCountEnemy : MonoBehaviour
{
	public Text text;
	
	void Update ()
	{
		text.text = "Enemy Life: " + MovmentPlayer.player. lifeEnemy.ToString();
	}
}