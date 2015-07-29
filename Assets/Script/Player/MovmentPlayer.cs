using UnityEngine;
using System.Collections;

public class MovmentPlayer : MonoBehaviour 
{
	public static MovmentPlayer player;

	public Animator anim;

	public Rigidbody2D rig;

	public GameObject obj;

	public bool stop;
	public bool fight;
	public bool prepareAttack;
	public bool esquiva;

	public int life;

	public float velX = 3;

	float temp;

	Enemy enemy;

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

	void Update ()
	{
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

		if(Input.GetKeyDown(KeyCode.LeftArrow) && !attack)
		{
			Defesa();
			anim.SetTrigger("Base");
			esquiva = true;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) && !esquiva && !attack)
		{
			prepareAttack = true;
			stop = true;
			StartCoroutine("HeavyAttack");
		}
		else if(Input.GetKeyUp(KeyCode.RightArrow) && !esquiva && !attackPower)
		{
			prepareAttack = false;
			if(!fight)
			{
				stop = false;
			}
			Attack();
			anim.SetTrigger("Attack");
			attack = true;
			StopCoroutine ("HeavyAttack");
		}
		/*else if(fight)
		{
			velX = temp / 2;
			anim.SetTrigger("Run");
			transform.Translate(velX * Time.deltaTime, 0, 0);
		}*/

		if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			if(!fight)
			{
				stop = false;
			}
			else if(attackPower)
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
			if(enemy.selectAttack == 1)
			{
				enemy.Dano();
				enemy.life -= 2;
			}
		}
	}

	void AttackF()
	{
		if(obj != null)
		{
			enemy = obj.GetComponent<Enemy> ();
			if(enemy.selectAttack == 2)
			{
				enemy.Dano();
				enemy.life -= 4;
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
		yield return new WaitForSeconds (1);
		prepareAttack = false;
		attackPower = true;
	}

	public void StopHeavyAttack()
	{
		attackPower = false;
	}

	public void StopAttack()
	{
		attack = false;
	}

	public void Esquivei()
	{
		velX = temp;
		anim.SetFloat ("VelX", velX);
		esquiva = false;
	}
}