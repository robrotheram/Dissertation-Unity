using UnityEngine;
using System.Collections;

public class Fotter : MonoBehaviour {
	public GameObject mainCamera;
	public GameObject aboveCamera;

	public bool selected = false;
	private string selectedText = "Show Map";
	private GameObject map;
	public GUITexture pin;

	public GUIStyle style;
	private static string BuildLocation = "Faraday";

	// Use this for initialization
	void Start () {
	    map = GameObject.Find ("map");
		map.SetActive (false);
	
		pin.gameObject.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject me = GameObject.Find("3rd Person Controller");
		BuildLocation = MainGUI.Check (me.transform.position);
	}
	private void OnGUI(){
		GUILayout.BeginArea (new Rect (40, (Screen.height - 25), 300, 50));
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (selectedText)) {
			if(selected){
				selectedText = "Hide Map";
					selected = false;
				aboveCamera.SetActive(true);
				mainCamera.SetActive(false);
				map.SetActive(true);
				GameObject me = GameObject.Find("3rd Person Controller");
				Rect  r = new Rect(((-(3f*me.transform.position)).x-75f), ((-(2f*me.transform.position)).z+510f), pin.pixelInset.width, pin.pixelInset.height);






				pin.pixelInset=r;
			
				pin.gameObject.SetActive (true);


				//GameObject aboveCam = GameObject.Find("AboveCam");
				//aboveCam.SetActive(false);
			}else{
				selectedText = "Show Map";
				//GameObject aboveCam = GameObject.Find("AboveCam");
				//aboveCam.SetActive(true);
					selected = true;
				aboveCamera.SetActive(false);
				mainCamera.SetActive(true);
				map.SetActive(false);
				pin.gameObject.SetActive (false);
			}
				
		}
		if (GUILayout.Button ("Show Menu")) {
			clickAndDrag.isenabled = false;
			MainGUI.enableMenu();

		}



		GUILayout.EndHorizontal ();

		GUILayout.EndArea ();
		GUI.Label(new Rect ((Screen.width - 400), (Screen.height-60), 200, 30), "Now at: "+BuildLocation,style);

	}
}
