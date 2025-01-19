using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    public TextAsset MessageCSV; // wechat数据CSV文件
    public TextAsset AppMessageCSV; // App数据CSV文件

    public List<Person> ContactPersonList = new List<Person>();
    public List<AppMessage> AppMessageList = new List<AppMessage>();

    System.Random random;

    ContactListManager contactListManager;

    AppMessageManager appMessageManager;

    AppMessage WechatMessage = new AppMessage(0, "appIcon/微信", "你收到了一条新消息");

    private new void Awake()
    {
        base.Awake();
        LoadCSV();
        LoadAppCSV();
        random = new System.Random();

        contactListManager = FindAnyObjectByType<ContactListManager>();
        appMessageManager = FindAnyObjectByType<AppMessageManager>();
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
                string chatMessage = ReplaceSemicolonWithComma(rowArray[3]);
                string replyMessage1 = ReplaceSemicolonWithComma(rowArray[4]);
                string replyMessage2 = ReplaceSemicolonWithComma(rowArray[5]);
                string furtherReplyMessage = ReplaceSemicolonWithComma(rowArray[6]);

                bool goodReply = false;
                if (rowArray[7] == "g")
                {
                    goodReply = true;
                }

                newPerson = new Person(index, false, name, imageLocation,
                    chatMessage, replyMessage1, replyMessage2, furtherReplyMessage,
                    goodReply);

                ContactPersonList.Add(newPerson);
            }
        }

    }

    private void LoadAppCSV()
    {
        string[] ContactPersonArray = AppMessageCSV.text.Split('\n');

        foreach (var row in ContactPersonArray)
        {
            AppMessage newAppMessage = null;
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
                // 没用
                string name = rowArray[2];
                string chatMessage = ReplaceSemicolonWithComma(rowArray[3]);

                newAppMessage = new AppMessage(index, imageLocation, chatMessage);

                AppMessageList.Add(newAppMessage);
            }
        }
    }

    public void GameStart()
    {

    }

    // 生成一个新的消息
    public void GenerateNewMessage()
    {
        // 不包括还没收到回复的消息
        List<int> excludedIndexes = new List<int>();
        foreach (var cpb in contactListManager.contactPersonBehaviors)
        {
            if (!cpb.hasReplayed)
            {
                excludedIndexes.Add(cpb.index);
            }
        }

        // 让还没有收到回复的人不要发送新的消息
        List<Person> newPersonList = new List<Person>();

        foreach (Person p in ContactPersonList)
        {
            if (!excludedIndexes.Contains(p.index))
            {
                newPersonList.Add(p);
            }
        }

        if (newPersonList.Count == 0)
        {
            Debug.Log("Everyone is wait for replay, do not send more");
            return;
        }

        Person newPerson = GetRandomItem<Person>(newPersonList);
        contactListManager.GenerateNewMessage(newPerson);

        // 通知微信收到消息
        appMessageManager.GenerateNewMessage(WechatMessage);

        // 增加压力值
        StressManager.Instance.IncreaseStress(5);
    }

    // 生成一个新的App消息
    public void GenerateNewAppMessage()
    {
        AppMessage newAppMessage = GetRandomItem<AppMessage>(AppMessageList);
        appMessageManager.GenerateNewMessage(newAppMessage);

        // 增加压力值
        StressManager.Instance.IncreaseStress(5);
    }

    // return一个随机单位，不会把list重洗
    public Person GetRandomItem<Person>(List<Person> personList)
    {
        int randomIndex = random.Next(personList.Count);
        return personList[randomIndex];
    }

    // helper
    private string ReplaceSemicolonWithComma(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input; // Return the original input if it's null or empty
        }

        return input.Replace(";", ",");
    }


}
