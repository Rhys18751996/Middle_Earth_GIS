using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Validation
{
    public class DatasetValidator : IGisValidator<GisDataset>
    {
        public IEnumerable<ValidationResult> Validate( GisDataset dataset)
        {
            if (string.IsNullOrWhiteSpace( dataset.DatasetId))
            {
                yield return
                    new ValidationResult
                    {
                        Severity =
                            ValidationSeverity.Error,

                        Message =
                            "DatasetId is missing."
                    };
            }
        }
    }
}