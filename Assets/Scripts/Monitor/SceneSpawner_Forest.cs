using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner_Forest : SceneSpawner {
    protected override void SpawnScene(bool _b)
    {
        if (setActitve)
            foreach (var i in sceneObject)
                i.SetActive(_b);
        else
        {
            foreach (var i in sceneObject)
                Destroy(i);

            ColorController.instance.ClearFireFly();
        }
    }
}
