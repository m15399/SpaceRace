using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Path : MonoBehaviour {

	public GameObject dotPrefab;
	public GameObject linePrefab;

	public enum SectionType {
		LINE,
		CURVE
	}
		
	class Section {

		public Vector2 start, end;
		public float angle; // degrees
//		public SectionType type;
	}

//	List<Handle> handles = new List<Handle>();
	List<Section> sections = new List<Section>();

	float lastRefresh = 0;

	void Start () {
		
	}
	
	void Update () {
		if(Application.isEditor)
			RefreshPath();
	}

	Transform GetTrack(){
		string name = "Track (Generated)";

		Transform o = transform.FindChild(name);
		if(!o){
			
			o = new GameObject().transform;
			o.name = name;
			o.transform.parent = transform;
			o.transform.localPosition = new Vector3(0, 0, 0);
		}
		return o.transform;
	}

	public void RefreshPath() {
		// Clear track
		//
		Transform track = GetTrack();
		GameObject.DestroyImmediate(track.gameObject);
		track = GetTrack();

		sections.Clear();


		// Create sections list
		//

		Handle[] handles = GameObject.FindObjectsOfType<Handle>();
		Array.Sort(handles);

		SectionType type = handles[0].type;
		int sectionStart = 0;

		for(int i = 1; i < handles.Length; i++){
			Handle h = handles[i];
			if(h.type != type || h.type == SectionType.LINE || i == handles.Length - 1){
				int sectionEnd = i;
//				Debug.Log("Doing section " + sectionStart + "-" + sectionEnd);

				switch(type){
				case SectionType.LINE:
					for(int j = sectionStart; j < sectionEnd; j++){
						Vector3 s = handles[j].transform.position;
						Vector3 e = handles[j+1].transform.position;
						AddSection(s, e, SectionType.LINE);
					}
					break;
				}
				sectionStart = i;
			}
			type = h.type;
		}

		// Create walls
		//

		// Create a dummy section to close the loop
		Vector2 start = sections[sections.Count - 1].end;
		Vector2 end = sections[0].start;
		Section dummySection = new Section();
		dummySection.angle = Utils.VectorToAngle(end - start);
		dummySection.start = start;
		dummySection.end = end;
		sections.Add(dummySection);

		for(int i = 0; i < sections.Count; i++){
			Section s = sections[i];
			Section p = null;
			Section n = null;

			if(i == 0)
				p = sections[sections.Count - 1];
			else
				p = sections[i - 1];

			if(i == sections.Count - 1)
				n = sections[0];
			else
				n = sections[i+1];

			Vector2 forwards = (s.end - s.start).normalized;
			Vector2 left = new Vector2(-forwards.y, forwards.x);

			float wStartSize = 4;
			float wEndSize = 4;

			float startMiterAngle = s.angle + 90;
			float endMiterAngle = s.angle + 90;

			if(p != null){
				startMiterAngle = (s.angle + p.angle)/2 + 90;
			}
			if(n != null){
				endMiterAngle = (s.angle + n.angle)/2 + 90;
			}

			// Find how much to offset line to line up miter
			//
			Vector2 slopeVStart = Utils.AngleToVector(startMiterAngle - s.angle);
			float slopeStart = slopeVStart.y / slopeVStart.x;
			float xStart = wStartSize / slopeStart;

			Vector2 slopeVEnd = Utils.AngleToVector(endMiterAngle - s.angle);
			float slopeEnd = slopeVEnd.y / slopeVEnd.x;
			float xEnd = wEndSize / slopeEnd;

			//  Calculate start and end points 
			//
			Vector2 wStartL = s.start + left * wStartSize;
			wStartL += forwards * xStart;
			Vector2 wEndL = s.end + left * wEndSize;
			wEndL += forwards * xEnd;

			Vector2 wStartR = s.start + -left * wStartSize;
			wStartR += forwards * -xStart;
			Vector2 wEndR = s.end + -left * wEndSize;
			wEndR += forwards * -xEnd;

			DrawLine(wStartL, wEndL, Color.green);
			DrawLine(wStartR, wEndR, Color.green);

		}
		// Remove dummy section
		sections.RemoveAt(sections.Count - 1);


		DrawCurve(
			new Vector2(0, 0),
			new Vector2(5, 0),
			new Vector2(15, -10),
			new Vector2(20, -10)
		);

		DrawCurve(
			new Vector2(-5, 0),
			new Vector2(-20, 0),
			new Vector2(-20, -30),
			new Vector2(-5, -30)
		);
	}

	void AddSection(Vector2 start, Vector2 end, SectionType type){
		Section s = new Section();
		s.angle = Utils.VectorToAngle(end - start);
		s.start = start;
		s.end = end;
		sections.Add(s);

		DrawLine(start, end, Color.blue);
	}

	void DrawPoint(Vector3 pos, Color color){
		GameObject p = GameObject.Instantiate(dotPrefab);
		p.SetActive(true);
		p.transform.parent = GetTrack();
		p.transform.localPosition = pos;

		p.GetComponent<SpriteRenderer>().color = color;

//		debugShapes.Add(p);
	}

	void DrawLine(Vector3 start, Vector3 end, Color color){
		GameObject l = GameObject.Instantiate(linePrefab);
		l.SetActive(true);
		l.transform.parent = GetTrack();
		l.transform.localPosition = start + (end - start) / 2;

		Vector3 scale = l.transform.localScale;
		scale.x = (end - start).magnitude;
		l.transform.localScale = scale;

		l.transform.Rotate(0, 0, Utils.VectorToAngle(end - start));

		l.GetComponent<SpriteRenderer>().color = color;

//		debugShapes.Add(l);
	}

	void DrawCurve(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4){

		float startSize = 4;
		float endSize = startSize;

		Vector2 lastPoint = p1 - p2.normalized * .1f;

		Vector2 forward = (p1 - lastPoint).normalized;
		Vector2 left = new Vector2(-forward.y, forward.x);
			
		Vector2 lastLPoint = p1 + left * startSize;
		Vector2 lastRPoint = p1 - left * startSize;

		float steps = 60;
		float step = 1 / steps;
		for(float t = 0; t < .99999999f; t += step){
			Vector2 p = Mathf.Pow(1-t, 3) * p1 + 3 * Mathf.Pow(1-t, 2) * t * p2 +
				3 * (1-t) * Mathf.Pow(t, 2) * p3 + Mathf.Pow(t, 3) * p4;

			forward = (p - lastPoint).normalized;
			left = new Vector2(-forward.y, forward.x);

			float w = startSize;

			Vector2 lp = p + left * w;
			Vector2 rp = p - left * w;

//			DrawLine(lp, rp, Color.gray);
			DrawLine(lastLPoint, lp, Color.gray);
			DrawLine(lastRPoint, rp, Color.gray);
			DrawLine(lastPoint, p, Color.blue);

			lastLPoint = lp;
			lastRPoint = rp;

			lastPoint = p;
		}


		forward = (p4 - p3).normalized;
		left = new Vector2(-forward.y, forward.x);

		Vector2 elp = p4 + left * endSize;
		Vector2 erp = p4 - left * endSize;

//		DrawLine(elp, erp, Color.gray);
		DrawLine(lastRPoint, erp, Color.gray);
		DrawLine(lastLPoint, elp, Color.gray);
		DrawLine(lastPoint, p4, Color.blue);
	}
}
