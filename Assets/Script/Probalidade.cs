using UnityEngine;
using System.Collections;

public class Probalidade : MonoBehaviour
{

    public Gift[] gifts;
    int giftIndex = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    int ChooseItem()
    {
        float total = 0;
        int i = 0;
        foreach (Gift elem in gifts)
        {
            total += elem.probability;
        }

        float randomPoint = Random.value * total;

        for (i = 0; i < gifts.Length; i++)
        {
            if (randomPoint < gifts[i].probability)
                return i;
            else
                randomPoint -= gifts[i].probability;
        }

        return gifts.Length - 1;
    }
}

[System.Serializable]
public class Gift
{
    public GameObject item;
    public float probability;
}
