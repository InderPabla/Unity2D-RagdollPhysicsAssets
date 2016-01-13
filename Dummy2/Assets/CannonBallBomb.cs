using UnityEngine;
using System.Collections;

public class CannonBallBomb : MonoBehaviour {
	private Transform mainCamera; //Transform of the main camera
	private const string TRACK_OBJECTS_METHOD = "trackObjects"; //Method in CameraTracker to track this object
	private const string DAMAGE_METHOD = "damage"; //Method in ObjectDamage to damage the object by some level
	private const string WOOD_NAME = "Wood"; //Wood string name of destroyable objects
	private const string ACTIVATE_BALL_EXPLOSION_METHOD = "activateBallExplosion";
	private const string STOP_OBJECT_TRACK_METHOD = "stopObjectTrack"; //Method in CameraTracker to stop object tracking
	private const string BREAK_PART_METHOD = "breakPart";
	private bool touchTrigger = false;

	public GameObject explosionPrefab;
	public float time = 1f;
	public float power = 20f;
	public float radius = 3f;

	// Use this for initialization
	void Start () 
	{
		mainCamera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(touchTrigger == true)
		{
			if(Input.GetMouseButtonDown(0))
			{
				activateBallExplosion();
			}
		}
	}

	/// <summary>
	/// Method is called from CannonAmmoHandler to set ammo physics.
	/// </summary>
	/// <param name = 'ammoPhysics'> Physics to be set upon this object. </param>
	public void fireAmmo(CannonAmmoPhysics ammoPhysics)
	{
		Invoke(ACTIVATE_BALL_EXPLOSION_METHOD,time);
		transform.position = ammoPhysics.positionOfAction; //postion set to position of action
		transform.rigidbody2D.velocity = ammoPhysics.velocity; //ridigbody2D velocity to given ammo velocity
		mainCamera.SendMessage(TRACK_OBJECTS_METHOD,new Transform[]{transform}); //inform CameraTracker to track this object
		touchTrigger = true;
	}

	public void activateBallExplosion()
	{
		Vector2 position = new Vector2(transform.position.x,transform.position.y);
		Collider2D[] affectedColliders = Physics2D.OverlapCircleAll(position,radius);
		for(int i=0;i<affectedColliders.Length;i++)
		{


			if(affectedColliders[i].name.Contains(WOOD_NAME) || affectedColliders[i].name.Contains("Break"))
			{	
			

				Vector2 affectedObject = new Vector2(affectedColliders[i].transform.position.x,affectedColliders[i].transform.position.y);
				float distance = Vector2.Distance(affectedObject,position);
				float angleRad = Mathf.Atan2((affectedObject.y-position.y),(affectedObject.x-position.x));
				Vector2 newVelocity = new Vector2(Mathf.Cos(angleRad)*power,Mathf.Sin(angleRad)*power);
				newVelocity.x *= radius/distance;/*(Mathf.Pow(distance,0.5f) Mathf.Pow(radius+1,0.5f));*/
				newVelocity.y *= radius/distance;/*(Mathf.Pow(distance,0.5f) Mathf.Pow(radius+1,0.5f));*/


				affectedColliders[i].rigidbody2D.velocity = newVelocity;


				//Debug.Log(distance+" "+affectedColliders[i].name+" "+newVelocity.magnitude+" "+power*0.9f);
				if(affectedColliders[i].name.Contains(WOOD_NAME))
				{
					//Debug.Log(newVelocity.magnitude+" "+power+" "+power*0.9f);
					if(newVelocity.magnitude >= power*1.75f)
					{
							affectedColliders[i].SendMessage(DAMAGE_METHOD,4);
							//Debug.Log(222);
					}
				}
				else if(affectedColliders[i].name.Contains("Break"))
				{

				}
				
				/*if(distance<=(radius/2f))
				{
					if(affectedColliders[i].name.Contains(WOOD_NAME))
					{
						affectedColliders[i].SendMessage(DAMAGE_METHOD,4);
					}
					else if(affectedColliders[i].name.Contains("Break"))
					{
						affectedColliders[i].SendMessage(BREAK_PART_METHOD);
					}
				}
				else if(distance>(radius/2f))
				{
					if(affectedColliders[i].name.Contains(WOOD_NAME))
					{
						affectedColliders[i].SendMessage(DAMAGE_METHOD,3);
						affectedColliders[i].rigidbody2D.velocity = newVelocity;
					}
					else if(affectedColliders[i].name.Contains("Break"))
					{
						affectedColliders[i].SendMessage(BREAK_PART_METHOD);
						affectedColliders[i].rigidbody2D.velocity = newVelocity;
					}
				}*/
				//Debug.Log("POWER: "+power);
				//Debug.Log("New Velo: "+newVelocity);

				
			}
		}
		mainCamera.SendMessage(STOP_OBJECT_TRACK_METHOD);
		GameObject explosion = Instantiate(explosionPrefab,transform.position,transform.rotation) as GameObject;
		Destroy(gameObject);
	}
}
