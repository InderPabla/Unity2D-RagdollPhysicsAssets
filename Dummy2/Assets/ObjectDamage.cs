using UnityEngine;
using System.Collections;

public class ObjectDamage : MonoBehaviour {
	public Sprite[] objects = new Sprite[3];
	private int index = 0;
	private int scorePerDestroy = 100;
	private SpriteRenderer spriteRenderer;

	public GameObject woodChips = null;
	public bool destroyable = true;

	private const string WOOD_NAME = "Wood"; //Wood string name of destroyable objects
	private const string GRASS_NAME = "Grass"; //Grass name
	private const string GAME_MASTER_NAME = "GameMaster"; //GameMaster name
	private const string DAMAGE_METHOD = "damage"; //Method in ObjectDamage to damage the object by some level
	private const string ADD_OBJECT_DAMAGE_SCORE_METHOD = "addObjectDamageScore"; //Method in ScoreTracker to increment object damage score

	//private const string ADD_SCORE = "addScore"; //Method in GameMaster to increment score

	private GameObject gameMaster;

	void Start () 
	{
		gameMaster = GameObject.Find(GAME_MASTER_NAME);
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.name.Contains("Wood"))
		{
			float otherMass; // other object's mass
			if (collision.rigidbody)
				otherMass = collision.rigidbody.mass;
			else 
				otherMass = 10; // static collider means huge mass
			float force = rigidbody2D.velocity.sqrMagnitude * rigidbody2D.mass;
			if(force>75)
			{
				//Debug.Log(force);
				//renderer.material.color = Color.red;
				//collision.collider.renderer.material.color = Color.red;
				collision.collider.SendMessage(DAMAGE_METHOD,1);
			}
		}
	}

	public void damage(int damageLevel)
	{
		if(destroyable == true)
		{
			index += damageLevel;

			if(index>=4)
			{
				gameMaster.SendMessage(ADD_OBJECT_DAMAGE_SCORE_METHOD,scorePerDestroy);
				GameObject chips = Instantiate(woodChips,transform.position,transform.rotation) as GameObject;
				Destroy(gameObject);
			}
			else
			{
				if(index-1>=0)
					spriteRenderer.sprite = objects[index-1];	
			}
		}
	}
}
