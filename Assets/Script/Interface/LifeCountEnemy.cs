using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeCountEnemy : MonoBehaviour
{
	public Text text;
	
	void Update ()
	{
		//mostra a vida do inimigo que vc ganho
		text.text = "Enemy Life: " + MovmentPlayer.player. lifeEnemy.ToString();
	}
}