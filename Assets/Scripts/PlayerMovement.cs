using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private int maxGridSize;

	private Grid grid;

	void Start () {
		grid = GameObject.Find("TilesGrid").GetComponent<Grid>();
		maxGridSize = grid.width - 1;
		int middleScreen = maxGridSize / 2;
		Move(middleScreen);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(transform.position.x - 1);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(transform.position.x + 1);
		}
	}

	private bool IsOutOfBound (float x) {
		return x < 0 || x > maxGridSize;
	}

	private void Move (float xAxis) {
		if (grid.enableCornerMovement) {
			if (xAxis < 0) xAxis = maxGridSize;
			else if (xAxis > maxGridSize) xAxis = 0;
		} else if (IsOutOfBound(xAxis)) {
			return;
		}
		Vector3 p = transform.position;
		transform.position = new Vector3(xAxis, p.y, p.z);
	}
}