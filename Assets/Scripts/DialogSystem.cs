using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Text dialogText;
    public Image face;

    public TextAsset text;
    [Header("播放速度")]
    public float playSpeed;
    public float accelerateSpeed;

    [Header("头像")]
    public Sprite face01;
    public Sprite face02;

    private List<string> contents;
    private int index;
    private bool runOver;
    private bool isAccelerate;

    // Start is called before the first frame update
    void Awake()
    {
        contents = new List<string>(text.text.Split('\n'));
    }

    private void OnEnable()
    {
        StartCoroutine(ShowTextOneByOne());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index >= contents.Count)
            {
                gameObject.SetActive(false);
                index = 0;
                return;
            }
            if (runOver)
                StartCoroutine(ShowTextOneByOne());
            else
            {
                isAccelerate = true;
            }
        }
        
    }

    public IEnumerator ShowTextOneByOne() 
    {
        float oldSpeed = playSpeed;
        
        runOver = false;
        switch (contents[index])
        {
            case "A":
                face.sprite = face01;
                break;
            case "B":
                face.sprite = face02;
                break;
        }

        index++;

        dialogText.text = "";
        for (int i = 0; i < contents[index].Length; i++) 
        {
            dialogText.text += contents[index][i];
            yield return new WaitForSeconds(playSpeed);
            if (isAccelerate)
            {
                playSpeed = accelerateSpeed;
            }
        }
        index++;
        runOver = true;
        playSpeed = oldSpeed;
        isAccelerate = false;
    }
}
