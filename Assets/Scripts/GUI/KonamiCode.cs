using UnityEngine;

public class KonamiCode : MonoBehaviour
{
	public float timeKey = 0f, timeCode = 0f;
	public GameObject receiver;
	public string message;
	
	KeyCode[] keycodes;
	int index = 0;
	float timeSinceStartCode = 0f, timeSinceLastKey = 0f;
	
	void Awake()
	{
		this.keycodes = new KeyCode[]
		{
			KeyCode.UpArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.DownArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.B,
			KeyCode.A
		};
	}
	
	void OnEnable()
	{
		if (this.receiver == null)
			this.enabled = false;
	}
	
	void Update()
	{
		this.timeSinceLastKey += Time.deltaTime;
		this.timeSinceStartCode += Time.deltaTime;
		if (Input.anyKeyDown == false) return;
		if (Input.GetKeyDown(this.keycodes[index]) == false || this.timeSinceStartCode >= this.timeCode || this.timeSinceLastKey >= this.timeKey)
		{
			this.index = 0;
		}
		if (Input.GetKeyDown(this.keycodes[index]))
		{
			if (this.index == 0)
			{
				this.timeSinceStartCode = 0f;
			}
			this.timeSinceLastKey = 0f;
			this.index++;
			if (this.index >= this.keycodes.Length)
			{
	
				Debug.Log(message);
				Follow.disable();
				if (this.receiver != null)
					this.receiver.SendMessage(this.message, SendMessageOptions.DontRequireReceiver);
				this.index = 0;
			}
		}
	}
}