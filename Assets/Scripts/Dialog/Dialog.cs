using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField]
    public List<Line> lines;
    
    [Serializable]
    public struct Line
    {
        public Sprite portrait;
        public string name;
        public string text;
        public int nextLine;
        public Branch branch;   
    }
}

[Serializable]
public class Branch
{
    public string name;
    public Sprite portrait;

    public Options[] options = new Options[3];
    [Serializable]
    public struct Options
    {
        public string text;
        public int nextLine;
    }

}
