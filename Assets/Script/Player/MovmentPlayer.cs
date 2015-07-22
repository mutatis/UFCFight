using UnityEngine;
using System.Collections;

public class MovmentPlayer : MonoBehaviour 
{
	public Animator anim;

	bool stop;
	bool esquiva;
	bool attackPower;
	bool attack;

	void Update ()
	{
		if(!esquiva && !attack && !attackPower && !stop)
		{
			anim.SetTrigger("Run");
			transform.Translate(1 * Time.deltaTime, 0, 0);
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow) && !attack)
		{
			anim.SetTrigger("Idle");
			esquiva = true;
			StartCoroutine("GO");
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) && !esquiva && !attack)
		{
			stop = true;
			StartCoroutine("HeavyAttack");
		}
		else if(Input.GetKeyUp(KeyCode.RightArrow) && !esquiva && !attackPower)
		{
			stop = false;
			anim.SetTrigger("Attack");
			attack = true;
			StopCoroutine ("HeavyAttack");
		}

		if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			stop = false;
		}

		if(attackPower && Input.GetKeyUp(KeyCode.RightArrow))
		{
			anim.SetTrigger("HeavyAttack");
			StopCoroutine ("HeavyAttack");
		}
	}

	IEnumerator HeavyAttack()
	{
		yield return new WaitForSeconds (1);
		print("Foca jovem");
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

	IEnumerator GO()
	{
		yield return new WaitForSeconds (1);
		esquiva = false;
		StopCoroutine("GO");
	}
}