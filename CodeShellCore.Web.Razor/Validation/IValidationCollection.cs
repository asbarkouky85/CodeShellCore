using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation
{
    public interface IValidationCollection
    {
        string FormName { get; }
        string FormFieldName { get; }
        List<Validator> Validators { get; }
        bool IsEmpty { get; }
        IValidationCollection AddLength(int maxLength, int minLenth = 0);
        IValidationCollection AddWebsite();
        IValidationCollection AddRequired(string reqCondition = null);
        IValidationCollection AddAlphaNumeric();
        IValidationCollection AddNumeric();
        IValidationCollection AddEmail();
        IValidationCollection AddDate(CalendarTypes type, DateRange range = null);
        IValidationCollection AddMinMax(float? min, float max = 0);
        IValidationCollection AddMinMax(string min, string max = null);

        IValidationCollection AddArabic();
        IValidationCollection AddPattern(string pattern, string message = null);
        IValidationCollection AddCustom(string validationType, string message);
        IValidationCollection AddUnique(string idExpression, string dataService = "Service");

        IValidationCollection Add(IValidator v);


        IHtmlContent GetMessages();
        string GetAttributes();

        bool HasRequired();
        string GetRequiredCondition();
        bool Has<TValidator>() where TValidator : IValidator;

        void SetMember(string columnId, string formFieldName, string alternateLabel = null);

        void SetNumericDefaults();
        void UseAnnotations(IEnumerable<CustomAttributeData> customAttributes);
    }
}
