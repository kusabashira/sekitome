﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    public Animator fade;
    public float sceneTransitionSec;
    public AudioClip menuBGM;
    public Text resultsLabel;

    private SoundBehaviour sound;

    void Start()
    {
        SetResultText();

        sound = SoundBehaviour.FindInstance();
        sound.PlayBGM(menuBGM);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
        {
            BackToStartMenu();
            return;
        }
    }

    void SetResultText()
    {
        var resultsText = "";

        var scoreRecords = SaveData.GetScoreRecords(10);
        var scoreRecordI = 1;

        foreach (var scoreRecord in scoreRecords)
        {
            if (scoreRecord.score > 0)
            {
                resultsText += $"{scoreRecordI}: {scoreRecord.date}: {scoreRecord.score}\n";
            }
            else
            {
                resultsText += $"{scoreRecordI}: -\n";
            }

            scoreRecordI++;
        }

        resultsLabel.text = resultsText;
    }

    void BackToStartMenu()
    {
        MoveScene("StartMenuScene");
    }

    void MoveScene(string dstSceneName)
    {
        fade.SetBool("fade", true);
        StartCoroutine(WaitLoadScene(dstSceneName, sceneTransitionSec));
    }

    IEnumerator WaitLoadScene(string dstSceneName, float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        SceneManager.LoadScene(dstSceneName);
    }
}
