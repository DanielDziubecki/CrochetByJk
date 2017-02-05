namespace CrochetByJk.Components.Validators
{
    public interface IValidator<in T>
    {
        void Validate(T objectToValidate);
    }

}