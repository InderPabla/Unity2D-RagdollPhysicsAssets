using UnityEngine;
using System.Collections;

public class CannonBallEnlarger : MonoBehaviour {

	private Transform mainCamera; //Transform of the main camera

	private const string ACTIVATE_BALL_ENLARGE = "activateBallEnlarge";
	private const string TRACK_OBJECTS_METHOD = "trackObjects"; //Method in CameraTracker to track this object
	private const string DAMAGE_METHOD = "damage"; //Method in ObjectDamage to damage the object by some level
	private const string WOOD_NAME = "Wood"; //Wood string name of destroyable objects
	private const string BREAK_PART_METHOD = "breakPart";
	private const string BREAK_NAME = "Break"; //Break string name of breakable parts of a ragdoll

	private float damageForce_1 = 400f;
	private float damageForce_2 = 200f;
	private float damageForce_3 = 100f;
	private float damageForce_4 = 50f;
	/*private float damageForce_1 = 2000f;
	private float damageForce_2 = 1000f;
	private float damageForce_3 = 500f;
	private float damageForce_4 = 250f;*/
	
	private bool touchTrigger = false;
	public float time = 1f;
	private float timeDelta = 0f;
	private float timeDelta2 = 0f;
	private bool once = false;
	private bool once2 = false;
	private bool isGettingBigger = true;
	private Vector3 initialScale;

	private bool isGrowingSmall = false;
	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		mainCamera = Camera.main.transform;
		initialScale = transform.localScale;
	}

	void Update ()
	{
		if(touchTrigger == true)
		{
			if(Input.GetMouseButtonDown(0))
			{
				activateBallEnlarge();
			}
		}

		if(once == true)
		{
			if(timeDelta < 1f)
			{
				transform.localScale = Vector3.Lerp(initialScale,initialScale*9f,timeDelta);
				timeDelta += Time.deltaTime*9f;
				//isGettingBigger = false;
			}
			else
			{

				isGettingBigger = false;
				if(once2 == false)
				{
					Invoke("growSmall",2f);
					once2 = true;
					initialScale = transform.localScale;
				}
			}

		}

		if(isGrowingSmall == true)
		{
			if(timeDelta2 < 1f)
			{
				rigidbody2D.drag = 0f;
				rigidbody2D.angularDrag = 0f;
				rigidbody2D.mass = 0.75f;
				transform.localScale = Vector3.Lerp(initialScale,initialScale/13f,timeDelta2);
				timeDelta2 += Time.deltaTime*9f;
			}
		}


	}

	/// <summary>
	/// Method is called from CannonAmmoHandler to set ammo physics.
	/// </summary>
	/// <param name = 'ammoPhysics'> Physics to be set upon this object. </param>
	public void fireAmmo(CannonAmmoPhysics ammoPhysics)
	{
		Invoke(ACTIVATE_BALL_ENLARGE,time);
		transform.position = ammoPhysics.positionOfAction; //postion set to position of action
		transform.rigidbody2D.velocity = ammoPhysics.velocity; //ridigbody2D velocity to given ammo velocity
		touchTrigger = true;
		mainCamera.SendMessage(TRACK_OBJECTS_METHOD,new Transform[]{transform}); //inform CameraTracker to track this object
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		//if(once == true)
			//rigidbody2D.drag = 2f;


		//Debug.Log(once + "  " + collision.collider.name.Contains(BREAK_NAME) + "  " + isGettingBigger);
		if(once == true && collision.collider.name.Contains(BREAK_NAME) && isGettingBigger == true)
		{
			bool damage = true;
			float force = (rigidbody2D.velocity.sqrMagnitude+300) * rigidbody2D.mass;
			if(force>1000)
				collision.collider.SendMessage(BREAK_PART_METHOD);
			//Debug.Log(force);
			
		}

		//collision material is wood
		if(collision.collider.name.Contains(WOOD_NAME))
		{
			bool damaged = true;
			int damageLevel = 0;
			float otherMass = 0; // other object's mass
			
			if (collision.rigidbody)
				otherMass = collision.rigidbody.mass;
			else 
				otherMass = 10; // static collider means huge mass
			
			//float force = collision.relativeVelocity.sqrMagnitude * rigidbody2D.mass;

			float force = 0;
			if(isGettingBigger == true)
				force = (rigidbody2D.velocity.sqrMagnitude+300) * rigidbody2D.mass;
			else
				force = rigidbody2D.velocity.sqrMagnitude * rigidbody2D.mass;
			
			//damage forces on this object from highest to lowest
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

	public void activateBallEnlarge()
	{
		if(once == false)
		{
			//transform.localScale *= 10f;
			rigidbody2D.mass = 5f;
			rigidbody2D.drag = 2f;
			rigidbody2D.angularDrag = 100f;
			rigidbody2D.velocity = Vector2.zero;
			once = true;
		}
	}

	public void growSmall()
	{
		isGrowingSmall = true;
	}

}
