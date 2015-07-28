using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Animator anim;

	public Rigidbody2D rig;

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

	bool anda;
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
	
		if(rig.velocity.x > 0)
		{
			rig.velocity = new Vector2((rig.velocity.x - 0.05f), 0);
		}
		else
		{
			rig.velocity = new Vector2(0, 0);
		}

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
						if(!MovmentPlayer.player.esquiva)
						{
							Attack();
						}
						else
						{
							anim.SetTrigger("Idle");
							Combat();
							sprawl = true;
							velX = 0;
						}
					}
					else
					{
						velX = temp;					
						anim.SetFloat ("VelX", temp);
						anim.SetTrigger("Run");
						transform.Translate(velX * Time.deltaTime, 0, 0);
					}
				break;

				case 1:
				//Corre pra frente e ataca ao se aproxima sem da sprawl;
					if(dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
					{
						fight = true;
						velX = temp;
						transform.Translate(velX * Time.deltaTime, 0, 0);
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

		if(escolha == 1)
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
		ReCombat ();
		rig.velocity = new Vector2 (4, 0);
	}

	public void Attack()
	{
		ReCombat ();
		MovmentPlayer.player.life -= 1;
		MovmentPlayer.player.rig.velocity = new Vector2(-4, 0);
	}

	IEnumerator SelectAttack()
	{
		yield return new WaitForSeconds (1);
		if(obj != null)
		{
			player = obj.GetComponent<MovmentPlayer> ();
		}
		intervalo = false;
//		if(player.prepareAttack == true)
	//	{
	//		//escolhe o ataque 
	//	}
		selectAttack = Random.Range (0, 3);
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
					anim.SetTrigger("PAttack");
					yield return new WaitForSeconds(1f);
					Attack ();
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

	void ReCombat()
	{
		anim.SetFloat ("VelX", temp);
		anim.SetTrigger("Run");
		anda = true;
		sprawl = false;
		selectSprawl = 1;
		escolha = 0;
		if(para)
		{
			StopCoroutine("SelectAttack");
			MovmentPlayer.player.ReturnPlayerMov ();
			para = false;
		}
		fight = false;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			obj = collision.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			obj = null;
		}
	}
}