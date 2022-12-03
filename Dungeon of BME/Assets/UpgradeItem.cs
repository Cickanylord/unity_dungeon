using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeItem
{
    public string Description { get; set; }

    public Func<int> Action { get; set; }

    public bool OneUse { get; set; }

}
