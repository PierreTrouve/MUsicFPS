using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackListenerScript : MonoBehaviour {

    public TextAsset trackFile;
    ParticleSystem particleSystemFeedback;
    bool[] musicNotes;
    private int cursor = 0;
    public GameObject notePrefab;
    public Transform noteStart;
    public Transform noteRight;
    public Transform noteEnd;
    public Transform trackParent;

    public int comboMeter = 0;

    protected List<GameObject> noteList = new List<GameObject>();



    // Use this for initialization
    void Start () {
        string text = trackFile.text;
        musicNotes = new bool[text.Length];
        int c = 0;
        foreach (char a in text) {
            if (a == '1' || a == '0')
                musicNotes[c++] = (a == '1');
        }
        particleSystemFeedback = GetComponent<ParticleSystem>();
        
	}
	
    public void StartLecture() {
        InvokeRepeating("AdvanceLecture", 0f, 0.02f);
    }

    public int GetDamage() {
        NoteBehavior closestNote = null;
        int distanceTo50 = 1000000;
        foreach (GameObject note in noteList) {
            NoteBehavior noteBehavior = note.GetComponent<NoteBehavior>();
            if (null == closestNote) {
                closestNote = noteBehavior;
                distanceTo50 = Mathf.Abs(noteBehavior.GetDelta());
            } else {
                if (Mathf.Abs(noteBehavior.GetDelta()) < distanceTo50) {
                    closestNote = noteBehavior;
                    distanceTo50 = Mathf.Abs(noteBehavior.GetDelta());
                }
            }
        }

        if (null == closestNote) {
            return 1;
        }
        int dmgFactor = closestNote.GetDamageFactor();
        HandleComboMeter(dmgFactor);
        return dmgFactor;
    }

    void HandleComboMeter(int dmgFactor) {
        if (dmgFactor == 3) {
            comboMeter += 2;
        } else if (dmgFactor == 2) {
            comboMeter += 1;
        } else if (dmgFactor == 0) {
            comboMeter = 0;
        }

        if (comboMeter > 10)
            comboMeter = 10;
    }

    void AdvanceLecture() {
        cursor++;
        int cursor50 = cursor + 50;

        if (musicNotes[cursor]) {
            particleSystemFeedback.Play();
        }

        for (int i = 0; i < noteList.Count;i++) {
            GameObject note = noteList[i];
            NoteBehavior noteBehavior = note.GetComponent<NoteBehavior>();
            float notePositionValue = (float)noteBehavior.GetPositionAndUpdate();
            if (notePositionValue <= 50) {
                note.transform.position = Vector3.Lerp(noteStart.position, noteRight.position, notePositionValue * 0.02f);
            } else if (notePositionValue <= 70 ) {
                note.transform.position = Vector3.Lerp(noteRight.position, noteEnd.position, (notePositionValue - 50f) * 0.05f);
            } else {
                note.SetActive(false);
                noteList.RemoveAt(i) ;
            }
        }

        if (musicNotes[cursor50]) {
            GameObject newNote = Instantiate(notePrefab);
            newNote.transform.parent = trackParent;
            newNote.transform.position = noteStart.position;
            noteList.Add(newNote);
        }
    }
}
