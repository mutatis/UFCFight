using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeCountEnemy : MonoBehaviour
{
	public Text text;
	
	void Update ()
	{
		text.text = "Life Enemy: " + MovmentPlayer.player. lifeEnemy.ToString();
	}
}