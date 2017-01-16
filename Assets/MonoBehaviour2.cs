using UnityEngine;
using System.Collections;

public class MonoBehaviour2 : MonoBehaviour {

	public float rot {
		get {
			return transform.eulerAngles.z;
		}
		set {
			Vector3 angles = transform.eulerAngles;
			angles.z = value;
			transform.eulerAngles = angles;
		}
	}

	public float x {
		get {
			return transform.localPosition.x;
		}
		set {
			Vector3 pos = transform.localPosition;
			pos.x = value;
			transform.localPosition = pos;
		}
	}

	public float y {
		get {
			return transform.localPosition.y;
		}
		set {
			Vector3 pos = transform.localPosition;
			pos.y = value;
			transform.localPosition = pos;
		}
	}

	public void MoveForward(float amt){
		Vector3 unit = Utils.AngleToVector(rot);
		transform.localPosition += unit * amt;
	}

	public void RotateTowards(float angle, float maxAngle){
		rot = Utils.MoveAngleTowards(rot, angle, maxAngle);
	}

	public void RotateTowards(Vector2 point, float maxAngle){
		Vector3 pos = transform.localPosition;

		Vector2 delta = point - (Vector2)pos;

		rot = Utils.MoveAngleTowards(rot, Utils.VectorToAngle(delta), maxAngle);
	}

}
