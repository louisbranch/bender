using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;
	public bool hasCornerMovement = false;
	private int maxGridSize;

	void Start () {
		maxGridSize = Grid.width - 1;
		int middleScreen = maxGridSize / 2;
		Move(middleScreen);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(player.transform.position.x - 1);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(player.transform.position.x + 1);
		}
	}

	private bool IsOutOfBound (float x) {
		return x < 0 || x > maxGridSize;
	}

	private void Move (float xAxis) {
		if (hasCornerMovement) {
			if (xAxis < 0) xAxis = maxGridSize;
			else if (xAxis > maxGridSize) xAxis = 0;
		} else if (IsOutOfBound(xAxis)) {
			return;
		}
		Vector3 p = player.transform.position;
		player.transform.position = new Vector3(xAxis, p.y, p.z);
	}
}