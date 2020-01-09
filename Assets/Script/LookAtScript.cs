using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour {

	Transform m_player;

	void Update () {
		m_player = GameObject.FindWithTag ("MainCamera").transform;
		transform.LookAt (m_player);
	}
}
