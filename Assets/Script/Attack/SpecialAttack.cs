using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

	public GameObject bullet;
	public List<GameObject> muzzle;
	public int bulletCount = 20;
	public float intervalTime = 0.3f;
	public float waitTime = 0.5f;
	public float speed = 300f;

	public float lockRadius = 10;
	bool funcLock;

	public GameObject shotSEObjcet;

	// Use this for initialization
	void Start () {
		funcLock = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Activate()
	{

		if (funcLock == false)
			yield break;

		funcLock = false;

		for(int i = 0; i < bulletCount; i++)
		{
			StartCoroutine(BulletFire(i % muzzle.Count));
			yield return new WaitForSeconds(intervalTime);
		}

		funcLock = true;

	}

	IEnumerator BulletFire(int index)
	{

		// オブジェクトを生成して、ノズル[index]の場所に設定
		GameObject newBullet = Instantiate(bullet, muzzle[index].transform);
		newBullet.transform.parent = null;

		// rayを飛ばして目標を設定、生成したbulletを目標に向ける
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.SphereCast(ray, lockRadius, out hit) && hit.collider.gameObject.tag == "Enemy")
			newBullet.transform.LookAt(hit.transform);
		else
			newBullet.transform.eulerAngles = this.transform.eulerAngles;

		yield return new WaitForSeconds(waitTime);

		if (newBullet == null)
			yield break;

		newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * speed;
		Destroy(newBullet, 5f);
		GameObject se = Instantiate(shotSEObjcet);
		Destroy(se, 2f);
	}

}
