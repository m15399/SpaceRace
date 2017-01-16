using UnityEngine;
using System.Collections;

/// <summary>
/// Every frame set local position = local position of another object
/// </summary>
[ExecuteInEditMode]
public class CopyLocalPosition : MonoBehaviour {

	public Transform target;

	void LateUpdate () {
		transform.localPosition = target.localPosition;
	}
}
