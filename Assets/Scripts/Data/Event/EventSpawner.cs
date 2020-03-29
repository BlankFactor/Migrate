using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSpawner
{
    public enum eventName {
        VastFoods,
    }

    public static BEvent GetEvent(eventName eventName) {
        switch (eventName) {
            case eventName.VastFoods: {
                    return new VastFoods();
                }
            default:return null;
        }
    }
}
