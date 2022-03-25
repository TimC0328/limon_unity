using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayDialog : MonoBehaviour
{
    public Dialog dialog;
    public Text lineText;
    public Sprite portraitImage;

    Dialog.Line currentLine;
    bool branching = false;


    public void InitDialog(Dialog input)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        
        dialog = input;

        currentLine = dialog.lines[0];
        lineText.text = currentLine.text;
    }

    public void NextLine()
    {
        if (currentLine.branch != null)
        {
            branching = true;
            HandleBranch(currentLine.branch);
            return;
        }

        if (currentLine.nextLine == -1)
            CloseDialog();

        currentLine = dialog.lines[currentLine.nextLine];

        lineText.text = currentLine.text;

    }

    void HandleBranch(Branch branch)
    {
        lineText.text = "";
        if (branch.options.Length > 3)
            return;

        for (int i = 0; i < branch.options.Length; i++)
            lineText.text += "" + i + ": " + branch.options[i].text + '\n';
    }

    void Update()
    {
        if (dialog == null)
            return;
        if (Input.GetMouseButtonDown(0) && !branching)
        {
            NextLine();
            return;
        }
    }

    public void CloseDialog()
    {
        dialog = null;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
