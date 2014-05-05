using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {

	public GUISkin mySkin;

	private Rect windowRectangle;
	private Rect messageRectangle;
	private static bool paused = false, wait = true,btnPress = false;

	public static rootPlace[] items;
	public Rect Box;
	public string slectedItem = "None";
	private string oldslectedItem = "None";
	private  Vector2 scrollPosition;
	public TextAsset GameAsset;
	private static bool message =true;
	private static string messages = "Hello, my name is Jeff and I will give you a tour around the campus. All you need to do is click on the location button below, select a location and click ok. Then I will take you there.";
	private static string title = "Welecome";
	private string department = "Search for department";
	private string oldText = "";
	public string initialText = "";
	private bool editing = false;

	// Use this for initialization
	void Start () {
		department = initialText;
		oldslectedItem = slectedItem;
		GameObject aboveCam = GameObject.Find("AboveCam");

		aboveCam.SetActive (false);
		GameObject master = GameObject.Find("master");
		master.SetActive(false);

		windowRectangle = new Rect(Screen.width/2-200,Screen.height/2-100,400,200);
		messageRectangle = new Rect(Screen.width/2-200,Screen.height/2-80,400,160);

		XMLReader xml = new XMLReader (GameAsset);
		items = xml.getList();
	}
	public static void enableMenu(){
		paused = true;
	}

	public static void showMessage(string m, string t){
		message = true;
		messages = m;
		title = t;
	}

	private void Update(){
		if(wait){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if(paused){
					paused = false;
				}else{
					paused = true;
				}
				wait = false;
				Invoke("waiting",0.3f);

			}
		}
		if (!department.Equals(oldText)) {
			Debug.Log("========= Change ======="); 
			oldText = department;
		}

	}

	private void waiting(){
				wait = true;
		}
	private void OnGUI(){
		GUI.skin = mySkin;
		if (message) {
			messageRectangle = GUI.Window (0, windowRectangle, beginMessage, title);

				}
		if (paused) {
			windowRectangle = GUI.Window(1,windowRectangle,windowFunc,"Navigation Menu");			
		}
	}
	private void windowFunc(int id){
		department = GUILayout.TextField(department, 25);
		if (GUILayout.Button(slectedItem))
		{
			editing = true;
			if(btnPress){
				editing = false;
				btnPress = false;
			}else{
				btnPress = true;
			}

		}
		if (editing)
		{

			scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Box.width), GUILayout.Height (80));

			for (int x = 0; x < items.Length; x++)
			{
				if((department.Equals(""))||(department.Equals(initialText))){
					if (GUILayout.Button(items[x].placeName))
					{
						slectedItem = items[x].placeName;
						editing = false;
					}
				}else{
					rootPlaceDepartmentListDepartment[] deptList= items[x].departmentList;
					if(deptList!=null){
					for (int y =0; y<deptList.Length;y++){
						if(deptList[y].Value.ToLower().Contains(department.ToLower())){
								if (GUILayout.Button(items[x].placeName))
								{
									slectedItem = items[x].placeName;
									editing = false;
								}
						
						}
					}
					}
				}
			}
			GUILayout.EndScrollView();
		}

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button("Ok"))
		{
			department = initialText;

			clickAndDrag.isenabled = true;
			for (int i = 0; i < items.Length; i++)
			{
				rootPlace element  = items[i];
				if(element.placeName.Equals(slectedItem)){
					Debug.Log("location = "+element.xcordinate+","+element.ycordinate+","+element.zcordinate);
					float x,y,z;
					x = float.Parse(element.xcordinate);
					y = float.Parse(element.ycordinate);
					z = float.Parse(element.zcordinate);

					Follow.updateLocation(new Vector3(x,y,z), element.placeDesc,slectedItem);

				}
			}
			paused = false;
			slectedItem = oldslectedItem;
			editing = false;
		}
		if (GUILayout.Button("Canel"))
		{
			department = initialText;
			slectedItem = oldslectedItem;
			editing = false;
			clickAndDrag.isenabled = true;
			paused = false;
		}
		if (GUILayout.Button("Quit"))
		{
			department = initialText;
			clickAndDrag.isenabled = true;
			editing = false;
			Application.Quit();
		}
		GUILayout.EndHorizontal ();
	}

	public static string Check(Vector3 p){
		string names = "";
		for (int i = 0; i < items.Length; i++)
		{
			rootPlace element  = items[i];
			float x,y,z;
			x = float.Parse(element.xcordinate);
			y = float.Parse(element.ycordinate);
			z = float.Parse(element.zcordinate);
			Vector3 build = new Vector3(x,y,z);
			if(((Vector3.Distance (build,p)) < 20)){
				names = element.placeName;
			}

		}
		return names;
	
	}

	private void beginMessage(int id ){
		GUILayout.Label (messages);
		if (GUILayout.Button ("Ok")) {
			clickAndDrag.isenabled = true;
			GameObject thisCam = GameObject.Find("MainCamera");
			GameObject followCam = GameObject.Find("FollowCam");
			GameObject aboveCam = GameObject.Find("AboveCam");

			Debug.Log(aboveCam);
			if(aboveCam !=null){aboveCam.SetActive(false);}
			if(thisCam !=null){thisCam.SetActive(false);}

			followCam.SetActive(true);
			message = false;
			messageRectangle.Set(0,0,0,0);

		}

	}

}




