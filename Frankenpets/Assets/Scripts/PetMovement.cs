using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public float walkSpeed = 0.8f;
    public float turnSpeed = 0.5f;
    public float turnBoost = 0.5f;
    public Transform frontHalf;
    public Transform backHalf;
    private FixedJoint fixedJoint;

    void Start()
    {
        fixedJoint = frontHalf.GetComponent<FixedJoint>();
    }

    void Update()
    {
        Vector3 frontHalfDirection = getFrontDirection();
        Vector3 backHalfDirection = getBackDirection();

        fixedJoint = frontHalf.GetComponent<FixedJoint>();

        if (fixedJoint != null) setSplitMovement(frontHalfDirection, backHalfDirection);
        else setConnectedMovement(frontHalfDirection, backHalfDirection);

        // setInPlace();
    }

    void setSplitMovement(Vector3 frontHalfDirection, Vector3 backHalfDirection)
    {
        // frontHalf.Translate(frontHalfDirection * Time.deltaTime, Space.Self);
        // backHalf.Translate(backHalfDirection * Time.deltaTime, Space.Self);
    }

    void setConnectedMovement(Vector3 frontHalfDirection, Vector3 backHalfDirection)
    {
        // Check if both halves are trying to turn together while connected
        if (bothHalvesTurningLeft()) 
        {
            // Faster turn speed if both halves are turning together
            backHalfDirection = new Vector3(0, backHalfDirection.y, backHalfDirection.z);
            frontHalfDirection += Vector3.left * turnBoost; 
        }

        if (bothHalvesTurningRight()) 
        {
            backHalfDirection = new Vector3(0, backHalfDirection.y, backHalfDirection.z);
            frontHalfDirection += Vector3.right * turnBoost; 
        }

        if (bothHalvesTurningOpposite())
        {
            
        }

        frontHalf.Translate(frontHalfDirection * Time.deltaTime, Space.Self);
        backHalf.Translate(backHalfDirection * Time.deltaTime, Space.Self);
    }

    // Set the direction vector for the front half. 
    // Do not use Time.deltaTime here; it's used in setMovement().
    Vector3 getFrontDirection()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey(KeyCode.LeftArrow)) direction += Vector3.left * turnSpeed;
        if (Input.GetKey(KeyCode.RightArrow)) direction += Vector3.right * turnSpeed;
        if (Input.GetKey(KeyCode.UpArrow)) direction += Vector3.forward * walkSpeed;
        if (Input.GetKey(KeyCode.DownArrow)) direction += Vector3.back * walkSpeed;

        return direction;
    }

    Vector3 getBackDirection()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey(KeyCode.A)) direction += Vector3.left * turnSpeed;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right * turnSpeed;
        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward * walkSpeed;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back * walkSpeed;

        return direction;
    }

    bool bothHalvesTurningLeft()
    {
        if (fixedJoint != null && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.A)) return true;
        
        return false;
    }

    bool bothHalvesTurningRight()
    {
        if (fixedJoint != null && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.D)) return true;
        
        return false;
    }

    bool bothHalvesTurningOpposite()
    {
        if (fixedJoint != null && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.D)) return true;

        if (fixedJoint != null && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.A)) return true;

        return false;
    }

    // temporary method for developing the backside's swinging motion
    // Plant the front half's body in place (mimicking the cat's climbing function)
    void setInPlace()
    {
        if (Input.GetKey(KeyCode.F)) 
        {
            frontHalf.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Front half constraints are frozen.");
        }

        if (Input.GetKey(KeyCode.G)) 
        {
            frontHalf.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Debug.Log("Unfrozen.");
        }
    }
}
