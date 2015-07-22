using UnityEngine;
using System.Collections;

public class MovmentPlayer : MonoBehaviour 
{
	public static MovmentPlayer player;

	public Animator anim;

	public bool stop;
	public bool fight;

	public float velX = 3;

	bool esquiva;
	bool attackPower;
	bool attack;

	void Awake()
	{
		player = this;
	}

	void Start()
	{
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

	public void IsFight()
	{
		if(fight)
		{
			anim.SetTrigger("Idle");
		}
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
		esquiva = false;
	}
}