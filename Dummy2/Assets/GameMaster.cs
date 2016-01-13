using UnityEngine;
using System.Collections;

/// <summary>
/// GameMaster class handles level transitions, level operation, and entity deaths.
/// </summary>
public class GameMaster : MonoBehaviour 
{
	public int entityCount = 0; //entity count in the scene
	public GameObject slowMotionEnd; //slow motion layer prefab

	private bool finished = false; //false = level still playing, true = level ended
	private bool childrenLoaded = false; //false = children not loaded, true = children loaded

	private const int NUMBER_OF_LEVELS = 2; //total numbers of levels in the game
	private const float LEVEL_END_TIME_SCALE = 0.25f; //change world time scale after level end
	private int scorePerNotUsedAmmo = 1000;

	private const string CHANGE_GRAVITY_METHOD = "changeGravity"; //Method in WorldPhysics 
	private const string CHANGE_TIME_SCALE_METHOD = "changeTimeScale"; //Method in WorldPhysics 
	private const string DISPLAY_SCORE_DETAILS_METHOD = "displayScoreDetails"; //Method in CameraTracker, and GameMaster
	private const string ADD_AMMO_SCORE_METHOD = "addAmmoScore"; //Method in ScoreTracker to increment ammo not used score
	private const string DISPLAY_STAR_METHOD = "displayStar"; 
	private const string BACK_BUTTON_NAME = "BackButton"; //back button name
	private const string NEXT_LEVEL_BUTTON_NAME = "NextLevel"; //next level button name
	private const string RESET_LEVEL_BUTTON_NAME = "ResetLevel"; //reset level button name
	private const string CANNON_AMMO_NAME = "CannonAmmo"; //GameObjects name inside Cannon which handels ammos

	private Transform worldPhysics = null; //world physics object in the scene (child of GameMaster)
	private Camera mainCamera = null; //main camera

	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start ()
	{
		this.mainCamera = Camera.main; //get main camera
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			Application.LoadLevel(0);
		}

		//if touch on screen
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

			//if something is found check its name
			if(hit.transform!=null)
			{
				string hitName = hit.transform.name;

				if(hitName.Equals(BACK_BUTTON_NAME))
				{
					Application.LoadLevel(0);
				}
				else if(hitName.Equals(RESET_LEVEL_BUTTON_NAME))
				{
					Application.LoadLevel(Application.loadedLevel);
				}
				else if(hitName.Equals(NEXT_LEVEL_BUTTON_NAME))
				{
					Application.LoadLevel(Application.loadedLevel+1);
				}
			}
		}

		//load children if false
		if(childrenLoaded == false)
		{
			getChildren();
		}
	}

	/// <summary>
	/// Method is called from BodyHandler to inform when an entity dies.
	/// </summary>
	public void entityDeathInform()
	{
		//if level is not finished
		if(finished == false)
		{
			entityCount --; //decrease entity count

			//if entity count reaches 0
			if(entityCount == 0)
			{
				finished = true; //level is finished
				worldPhysics.SendMessage(CHANGE_TIME_SCALE_METHOD,LEVEL_END_TIME_SCALE); //set new time scale
				GameObject slowMotionEndLayer = Instantiate(slowMotionEnd) as GameObject; // create slowmo layer

				CannonAmmoHandler cannonAmmo = GameObject.Find(CANNON_AMMO_NAME).GetComponent<CannonAmmoHandler>();
				int ammoScore = cannonAmmo.getAmmoNotUsed()*scorePerNotUsedAmmo;
				this.SendMessage(ADD_AMMO_SCORE_METHOD,ammoScore);
				Invoke(DISPLAY_SCORE_DETAILS_METHOD,0.25f); //wait 1/4th of a second (some objects might still be being destroyed at this moment)
			}
		}
	}

	/// <summary>
	/// Method which initializes children and its components to variables
	/// </summary>
	private void getChildren()
	{
		if(transform.GetChild(0)!=null)
		{
			worldPhysics = transform.GetChild(0);
			childrenLoaded = true;
		}
	}

	void displayScoreDetails()
	{
		//Get and display score
		ScoreTracker scoreTracker = GetComponent<ScoreTracker>();
		string scoreMessage = "";
		scoreMessage += "-Score-\n";
		scoreMessage += "Destruction Score: "+scoreTracker.getObjectDamageScore()+".\n";
		scoreMessage += "Enemy Score: "+scoreTracker.getEnemyDamageScore()+".\n";
		scoreMessage += "Ammo Score: "+scoreTracker.getAmmoScore()+".\n";
		scoreMessage += "Total Score: "+scoreTracker.getTotalScore()+".";

		mainCamera.SendMessage(DISPLAY_SCORE_DETAILS_METHOD,scoreMessage);
		LevelDetailHandler levelDetailHandler = new LevelDetailHandler();
		LevelDetail detail = new LevelDetail(Application.loadedLevel,scoreTracker.getTotalScore());

		mainCamera.SendMessage(DISPLAY_STAR_METHOD,levelDetailHandler.getStarIndex(detail));

	}


}
