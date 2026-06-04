using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Validation
{
    public class FeatureIdValidator
        : IGisValidator<GisFeature>
    {
        public IEnumerable<ValidationResult> Validate(GisFeature feature)
        {
            if (string.IsNullOrWhiteSpace(feature.FeatureId))
            {
                yield return
                    new ValidationResult
                    {
                        Severity = ValidationSeverity.Error,
                        Message = "FeatureId is missing."
                    };
            }
        }
    }
}