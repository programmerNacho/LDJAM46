using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJAM46
{
    public class ServerModificationManager : MonoBehaviour
    {
        private List<ServerModification> modifications = new List<ServerModification>();

        public void AddModification(ServerModification serverModification)
        {
            if(!modifications.Contains(serverModification))
            {
                modifications.Add(serverModification);
            }
        }

        public void RemoveModification(ServerModification serverModification)
        {
            if(modifications.Contains(serverModification))
            {
                modifications.Remove(serverModification);
            }
        }

        public ServerModification ProcessModifications()
        {
            ServerModification result = new ServerModification();
            for (int i = 0; i < modifications.Count; i++)
            {
                result.VisualizationsPerSecondBoost += modifications[i].VisualizationsPerSecondBoost;
                result.MaxTemperatureBoost += modifications[i].MaxTemperatureBoost;
                result.MinTemperatureBoost += modifications[i].MinTemperatureBoost;
                result.InstantTemperatureChangeBoost += modifications[i].InstantTemperatureChangeBoost;
                modifications[i].InstantTemperatureChangeBoost = 0;
                result.TemperatureChangePerSecondBoost += modifications[i].TemperatureChangePerSecondBoost;
                result.BlockCooldownTimeBoost += modifications[i].BlockCooldownTimeBoost;
            }
            return result;
        }
    }
}
