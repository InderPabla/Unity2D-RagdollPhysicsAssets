using UnityEngine;
using System.Collections;

public class LevelChooserAnimater : MonoBehaviour {

	public GameObject[] children = new GameObject[10];
	private Vector3 normalAngle;
	private Vector3[] endAngles;
	private Vector3 tempAngle;

	private float duration = 4f; // duration in seconds
	private float t = 0f;
	private bool state = false; 
	private const string SET_NAME_METHOD = "setName";
	public const string SET_STAR_METHOD = "setStar"; 
	private int index = 0;


	// Use this for initialization
	void Start () 
	{
		normalAngle = Vector3.zero;
		endAngles = new Vector3[] 
		{
			new Vector3(7,7,0),
			new Vector3(-7,7,0),
			new Vector3(7,-7,0),
			new Vector3(-7,-7,0)
		};

		tempAngle = endAngles[0];
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(state == true)
		{
			transform.eulerAngles = Vector3.Lerp(tempAngle,normalAngle, t);
			if (t < 1)
			{ 
				t += Time.deltaTime/duration;
			}
			else
			{
				t = 0;
				state = false;
				tempAngle = endAngles[Random.Range(0,endAngles.Length-1)];
			}
		}
		else
		{
			transform.eulerAngles = Vector3.Lerp(normalAngle, tempAngle, t);
			if (t < 1)
			{ 
				t += Time.deltaTime/duration;
			}
			else
			{
				t = 0;
				state = true;
			}
		}
	}

	public void changeLevelNames(int index)
	{
		int number = (index*10)+1;
		LevelDetailHandler levelDetailHandler = new LevelDetailHandler();
		foreach (GameObject child in children)
		{
			child.SendMessage(SET_NAME_METHOD,number+"");
			child.SendMessage(SET_STAR_METHOD,levelDetailHandler.getStarIndex(number));
			number++;
		}
	}
	
}
