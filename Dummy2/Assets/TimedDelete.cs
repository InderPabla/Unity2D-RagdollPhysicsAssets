using UnityEngine;
using System.Collections;

/// <summary>
/// TimedDelete class delete an object after a certian period of time.
/// </summary>
public class TimedDelete : MonoBehaviour 
{
	public float timeToDelete = 1f; //time to delete this object 
	
	/// <summary>
	/// Initialize components.
	/// </summary>
	void Start () 
	{
		Invoke("Delete",timeToDelete); //call invoke to create a thread which calls Delete after a few seconds
	}


	/// <summary>
	/// Method deletes object
	/// </summary>
	void Delete()
	{
		Destroy(gameObject); //destory this gameObject
	}
}
