using UnityEngine;
using System.Collections;
using System;

public class Handle : MonoBehaviour, IComparable<Handle> {

	public Path.SectionType type = Path.SectionType.LINE;

	public int CompareTo(Handle other){
		return gameObject.name.CompareTo(other.gameObject.name);
	}

	void Start () {
	
	}
	
	void Update () {
		
	}

}
