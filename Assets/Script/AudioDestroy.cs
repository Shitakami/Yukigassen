using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour {

	private AudioSource m_audioSorce;

	void Start () {
		m_audioSorce = GetComponent<AudioSource> ();
	}

	void Update () {
		if (!m_audioSorce.isPlaying) {
			Destroy (this.gameObject);
		}
	}
}
