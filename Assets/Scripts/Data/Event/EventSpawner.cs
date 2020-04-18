using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSpawner
{
    public enum eventName {
        VastFoods,
        Foraging_Richness,
        Foraging,
        Foraging_HalfFull,
        NaturalSelection,
        Poaching,
        Trap,
        GetLost,
        PureWater,
        DirtyWater
    }

    public static BEvent GetEvent(eventName eventName) {
        switch (eventName) {
            case eventName.VastFoods: {
                    return new VastFoods();
                }
            case eventName.Foraging: {
                    return new Foraging();
                }
            case eventName.Foraging_Richness: {
                    return new Foraging_Richness();
                }
            case eventName.Foraging_HalfFull: {
                    return new Foraging_HalfFull();
                }
            case eventName.NaturalSelection: {
                    return new NaturalSelection();
                }
            case eventName.Poaching: {
                    return new Poaching();
                }
            case eventName.Trap: {
                    return new Trap();
                }
            case eventName.GetLost: {
                    return new GetLost();
                }
            case eventName.PureWater: {
                    return new PureWater();
                }
            case eventName.DirtyWater: {
                    return new DirtyWater();
                }
            default:return null;
        }
    }
}
