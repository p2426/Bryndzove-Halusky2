﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LogIn : MonoBehaviour {

    private InputField m_NickName;
    private InputField m_PassName;
    public GameObject DBM;
    private DatabaseManager Database;

    // variables that need DB replies
    [Header("Database Reply Variables")]
    public Text loginReply;

    void Start()
    {
        m_NickName = this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<InputField>();
        m_PassName = this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<InputField>();
        Database = DBM.GetComponent<DatabaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Database.loginBool == true) { LoginFinished(); }
        else { loginReply.text = Database.loginReply; }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && gameObject.transform.GetChild(1).gameObject.activeInHierarchy == true)
        {
            Database.Login(m_NickName.text, m_PassName.text);
        }

        // If player is typing down his login or password and press tab, change focus to other input field
        if (m_NickName.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            m_NickName.DeactivateInputField();
            m_PassName.ActivateInputField();
        }
        else if (m_PassName.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            m_NickName.ActivateInputField();
            m_PassName.DeactivateInputField();
        }
    }

    // button functions
    private void LoginFinished()
    {
        loginReply.text = Database.createAccountReply;
        PhotonNetwork.player.NickName = m_NickName.text;
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        enabled = false;
    }
}
