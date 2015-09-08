using UnityEngine;
using System.Collections;

public class CriaCriador : MonoBehaviour 
{
    public GameObject enemy;

	public void Destroy ()
	{
        Destroy(enemy);
	}
}
