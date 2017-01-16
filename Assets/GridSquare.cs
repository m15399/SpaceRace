using UnityEngine;
using System.Collections;

public class GridSquare : MonoBehaviour {

	
	void Update () {
		float gridSize = 40;

		Vector2 camPos = transform.position - Camera.main.transform.position;
		Vector3 pos = transform.position;

		if(camPos.x < -gridSize/2)
			pos.x += gridSize;
		if(camPos.x > gridSize/2)
			pos.x -= gridSize;
		if(camPos.y < -gridSize/2)
			pos.y += gridSize;
		if(camPos.y > gridSize/2)
			pos.y -= gridSize;

		transform.position = pos;
	}
}
