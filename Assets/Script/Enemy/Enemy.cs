using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Animator anim;

	public float velX = -1.5f;
	public float distancia;

	float dist;
	float temp;

	bool fight;

	void Start()
	{
		temp = velX;
		anim.SetFloat ("VelX", velX);
	}

	void Update()
	{
		dist = Vector3.Distance(MovmentPlayer.player.transform.position, transform.position);

		if(dist <= distancia && !fight)
		{
			velX = 0;
			anim.SetFloat("velX", velX);
			anim.SetTrigger("Idle");
			Combat ();
		}

		if(dist > distancia && !fight)
		{
			velX = temp;
			transform.Translate(velX * Time.deltaTime, 0, 0);
		}
		else
		{
			velX = 0;
		}
	}

	void Combat()
	{
		fight = true;
		MovmentPlayer.player.StopPlayer ();
	}
}
