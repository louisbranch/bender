using UnityEngine;
using System.Collections;

public class TileGroupMovement : MonoBehaviour {

	// Up and down movement speed
	public float tileSpeed = 20.0f;
	
	public GameObject[] tiles;
	public Vector3 origin;
	public Vector3 destiny;
	public delegate void OnEndCallback(GameObject[] tiles);
	public OnEndCallback callback;

	private void Start() {
		transform.position = origin;
		for (int i = 0; i < tiles.Length; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			tile.renderer.enabled = true;
			tile.transform.parent = transform;
			tile.transform.localPosition = new Vector3(0, -i, 0);
		}
	}

	private void Update() {
		if (GameOptions.IsPaused ()) return;

		bool isStillParent = true;
		Vector3 origin = transform.localPosition;
		if (origin == destiny) {
			for (int i = 0; i < tiles.Length; i++) {
				GameObject tile = tiles[i];
				if (tile == null) break;

				// Another movement has taken place
				if (tile.transform.parent != transform) {
					isStillParent = false;
					break;
				}
				tile.transform.parent = transform.parent;
			}
			if (isStillParent && callback != null) callback(tiles);
			Destroy(gameObject);
		} else {
			float step = tileSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(origin, destiny, step);
		}
	}

	public void OnMovementEnd(OnEndCallback fn) {
		callback += fn;
	}

}