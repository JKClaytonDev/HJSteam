using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textInput : MonoBehaviour
{
    float deleteTime;
    public string input = "";
    string lastInput;
    public AudioClip[] sounds;
    // Update is called once per frame
    void Update()
    {
        lastInput = input;
        if (Input.GetKeyDown(KeyCode.A))
            input += "a";
        if (Input.GetKeyDown(KeyCode.B))
            input += "b";
        if (Input.GetKeyDown(KeyCode.C))
            input += "c";
        if (Input.GetKeyDown(KeyCode.D))
            input += "d";
        if (Input.GetKeyDown(KeyCode.E))
            input += "e";
        if (Input.GetKeyDown(KeyCode.F))
            input += "f";
        if (Input.GetKeyDown(KeyCode.G))
            input += "g";
        if (Input.GetKeyDown(KeyCode.H))
            input += "h";
        if (Input.GetKeyDown(KeyCode.I))
            input += "i";
        if (Input.GetKeyDown(KeyCode.J))
            input += "j";
        if (Input.GetKeyDown(KeyCode.K))
            input += "k";
        if (Input.GetKeyDown(KeyCode.L))
            input += "l";
        if (Input.GetKeyDown(KeyCode.N))
            input += "n";
        if (Input.GetKeyDown(KeyCode.M))
            input += "m";
        if (Input.GetKeyDown(KeyCode.O))
            input += "o";
        if (Input.GetKeyDown(KeyCode.P))
            input += "p";
        if (Input.GetKeyDown(KeyCode.Q))
            input += "q";
        if (Input.GetKeyDown(KeyCode.R))
            input += "r";
        if (Input.GetKeyDown(KeyCode.S))
            input += "s";
        if (Input.GetKeyDown(KeyCode.T))
            input += "t";
        if (Input.GetKeyDown(KeyCode.U))
            input += "u";
        if (Input.GetKeyDown(KeyCode.V))
            input += "v";
        if (Input.GetKeyDown(KeyCode.W))
            input += "w";
        if (Input.GetKeyDown(KeyCode.X))
            input += "x";
        if (Input.GetKeyDown(KeyCode.Y))
            input += "y";
        if (Input.GetKeyDown(KeyCode.Z))
            input += "z";
        if (lastInput != input)
        {
            GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        }
        if (Input.GetKeyDown("1"))
            input += "1";
        if (Input.GetKeyDown("2"))
            input += "2";
        if (Input.GetKeyDown("3"))
            input += "3";
        if (Input.GetKeyDown("4"))
            input += "4";
        if (Input.GetKeyDown("5"))
            input += "5";
        if (Input.GetKeyDown("6"))
            input += "6";
        if (Input.GetKeyDown("7"))
            input += "7";
        if (Input.GetKeyDown("8"))
            input += "8";
        if (Input.GetKeyDown("9") && !Input.GetKey(KeyCode.LeftShift))
            input += "(";
        else if (Input.GetKeyDown("9"))
            input += "9";
        if (Input.GetKeyDown("0") && !Input.GetKey(KeyCode.LeftShift))
            input += ")";
        else if (Input.GetKeyDown("0"))
            input += "0";
        if (Input.GetKeyDown(","))
            input += ",";
        if (Input.GetKeyDown("."))
            input += ".";
        if (Input.GetKeyDown(":") && !Input.GetKey(KeyCode.LeftShift))
            input += ":";
        else if (Input.GetKeyDown(";"))
            input += ";";
        if (Input.GetKeyDown("-"))
            input += "-";
        if (Input.GetKeyDown(KeyCode.Space))
            input += " ";
        if (Input.GetKeyDown(KeyCode.Return))
            FindObjectOfType<TextEventHandler>().pickOption();


        input = input.Substring(0, Mathf.Min(input.Length, 80));
        if (Input.GetKey(KeyCode.Backspace) && Time.realtimeSinceStartup > deleteTime)
        {
            input = input.Substring(0, input.Length - 1);
            deleteTime = Time.realtimeSinceStartup + 0.1f;
        }
        GetComponent<Text>().text = ">"+input;
    }
}
