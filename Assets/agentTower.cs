using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentTower : MonoBehaviour
{
	public GameObject agent1;
	public GameObject player;
	int reactivation=100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(agent1 == null || player == null) return;
		if(reactivation<=0) {
         if(Vector3.Distance(agent1.transform.position,player.transform.position) < 5)
		{
			Debug.Log("ok");
		}
		else{
			//udziel pomocy
			//trzeba dodac warunek kiedy
			player.GetComponent<AIAgent>().Communication("left");
			}
		}
		if(reactivation>0) reactivation--;
    }
}
