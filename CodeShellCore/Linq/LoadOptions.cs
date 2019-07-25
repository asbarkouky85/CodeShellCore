using CodeShellCore.Linq.Filtering;
using CodeShellCore.Text;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Linq
{
    /// <summary>
    /// An object used to transmit the load options using ajax 
    /// </summary>
    public class LoadOptions
    {
        public bool AsLookup { get; set; }
        public string SearchTerm { get; set; }
        /// <summary>
        /// starting from (Showing * PageNumber)
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// how many records to show
        /// </summary>
        public int Showing { get; set; }
        /// <summary>
        /// Property for ORDER BY
        /// </summary>
        public string OrderProperty { get; set; }
        /// <summary>
        /// DESC : for descending &lt;br/&gt;
        /// ASC : for ascending
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// json string of a <see cref="PropertyFilter"/> array
        /// </summary>
        /// <example>
        /// <code>
        /// [ 
        ///     { MemberName : "NAME" , FilterType : "string" , Value1 : "Ahm" , Value2:"" , Ids : [] },
        ///     { MemberName : "AGE" , FilterType : "int" , Value1 : "15" , Value2:"30" , Ids : [] }
        /// ]
        /// </code>
        /// </example>
        public string Filters { get; set; }
        private IEnumerable<PropertyFilter> _propertyFilters;
        public IEnumerable<PropertyFilter> PropertyFilters
        {
            get
            {
                if (_propertyFilters == null)
                {
                    if (Filters != null)
                        _propertyFilters = Filters.FromJson<IEnumerable<PropertyFilter>>();
                    else
                        _propertyFilters = new List<PropertyFilter>();

                }
                return _propertyFilters;
            }
        }
        /// <summary>
        /// An object used to transmit the load options using ajax 
        /// </summary>
        public LoadOptions() { }
        /// <summary>
        /// An object used to transmit the load options using ajax 
        /// </summary>
        /// <param name="show"></param>
        public LoadOptions(int show)
        {

            Showing = show;
            OrderProperty = "ID";

        }

        public ListOptions<T> GetOptionsFor<T>(bool ignoreFilters = false) where T : class
        {
            return (new ExpressionGenerator<T>()).ToModelGetOptions(this,ignoreFilters);
        }
    }
}
