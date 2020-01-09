using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int damageScore;
	public int timeScore;
	public int ballScore;

	public int CriterionHP = 50;
	public int CriterionTime = 240;
	public int ballPoint = 20;
	public Text scoreBoard;

	GameObject player;
	float startTime = 0;
	float endTime = 0;

	int wallCount = 0;

	int ballCount = 0;
	int hitBallCount = 0;

	public GameObject clearText;
	public GameObject clearTextPos;
	public GameObject clearParticle1;
	public GameObject clearParticle2;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera");
		scoreBoard.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetStartTime()
	{
		startTime = Time.time;
		ballCount = 0;
		hitBallCount = 0;
	}

	public void SetEndTime()
	{
		endTime = Time.time;
	}
	
	public void WallCountIncrement(int value = 1)
	{
		wallCount += value;
	}

	public void BallCountIncrement(int value = 1)
	{
		ballCount += value;
	}

	public void HitBallCountIncrement(int value = 1)
	{
		hitBallCount += value;
	}

	public int CalcScore()
	{
		scoreBoard.gameObject.SetActive(true);
		scoreBoard.text = "Score\n";

		int score = 0;
		int timeScore;
		int damageScore;
		int effortScore;

		SetEndTime();

		// ダメージからスコアを計算
		damageScore = (int)((CriterionHP - player.GetComponent<Player>().GetDamage()) * 1000 / CriterionHP);
		score += damageScore;
		scoreBoard.text += "Damage : " + damageScore.ToString();

		if (damageScore >= 900)
			scoreBoard.text += " S";
		else if (damageScore >= 700)
			scoreBoard.text += " A";
		else if (damageScore >= 500)
			scoreBoard.text += " B";
		else
			scoreBoard.text += " C";

		scoreBoard.text += "\n";

		// 時間からスコアを計算
		timeScore = (int)(((CriterionTime - (endTime - startTime)) * 1000 / CriterionTime));
		score += timeScore;
		scoreBoard.text += "Time : " + timeScore.ToString();

		// 雪玉の使用数からスコアを計算

		if (timeScore >= 900)
			scoreBoard.text += " S";
		else if (timeScore >= 700)
			scoreBoard.text += " A";
		else if (timeScore >= 500)
			scoreBoard.text += " B";
		else
			scoreBoard.text += " C";

		scoreBoard.text += "\n";

		if (ballCount != 0)
			effortScore = (hitBallCount * 1000 / ballCount);
		else
			effortScore = 1000;

		int sum = wallCount * 5 + ballCount;

		score += effortScore;
		scoreBoard.text += "Accuracy: " + effortScore.ToString() + "\n";

		scoreBoard.text += "<size=3000><color=#ff0000>T otal : " + score.ToString() + "</color></size>";

		if (score >= 2700)
			scoreBoard.text += " <size=3000><color=#ffff00>SSS</color></size>";
		else if (score >= 2500)
			scoreBoard.text += " <size=3000><color=#ffff00>S</color></size>";
		else if (score >= 2000)
			scoreBoard.text += " <size=3000><color=#ff0000>A</color></size>";
		else if (score >= 1500)
			scoreBoard.text += " <size=3000><color=#00ff00>B</color></size>";
		else
			scoreBoard.text += " <size=3000><color=#000000>C</color></size>";
		

		// クリア演出がうまくいかなかったので追加した

		// GameObject newText = Instantiate(clearText);
		// newText.transform.position = clearTextPos.transform.position;
	
		
		
	//	Instantiate(clearParticle1);

	//	clearParticle2.SetActive(true);

		return score;
	}

}
