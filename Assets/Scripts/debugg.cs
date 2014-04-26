using UnityEngine;
using System.Collections;

public class debugg : MonoBehaviour {
	public Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Debug.Log ("<xcordinate>"+player.position.x + "</xcordinate> <ycordinate> " + player.position.y + "</ycordinate>  <zcordinate>" + player.position.z+"</zcordinate>");
				}
	}
}
