using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : SingletonBehaviour<WaveManager> {

	[SerializeField]
	List<Wave> sections;

	public Wave currentSection {
		get;
		private set;
	}

	protected override void Awake () {
		base.Awake ();
		sections.Aggregate ((acc, x) => {
			acc.nextSection = x;
			return x;
		});
		currentSection = sections.First ();
	}

	public bool Next () {
		if (currentSection.nextSection != null) {
			currentSection = currentSection.nextSection;
			return true;
		} else {
			return false;
		}
	}

	public bool Previous () {
		if (currentSection.previousSection != null) {
			currentSection = currentSection.previousSection;
			return true;
		} else {
			return false;
		}
	}

	public bool MoveSection (int index) {
		if (sections.Count >= index) {
			currentSection = sections [index];
			return true;
		} else {
			return false;
		}
	}
}
