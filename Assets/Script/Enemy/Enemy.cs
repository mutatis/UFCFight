using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Animator anim;

	public float velX = -1.5f;
	//distancia para socar ou defender
	public float distanciaSD;
	public float distanciaSprawl;
	public float distFight;

	public int escolha;

	int selectSprawl;

	float dist;
	float temp;

	bool fight;
	bool sprawl;

	void Start()
	{
		temp = velX;
		anim.SetFloat ("VelX", velX);
		escolha = Random.Range (0, 2);
		if(escolha == 0)
		{
			selectSprawl = 0;
				//Random.Range(0, 4);
		}
	}

	void Update()
	{
		dist = Vector3.Distance(MovmentPlayer.player.transform.position, transform.position);

		if(escolha == 0)
		{

			switch(selectSprawl)
			{
				case 0:
					if(dist > distanciaSprawl - 1)
					{
						velX = 0;
						anim.SetFloat("velX", velX);
						anim.SetTrigger("Idle");
					}
					if(dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
					{
						fight = true;
						velX = temp;
						transform.Translate(velX, 0, 0);
						anim.SetFloat("velX", velX);
						anim.SetTrigger("Sprawl");
					}
					else if(dist <= distanciaSD)
					{
						Combat();
						sprawl = true;
						velX = 0;
					}
				break;

				case 1:
				if(dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
				{
					fight = true;
					velX = temp;
					transform.Translate(velX, 0, 0);
					anim.SetFloat("velX", velX);
					anim.SetTrigger("Run");
				}
				else if(dist <= distanciaSD)
				{
					Combat();
					sprawl = true;
					velX = 0;
				}
				break;
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