using UnityEngine;
using System.Collections;

public class CannonShellCluster : MonoBehaviour {

	public GameObject explosionPrefab = null;
	public GameObject cannonBallClusterPrefab = null;
	public float time = 1f;


	private const string ACTIVATE_CLUSTER_BOMB = "activateClusterBomb";

	private Transform mainCamera; //Transform of the main camera
	
	private const string TRACK_OBJECTS_METHOD = "trackObjects"; //Method in CameraTracker to track this object
	private const string STOP_OBJECT_TRACK_METHOD = "stopObjectTrack"; //Method in CameraTracker to stop object tracking
	private const string DAMAGE_METHOD = "damage"; //Method in ObjectDamage to damage the object by some level
	private const string FIRE_AMMO_METHOD = "fireAmmo";
	private const string WOOD_NAME = "Wood"; //Wood string name of destroyable objects
	
	private CannonAmmoPhysics ammoPhysics;
	private float damageForce_1 = 2000f;
	private float damageForce_2 = 1000f;
	private float damageForce_3 = 500f;
	private float damageForce_4 = 250f; 
	
	private bool fired = false;
	private bool touchTrigger = false;
	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		mainCamera = Camera.main.transform;
	}
	
	void Update()
	{
		if(fired == true)
		{
			Vector2 velocity = rigidbody2D.velocity;
			float angleDeg = (Mathf.Atan2(velocity.y,velocity.x)*Mathf.Rad2Deg)-90;
			transform.eulerAngles = new Vector3(0,0,angleDeg);
		}
		if(touchTrigger == true)
		{
			if(Input.GetMouseButtonDown(0))
			{
				activateClusterBomb();
			}
		}
	}
	
	/// <summary>
	/// Method is called from CannonAmmoHandler to set ammo physics.
	/// </summary>
	/// <param name = 'ammoPhysics'> Physics to be set upon this object. </param>
	public void fireAmmo(CannonAmmoPhysics ammoPhysics)
	{
		Invoke(ACTIVATE_CLUSTER_BOMB,time);
		transform.position = ammoPhysics.positionOfAction; //postion set to position of action
		transform.rigidbody2D.velocity = ammoPhysics.velocity; //ridigbody2D velocity to given ammo velocity
		transform.eulerAngles = new Vector3(0,0,(Mathf.Rad2Deg*ammoPhysics.angleRad)-90);
		mainCamera.SendMessage(TRACK_OBJECTS_METHOD,new Transform[]{transform}); //inform CameraTracker to track this object
		this.ammoPhysics = ammoPhysics;
		fired = true;
		touchTrigger = true;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		fired = false;
		if(collision.collider.name.Contains("Wood"))
		{
			bool damaged = true;
			int damageLevel = 0;
			float otherMass = 0; // other object's mass
			
			if (collision.rigidbody)
				otherMass = collision.rigidbody.mass;
			else 
				otherMass = 10; // static collider means huge mass
			//float force = collision.relativeVelocity.sqrMagnitude * rigidbody2D.mass;
			//Debug.Log(rigidbody2D.velocity.sqrMagnitude);
			float force = rigidbody2D.velocity.sqrMagnitude * rigidbody2D.mass;
			
			if(force>damageForce_1)
			{
				damageLevel = 4;
			}
			else if(force>damageForce_2)
			{
				damageLevel = 3;
			}
			else if (force>damageForce_3)
			{
				damageLevel = 2;
			}
			else if (force>damageForce_4)
			{
				damageLevel = 1;
			}
			else
			{
				damaged = false;
			}
			
			if(damaged == true)
			{
				collision.collider.SendMessage(DAMAGE_METHOD,damageLevel);
			}
		}
	}

	public void activateClusterBomb()
	{
		mainCamera.SendMessage(STOP_OBJECT_TRACK_METHOD);
		GameObject explosion = Instantiate(explosionPrefab) as GameObject;
		explosion.transform.eulerAngles = transform.eulerAngles+new Vector3(0,0,90f);
		explosion.transform.position = transform.position;

		GameObject cannonBallCluster = Instantiate(cannonBallClusterPrefab,transform.position,transform.rotation) as GameObject;
		cannonBallCluster.SendMessage(FIRE_AMMO_METHOD,this.ammoPhysics);
		//Debug.Log(rigidbody2D.velocity.magnitude);
		Destroy(gameObject);
	}
}
