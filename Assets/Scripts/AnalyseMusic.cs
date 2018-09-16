using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class AnalyseMusic : MonoBehaviour {

    public float[][] samples;
    public int count = -1;

    private List<string[]> rowData = new List<string[]>();
    public bool done = false;

    public int sizeMusique = 300;


    AudioSource audioSource;
    // Use this for initialization
    void Start () {
        samples = new float[sizeMusique][];
        audioSource = GetComponent<AudioSource>();
        for (int i =0; i < sizeMusique; i++) {
            samples[i] = new float[512];
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (count >= sizeMusique -1) {
            SaveData();
            return;
        } 
        audioSource.GetSpectrumData(samples[++count], 0, FFTWindow.Blackman);
    }

    protected void SaveData() {
        if (done) {
            return;
        }

        int outputSize = sizeMusique / 10;
        string[][] output = new string[outputSize][];
        for (int i = 0; i < outputSize; i++) {
            output[i] = new string[8];
        }

        for (int i = 0; i < outputSize; i++) {
            for (int j = 0; j < 8; j++) {
                output[i][j] = string.Format("{0:N4}", samples[i][2^j]);
            }
        }
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < sizeMusique -10; i++) {
            if (i % 10 == 0) {
                sb.AppendLine(string.Join(";", output[i/10]));
            }
        }

        string filePath = GetPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
        done = true;
    }

    private string GetPath() {
        return Application.dataPath + "/CSV/" + "data.csv";
    }

}
