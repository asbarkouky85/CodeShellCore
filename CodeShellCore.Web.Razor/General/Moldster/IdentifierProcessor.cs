﻿using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.General.Moldster;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor
{
    public class IdentifierProcessor
    {
        public Accessibility Process(IHtmlHelper helper, string id, string cont)
        {
            helper.AddToViewControls(new ControlDTO
            {
                Identifier = id?.ToLower(),
                ControlType = cont
            });
            return helper.GetAccessibility(id);
        }

        private Accessibility Process(IHtmlHelper helper, MoldsterHtmlContainer parent, string id, string cont)
        {
            parent.Control.Children.Add(new ControlDTO
            {
                Identifier = id?.ToLower(),
                ControlType = cont
            });
            return helper.GetAccessibility(id);
        }

        public Accessibility Process<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string cont, string extra = null)
        {
            string id = RazorUtils.GetIdentifier(exp);
            if (extra != null)
                id += extra;
            return Process(helper, id, cont.ToString());
        }

        public Accessibility Process<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls cont, string extra = null)
        {
            return Process(helper, exp, cont.ToString(), extra);
        }

        public Accessibility ProcessCell(IHtmlHelper helper, string textId, string cont, MoldsterHtmlContainer parent = null)
        {
            string id = textId;

            if (parent != null)
            {
                id = parent.Control.Identifier.ToLower() + "__" + id;
                return Process(helper, parent, id, "TableCell_" + cont);
            }
            else
            {
                id = "cell__" + id;
                return Process(helper, id, "TableCell_" + cont);
            }
        }

        public Accessibility ProcessCell(IHtmlHelper helper, string textId, InputControls cont, MoldsterHtmlContainer parent = null)
        {
            return ProcessCell(helper, textId, cont.ToString(), parent);
        }

        public Accessibility ProcessCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls cont, MoldsterHtmlContainer parent = null)
        {
            string id = RazorUtils.GetIdentifier(exp);
            return ProcessCell(helper, id, cont.ToString(), parent);
        }

        public Accessibility ProcessCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string cont, MoldsterHtmlContainer parent = null)
        {
            string id = RazorUtils.GetIdentifier(exp);
            return ProcessCell(helper, id, cont, parent);
        }
    }
}
