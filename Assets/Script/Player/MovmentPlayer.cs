﻿using UnityEngine;
using System.Collections;

public class MovmentPlayer : MonoBehaviour 
{
	public static MovmentPlayer player;

    public PlayerAudioController audioController;

	public Animator anim;

	public Rigidbody2D rig;
    
	public GameObject obj;

    public bool isEsquiva;
	public bool stop;
	public bool fight;
	public bool prepareAttack;
	public bool esquiva;

	public int life;

	public float velX = 3;

	float temp;

	Enemy enemy;

    bool isAttack = true;
	bool attackPower;
	bool attack;

	void Awake()
	{
		player = this;
	}

	void Start()
	{
		temp = velX;
		anim.SetFloat ("VelX", velX);
	}

    float dist;

	void Update ()
	{
        if (obj != null)
        {
            dist = Vector3.Distance(transform.position, obj.transform.position);
            if(dist < 1.5f)
            {
                obj = null;
            }
        }

		if(rig.velocity.x < 0)
		{
			rig.velocity = new Vector2((rig.velocity.x + 0.05f), 0);
		}
		else
		{
			rig.velocity = new Vector2(0, 0);
		}

		if(life <= 0)
		{
			Morreu();
		}

		if(!esquiva && !attack && !attackPower && !stop)
		{
			velX = temp;
			anim.SetTrigger("Run");
			transform.Translate(velX * Time.deltaTime, 0, 0);
		}

        if (isAttack)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !attack)
            {
                isAttack = false;
                Defesa();
                anim.SetTrigger("Base");
                esquiva = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !esquiva && !attack)
            {
                prepareAttack = true;
                
                StartCoroutine("HeavyAttack");
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) && !esquiva && !attackPower)
            {
                isAttack = false;
                prepareAttack = false;
                if (!fight)
                {
                    stop = false;
                }
                if(obj == null)
                {
                    audioController.Soco();
                }
                Attack();
                anim.SetTrigger("Attack");
                attack = true;
                StopCoroutine("HeavyAttack");
            }
            /*else if(fight)
		    {
			    velX = temp / 2;
			    anim.SetTrigger("Run");
			    transform.Translate(velX * Time.deltaTime, 0, 0);
		    }*/
        }

		if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			if(!fight)
			{
				stop = false;
            }
			if(attackPower)
			{
				AttackF();
				anim.SetTrigger("HeavyAttack");
				StopCoroutine ("HeavyAttack");
			}
		}
	}

	void Morreu()
	{
		Application.LoadLevel("GameOver");
	}

	public void StopPlayer()
	{
		velX = 0;
		anim.SetFloat ("VelX", velX);
		fight = true;
		anim.SetTrigger("Idle");
		stop = true;
	}

	public void ReturnPlayerMov()
	{
		velX = temp;
		anim.SetFloat ("VelX", velX);
		fight = false;
		anim.SetTrigger("Run");
		stop = false;
	}

	public void IsFight()
	{
		if(fight)
		{
			anim.SetTrigger("Idle");
		}
	}

	void Attack()
	{
		if(obj != null)
		{
			enemy = obj.GetComponent<Enemy> ();
			if(enemy.selectAttack != 2)
            {
                audioController.Soco();
                enemy.Dano();
				enemy.life -= 2;
			}
            else if(enemy.selectAttack == 2)
            {
                audioController.BlockEnemy();
            }
		}
	}

	void AttackF()
	{
		if(obj != null)
		{
			enemy = obj.GetComponent<Enemy> ();
            //se der soco forte e nao esta defendendo, da  dano normal;
			if(enemy.selectAttack == 1)
			{
				enemy.Dano();
				enemy.life -= 4;
			}
            //se der soco forte com a defesa, dano reduzido mas da stun;
            else if(enemy.selectAttack == 2)
            {
                enemy.Stun();
                enemy.life -= 1;
            }
            else
            {
                enemy.Stun();
                enemy.life -=4;
            }
		}
	}

	void Defesa()
	{
		velX = 0;
		anim.SetFloat ("VelX", velX);
	}

	IEnumerator HeavyAttack()
	{
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("Carrega");
		yield return new WaitForSeconds (0.8f);
		prepareAttack = false;
		attackPower = true;
	}

	public void StopHeavyAttack()
    {
        attackPower = false;
        isAttack = true;
    }

	public void StopAttack()
	{
		attack = false;
        isAttack = true;
    }

	public void Esquivei()
	{
        isAttack = true;
        velX = temp;
		anim.SetFloat ("VelX", velX);
		esquiva = false;
	}
}