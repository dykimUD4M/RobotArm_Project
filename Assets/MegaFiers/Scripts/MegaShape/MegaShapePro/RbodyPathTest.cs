﻿using UnityEngine;

namespace MegaFiers
{
	[RequireComponent(typeof(Rigidbody))]
	public class RbodyPathTest : MonoBehaviour
	{
		public MegaShape	path;					// The Shape that will attract the rigid body
		public int			curve		= 0;		// The sub curve of that shape usually 0
		public bool			usealpha	= false;	// Set to true to use alpha value instead of finding the nearest point on the curve.
		public float		impulse		= 10.0f;	// The force that will applied if the rbody is 1 unit away from the curve
		public float		inputfrc	= 10.0f;	// Max forcce for user input
		public bool			align		= true;		// Should rigid body align to the spline direction
		public float		alpha		= 0.0f;		// current position on spline, The alpha value to use is usealpha mode set, allows you to set the point on the curve to attract the rbody (0 - 1)
		public float		delay		= 1.0f;		// how quickly user input gets to max force
		public float		drag		= 0.0f;		// slows object down when moving
		public float		jump		= 10.0f;	// Jump force to apply when space is pressed
		public float		breakforce	= 100.0f;	// force above which the rigidbody will break free from the path
		public bool			connected	= true;		// Controls whether the object is connected to spline or not
		Rigidbody			rb;
		float				drive		= 0.0f;
		float				vel			= 0.0f;
		float				tfrc		= 0.0f;
		Vector3				nps;

		public MegaAxis		forwardAxis	= MegaAxis.Z;
		public Vector3		rot			= Vector3.zero;
		public float		horizontalPos;
		public float		hSpeed		= 1.0f;
		public float		hLimit		= 0.5f;

		void Start()
		{
			rb = GetComponent<Rigidbody>();

			for ( int i = 0; i < 4; i++ )
				Position();
		}

		void Update()
		{
			tfrc = 0.0f;
			if ( Input.GetKey(KeyCode.UpArrow) )
				tfrc = -inputfrc;
			else
			{
				if ( Input.GetKey(KeyCode.DownArrow) )
					tfrc = inputfrc;
			}

			if ( Input.GetKey(KeyCode.RightArrow) )
			{
				horizontalPos -= hSpeed * Time.deltaTime;
				if ( horizontalPos < -hLimit )
					horizontalPos = -hLimit;
			}

			if ( Input.GetKey(KeyCode.LeftArrow) )
			{
				horizontalPos += hSpeed * Time.deltaTime;
				if ( horizontalPos > hLimit )
					horizontalPos = hLimit;
			}

			if ( Input.GetKeyDown(KeyCode.Space) )
				rb.AddForce(Vector3.up * jump);

			drive = Mathf.SmoothDamp(drive, tfrc, ref vel, delay);

			Debug.DrawLine(transform.position, nps);
		}


		// Position object on spline
		public void Position()
		{
			if ( path && rb && connected )
			{
				Vector3 p = rb.position;    //transform.position;

				Vector3 tangent = Vector3.zero;
				int kn = 0;

				Vector3 np = Vector3.zero;
				if ( usealpha )
					np = path.transform.TransformPoint(path.InterpCurve3D(curve, alpha, true));
				else
					np = path.FindNearestPointWorldXZ(p, 15, ref kn, ref tangent, ref alpha);

				Vector3 p1 = path.transform.TransformPoint(path.InterpCurve3D(curve, alpha + 0.0001f, true));
				Vector3 cross = Vector3.Cross((p1 - np).normalized, Vector3.up) * horizontalPos;
				np += cross;    //Vector3.Cross((p1 - np).normalized, Vector3.up) * horizontalPos;

				nps = np;
				np.y = p.y;
				Vector3 dir = np - p;
				dir.y = 0.0f;

				Vector3 iforce = dir * impulse;

				float mag = iforce.magnitude / Time.fixedDeltaTime;
				if ( mag > breakforce )
				{
					connected = false;
					rb.angularVelocity = Vector3.zero;
				}

				if ( connected )
				{
					rb.AddForce(iforce, ForceMode.Impulse);

					np.y = p.y;
					rb.MovePosition(np);

					p1 = path.transform.TransformPoint(path.InterpCurve3D(curve, alpha + 0.0001f, true));
					p1 += cross;
					p1.y = p.y;

					if ( align )
					{
						Vector3 ndir = (p1 - np).normalized;
						Vector3 rdir = Vector3.forward; //transform.forward;

						switch ( forwardAxis )
						{
							case MegaAxis.X:
								rdir = transform.right;
								break;
							case MegaAxis.Y:
								rdir = transform.up;
								break;
							case MegaAxis.Z:
								rdir = transform.forward;
								break;
						}
						rdir.y = 0.0f;
						rdir = rdir.normalized;

						float angle = Vector3.Angle(rdir, ndir);

						cross = Vector3.Cross(rdir, ndir);

						if ( cross.y < 0.0f )
							angle = -angle;

						Quaternion erot = Quaternion.Euler(rot);
						Quaternion qrot = rb.rotation;
						Quaternion yrot = Quaternion.Euler(new Vector3(0.0f, angle, 0.0f)); //LookRotation(p1 - np);	//.eulerAngles;

						rb.MoveRotation(qrot * yrot * erot);
					}

#if UNITY_6000_0_OR_NEWER
					if ( drag != 0.0f )
						rb.AddForce(-rb.linearVelocity * drag);
#else
					if ( drag != 0.0f )
						rb.AddForce(-rb.velocity * drag);
#endif
					if ( drive != 0.0f )
						rb.AddForce((np - p1).normalized * drive, ForceMode.Force);
				}
			}
		}

		void FixedUpdate()
		{
			Position();
		}
	}
}