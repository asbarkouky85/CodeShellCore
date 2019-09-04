using CodeShellCore.Web.Razor.Text;
using CodeShellCore.Web.Razor.Themes;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Web.Razor.Validation.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Web.Razor
{
    public class RazorConfig
    {
        private static RazorConfig _instance;
        private string _modelName;
        private string _calendarCssClass;
        private string _validationMessageContainer;
        private string _fieldErrorMessagesTemplate;
        private string _dateValidationPattern;
        private string _formName;
        private Type _validatorCollectionType;
        private IRazorLocaleTextProvider _localeTextProvider;
        private IExpressionStringifier _stringifier;
        private IRazorTheme _theme;
        private static RazorConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RazorConfig();
                return _instance;
            }
        }

        public static void SetCollectionType<T>() where T : class, IValidationCollection
        {
            Instance._validatorCollectionType = typeof(T);
        }

        public static Type ValidationCollectionType { get { return Instance._validatorCollectionType; } }

        public static string ModelName
        {
            get { return Instance._modelName; }
            set { Instance._modelName = value; }
        }

        public static string CalendarCssClass
        {
            get { return Instance._calendarCssClass; }
            set { Instance._calendarCssClass = value; }
        }

        /// <summary>
        /// 0: form name, 1: field name,2: error name, 3:message
        /// </summary>
        public static string ErrorMessageTemplate
        {
            get { return Instance._validationMessageContainer; }
            set { Instance._validationMessageContainer = value; }
        }

        /// <summary>
        /// 0: form name, 1: field name,2: messages
        /// </summary>
        public static string FieldErrorMessagesTemplate
        {
            get { return Instance._fieldErrorMessagesTemplate; }
            set { Instance._fieldErrorMessagesTemplate = value; }
        }

        public static string DateValidationPattern
        {
            get { return Instance._dateValidationPattern; }
            set { Instance._dateValidationPattern = value; }
        }

        public static string FormName
        {
            get { return Instance._formName; }
            set { Instance._formName = value; }
        }

        public static IRazorLocaleTextProvider LocaleTextProvider
        {
            get { return Instance._localeTextProvider; }
            set { Instance._localeTextProvider = value; }
        }

        public static IRazorTheme Theme
        {
            get { return Instance._theme; }
            set { Instance._theme = value; }
        }

        public static IExpressionStringifier ExpressionStringifier
        {
            get { return Instance._stringifier; }
            set { Instance._stringifier = value; }
        }

        private RazorConfig()
        {
            _modelName = "model";
            _calendarCssClass = "date-picky";
            _validationMessageContainer = "<small ng-show='{0}.{1}.$error.{2}' class='form-text text-danger'>{3}</small>";
            _fieldErrorMessagesTemplate = "<span ng-show='{0}.{1}.$invalid'>\n{2}</span>\n";
            _dateValidationPattern = "^([0-2][0-9]|3[0-1])[/]([0-1][0-9])[/]([1-9][0-9]{3})$";
            _formName = "Form";
            _validatorCollectionType = typeof(ValidationCollection);
            _localeTextProvider = Shell.RootInjector.GetService<IRazorLocaleTextProvider>();
            _stringifier = new DefaultExpressionStringifier();
            _theme = new DefaultTheme();
        }
    }
}
