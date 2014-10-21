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

	void Move (float xAxis) {
		player.transform.position = new Vector3(xAxis, player.transform.position.y, player.transform.position.z);
	}
}