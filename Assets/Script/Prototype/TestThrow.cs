using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThrow : MonoBehaviour {

	public List<GameObject> ball;
	public List<Vector3> ballPos;
	public GameObject rollingBall;
	public GameObject wall;

	public float speedWhenMakeRollingBall = 3;
	public float intervalTime = 0.5f;

	public GameObject scoreManager;
	public GameObject specialAttackmanager;

	GameObject newBall = null;
	GameObject newRollingBall = null;
	GameObject newWall = null;
	GameObject grabWall = null;

	AudioSource[] throwSE;

	int state;  // 情報を見るためにpublic設定

	float makingSnowBallTime;
	int snowBallIndex;
	bool rollingBallFlag = true;

	// state用定数
	const int OUT = 0;
	const int PLANE = 1;
	const int MAKEWALL = 2;
	const int MAKESNOWBALL = 3;
	const int WALL = 4;
	const int GRABWALL = 5;
	const int ROLLINGBALL = 6;
	const int NOTHING = 6;

	// Use this for initialization
	void Start() {

		throwSE = GetComponents<AudioSource>();
	}

	// Update is called once per frame
	void Update() {


		if (state == OUT || state == MAKESNOWBALL)
			MakeSnowBall();
		else if (state == PLANE)
			InPlane();
		else if (state == MAKEWALL)
			MakeWall();
		else if (state == WALL)
			InWall();
		else if (state == GRABWALL)
			GrabbingWall();
		else if (state == ROLLINGBALL)
			DoNothing();


	}

	private void OnTriggerEnter(Collider other)
	{
		if (state == MAKESNOWBALL || state == MAKEWALL || state == GRABWALL || state == ROLLINGBALL)
			return;

		if (other.gameObject.tag == "wall")
			state = WALL;
		else if (other.gameObject.tag == "plane")
			state = PLANE;


	}

	private void OnTriggerStay(Collider other)
	{
		

		if (state == MAKESNOWBALL || state == MAKEWALL || state == GRABWALL || state == ROLLINGBALL)
			return;



		if (other.gameObject.tag == "wall")
		{
			state = WALL;
			grabWall = other.gameObject;
		}
		else if (other.gameObject.tag == "Plane")
			state = PLANE;

	}

	private void OnTriggerExit(Collider other)
	{

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);


		float speed = device.velocity.x * device.velocity.x + device.velocity.z * device.velocity.z;
		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && state == PLANE /*&& speed < 0.8f*/ )  // speed < 0.8fをコメントアウトしてrollingballを出さないようにする
		{
			state = MAKEWALL;
			newWall = Instantiate(wall);
			newWall.GetComponent<Ball>().enabled = false;
			newWall.gameObject.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
			newWall.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
			newWall.gameObject.tag = "Untagged";

			if (scoreManager != null)
				scoreManager.GetComponent<ScoreManager>().WallCountIncrement();

		}
		else if(device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && state == PLANE)
		{
			newRollingBall = Instantiate(rollingBall);
			newRollingBall.transform.position = this.transform.position;
			newRollingBall.GetComponent<Rigidbody>().velocity = device.velocity * 5;
			Destroy(newRollingBall, 5f);

			rollingBallFlag = false;
			state = ROLLINGBALL;
			StartCoroutine(Interval());
		}

		//if (state != MAKESNOWBALL && state != MAKEWALL && state != GRABWALL && state == ROLLINGBALL)
		if(state == PLANE || state == WALL)
			state = OUT;

	}

	void MakeSnowBall()
	{

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);



		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{

			newBall = Instantiate(ball[0]);
			newBall.transform.parent = this.gameObject.transform;
			newBall.transform.localPosition = ballPos[0];
			makingSnowBallTime = 0;
			snowBallIndex = 1;
			state = MAKESNOWBALL;
			

		}
		else if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
		{

			if(newBall == null)
			{
				state = NOTHING;
				return;
			}

			/*if (newBall.transform.localScale.x < 2.5f)
				newBall.transform.localScale = new Vector3(
					newBall.transform.localScale.x + 0.001f,
					newBall.transform.localScale.y + 0.001f,
					newBall.transform.localScale.z + 0.001f
					);*/

			makingSnowBallTime += Time.deltaTime;

			if(snowBallIndex != ball.Count && makingSnowBallTime > snowBallIndex)
			{
				Destroy(newBall);
				newBall = Instantiate(ball[snowBallIndex]);
				newBall.transform.parent = this.gameObject.transform;
				newBall.transform.localPosition = ballPos[snowBallIndex];
				makingSnowBallTime = 0;
				throwSE[throwSE.Length - 3 + snowBallIndex].Play();
				snowBallIndex++;
				StartCoroutine(Vibration(800, 30));
				
				
			}
			

		}
		else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
		{

			newBall.transform.parent = null;

			/*
			 * 雪玉を水平になげるように補正を入れる
			 * もし極端に上方向に投げたのなら、補正をなくす
			 * 
			 */
			if (Mathf.Pow(device.velocity.x, 2) + Mathf.Pow(device.velocity.z, 2) > 3 && device.velocity.y < 2)
			{
				Vector3 speed = device.velocity * 7;
				speed.y *= 0.4f;
				speed.y += 0.5f;
				newBall.GetComponent<Rigidbody>().velocity = speed;
			} 
			else
			{

				newBall.GetComponent<Rigidbody>().velocity = device.velocity * 7;

			}


			// 補正を入れるのならばこれを消す
			// newBall.GetComponent<Rigidbody>().velocity = device.velocity * 7;

			// 壁に当たると消えるようにする
			newBall.GetComponent<Ball>().Throw();

			newBall.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
			newBall.GetComponent<Rigidbody>().maxAngularVelocity = newBall.GetComponent<Rigidbody>().angularVelocity.magnitude;
			newBall.GetComponent<Rigidbody>().useGravity = true;

			throwSE[Random.Range(0, throwSE.Length - 2)].Play();

			


			if (specialAttackmanager != null)
				specialAttackmanager.GetComponent<SpecialAttackManager>().AttackValue();

			Destroy(newBall, 5f);
			state = OUT;

		}


	}

	void InPlane()
	{

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);


		// テスト用コメントアウト
		/*
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			state = MAKEWALL;
			newWall = Instantiate(wall);
			newWall.gameObject.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
			newWall.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
			
		}
		*/

		/*
		float speed = device.velocity.x * device.velocity.x + device.velocity.z * device.velocity.z;
		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && rollingBallFlag && speed > speedWhenMakeRollingBall)
		{
			newRollingBall = Instantiate(rollingBall);
			newRollingBall.transform.position = this.transform.position;
			newRollingBall.GetComponent<Rigidbody>().velocity = device.velocity * 5;
			Destroy(newRollingBall, 5f);

			rollingBallFlag = false;
			state = ROLLINGBALL;
			StartCoroutine(Interval());

		}
		*/

	}

	void MakeWall()
	{
		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);

		if(newWall == null)
		{
			state = NOTHING;
			return;
		}

		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
		{

			state = OUT;
			newWall.gameObject.tag = "wall";
			
			if (newWall.transform.localScale.y < 0.7)
				Destroy(newWall);


			return;

		}

		device.TriggerHapticPulse(500);
		newWall.transform.position = new Vector3(newWall.transform.position.x, transform.position.y / 2, newWall.transform.position.z);
		newWall.transform.localScale = new Vector3(newWall.transform.localScale.x, transform.position.y, newWall.transform.localScale.z);

	}

	void InWall()
	{

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);


		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			state = GRABWALL;
			grabWall.transform.parent = this.transform;
			grabWall.gameObject.tag = "Untagged";
			grabWall.GetComponent<Wall>().GrabWall(this.gameObject);

		}

	}

	void GrabbingWall()
	{

		if (grabWall == null)
		{
			state = NOTHING;
			return;
		}

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);

		if (!device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
			return;

		grabWall.gameObject.tag = "Attack";
		grabWall.GetComponent<Ball>().enabled = true;

		grabWall.transform.parent = null;
		grabWall.GetComponent<Rigidbody>().velocity = device.velocity * 7;
		grabWall.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
		grabWall.GetComponent<Rigidbody>().maxAngularVelocity = grabWall.GetComponent<Rigidbody>().angularVelocity.magnitude;
		grabWall.GetComponent<Rigidbody>().useGravity = true;
		Destroy(grabWall, 5);
		state = OUT;

	}

	void DoNothing()
	{

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);

		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
			state = OUT;


	}



	IEnumerator Interval()
	{
		yield return new WaitForSeconds(intervalTime);

		rollingBallFlag = true;

	}

	public IEnumerator Vibration(ushort value = 1500, int count = 10)
	{
		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);

		for (int i = 0; i < count; i++)
		{
			device.TriggerHapticPulse(value);
			yield return null;
		}

	}


}

