using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person
{

    public int index;
    public bool isBoss;
    public string name;
    public string imageLocation;

    public string chatMessage;

    public string replyMessage1;
    public string replyMessage2;

    public string furtherReplyMessage;

    public Person(int _index, bool _isBoss, string _name, string _imageLocation,
        string _chatMessage, string _replyMessage1, string _replyMessage2, string _furtherReplyMessage)
    {
        this.index = _index;
        this.isBoss = _isBoss;
        this.name = _name;
        this.imageLocation = _imageLocation;
        this.chatMessage = _chatMessage;
        this.replyMessage1 = _replyMessage1;
        this.replyMessage2 = _replyMessage2;
        this.furtherReplyMessage = _furtherReplyMessage;
    }
}

