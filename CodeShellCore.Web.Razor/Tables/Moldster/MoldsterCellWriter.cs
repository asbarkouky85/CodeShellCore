﻿using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Web.Razor.General.Moldster;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Moldster
{
    public class MoldsterCellWriter<T> : CellWriter
    {
        
        protected ControlDTO Parent { get; set; }
        protected string Identifier { get; set; }

        public MoldsterCellWriter(IHtmlHelper<T> helper, ControlDTO parent = null, string identifier = null) : base(helper)
        {
            Parent = parent;
            if (identifier != null)
                Identifier = ((Parent != null ? Parent.Identifier?.ToLower() : "cell") + "__" + identifier).ToLower();

        }

        public void UseExpression<TValue>(Expression<Func<T, TValue>> exp, ControlDTO parent = null)
        {
            Parent = parent;
            Identifier = (parent != null ? parent.Identifier?.ToLower() + "__" : "cell__") + RazorUtils.GetIdentifier(exp);

            base.UseExpression(exp);
        }

        public void AddToControls(string type, Lister lister = null)
        {
            var ctrl = new ControlDTO
            {
                ControlType = type,
                Identifier = Identifier?.ToLower()
            };
            if (lister != null)
            {
                ctrl.Collection = new CollectionDTO
                {
                    Name = lister.CollecionName,
                };
                ((IHtmlHelper<T>)Helper).AddSource(lister);
            }

            if (Parent != null)
                Parent.Children.Add(ctrl);
            else
                ((IHtmlHelper<T>)Helper).AddToViewControls(ctrl);
        }

        public Accessibility GetAccessibility()
        {
            if (Identifier == null)
                return new Accessibility(2);
            return Helper.GetAccessibility(Identifier);
        }

    }
}
