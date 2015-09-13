using UnityEngine;
using System.Collections;
using BladeCast;

public class PlayerMessanger : MonoBehaviour {
	
	public GameObject[] players;
	
	float delay = 0.2f;
	float elapsedTime = 0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (elapsedTime >= delay) {
			elapsedTime -= delay;
			// send message to controller
			SendPositionToController ();
		}
		
		elapsedTime += Time.deltaTime;
	}
	
	void SendPositionToController() {
		// change position to bytes
		int positions = 0x0;
		int[] pos = {
			ConvertPositionToByte(players[0]),
			ConvertPositionToByte(players[1]),
			ConvertPositionToByte(players[2]),
			ConvertPositionToByte(players[3])
		};
		
		positions |= pos [0] << 24;
		positions |= pos [1] << 16;
		positions |= pos [2] << 8;
		positions |= pos [3];

		Debug.Log ("send position message: " + positions);
		BCMessenger.Instance.SendToListeners ("update_position", "pos", positions, -1);
	}
	
	int ConvertPositionToByte(GameObject gameObject) {
		int posX = Mathf.RoundToInt(gameObject.transform.position.x);
		int posY = Mathf.RoundToInt(gameObject.transform.position.y);
		posX = InsideBound (-1, 14, posX) + 1;
		posY = InsideBound (-1, 10, posY) + 1;
		
		int result = 0x0;
		result |= posX << 4;
		result |= posY;
		
		return result;
	}
	
	int InsideBound(int min, int max, int val) {
		if (val < min)
			val = min;
		if (val > max)
			val = max;
		return val;
	}
}
