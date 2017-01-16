using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject squarePrefab;

	void Start () {

		float scale = 4;
		float w = 40;

		for(float i = -w; i <= w; i += scale){
			for(float j = -w; j <= w; j += scale){
				GameObject square = GameObject.Instantiate(squarePrefab);
				square.transform.position = new Vector3(i, j, 100);
				square.transform.localScale = new Vector3(scale, scale, scale);
				square.transform.parent = transform;
			}
		}

	}

}
