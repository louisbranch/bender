using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(player.transform.position.x - 1);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(player.transform.position.x + 1);
		}
	}

	bool isOutOfBound (float x) {
		float half = Grid.width / 2.0f;
		return x < -half || x >= half;
	}

	void Move (float xAxis) {
		if (isOutOfBound(xAxis)) return;
		player.transform.position = new Vector3(xAxis, player.transform.position.y, player.transform.position.z);
	}
}