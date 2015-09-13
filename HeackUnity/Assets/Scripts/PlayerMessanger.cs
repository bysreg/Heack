using UnityEngine;
using System.Collections;
using BladeCast;
using Heack;

public class PlayerMessanger : MonoBehaviour {
	
	public Player[] players;
	
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
		int[] pos = new int[] {
			ConvertPositionToByte (players [0].gameObject),
			ConvertPositionToByte (players [1].gameObject),
			ConvertPositionToByte (players [2].gameObject),
			ConvertPositionToByte (players [3].gameObject)
		};

		positions |= (pos[0] << 24);
		positions |= (pos[1] << 16);
		positions |= (pos[2] << 8);
		positions |= pos[3];

		BCMessenger.Instance.SendToListeners ("update_position", "pos", positions.ToString(), -1);
	}
	
    public void SendDiedSignalToController(int playerIndex)
    {
        BCMessenger.Instance.SendToListeners("died_signal", "spawn_time", players[playerIndex].respawner.maxSpawnTime, -1);
    }

    public void SendSpawnSignalToController(int playerIndex)
    {
        BCMessenger.Instance.SendToListeners("spawn_signal", playerIndex);
    }

	int ConvertPositionToByte(GameObject obj) {
		int posX = (int) obj.transform.position.x;
		int posY = (int) obj.transform.position.y;

		posX = InsideBound (-1, 14, posX) + 1;
		posY = InsideBound (-1, 10, posY) + 1;

		int result = 0x0;
		result |= ((byte)posX) << 4;
		result |= (byte)posY;

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
