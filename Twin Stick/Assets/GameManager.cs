using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour 
{
  private MyKeyFrame _continueKeyFrame;
  private bool _isRecording;
  private Replay[] _replayObjects;
  private bool _requiresResume;
  private float _fixedDeltaTime;
  private bool _isPaused;

  // Use this for initialization
  void Start () 
  {
    _replayObjects = FindObjectsOfType<Replay>();
    _isRecording = true;
    _requiresResume = false;
    _fixedDeltaTime = Time.fixedDeltaTime;
    _isPaused = false;
  }
  
  // Update is called once per frame
  void Update () 
  {
    _isRecording = !CrossPlatformInputManager.GetButton("Fire1");

    if (!_isRecording)
    {
      _requiresResume = true;
      ReplayFrame();
    }
    //else
    //  ResumeOriginalFrame();

    if (Input.GetKeyDown(KeyCode.P) && !_isPaused)
      Pause();
    else if (Input.GetKeyDown(KeyCode.P) && _isPaused)
      Unpause();

    print("updating...");
  }

  void FixedUpdate()
  {
    print("fixed updating...");
  }

  void OnApplicationPause(bool pauseStatus)
  {
    print("is paused");
    _isPaused = pauseStatus;
  }

  private void Pause()
  {
    Time.timeScale = 0;
    Time.fixedDeltaTime = 0;
    print("pausing...");
  }

  private void Unpause()
  {
    Time.timeScale = 1;
    Time.fixedDeltaTime = _fixedDeltaTime;
  }

  private void ReplayFrame()
  {
    foreach (var replayObject in _replayObjects)
    {
      replayObject.Playback();
    }
  }

  private void ResumeOriginalFrame()
  {
    if (!_requiresResume)
      return;

    foreach (var replayObject in _replayObjects)
    {
      replayObject.Resume();
    }

    _requiresResume = false;
  }
}
