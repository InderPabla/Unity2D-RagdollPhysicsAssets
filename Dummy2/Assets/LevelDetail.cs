using UnityEngine;
using System.Collections;

public class LevelDetail
{
	public float score = 0f;
	public int level = 1;

	public LevelDetail (int level, float score)
	{
		this.score = score;
		this.level = level;
	}


}
