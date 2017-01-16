using UnityEngine;
using UnityEditor;
using System.Collections;

public static class DebugKeys {

	[MenuItem("DebugKeys/Refresh Paths #r")]
	static void RefreshPaths() {
		foreach(Path p in GameObject.FindObjectsOfType<Path>()){
			p.RefreshPath();
		}
	}

}
