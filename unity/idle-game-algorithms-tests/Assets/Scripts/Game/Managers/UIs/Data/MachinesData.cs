using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers.UIs.Data
{
    public class MachinesData
    {
        public Stack<(GameObject machine, float initialCost)> Machines { get; } = new();
    }
}