﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class used to store information of an event
/// </summary>
public class EventInfo : MonoBehaviour {

	// ----------VARIABLES----------

	// Reference to the puzzle camera controller, to move the camera to view this event
	private PuzzleCameraController puzzleCameraController;

	// Reference to the panel manager, so we can tell it to remove us from the list when necessary
	private  EventPanelManager man;

	// Name of this event
	private string name;

	// Highest cumulative energy of this event
	private float peak_energy;

	// Date and time the event was swiped
	private DateTime date_captured;

	// The start of the neutrino path of this event, in world coordinates
	private Vector3 pathStart;

	// The end of the neutrino path of this event, in world coordinates
	private Vector3 pathEnd;

	// The list of doms affected by this event and their final states
	private List<VisualizeEvent.DomState> domStates;

	// ----------END VARIABLES----------

	/// <summary>
	/// Start used for initialization
	/// </summary>
	void Start() {
		man = this.transform.parent.GetComponent<EventPanelManager> ();
		GameObject puzzleCamera = GameObject.Find ("Puzzle Camera");

		if (puzzleCamera == null) {
			Debug.LogError ("Puzzle camera not present in scene");
		}

		puzzleCameraController = puzzleCamera.GetComponent<PuzzleCameraController> ();
	}

	/// <summary>
	/// Constructor for an EventInfo (not used)
	/// </summary>
	/// <param name="nameP">Name of the event</param>
	/// <param name="energy">Cumulative energy for the event</param>
	/// <param name="pathStart">The start of the neutrino path for this event</param>
	/// <param name="pathEnd">The end of the neutrino path for this event</param>
	public EventInfo(string nameP, float energy, Vector3 pathStart, Vector3 pathEnd) {
		this.name = nameP;
		this.peak_energy = energy;
		this.date_captured = DateTime.Now;
		this.domStates = new List<VisualizeEvent.DomState> ();
		this.pathStart = pathStart;
		this.pathEnd = pathEnd;
	}

	/// <summary>
	/// Copy constructor for an EventInfo (unused)
	/// </summary>
	/// <param name="other">The EventInfo to copy</param>
	public EventInfo(EventInfo other) {
		if (other != null) {
			this.name = other.name;
			this.peak_energy = other.peak_energy;
			this.date_captured = other.date_captured;
			this.domStates = other.domStates;
			this.pathStart = pathStart;
			this.pathEnd = pathEnd;
		}
	}

	/// <summary>
	/// Sets the name
	/// </summary>
	/// <param name="name">The name to set</param>
	public void setName(string name) {
		this.name = name;
	}

	/// <summary>
	/// Sets the energy
	/// </summary>
	/// <param name="energy">The energy value to set</param>
	public void setEnergy(float energy) {
		this.peak_energy = energy;
	}

	/// <summary>
	/// Sets the date
	/// </summary>
	/// <param name="date">The date to set</param>
	public void setDate(DateTime date) {
		this.date_captured = date;
	}

	/// <summary>
	/// Sets the list of DomStates for this event
	/// </summary>
	/// <param name="domStates">The List of DomStates to set</param>
	public void setDomStates(List<VisualizeEvent.DomState> domStates) {
		this.domStates = domStates;
	}

	/// <summary>
	/// Sets the start position for the event path
	/// </summary>
	/// <param name="vStart">The start position in world coordinates</param>
	public void setStart(Vector3 vStart) {
		this.pathStart = vStart;
	}

	/// <summary>
	/// Sets the end position for the event path
	/// </summary>
	/// <param name="vEnd">The end position in world coordinates</param>
	public void setEnd(Vector3 vEnd) {
		this.pathEnd = vEnd;
	}

	/// <summary>
	/// Gets the start position of the path
	/// </summary>
	/// <returns>The start position of the path</returns>
	public Vector3 getStart() {
		return this.pathStart;
	}

	/// <summary>
	/// Gets the end position of the path
	/// </summary>
	/// <returns>The end position of the path</returns>
	public Vector3 getEnd() {
		return this.pathEnd;
	}
		
	/// <summary>
	/// Returns the DomStates of this event
	/// </summary>
	/// <returns>The DOM states.</returns>
	public List<VisualizeEvent.DomState> getDomStates() {
		return this.domStates;
	}
		
	/// <summary>
	/// Used for the button on the EventPanels, this moves the puzzle camera
	/// to focus on this event
	/// </summary>
	public void GoToPuzzleView() {
		puzzleCameraController.MoveCamera (this);
	}

	/// <summary>
	/// Resets the puzzle camera to default position, and removes this event from the list (permanantly)
	/// </summary>
	public void delete() {
		puzzleCameraController.MoveCamera (null);
		man.removeEvent (this.gameObject);
	}
}
