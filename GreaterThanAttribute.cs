using System.ComponentModel.DataAnnotations;

// 1. Создаем свой класс-атрибут и говорим, что он проверяет типы DateTime
public class GreaterThanAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public GreaterThanAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    // 2. Переопределяем метод IsValid
    // value — это значение того свойства, к которому мы прикрепили атрибут (то есть EndAt)
    // validationContext — это контекст всей модели, где лежат ВСЕ свойства (и StartAt, и EndAt)
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var instance = validationContext.ObjectInstance;
        var type = validationContext.ObjectType;
        var otherProperty = type.GetProperty(_otherPropertyName);

        if (otherProperty == null)
        {
            return new ValidationResult($"Property '{_otherPropertyName}' is not found.");
        }

        var otherValue = otherProperty.GetValue(instance);

        if (value is DateTime endDate && otherValue is DateTime startDate)
        {
            if (endDate <= startDate)
            {
                return new ValidationResult("Дата окончания (EndAt) должна быть позже даты начала (StartAt).");
            }
        }

        return null;
    }
}