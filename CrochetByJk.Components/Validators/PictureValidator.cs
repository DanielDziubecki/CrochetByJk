using System.Collections.Generic;
using System.Linq;
using CrochetByJk.Common.ShortGuid;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.Validators
{
    public class PictureValidator : IValidator<IEnumerable<Picture>>
    {
        public void Validate(IEnumerable<Picture> objectToValidate)
        {
            var duplicatedPictureNames = objectToValidate.GroupBy(x => x.Name)
                                      .Where(x => x.Count() > 1)
                                      .ToList();

            if (!duplicatedPictureNames.Any()) return;

            foreach (var duplicatedGroup in duplicatedPictureNames)
            {
                foreach (var picture in duplicatedGroup)
                {
                    var dupName = picture.Name;
                    picture.Name = dupName + ShortGuid.NewGuid();
                }
            }
        }
    }
}