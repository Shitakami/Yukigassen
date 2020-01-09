using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : ScriptableObject {

	Wave m_nextSection;

	public Wave nextSection {
		get {
			return m_nextSection;
		}
		set {
			m_nextSection = value;
			value.previousSection = this;
		}
	}

	public Wave previousSection {
		get;
		set;
	}

	public virtual void Play () {
	}

}