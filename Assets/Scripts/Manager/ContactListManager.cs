using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactListManager : MonoBehaviour
{
    public GameObject personObject;
    public Transform ContactPersonParent;

    public List<ContactPersonBehavior> contactPersonBehaviors;

    public void GenerateNewMessage(Person person)
    {
        ContactPersonBehavior newPerson = null;

        // 查看对话列表是否已经存在
        foreach (var cpb in contactPersonBehaviors)
        {
            if (cpb.index == person.index)
            {
                newPerson = cpb;
                break;
            }
        }

        if (newPerson == null)
        {
            newPerson = GameObject.Instantiate(personObject, ContactPersonParent).GetComponent<ContactPersonBehavior>();
            contactPersonBehaviors.Add(newPerson);
        }

        // 确保新消息出现在最上面
        newPerson.transform.SetSiblingIndex(0);

        newPerson.SetupImageAndText(person);
    }
}
