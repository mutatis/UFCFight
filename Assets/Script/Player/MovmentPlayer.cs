using UnityEngine;
using System.Collections;

public class MovmentPlayer : MonoBehaviour 
{
	public static MovmentPlayer player;

	public Animator anim;

	public bool stop;
	public bool fight;

	public float velX = 3;

	float temp;

	GameObject obj;

	Enemy enemy;

	bool esquiva;
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
		if(!esquiva && !attack && !attackPower && !stop)
		{
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
			stop = true;
			StartCoroutine("HeavyAttack");
		}
		else if(Input.GetKeyUp(KeyCode.RightArrow) && !esquiva && !attackPower)
		{
			if(!fight)
			{
				stop = false;
			}
			Attack();
			anim.SetTrigger("Attack");
			attack = true;
			StopCoroutine ("HeavyAttack");
		}

		if(Input.GetKeyUp(KeyCode.RightArrow) && !fight)
		{
			stop = false;
		}

		if(attackPower && Input.GetKeyUp(KeyCode.RightArrow))
		{
			AttackF();
			anim.SetTrigger("HeavyAttack");
			StopCoroutine ("HeavyAttack");
		}
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
		enemy = obj.GetComponent<Enemy> ();
		if(enemy.selectAttack == 1)
		{
			enemy.life -= 2;
		}
		print("bateu");
	}

	void AttackF()
	{
		enemy = obj.GetComponent<Enemy> ();
		if(enemy.selectAttack == 2)
		{
			enemy.life -= 4;
		}
		print("attack forte");
	}

	void Defesa()
	{
		velX = 0;
		anim.SetFloat ("VelX", velX);
		print ("defendeu");
	}

	IEnumerator HeavyAttack()
	{
		yield return new WaitForSeconds (1);
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

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			obj = collision.gameObject;
		}
	}
}