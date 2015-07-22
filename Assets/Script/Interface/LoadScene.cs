using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour 
{
	public void Loading(string nome)
	{
		Application.LoadLevel (nome);
	}
}
