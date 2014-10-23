using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;

	void Start () {
		int middle = Grid.width / 2;
		Move(middle);
	}

	
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(player.transform.position.x - 1);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(player.transform.position.x + 1);
		}
	}

	bool IsOutOfBound (float x) {
		return x < 0 || x >= Grid.width;
	}

	void Move (float xAxis) {
		if (IsOutOfBound(xAxis)) return;
		Vector3 p = player.transform.position;
		player.transform.position = new Vector3(xAxis, p.y, p.z);
	}
}