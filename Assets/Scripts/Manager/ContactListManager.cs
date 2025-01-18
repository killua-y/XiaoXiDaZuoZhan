using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactListManager : MonoBehaviour
{
    public GameObject personObject;
    public Transform ContactPersonParent;


    public void GenerateNewMessage(Person person)
    {
        ContactPersonBehavior newPerson = GameObject.Instantiate(personObject, ContactPersonParent).GetComponent<ContactPersonBehavior>();

        newPerson.SetupImageAndText(person);

    }
}
