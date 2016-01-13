using UnityEngine;
using System.Collections;

public class ScorePopUp
{
	public Vector3 startPosition;
	public string score;

	public ScorePopUp (Vector3 startPosition,string score)
	{
		this.startPosition = startPosition;
		this.score = score;
	}

}
