using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Grid grid;
	private PlayerTiles tiles;

	private int minX;
	private int maxX;

	private float movementSpeed = 0.2f;
	private float nextMovement;

	private void Awake () {
		nextMovement = Time.time + movementSpeed;
	}

	private void Start () {
		grid = GetComponentInParent<Grid>();
		tiles = GetComponent<PlayerTiles>();
		SetBoundaries();
		Move(0);
	}
	
	private void Update () {
		int x = (int)transform.localPosition.x;

		if (Input.GetKey(KeyCode.LeftArrow)) {
			if (nextMovement < Time.time) {
				Move (x - 1);
				nextMovement = Time.time + movementSpeed;
			}
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			if (nextMovement < Time.time) {
				Move (x + 1);
				nextMovement = Time.time + movementSpeed;
			}
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Push(x);
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Pull(x);
		}
	}

	private bool IsOutOfBound (float x) {
		return x < minX || x > maxX;
	}

	private void Move (float xAxis) {
		if (grid.enableCornerMovement) {
			if (xAxis < minX) xAxis = maxX;
			else if (xAxis > maxX) xAxis = minX;
		} else if (IsOutOfBound(xAxis)) {
			return;
		}
		Vector3 p = transform.localPosition;
		transform.localPosition = new Vector3(xAxis, p.y, p.z);
	}

	private void Push (int x) {
		if (!tiles.IsEmpty()) {
			grid.PushTilesTo(x, tiles.Clear());
		}
	}
	
	private void Pull (int x) {
		if (tiles.IsEmpty()) {
			tiles.Add(grid.PullAnyTilesFrom(x));
		} else {
			string type = tiles.Type();
			tiles.Add(grid.PullTilesTypeFrom(x, type));
		}
	}

	private void SetBoundaries () {
		int half = grid.width / 2;
		minX = -half;
		maxX = grid.width % 2 == 0 ? half - 1 : half;
	}

}