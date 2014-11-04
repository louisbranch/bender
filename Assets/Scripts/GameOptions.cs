using UnityEngine;
using System.Collections;

public class GameOptions {

	private static bool paused = false;

	public static void Pause() {
		paused = true;
	}

	public static void Unpause() {
		paused = true;
	}

	public static bool IsPaused() {
		return paused;
	}

}
