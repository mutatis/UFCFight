using UnityEngine;
using System.Collections;

public class ChaoMov : MonoBehaviour
{

    public Transform limit;
    public Transform atras;

    public float soma;

	void Update ()
    {
        if (MovmentPlayer.player.velX > 0 || MovmentPlayer.player.obj == null)
        {
            //transform.Translate((MovmentPlayer.player.velX * Time.deltaTime) * -1, 0, 0);
        }

        if(transform.position.x < limit.position.x)
        {
            transform.position = new Vector3((atras.position.x + soma), transform.position.y, transform.position.z);
        }
	}
}
