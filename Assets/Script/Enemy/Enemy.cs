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
	public int selectAttack = 1;

	public GameObject obj;

	MovmentPlayer player;

	public int selectSprawl;
    public int random;

    float dist;	
	float temp;
    float distTemp;

    int cont;
    int numAttack;

	bool primeiro;
	bool anda;
	bool fight;
	bool sprawl;
	bool para;
	bool intervalo;
    bool atacatroll;
    bool dano;

	void Start()
	{
        temp = velX;
		anim.SetFloat ("VelX", velX);
        escolha = Random.Range (0, 2);
		if(escolha == 0)
		{
            selectSprawl = Random.Range(0, 3);
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
            anim.SetBool("Kill", true);
        }

       /* if(obj != null)
        {
            velX = 0;
        }*/

        if(selectAttack == 2)
        {
            if (player.prepareAttack == true)
            {
                //escolhe o ataque
                atacatroll = true;
                int prob = Random.Range(0, 10);
                if (prob == 3)
                {
                   selectAttack = 1;
                    Attack();
                    anim.SetTrigger("Attack");
                    intervalo = true;
                }
                atacatroll = false;
            }
        }

        if(cont >= 1 && dist >= distanciaSD || (obj == null && escolha != 0 && selectSprawl != 0))
        {
            velX = temp;
            anim.SetFloat("VelX", temp);
            anim.SetTrigger("Run");
            transform.Translate(velX * Time.deltaTime, 0, 0);
        }
        else
        {
            anim.SetFloat("VelX", 0);
        }

		if(dist <= (distanciaSD + 0.5f))
		{

        }
        else if (cont > 0)
        {
            MovmentPlayer.player.ReturnPlayerMov();
        }

        if (rig.velocity.x > 0)
		{
			rig.velocity = new Vector2((rig.velocity.x - 0.05f), 0);
		}
		else
		{
			rig.velocity = new Vector2(0, 0);
		}

        if (!dano)
        {
            if (escolha == 0)
            {

                switch (selectSprawl)
                {
                    case 0:
                        //Vai da Takedown;
                        if (dist > (distanciaSprawl - 1) && dist < (distanciaSprawl + 1))
                        {
                            velX = 0;
                            anim.SetFloat("VelX", 0);
                            anim.SetTrigger("Idle");
                        }
                        else if (dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
                        {
                            fight = true;
                            velX = temp;
                            transform.Translate((velX * 10) * Time.deltaTime, 0, 0);
                            anim.SetFloat("VelX", velX);
                            anim.SetTrigger("Sprawl");
                        }
                        else if (dist <= distanciaSD)
                        {
                            if (!MovmentPlayer.player.esquiva)
                            {
                                Sprawl();
                            }
                            else
                            {
                                MovmentPlayer.player.isEsquiva = true;
                                MovmentPlayer.player.Esquivei();
                                anim.SetFloat("VelX", 0);
                                anim.SetTrigger("Idle");
                                Combat();
                                sprawl = true;
                                velX = 0;
                            }
                        }
                        else
                        {
                            velX = temp;
                            anim.SetFloat("VelX", temp);
                            anim.SetTrigger("Run");
                            transform.Translate(velX * Time.deltaTime, 0, 0);
                        }
						if(numAttack != 3)
							numAttack = 3;
                        break;

                    case 1:
                        //Corre pra frente e ataca ao se aproxima sem da takedown;
                        if (dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
                        {
                            fight = true;
                            velX = temp;
                            transform.Translate(velX * Time.deltaTime, 0, 0);
                            anim.SetFloat("VelX", velX);
                            anim.SetTrigger("Run");
                        }
                        else if (dist <= distanciaSD)
                        {
                            Combat();
                            sprawl = true;
                            velX = 0;
                        }
                        break;

                    case 2:
                        //fake takedown;
                        //prepara o takedown;
                        if (dist > (distanciaSprawl - 1.5f) && dist < (distanciaSprawl + 1.5f))
                        {
                            velX = 0;
                            anim.SetFloat("VelX", 0);
                            anim.SetTrigger("Idle");
                        }
                        else if (dist < (distanciaSprawl - 1.5f) && random == 0)
                        {
                            random = Random.Range(1, 4);
                        }
                        if (random == 1)
                        {
                            //anda normal
                            if (dist <= distanciaSprawl && dist >= distanciaSD && !sprawl)
                            {
                                fight = true;
                                velX = temp;
                                transform.Translate(velX * Time.deltaTime, 0, 0);
                                anim.SetFloat("VelX", velX);
                                anim.SetTrigger("Run");
                            }
                            //para;
                            else if (dist <= distanciaSD)
                            {
                                MovmentPlayer.player.Esquivei();
                                anim.SetTrigger("Run");
                                Combat();
                                sprawl = true;
                                velX = 0;

                            }
                            else
                            {
                                velX = temp;
                                anim.SetFloat("VelX", temp);
                                anim.SetTrigger("Run");
                                transform.Translate(velX * Time.deltaTime, 0, 0);
                            }
                        }
                        else if (random == 2)
                        {
                            if (dist > (distanciaSprawl - 1.5f) && dist < (distanciaSprawl + 1.5f))
                            {
                                velX = 0;
                                anim.SetFloat("VelX", 0);
                                anim.SetTrigger("Idle");
                            }
                            else
                            {
                                random = Random.Range(1, 4);
                            }
                        }
                        else if (random == 3)
                        {
                            selectSprawl = 0;
                        }
                        break;

                    default:
                        //sei la
                        break;
                }
            }

            if (escolha == 1)
            {
                switch (selectAttack)
                {
                    case 0:
                        if (dist <= distanciaSD && !fight)
                        {
                            velX = 0;
                            anim.SetFloat("VelX", 0);
                            anim.SetTrigger("Idle");
                            Combat();
                        }

                        if (dist > distanciaSD && !fight)
                        {
                            velX = temp;
                            transform.Translate(velX * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            velX = 0;
                        }
                        break;
                    case 3:
                        if (dist <= distanciaSD && !fight)
                        {
                            velX = 0;
                            anim.SetFloat("VelX", 0);
                            anim.SetTrigger("Idle");
                            Combat();
                        }
                        break;
                }
            }
        }

		if(fight)
		{
			velX = 0;
		}
	}
	//tomo chute e fico tonto
    public void Stun()
    {
        anim.SetTrigger("Dano");
        StopCoroutine("SelectAttack");
        StartCoroutine("SelectAttack");
        ReCombat();
    }
	//acerto o takedown
    void Sprawl()
    {
        MovmentPlayer.player.life = 0;
    }
	//tomo dano
	public void Dano()
	{
		anim.SetTrigger("Dano");
		StopCoroutine("SelectAttack");
		StartCoroutine ("SelectAttack");
        escolha = 1;
		ReCombat ();
		rig.velocity = new Vector2 (6, 0);
	}
	//acerto o soco
	public void Attack()
	{
		ReCombat ();
		if(obj != null)
		{
            MovmentPlayer.player.isAttack = true;
            MovmentPlayer.player.Dano();
			MovmentPlayer.player.life -= 1;
			MovmentPlayer.player.rig.velocity = new Vector2(-4, 0);
		}
	}
	//escolhe oq vai fazer (soco, defesa, midtakedown)
	IEnumerator SelectAttack()
	{
		if(velX == 0)
		{
			if(!primeiro)
			{
				primeiro = true;
			}
			else
			{
				yield return new WaitForSeconds (1);
			}
			if(obj != null)
			{
				player = obj.GetComponent<MovmentPlayer> ();
			}

			intervalo = false;

            selectAttack = Random.Range(0, 4);

			switch(selectAttack)
			{
				case 0:
                    //nao faz nada
					if(dist <= distanciaSD && !fight)
					{
						velX = 0;
						anim.SetFloat("VelX", 0);
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
					if(numAttack != 4)
						numAttack = 4;
                    StartCoroutine("SelectAttack");
                    break;
					
				case 1:
                    //da soco
					if(!intervalo)
					{
					    //anim.SetTrigger("PAttack");
						//yield return new WaitForSeconds(1f);
						Attack ();
						anim.SetTrigger("Attack");
						intervalo = true;
					}
                    if (dist <= distanciaSD && !fight)
                    {
                        velX = 0;
                        anim.SetFloat("VelX", 0);
                        Combat();
                    }
					if (numAttack != 4)
						numAttack = 4;
                    StartCoroutine("SelectAttack");
                    break;
					
				case 2:
                    //defende
					if(!intervalo)
					{
						anim.SetTrigger("Defesa");
						intervalo = true;
                    }
                    if (dist <= distanciaSD && !fight)
                    {
                        velX = 0;
                        anim.SetFloat("VelX", 0);
                        Combat();
                    }
					if (numAttack != 4)
						numAttack = 4;
                    StartCoroutine("SelectAttack");
                    break;

                case 3:
                    //mid takedown
                    rig.velocity = new Vector2(10, 0);
                    yield return new WaitForSeconds(0.5f);
                    rig.velocity = new Vector2(0, 0);
                    sprawl = false;
                    escolha = 0;
                    velX = temp;
                    //anim.SetTrigger("Sprawl");
                    anim.SetFloat("VelX", 0);
                    //cont = 0;
                    ReCombat();
                    selectSprawl = Random.Range(0, 4);
                    selectAttack = 0;
					if (numAttack != 3)
						numAttack = 3;
                    StopCoroutine("SelectAttack");
                    break;
			}
			
		}
	}
	//tomo dano nao pode fazer nada bool toma conta
    public void IDano()
    {
        dano = true;
    }
	//morreu
	public void Kill()
	{
        dano = false;
		if(life <= 0)
		{
			fight = false;
			MovmentPlayer.player.ReturnPlayerMov ();
			GameMaster.master.vitorias += 1;
			Destroy (gameObject);
		}
        else
        {
            escolha = 1;
            selectSprawl = 1;
        }
	}
	//entro em combate
	public void Combat()
	{
        cont++;
		escolha = 1;
		if(!para)
		{
            velX = 0;
            primeiro = false;
			StartCoroutine("SelectAttack");
			MovmentPlayer.player.StopPlayer ();
			para = true;
		}
		fight = true;
	}
	//volta ao combate
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
}	