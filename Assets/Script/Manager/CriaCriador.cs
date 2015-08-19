using UnityEngine;
using System.Collections;

public class CriaCriador : MonoBehaviour 
{
	public SpriteRowCreator creator;

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			creator.CreateSprites();
		}
	}
}
