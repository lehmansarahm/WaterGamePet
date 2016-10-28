using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Recognizer : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
			ToggleDrinking (true);
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () { /* do nothing */ }

	/// <summary>
	/// Called when the trackable state has changed.
	/// </summary>
	/// <param name="previousStatus">Previous status.</param>
	/// <param name="newStatus">New status.</param>
	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus) {
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			OnTrackingFound ();
		} else {
			OnTrackingLost ();
		}
	}

	/// <summary>
	/// Raises the tracking found event.
	/// </summary>
	private void OnTrackingFound() {
		if (mTrackableBehaviour is ImageTargetAbstractBehaviour) {
			ToggleDrinking (false);
		}
	}

	/// <summary>
	/// Raises the tracking lost event.
	/// </summary>
	private void OnTrackingLost() {
		if (mTrackableBehaviour is ImageTargetAbstractBehaviour) {
			ToggleDrinking (true);
		}
	}

	/// <summary>
	/// Toggles the drinking.
	/// </summary>
	/// <param name="isThirsty">If set to <c>true</c> is thirsty.</param>
	private void ToggleDrinking(bool isThirsty) {
		Text userText = 
			GameObject.Find ("UserText").GetComponent<Text> ();
		UnityEngine.UI.Image thirstyDog = 
			GameObject.Find ("ThirstyDog").GetComponent<UnityEngine.UI.Image> ();
		UnityEngine.UI.Image happyDog = 
			GameObject.Find ("HappyDog").GetComponent<UnityEngine.UI.Image> ();

		if (isThirsty) {
			userText.text = "Your puppy is thirsty!  Get it some water!";
			thirstyDog.transform.localScale = new Vector3 (5, 5, 5);
			happyDog.transform.localScale = new Vector3 (0, 0, 0);
		} else {
			userText.text = "Good job!  Your puppy is no longer thirsty!";
			happyDog.transform.localScale = new Vector3 (5, 5, 5);
			thirstyDog.transform.localScale = new Vector3 (0, 0, 0);
		}
	}
}