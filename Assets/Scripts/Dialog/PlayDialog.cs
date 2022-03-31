using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayDialog : MonoBehaviour
{
    private Player _player;

    public Dialog dialog;
    public Text lineText;
    public Text nameText;

    public GameObject portrait;
    private Image portraitImage;
    private Animator portraitAnimated;

    public float typingSpeed = .025f;

    public AudioSource soundManager;
    public AudioClip writeSFX, dingSFX;

    [SerializeField]
    private Dialog.Line currentLine;

    bool branching = false;
    bool isTyping = false;

    void Start()
    {
        portraitImage = portrait.GetComponent<Image>();
        portraitAnimated = portrait.GetComponent<Animator>();
        portraitAnimated.enabled = false;
    }


    public void InitDialog(Dialog input, Player player)
    {
        _player = player;

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        
        dialog = input;

        currentLine = dialog.lines[0];

        HandleDialog();
    }

    public void NextLine()
    {
        if (currentLine.nextLine == -1)
        {
            CloseDialog();
            return;
        }

        if (currentLine.branch != null)
        {
            if (currentLine.branch.options.Length > 0)
            {
                branching = true;
                HandleBranch(currentLine.branch);
                return;
            }
        }

        currentLine = dialog.lines[currentLine.nextLine];

        HandleDialog();
    }

    void HandleBranch(Branch branch)
    {
        Transform options, option;
        options = transform.GetChild(2);

        portraitAnimated.enabled = false;

        if (branch.animatedPortrait == null)
            portraitImage.sprite = branch.portrait;
        else
        {
            portraitAnimated.enabled = true;
            portraitAnimated.runtimeAnimatorController = branch.animatedPortrait;
            portraitAnimated.Rebind();
            portraitAnimated.Update(0f);
        }


        lineText.text = "";
        nameText.text = branch.name;


        if (branch.options.Length > 4)
            return;

        for (int i = 0; i < 4; i++)
        {
            option = options.GetChild(i);
            if (i  < branch.options.Length)
            {
                option.gameObject.SetActive(true);
                option.GetChild(0).GetComponent<Text>().text = " " + i + ": " + branch.options[i].text;
            }
            else
                option.gameObject.SetActive(false);
        }
    }

    public void SelectOption(int choice)
    {
        int nextLine;

        Transform options, option;
        options = transform.GetChild(2);

        for (int i = 0; i < 4; i++)
        {
            option = options.GetChild(i);
            option.gameObject.SetActive(false);
        }

        branching = false;

        nextLine = currentLine.branch.options[choice].nextLine;

        if (nextLine == -1)
        {
            CloseDialog();
            return;
        }

        currentLine = dialog.lines[nextLine];

        HandleDialog();


    }

    void HandleDialog()
    {

        if (currentLine.animatedPortrait == null)
        {
            portraitAnimated.enabled = false;
            portraitImage.sprite = currentLine.portrait;
        }
        else
        {
            portraitAnimated.enabled = true;
            portraitAnimated.runtimeAnimatorController = currentLine.animatedPortrait;
            portraitAnimated.Rebind();
            portraitAnimated.Update(0f);
        }

        nameText.text = currentLine.name;
        lineText.text = "";

        isTyping = true;
        StartCoroutine(TypeWriter(0));
    }

    void Update()
    {
        if (dialog == null)
            return;
        if (Input.GetMouseButtonDown(0) && !branching)
        {
            if(isTyping)
            {
                lineText.text = currentLine.text;

                isTyping = false;
            }
            else
                NextLine();
            return;
        }
    }
    


    IEnumerator TypeWriter(int textPos)
    {
        if (isTyping)
        {

            lineText.text += currentLine.text[textPos].ToString();
            if ((textPos + 1) < currentLine.text.Length)
            {
                yield return new WaitForSeconds(.025f);
                soundManager.clip = writeSFX;
                soundManager.Play();
                StartCoroutine(TypeWriter(textPos + 1));
            }
            else
            {
                isTyping = false;
                soundManager.clip = dingSFX;
                soundManager.Play();
            }
        }
        else
        {
            soundManager.clip = dingSFX;
            soundManager.Play();
            yield return null;
        }
     
    }

    public void CloseDialog()
    {
        dialog = null;

        soundManager.clip = dingSFX;
        soundManager.Play();

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        _player.canMove = true;
    }
}
