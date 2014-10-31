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
		transform.localPosition = origin;
		for (int i = 0; i < tiles.Length; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			tile.renderer.enabled = true;
			tile.transform.parent = transform;
			tile.transform.localPosition = new Vector3(0, -i, 0);
		}
	}

	private void Update() {
		if (transform.localPosition == destiny) {
			for (int i = 0; i < tiles.Length; i++) {
				GameObject tile = tiles[i];
				if (tile == null) break;
				tile.transform.parent = transform.parent;
			}
			if (callback != null) callback(tiles);
			Destroy(gameObject);
		} else {
			float step = tileSpeed * Time.deltaTime;
			Vector3 origin = transform.localPosition;
			transform.localPosition = Vector3.MoveTowards(origin, destiny, step);
		}
	}

	public void OnMovementEnd(OnEndCallback fn) {
		callback += fn;
	}

}