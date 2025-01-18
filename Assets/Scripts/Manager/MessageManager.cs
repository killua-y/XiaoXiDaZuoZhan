using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    public TextAsset MessageCSV; // 卡牌数据CSV文件

    public List<Person> ContactPersonList = new List<Person>();

    System.Random random;

    ContactListManager contactListManager;

    private new void Awake()
    {
        base.Awake();
        LoadCSV();
        random = new System.Random();

        contactListManager = FindAnyObjectByType<ContactListManager>();
    }

    private void LoadCSV()
    {
        string[] ContactPersonArray = MessageCSV.text.Split('\n');

        foreach (var row in ContactPersonArray)
        {
            Person newPerson = null;
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else
            {
                int index = 0;
                if (rowArray[0] != "")
                {
                    index = int.Parse(rowArray[0]);
                }

                string imageLocation = rowArray[1];
                string name = rowArray[2];
                string chatMessage = rowArray[3];
                string replyMessage1 = rowArray[4];
                string replyMessage2 = rowArray[5];
                string furtherReplyMessage = rowArray[6];

                newPerson = new Person(index, false, name, imageLocation, chatMessage, replyMessage1, replyMessage2, furtherReplyMessage);

                ContactPersonList.Add(newPerson);
            }

        }
    }

    // 生成一个新的消息
    public void GenerateNewMessage()
    {
        Person newPerson = GetRandomItem<Person>(ContactPersonList);
        contactListManager.GenerateNewMessage(newPerson);
    }

    // return一个随机单位，不会把list重洗
    public T GetRandomItem<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("The list is null or empty.");
        }

        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
