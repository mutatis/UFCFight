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
	public int life;

	[HideInInspector]
	public int selectAttack;

	MovmentPlayer player;

	GameObject obj;

	int selectSprawl;

	float dist;
	float temp;

	bool fight;
	bool sprawl;
	bool para;
	bool intervalo;

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
		else
		{
			selectAttack = 0;
				//Random.Range(0, 2);
		}
	}

	void Update()
	{
		dist = Vector3.Distance(MovmentPlayer.player.transform.position, transform.position);

		if(life <= 0)
		{
			Kill();
		}

		if(escolha == 0)
		{

			switch(selectSprawl)
			{
				case 0:
				//Vai da Sprawl;
					if(dist > (distanciaSprawl - 1) && dist < (distanciaSprawl + 1))
					{
						print("paro");
						velX = 0;
						anim.SetFloat("VelX", velX);
						anim.SetTrigger("Idle");
					}
					else if(dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
					{
						fight = true;
						velX = temp;
						transform.Translate((velX * 10) * Time.deltaTime, 0, 0);
						anim.SetFloat("VelX", velX);
						anim.SetTrigger("Sprawl");
					}
					else if(dist <= distanciaSD)
					{
						anim.SetTrigger("Idle");
						Combat();
						sprawl = true;
						velX = 0;
					}
					else
					{
						velX = temp;
						transform.Translate(velX * Time.deltaTime, 0, 0);
					}
				break;

				case 1:
				//Corre pra frente e ataca ao se aproxima sem da sprawl;
					if(dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
					{
						fight = true;
						velX = temp;
						transform.Translate(velX, 0, 0);
						anim.SetFloat("VelX", velX);
						anim.SetTrigger("Run");
					}
					else if(dist <= distanciaSD)
					{
						Combat();
						sprawl = true;
						velX = 0;
					}
				break;

				case 2:
				//fake sprawl;
				break;

				default:
				//sei la
				break;
			}
		}
		else if(escolha == 1)
		{
			switch(selectAttack)
			{
				case 0:
					if(dist <= distanciaSD && !fight)
					{
						velX = 0;
						anim.SetFloat("VelX", velX);
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
				break;

				case 1:
					if(!intervalo)
					{
						anim.SetTrigger("Attack");
						intervalo = true;
					}
				break;

				case 2:
					if(!intervalo)
					{
						anim.SetTrigger("Defesa");
						intervalo = true;
					}
				break;
			}

		}
		if(fight)
		{
			velX = 0;
		}
	}

	public void Dano()
	{
		anim.SetTrigger("Dano");
		StopCoroutine("SelectAttack");
		StartCoroutine ("SelectAttack");
	}

	public void Attack()
	{
		MovmentPlayer.player.life -= 1;
	}

	IEnumerator SelectAttack()
	{
		yield return new WaitForSeconds (1);
		player = obj.GetComponent<MovmentPlayer> ();
		intervalo = false;
		if(player.prepareAttack == true)
		{
			//escolhe o ataque 
		}
		selectAttack = Random.Range (0, 3);
		StartCoroutine("SelectAttack");
	}

	void Kill()
	{
		fight = false;
		MovmentPlayer.player.ReturnPlayerMov ();
		Destroy (gameObject);
	}

	void Combat()
	{
		escolha = 1;
		if(!para)
		{
			StartCoroutine("SelectAttack");
			MovmentPlayer.player.StopPlayer ();
			para = true;
		}
		fight = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			obj = collision.gameObject;
		}
	}
}