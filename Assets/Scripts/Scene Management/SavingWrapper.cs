﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        string defaultSaveFile = "save";
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }

}