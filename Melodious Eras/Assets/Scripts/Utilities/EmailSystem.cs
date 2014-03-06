using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class EmailSystem : MonoBehaviour {

    public TextAsset emailFile;
    private Dictionary<string, Email> emails;
    private XmlDocument xmlDoc;
	// Use this for initialization
	void Awake () {
        xmlDoc = new XmlDocument();
        emails = new Dictionary<string, Email>(50);
        xmlDoc.LoadXml(emailFile.text);
        
        ParseEmails();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void ParseEmails()
    {
        foreach (XmlNode node in xmlDoc.SelectNodes("emails/email"))
        {
            string id = node.Attributes["id"].Value;
            string hasRead = node.Attributes["hasread"].Value;

            string from = node.SelectSingleNode("from").InnerText;
            string to = node.SelectSingleNode("to").InnerText;
            string title = node.SelectSingleNode("title").InnerText;
            string body = node.SelectSingleNode("body").InnerText;


            XmlNode keycodeNode = node.SelectSingleNode("keycode"); //keycodes in emails are optional
            if (keycodeNode != null)
                emails.Add(id, new Email(id, from, to, title, body, hasRead, keycodeNode.InnerText));
            else
                emails.Add(id, new Email(id, from, to, title, body, hasRead));
        }
    }
    public Email GetEmail(string id)
    {
        return emails[id];
    }
}

public struct Email
{
    public string id, from, to, title, body, keycode;
    public bool hasRead;
    public Email(string idP, string fromP, string toP, string titleP, string bodyP, string hasReadP)
    {
        this.id = idP;
        this.from = fromP;
        this.to = toP;
        this.title = titleP;
        this.body = bodyP;
        this.hasRead = hasReadP == "true";
        keycode = null;
    }
    public Email(string idP, string fromP, string toP, string titleP, string bodyP, string hasReadP, string keycodeP)
    {
        this.id = idP;
        this.from = fromP;
        this.to = toP;
        this.title = titleP;
        this.body = bodyP;
        this.hasRead = hasReadP == "true";
        this.keycode = keycodeP;
    }
    public bool HasKeycode()
    {
        return keycode != null;
    }
}