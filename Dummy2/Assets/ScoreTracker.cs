using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	//private int score = 0;
	private int enemyDamageScore = 0;
	private int objectDamageScore = 0;
	private int ammoScore = 0;

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	/*void addScore (int scoreAdd)
	{
		score += scoreAdd;
		Debug.Log(score);
	}*/



	/*public int getScore ()
	{
		return score;
	}*/

	public void addEnemyDamageScore(int score)
	{
		enemyDamageScore += score;
	}
	
	public void addObjectDamageScore(int score)
	{
		objectDamageScore += score;
	}
	
	public void addAmmoScore(int score)
	{
		ammoScore += score;
	}

	public int getEnemyDamageScore()
	{
		return enemyDamageScore;
	}

	public int getObjectDamageScore()
	{
		return objectDamageScore;
	}

	public int getAmmoScore()
	{
		return ammoScore;
	}

	public int getTotalScore()
	{
		return ammoScore+objectDamageScore+enemyDamageScore;
	}
	
}
