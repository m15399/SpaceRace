using UnityEngine;
using System.Collections;

public class ThePlayer : MonoBehaviour2 {

	Ballistic ballistic;

	void Start () {
		ballistic = GetComponent<Ballistic>();
	}

	void FixedUpdate () {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 delta = mousePos - (Vector2)transform.position;

		float targetAngle = Utils.VectorToAngle(delta);

		float dAngle = Mathf.Abs(Utils.AngleBetween(rot, targetAngle));
		dAngle = Mathf.Clamp(dAngle, 0, 60);

		RotateTowards(targetAngle, dAngle/10);


		float a = .1f;


		if(Input.GetKey("x") || Input.GetMouseButton(1)){
			ballistic.Accelerate(rot, a * 2);
		} else if(Input.GetKey("z") || Input.GetMouseButton(0)){
			ballistic.Accelerate(rot, a);
		}

//		Debug.Log(ballistic.v.magnitude);
	}
}
