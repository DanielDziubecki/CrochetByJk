using System.Collections.Generic;
using System.Linq;
using CrochetByJk.Common.ShortGuid;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.Validators
{
    //todo: testy do validatora
    public class PictureValidator : IValidator<IEnumerable<Picture>>
    {
        public void Validate(IEnumerable<Picture> objectToValidate)
        {
            var toValidate = objectToValidate as Picture[] ?? objectToValidate.ToArray();
            var duplicatedPictureNames = toValidate.GroupBy(x => x.Name)
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
            var toLongPicNames = toValidate.Where(x => x.Name.Length > 50);
            foreach (var longPicName in toLongPicNames)
                longPicName.Name = longPicName.Name.Remove(10, longPicName.Name.Length-10);
        }
    }
}