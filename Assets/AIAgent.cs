using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
	public float speed=16;
	
	Vector2 lastWallEnd;
	
		
	private string actualDirection="";
	Collider2D wall;
	public GameObject player;
	public GameObject wallPrefab;
	public void Communication(string info){
		if(counter%1000==0&&info!=actualDirection){
			ChangeDirection(info);
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity=Vector2.up*speed;
		spawnWall();
    }
	private int counter=0;
	float distanceMetric(Vector2 pos)
	{
		return Mathf.Sqrt(
		Mathf.Pow(pos.x-player.transform.position.x,2)
		+
		Mathf.Pow(pos.y-player.transform.position.y,2));
	}
    // Update is called once per frame
    void Update()
    {
		if(counter%10==0){
				//oblicz odleglosc od gracza w zaleznosci od kierunku
			float[] dir=new float[4]{0,0,0,0}; //up,down,left,right
			
			dir[0]=distanceMetric(new Vector2(transform.position.x,
			transform.position.y+1));
			dir[1]=distanceMetric(new Vector2(transform.position.x,
			transform.position.y-1));
			dir[2]=distanceMetric(new Vector2(transform.position.x-1,
			transform.position.y));
			dir[3]=distanceMetric(new Vector2(transform.position.x+1,
			transform.position.y));
			//wybierz kierunek o najmniejszym koszcie
			int index = 0;
			for (int i = 0; i < dir.Length; i++)
			{
				if (dir[i] < dir[index]) { index = i; }
			}
			//zmien kierunek
			if(index==0) ChangeDirection("up");
			if(index==1) ChangeDirection("down");
			if(index==2) ChangeDirection("left");
			if(index==3) ChangeDirection("right");
		}
		counter++;
		fitColliderBetween(wall,lastWallEnd,transform.position);
    }
	void ChangeDirection(string direction)
	{
		if(actualDirection==direction) return;
			actualDirection=direction;
			if(direction == "up"){
				GetComponent<Rigidbody2D>().velocity=Vector2.up*speed;
				spawnWall();
			}
			if(direction == "down"){
				GetComponent<Rigidbody2D>().velocity=-Vector2.up*speed;
				spawnWall();
			}
			if(direction == "left"){
				GetComponent<Rigidbody2D>().velocity=-Vector2.right*speed;
				spawnWall();
			}
			if(direction == "right"){
				GetComponent<Rigidbody2D>().velocity=Vector2.right*speed;
				spawnWall();
			}
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
