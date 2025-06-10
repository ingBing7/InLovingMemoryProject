using UnityEngine;

public class MoveTheFuckingCube : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float speed = 5f;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Go"))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            m_Rigidbody.AddForce(transform.forward * speed);
        }
    }

    public void ObtainGift()
    {

        ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("hasGift")).value = true;
        Debug.Log(((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("hasGift")).value);
    }
}
