using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode leftKey;
	public KeyCode rightKey;
	
	public float speed=16;
	
	Vector2 lastWallEnd;
	
	Collider2D wall;
	
	public GameObject wallPrefab;
	
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity=Vector2.up*speed;
		spawnWall();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(upKey)) {
			GetComponent<Rigidbody2D>().velocity=Vector2.up*speed;
			spawnWall();
		}
		if(Input.GetKeyDown(downKey)) {
			GetComponent<Rigidbody2D>().velocity=-Vector2.up*speed;
			spawnWall();
		}
		if(Input.GetKeyDown(leftKey)) {
			GetComponent<Rigidbody2D>().velocity=-Vector2.right*speed;
			spawnWall();
		}
		if(Input.GetKeyDown(rightKey)) {
			GetComponent<Rigidbody2D>().velocity=Vector2.right*speed;
			spawnWall();
		}
		////dodac
		 fitColliderBetween(wall,lastWallEnd,transform.position);
    }
	
	void spawnWall(){
		lastWallEnd=transform.position;
		
		GameObject c= Instantiate(wallPrefab,transform.position,Quaternion.identity);
		wall=c.GetComponent<Collider2D>();
	}
	
	void fitColliderBetween(Collider2D what, Vector2 a, Vector2 b){
		what.transform.position=a+(b-a)*0.5f;
		float distance = Vector2.Distance(a,b);
		if(a.x != b.x){
			what.transform.localScale=new Vector2(distance+1,1);
		}
		else {
			what.transform.localScale=new Vector2(1,distance+1);
		}
		
	}	
	
	void OnTriggerEnter2D(Collider2D co) {
    if (co != wall) {
        print("Player lost: " + name);
        Destroy(gameObject);
    }
}
}
