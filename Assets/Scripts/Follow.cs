using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpPoseAnimation;
	public GameObject ThisThing;
	public float walkMaxAnimationSpeed   = 0.75f;
	public float trotMaxAnimationSpeed  = 1.0f;
	public float runMaxAnimationSpeed  = 1.0f;
	public float jumpAnimationSpeed  = 1.15f;
	public float landAnimationSpeedt = 1.0f;



	private Animation _animation;
	private bool hasStop = false;
	private  static string title;
	private  static string description;
	public static Vector3 newLocation;
	// Use this for initialization
	void Start () {

	
		if (idleAnimation != null) {
			animation.AddClip (idleAnimation, "stop");
		}
		if (walkAnimation != null) {
			animation.AddClip (walkAnimation, "walk");
		}
		if (runAnimation != null) {
			animation.AddClip (runAnimation, "run");
		}
	}

	public static void updateLocation(Vector3 newPos, string desc, string placename){
			newLocation = newPos;
			GameObject followCam = GameObject.Find ("FollowCam");
			GameObject jeff = GameObject.Find ("3rd Person Controller");
		followCam.transform.rotation = jeff.transform.rotation;
		title = placename;
		description = desc;

		}
	// Update is called once per frame
	 void Update () {

		if (GameObject.Find ("3rd Person Controller").GetComponent <NavMeshAgent>().enabled ==true) {
						GetComponent<NavMeshAgent> ().destination = newLocation;


						if ((Vector3.Distance (ThisThing.transform.position, newLocation)) < 2) {
								animation.Play ("stop");
								if (hasStop) {
										ThisThing.transform.rotation *= Quaternion.AngleAxis (180, transform.up);
										hasStop = false;
										MainGUI.showMessage (description, title);
								}
						} else {
								animation.Play ("run");
								hasStop = true;
						}
				}
	}


	public static void disable(){
		GameObject.Find ("3rd Person Controller").GetComponent <NavMeshAgent>().enabled = false;
		GameObject.Find ("3rd Person Controller").GetComponent <ThirdPersonCamera>().enabled = true;
		GameObject.Find ("3rd Person Controller").GetComponent <ThirdPersonController>().enabled = true;
//		UnitSpawner.play = true;
	
	
	}
}
