using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Animator anim;

	public float velX = -1.5f;
	//distancia para socar ou defender
	public float distanciaSD;
	public float distanciaSprawl;

	public int escolha;

	float dist;
	float temp;

	bool fight;

	void Start()
	{
		temp = velX;
		anim.SetFloat ("VelX", velX);
		escolha = Random.Range (0, 2);
	}

	void Update()
	{
		dist = Vector3.Distance(MovmentPlayer.player.transform.position, transform.position);

		if(escolha == 0)
		{
			if(dist <= distanciaSprawl)
			{
				//sprawl
				fight = true;
				velX = 0;
				anim.SetFloat("velX", velX);
				anim.SetTrigger("Idle");
			}
			else
			{
				velX = temp;
				transform.Translate(velX * Time.deltaTime, 0, 0);
			}
		}

		if(escolha == 1)
		{

			if(dist <= distanciaSD && !fight)
			{
				velX = 0;
				anim.SetFloat("velX", velX);
				anim.SetTrigger("Idle");
				Combat ();
			}

			if(dist > distanciaSD && !fight)
			{
				velX = temp;
				transform.Translate(velX * Time.deltaTime, 0, 0);
			}
			else
			{
				velX = 0;
			}
		}
		if(fight)
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