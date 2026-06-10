using UnityEngine;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Validation
{
    public class ValidationTest : MonoBehaviour
    {
        private void Start()
        {
            SettlementFeature settlement = new();
            FeatureIdValidator validator = new();

            foreach (ValidationResult result in validator.Validate(settlement))
            {
                Debug.Log($"{result.Severity}: " + $"{result.Message}");
            }
        }
    }
}