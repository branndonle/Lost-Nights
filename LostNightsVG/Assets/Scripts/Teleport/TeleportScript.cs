using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
	public GameObject teleportDestination;
	public KeyCode interactionKey = KeyCode.E;
	public GameObject[] GOAudioSources;
	private AudioSource[] AudioSources;
	public AudioClip teleportSound;

	private bool isPlayerOnPlatform = false;
	private bool isTeleporting = false;
	private CharacterController characterController;

	public Rect BoxSize = new Rect(0, 0, 200, 100);
	public bool GuiToggle;
	public string Text = "Press E to teleport";

	private void Start()
	{
		AudioSources = new AudioSource[GOAudioSources.Length];
		GuiToggle = false;
		characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
		for (int i = 0; i < GOAudioSources.Length; i++)
		{
			AudioSources[i] = GOAudioSources[i].GetComponent<AudioSource>();
			AudioSources[i].playOnAwake = false;
		}
	}

	private void Update()
	{

		if (isPlayerOnPlatform && Input.GetKeyDown(interactionKey))
		{
			foreach (AudioSource audioSource in AudioSources)
			{
				audioSource.Play();
			}
			isTeleporting = true;
			if (isTeleporting)
			{
				TeleportPlayer();
				isPlayerOnPlatform = false;
				isTeleporting = false;
				GuiToggle = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerOnPlatform = true;
			GuiToggle = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerOnPlatform = false;
			GuiToggle = false;
		}
	}

	private void TeleportPlayer()
	{
		if (isPlayerOnPlatform && teleportDestination != null && characterController != null)
		{
			characterController.enabled = false;
			characterController.transform.position = teleportDestination.transform.position;
			characterController.enabled = true;
		}
	}
	void OnGUI()
	{
		if (GuiToggle == true)
		{
			GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
			GUI.Label(BoxSize, Text);
			GUI.EndGroup();

		}
	}
}