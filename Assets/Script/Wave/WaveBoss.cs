using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu]
public class WaveBoss : Wave
{

	public override void Play()
	{
		Debug.Log("Scene : Boss");
		Instantiate(Waves.instance.Enemies[3], Waves.instance.InstanceObjects[4].transform.position, Quaternion.identity);
		
	}

}
