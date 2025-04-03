//using System.ComponentModel.DataAnnotations;

//public class PositiveIntegerAttributeValidation : ValidationAttribute
//{
//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        if (value == null)
//        {
//            return new ValidationResult("Relacione o cooperado a uma cooperativa válida");
//        }

//        if (!(value is int intValue))
//        {
//            return new ValidationResult("Relacione o cooperado a uma cooperativa válida");
//        }

//        if (intValue <= 0)
//        {
//            return new ValidationResult("Relacione o cooperado a uma cooperativa válida");
//        }

//        return ValidationResult.Success;
//    }
//}


using System;
using System.ComponentModel.DataAnnotations;

public class PositiveIntegerAttributeValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("O campo não pode estar vazio. Relacione o cooperado a uma cooperativa válida.");
        }

        if (!int.TryParse(value.ToString(), out int intValue))
        {
            return new ValidationResult("Relacione o cooperado a uma cooperativa válida.");
        }

        if (intValue <= 0)
        {
            return new ValidationResult("Relacione o cooperado a uma cooperativa válida.");
        }

        return ValidationResult.Success;
    }
}
