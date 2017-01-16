using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils {

	/*
	static Dictionary<string, GameObject> found = new Dictionary<string, GameObject>();

	public static GameObject Find(string name){
		if(found.ContainsKey(name)){
			GameObject o = found[name];
			if(o == null || o.Equals(null)){
				found[name] = GameObject.Find(name);
			}
			return o;

		} else {
			found[name] = GameObject.Find(name);
			return found[name];
		}
	}
	*/

	public static Vector3 CameraPos {
		get {
			return Camera.main.transform.position;
		}
	}

//	public static bool OutOfCamera(Vector3 p, float tolerance){
//		p = p - CameraPos;
//
//		if(Mathf.Abs(p.x) > (TheGameManager.screenWidth/2.0f + tolerance) ||
//			Mathf.Abs(p.y) > (TheGameManager.screenHeight/2.0f + tolerance)){
//			return true;
//		}
//		return false;
//	}

//	public static bool OutOfCamera(Vector3 p){
//		return OutOfCamera(p, 2);
//	}
//
//	// "Passed" the camera
//	//
//	public static bool OffCamera(Vector3 p){
//		p = p - CameraPos;
//		return p.y < -20;
//	}

	// Tell object to parent itself to the camera
	//
	public static void TrackWithCamera(Transform t){
		t.SetParent(Camera.main.transform);
	}

	public static void CancelTrackWithCamera(Transform t){
		t.SetParent(null);
	}

	// Destroy all bullets on screen
	//
	public static void KillAllBullets(){
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
		foreach(GameObject o in bullets){
			GameObject.Destroy(o.gameObject);
		}
	}

	// Round a float to n decimal places
	//
	public static float RoundToPlaces(float val, int n){
		float pow = Mathf.Pow(10, n);
		return Mathf.Round(val * pow) / pow;
	}

	public static Vector2 AngleToVector(float angle){
		return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
	}

	public static float VectorToAngle(Vector2 vector){
		return Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
	}

	public static float AngleBetween(float a1, float a2){
		float angle = a2 - a1;
		while(angle < -180) angle += 360;
		while(angle > 180) angle -= 360;
		return angle;
	}

	public static float MoveAngleTowards(float a1, float a2, float maxAngle){
		float dAngle = Utils.AngleBetween(a1, a2);
		float absDAngle = Mathf.Abs(dAngle);

		if(absDAngle > maxAngle)
			dAngle = dAngle / absDAngle * maxAngle;

		return a1 + dAngle;
	}

}
