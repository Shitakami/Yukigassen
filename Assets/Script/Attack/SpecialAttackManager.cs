using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpecialAttackManager : MonoBehaviour {

	public GameObject leftController;
	public GameObject rightController;
	public GameObject SpecialAttack;


	public float maxGauge = 100;
	public float attackValue = 1;
	public FloatReactiveProperty gauge;


	public GameObject lEffect;
	public GameObject rEffect;
	// Use this for initialization
	void Start() {
		gauge = new FloatReactiveProperty(0);
		gauge.Where((x) => x >= maxGauge)
			.Subscribe(_ =>
			{
				lEffect.SetActive(true);
				rEffect.SetActive(true);

				

			});


		

	}
	
	// Update is called once per frame
	void Update () {



		if (leftController.activeSelf == false)
			return;
			var trackedObjectLeft = leftController.GetComponent<SteamVR_TrackedObject>();
			var deviceLeft = SteamVR_Controller.Input((int)trackedObjectLeft.index);


		if (rightController.activeSelf == false)
			return;
			var trackedObjectRight = rightController.GetComponent<SteamVR_TrackedObject>();
			var deviceRight = SteamVR_Controller.Input((int)trackedObjectRight.index);
		
		if (gauge.Value >= maxGauge && (deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) || deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)))
		{
			StartCoroutine(SpecialAttack.GetComponent<SpecialAttack>().Activate());
			gauge.Value = 0;
			rEffect.SetActive(false);
			lEffect.SetActive(false);
		}

		gauge.Value += Time.deltaTime;

	}

	public void AttackValue()
	{
		gauge.Value += attackValue;
	}



}
