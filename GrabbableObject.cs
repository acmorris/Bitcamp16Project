/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;

public class GrabbableObject : MonoBehaviour {

  public bool useAxisAlignment = false;
  public Vector3 rightHandAxis;
  public Vector3 objectAxis;

  public bool rotateQuickly = true;
  public bool centerGrabbedObject = false;

  public Rigidbody breakableJoint;
  public float breakForce;
  public float breakTorque;

  protected bool grabbed_ = false;
  protected bool hovered_ = false;

  public bool IsHovered() {
    return hovered_;
  }

  public bool IsGrabbed() {
    return grabbed_;
  }

  public virtual void OnStartHover() {
    hovered_ = true;
  }

  public virtual void OnStopHover() {
    hovered_ = false;
  }

  public virtual void OnGrab() {
    grabbed_ = true;
    hovered_ = false;

    if (breakableJoint != null) {
      Joint breakJoint = breakableJoint.GetComponent<Joint>();
      if (breakJoint != null) {
        breakJoint.breakForce = breakForce;
        breakJoint.breakTorque = breakTorque;
      }
    }
  }

  public virtual void OnRelease() {
    grabbed_ = false;

    if (breakableJoint != null) {
      Joint breakJoint = breakableJoint.GetComponent<Joint>();
      if (breakJoint != null) {
        breakJoint.breakForce = Mathf.Infinity;
        breakJoint.breakTorque = Mathf.Infinity;
      }
    }
  }
	public float colourChangeDelay = 0.5f;
	float currentDelay = 0f;
	bool colourChangeCollision = false;
	public float timeLastPlayed = 0.0f;

	void Update() {
		checkColourChange();
	}

	void OnTriggerEnter(Collider other) {
		if (timeLastPlayed == 0.0f) {
			timeLastPlayed = Time.time;
			Debug.Log("SetCurTime: " + timeLastPlayed);
			GetComponent<AudioSource>().Play();
		}
		else if (timeLastPlayed + .25 < Time.time) {
			Debug.Log("Threshold passed!");
			Debug.Log("Curtime: " + Time.time);
			timeLastPlayed = Time.time;
			GetComponent<AudioSource>().Play();
		}
		Debug.Log("Contact was made!");
		colourChangeCollision = true;
		currentDelay = Time.time + colourChangeDelay;
	}

	void checkColourChange() {        
		if(colourChangeCollision) {
			
			transform.GetComponent<Renderer>().material.color = Color.yellow;
			if(Time.time > currentDelay) {
				transform.GetComponent<Renderer>().material.color = Color.white;
				colourChangeCollision = false;
			}
		}
	}


}
