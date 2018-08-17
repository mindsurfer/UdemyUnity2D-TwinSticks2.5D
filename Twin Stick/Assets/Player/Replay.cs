using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour 
{
  private const int _bufferSize = 100;
  private MyKeyFrame[] _keyFrames = new MyKeyFrame[_bufferSize];
  private Rigidbody _rigidBody;
  private bool _isFirstPlaybackFrame;
  private MyKeyFrame _resumeFrame;

  // Use this for initialization
  void Start () 
  {
    _rigidBody = GetComponent<Rigidbody>();
    _isFirstPlaybackFrame = true;
  }
  
  // Update is called once per frame
  void Update ()
  {
    Record();
  }

  public void Playback()
  {
    if (_isFirstPlaybackFrame)
    {
      _resumeFrame = new MyKeyFrame(Time.time, transform.position, transform.rotation) { Velocity = _rigidBody.velocity };
      _isFirstPlaybackFrame = false;
    }

    _rigidBody.isKinematic = true;
    var frame = Time.frameCount % _bufferSize;
    transform.position = _keyFrames[frame].Position;
    transform.rotation = _keyFrames[frame].Rotation;
  }

  public void Resume()
  {
    transform.position = _resumeFrame.Position;
    transform.rotation = _resumeFrame.Rotation;
    _rigidBody.velocity = _resumeFrame.Velocity;
    
    _isFirstPlaybackFrame = true;
  }

  private void Record()
  {
    _rigidBody.isKinematic = false;
    var frame = Time.frameCount % _bufferSize;
    var time = Time.time;
    _keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
  }
}

public class MyKeyFrame
{
  public float Time { get; private set; }
  public Vector3 Position { get; private set; }
  public Quaternion Rotation { get; private set; }
  public Vector3 Velocity { get; set; }

  public MyKeyFrame(float time, Vector3 pos, Quaternion rot)
  {
    Time = time;
    Position = pos;
    Rotation = rot;
  }
}
