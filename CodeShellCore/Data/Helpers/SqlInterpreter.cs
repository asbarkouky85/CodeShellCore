using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Data.Helpers
{
    public class SqlInterpreter
    {
        static SortedList<int, string> _messages;
        public static SortedList<int, string> SQLErrorMessages
        {
            get
            {
                if (_messages == null)
                {
                    _messages = new SortedList<int, string>();
                    _messages[2627] = "repeated_data";
                    _messages[547] = "delete_conflict";
                }
                return _messages;
            }
        }

        

        public static string[] GetValues(string template, string message)
        {
            string[] x = message.Split(new char[] { '\n' });

            string value = x[0].Trim();
            string pattern = "^" + template.Replace("%.*ls", "(.*?)").Replace("%ls", "(.*?)") + "$";
            Regex r = new Regex(pattern);

            List<string> vals = new List<string>();
            var m = r.Match(value);

            for (int i = 0; i < m.Groups.Count; i++)
                vals.Add(m.Groups[i].Value);

            return vals.ToArray();
        }

        public static string[] N00547(string Ex)
        {
            string template = "The %ls statement conflicted with the %ls constraint \"%.*ls\". The conflict occurred in database \"%.*ls\", table \"%.*ls\", column '%.*ls'.";
            //The DELETE statement conflicted with the REFERENCE constraint "FK_PurchaseOrder_Vendor". The conflict occurred in database "crowntex", table "dbo.PurchaseOrder", column 'VendorId'.

            return GetValues(template, Ex);
        }

        public static string[] N02627(string Ex)
        {
            string template = @"Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'. The duplicate key value is \(%.*ls\).";
            //string value = "Violation of UNIQUE KEY constraint 'IX_StockItemType_CODE'. Cannot insert duplicate key in object 'dbo.ItemType'. The duplicate key value is (B001).";

            return GetValues(template, Ex);
        }
        
    }
}
