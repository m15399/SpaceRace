using UnityEngine;
using System.Collections;

/// <summary>
/// Moving object with velocity, friction
/// </summary>
public class Ballistic : MonoBehaviour {

	public Vector2 v;
	public Vector2 fric = new Vector2(0, -1);
	public float maxSpeed = -1;
	public float rv = 0; // Degrees / sec
	public float rFric = 0;
	public bool destroyOffCamera = true;


	public void Accelerate(float angle, float amt){
		Accelerate(Utils.AngleToVector(angle) * amt);
	}

	public void Accelerate(Vector2 dir){
		v += dir;
	}

	public void SetSpeed(float angle, float amt){
		v = Utils.AngleToVector(angle) * amt;
	}

	void Start () {

	}

	void FixedUpdate () {
		float dt = Time.fixedDeltaTime;

		// If fric.y is negative, default it to fric.x
		//
		if(fric.y < 0)
			fric.y = fric.x;

		// Friction
		//
		v.x *= 1 - fric.x;
		v.y *= 1 - fric.y;

		// Cap max speed
		//
		if(maxSpeed >= 0){
			float speed = v.magnitude;
			if(speed > maxSpeed){
				v *= (maxSpeed / speed);
			}
		}

		// Position
		//
		transform.localPosition += (Vector3)(v * dt);

		// Rotation
		//
		rv *= 1 - rFric;

		transform.Rotate(new Vector3(0, 0, rv * dt));

		// Check if off camera
		//
//		if(destroyOffCamera && Utils.OffCamera(transform.position)){
//			GameObject.Destroy(gameObject);
//		}
	}
}
