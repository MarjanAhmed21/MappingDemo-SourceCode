using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveScript : MonoBehaviour
{

    public float jumpspeed = 15;
    public float turnspeed = 100;
    public float walkspeed = 10;
    public float runMultiplier = 3;
    public float gravity = 20;
    public float airtime = 0;
    public float jumptime = 1;
    public float fall = 1;

    public CollisionFlags collideflags;


    private Vector3 movedirection;
    private CharacterController ctrl;
    private Transform mytransform;

    void Start()
    {

        mytransform = transform;
        ctrl = GetComponent<CharacterController>();
        movedirection = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {

        Turn();
        Walk();
        Strafe();
        Jump();

        ctrl.Move(movedirection * Time.deltaTime);
    }

    void Turn()
    {

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            mytransform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnspeed, 0);
        }
    }

    void Walk()
    {

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            if (Input.GetButton("Sprint"))
            {
                ctrl.Move(mytransform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * walkspeed * runMultiplier * Time.deltaTime);
            }
            else
            {
                ctrl.Move(mytransform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * walkspeed * Time.deltaTime);
            }
        }
    }

    void Strafe()
    {

        if (Mathf.Abs(Input.GetAxis("Strafe")) > 0)
        {
            ctrl.Move(mytransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * walkspeed * Time.deltaTime);
        }
    }

    void Jump()
    {

        movedirection.y -= gravity * Time.deltaTime;
        collideflags = ctrl.Move(movedirection * Time.deltaTime);

        if (ctrl.isGrounded)
        {

            airtime = 0;

            if (Input.GetButton("Jump"))
            {
                if (airtime < jumptime)
                {
                    movedirection.y += jumpspeed;
                }
            }
        }
        else
        {

            if ((collideflags & CollisionFlags.CollidedBelow) == 0)
            {
                airtime += Time.deltaTime;

                if (airtime > fall)
                {
                    Fall();
                }

            }
        }
    }

    public void Fall()
    {

    }
}
