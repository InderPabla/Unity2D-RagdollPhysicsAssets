using UnityEngine;
using System.Collections;

/// <summary>
/// CannonTouch class is a script attached to the CannonBarrel prefab and handles major touch events on the screen.
/// </summary>
public class CannonTouch : MonoBehaviour 
{
	private Camera mainCamera = null; //main camers

	private Transform parent = null; //parent of this object
	private Transform cannonTouch = null; //child 2
	private Transform cannonNose = null; //child 1

	private LineRenderer line = null; //line component of this object
	private LineRenderer cannonNoseLine = null; //line component of child 1

	private bool childrenLoaded = false; //false = children not loaded, true = children loaded
	private bool touch = false; //false = no majour event, true = CannonTouch (child 2) is pressed, 
	private bool touchCamera = false; //false = no camera movement with user, true = move camera with user's finger
	private bool touchLock = false; //false = carry on with touch, true = stop user from interacting with CannonTouch
	
	private const string CANNON_TOUCH_NAME = "CannonTouch"; //name of child 2
	private const string FIRE_CANNON_AMMO_METHOD = "fireCannonAmmo"; //method in CannonAmmoHandler
	private const string TOGGLE_TOUCH_LOCK_METHOD = "toggleTouchLock"; //method in this class 
	private const string CANNON_RICOCHET_ANIMATE_METHOD = "cannonRicochetAnimate"; //method in CannonBarrelRicochet
	private const string TRANSFORM_TO_FOLLOW_METHOD = "transformToFollow"; //method in CameraTracker
	private const string FIX_GIVEN_CAMERA_POSITION_METHOD = "fixGivenCameraPosition"; //method in CameraTracker
	private const string STOP_AUDIO_METHOD = "stopAudio"; //stop audio method

	private float angleRad = 0f; //angle in radians
	private float touchLockToggleWaitTime = 1f; //max time to wait before toggle unlock of touch
	private Vector2 velocity = Vector2.zero; 
	private Vector3 cameraTouchPosition = Vector3.zero;

	public Transform cannonAmmo = null; //CannonAmmo object in the scene
	public GameObject cannonFire = null; //CannonFire prefab

	public float lineDistance = 0f; //size of line
	public float margin  = 10f; //scaling power of the cannon
	public int numberOfPoints = 10; //max number of line points that should be drawn

	public Material lineShaderMaterial; //shader material to colour line

	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start ()
	{
		this.mainCamera = Camera.main;
		this.line = this.GetComponent<LineRenderer>();
		this.line.material = lineShaderMaterial;
		this.parent = transform.parent;
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		//handel touches on the screen
		initialTouch();
		afterTouch();
		releaseTouch();

		if(touchCamera == true)
		{
			Vector3 touchPosition2 = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3 newCameraPosition = Vector3.zero;
			newCameraPosition.x = mainCamera.transform.position.x + (cameraTouchPosition.x-touchPosition2.x)*0.25f;
			newCameraPosition.z = -100f;
			newCameraPosition.y = 0f;

			mainCamera.SendMessage(FIX_GIVEN_CAMERA_POSITION_METHOD,newCameraPosition);
		}

		//load children if false
		if(childrenLoaded == false)
		{
			getChildren();
		}
	}

	/// <summary>
	/// Method deals with when touch is released from the screen
	/// </summary>
	private void releaseTouch()
	{
		if(Input.GetMouseButtonUp(0))
		{
			//if was touch is true, then reset all times and send trajectory information to CannonAmmpHandler and invoke lock
			if(touch == true)
			{
				line.SetPosition(0,Vector3.zero);
				line.SetPosition(1,Vector3.zero);


				for(int i = 0;i<numberOfPoints;i++)
				{
					cannonNoseLine.SetPosition(i,Vector3.zero);
				}

				CannonAmmoPhysics ammoPhysics = new CannonAmmoPhysics(angleRad,velocity,cannonNose.position);
				cannonAmmo.SendMessage(FIRE_CANNON_AMMO_METHOD,ammoPhysics);

				GameObject fire = Instantiate(cannonFire) as GameObject;
				fire.transform.position = cannonNose.position;
				fire.transform.eulerAngles = new Vector3(0,0,cannonFire.transform.eulerAngles.z + (Mathf.Rad2Deg*angleRad));
				fire.SendMessage(TRANSFORM_TO_FOLLOW_METHOD,cannonNose);

				touch = false;
				toggleTouchLock();

				Invoke (TOGGLE_TOUCH_LOCK_METHOD,touchLockToggleWaitTime); //call invoke to create a thread which calls toggleTouchLock after a few seconds

				this.SendMessage(CANNON_RICOCHET_ANIMATE_METHOD);
				playAudio();

				touchLock = true;
			}
			touchCamera = false;
		}
	}

	public void playAudio()
	{
		this.GetComponent<AudioSource>().Play();
		Invoke(STOP_AUDIO_METHOD,1f);
	}

	public void stopAudio()
	{
		this.GetComponent<AudioSource>().Stop();
	}

	/// <summary>
	/// Method deals with when touch is pressed down on to the screen
	/// </summary>
	private void initialTouch()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//if touch if false then check for touch lock
			if(touch == false)
			{
				//if touch lock is false then check for objects touched on the screen
				if(touchLock == false)
				{
					Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
					RaycastHit2D[] hits = Physics2D.RaycastAll(touchPosition, Vector2.zero);
					bool cannonTouchFound = false;

					//check if any ray cast hits are cannon touch
					for(int i=0;i<hits.Length;i++)
					{
						string hitName = hits[i].collider.name;
						if(hitName.Equals(CANNON_TOUCH_NAME))
						{
							touch = true;
							cannonTouchFound = true;
							break;
						}
					}

					//if cannon touch not found, then start user camera tracking
					if(cannonTouchFound != true)
					{
						touchCamera = true;
						cameraTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
					}

				}

				//if touch lock is true, then start user camera tracking
				else
				{
					touchCamera = true;
					cameraTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
				}
			}

			//if touch is true, then start user camera tracking
			else
			{
				touchCamera = true; 
				cameraTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			}
		}
	}

	/// <summary>
	/// Method with initial touch moments.
	/// </summary>
	private void afterTouch()
	{
		//if touch is true
		if(touch == true)
		{

			Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //get position of touch

			//calulate touch angle from CannonTouch and touch position in radians and degrees
			angleRad = Mathf.Atan2((parent.position.y-touchPosition.y) , (parent.position.x-touchPosition.x));
			float angleDeg = Mathf.Rad2Deg*angleRad;

			transform.eulerAngles = new Vector3(0,0,angleDeg); //set cannon barrel's angles to the angle degree

			float distance = lineSet(cannonTouch.position,touchPosition,angleRad + 180*Mathf.Deg2Rad); //set power lines

			velocity = new Vector2(Mathf.Cos(angleRad)*margin*distance,Mathf.Sin(angleRad)*margin*distance); //calulate 2D velocity vector
			drawProjectileLine(velocity); //draw projectile lines
		}
	}

	/// <summary>
	/// Method sets powerlines comming out from behind the cannon.
	/// </summary>
	/// <param name = 'from'> Vector to draw from. </param>
	/// <param name = 'to'> Vector to draw to. </param>
	/// <param name = 'angleRad'> Angle of the two vectors in radians </param>
	/// <returns> Distance between the vectors set on the drawn powerlines </returns>
	private float lineSet(Vector3 from, Vector3 to, float angleRad)
	{
		line.SetPosition(0,from); //set first position to from
		to.z = 0;

		float distance = Vector3.Distance(from,to); //calulate distance

		//if diatance is greater then chosen line distance then set distance to line distance
		if(distance>lineDistance)
		{
			distance = lineDistance;
		}

		//Get new line position at the given angle and distance
		Vector2 newTo2D = GetPosition(from.x,from.y,angleRad,distance);
		Vector3 newTo3D = new Vector3(newTo2D.x,newTo2D.y,-3);
		line.SetPosition(1,newTo3D);

		return distance;
	}
	
	/// <summary>
	/// Method which initializes children and its components to variables
	/// </summary>
	private void getChildren()
	{
		if(transform.GetChild(0)!=null && transform.GetChild(1)!=null)
		{
			cannonNoseLine = transform.GetChild(0).GetComponent<LineRenderer>();
			cannonNoseLine.material = lineShaderMaterial;
			cannonNose = transform.GetChild(0);
			cannonTouch = transform.GetChild(1);
			childrenLoaded = true;
		}
	}

	/// <summary>
	/// Gets position of new vector.
	/// </summary>
	/// <param name = 'x'> X value. </param>
	/// <param name = 'y'> Y value. </param>
	/// <param name = 'angle'> Angle in radians to find new vector from. </param>
	/// <param name = 'distance'> Distance offset from angle </param>
	/// <returns> Offseted 2D vector </returns>
	private Vector2 GetPosition(float x,float y, float angle, float distance)
	{
		Vector2 newVector;
		float opposite = Mathf.Sin(angle) * distance; //Get SOH
		float ajacent = Mathf.Cos(angle) * distance; //Get CAH
		newVector.x = x+ajacent; //ajacent : Add to old Vector
		newVector.y = y+opposite; //opposite : Add to old Vector
		return newVector;				
	}

	/// <summary>
	/// Draws the projectile lines.
	/// </summary>
	/// <param name = 'velocity'> Veclocity to draw lines with. </param>
	private void drawProjectileLine(Vector2 velocity)
	{
		float velocityMagnitude = Mathf.Sqrt((velocity.x * velocity.x) + (velocity.y * velocity.y)); //magnitude of the velocity
		float angleRad = Mathf.Atan2(velocity.y ,velocity.x); //angle of the velocity in radians
		float fTime = 0; //delta time
		cannonNoseLine.SetVertexCount(numberOfPoints); //set line's vertex count to max allowed

		//draw lines
		for(int i = 0;i<numberOfPoints;i++)
		{
			float dx = velocityMagnitude * fTime * Mathf.Cos(angleRad); //2D kinematics, x-axis has a constant change
			//2D kinematics, y-axis changes with gravity

			//Change in Y = (velocity mag * delta time * angle) - (gravity * (delta time ^ 2) /2)
			float dy = velocityMagnitude * fTime * Mathf.Sin(angleRad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);

			Vector3 pos = new Vector3(cannonNose.position.x + dx ,cannonNose.position.y + dy,-3); //add deltas 
			cannonNoseLine.SetPosition(i,pos);
			fTime += 0.075f; //update delta time step
		}
	}

	/// <summary>
	/// Method which toggles touch lock.
	/// </summary>
	private void toggleTouchLock()
	{
		touchLock = !touchLock;
	}
}