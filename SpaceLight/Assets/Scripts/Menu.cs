﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
