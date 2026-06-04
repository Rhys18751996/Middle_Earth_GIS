using UnityEngine;

namespace Fantasy_World_GIS.GIS.Integration
{
    public class GisCoreIntegrationTest : MonoBehaviour
    {
        private void Start()
        {
            SettlementFeature settlement =
                new();

            settlement.FeatureId =
                "HOBBITON";

            settlement.FeatureType =
                FeatureType.Settlement;

            settlement.Attributes.Add(
                new GisAttribute
                {
                    Name = "Population",
                    Value = "1200"
                });

            settlement.Attributes.Add(
                new GisAttribute
                {
                    Name = "Owner",
                    Value = "Shire"
                });

                SettlementDataset dataset =
                new()
                {
                    DatasetId =
                        "SHIRE_SETTLEMENTS",

                    Name =
                        "Shire Settlements"
                };

            dataset.Features.Add(settlement);

            DatasetRegistry registry =
                new();

            registry.Register(
                dataset);

                var results =
                DatasetQueryService
                    .GetFeaturesByType(
                        dataset,
                        FeatureType.Settlement);

                        FeatureIdValidator validator =
                new();

            foreach (var feature in results)
            {
                foreach (var validationResult in validator.Validate(feature))
                {
                    Debug.Log(validationResult.Message);
                }
            }
        }
    }
}