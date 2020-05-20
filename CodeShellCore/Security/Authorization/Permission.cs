using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Security.Authorization
{
    public class Permission
    {
        protected int Privilege = 0;
        public List<string> Actions { get; set; }
        public string CollectionId;

        public Permission()
        {

        }

        public static int GetInt(IDataPermission p)
        {
            var P = new Permission();
            P.SetBit(0, true);
            P.SetBit(1, p.CanViewDetails);
            P.SetBit(2, p.CanInsert);
            P.SetBit(3, p.CanUpdate);
            P.SetBit(4, p.CanDelete);
            return P.Privilege;
        }

        public DataAccessPermission ToDataPermission()
        {
            return new DataAccessPermission
            {
                Actions = Actions,
                Details = FromBit(1),
                Insert = FromBit(2),
                Update = FromBit(3),
                Delete = FromBit(4),
                CollectionId = CollectionId
            };
        }

        public static Dictionary<string, int> CompressResourceData(IEnumerable<ResourceV> items)
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();
            foreach (var d in items)
            {
                int perm = 0;
                ret.TryGetValue(d.Id, out perm);
                perm = Combine(perm, d);
                ret[d.Id] = perm;
            }
            return ret;
        }

        public static int Combine(int p1, IDataPermission p2)
        {
            var p = new Permission(p1);
            p.Append(GetInt(p2));
            return p.Privilege;
        }

        public static int Combine(int p1, int p2)
        {
            var p = new Permission(p1);
            p.Append(p2);
            return p.Privilege;
        }

        public Permission(int perm, IEnumerable<string> actions = null)
        {
            Privilege = perm;
            if (actions != null)
            {
                Actions = new List<string>();
                Actions.AddRange(actions.Distinct());
            }

        }

        public void Append(int priv)
        {
            Privilege = Privilege | priv;
        }

        public void Append(string action)
        {
            if (Actions == null)
                Actions = new List<string>();

            Actions.Add(action);
            Actions = Actions.Distinct().ToList();
        }

        public void Append(IEnumerable<string> actions)
        {
            if (Actions == null)
                Actions = new List<string>();
            Actions.AddRange(actions);
            Actions = Actions.Distinct().ToList();
        }

        public bool Can(string action)
        {
            if (Actions == null)
                return false;

            return Actions.Contains(action);
        }


        public bool FromBit(int bitOrder)
        {
            int data = (int)Math.Pow(2D, bitOrder);
            bool val = (((Privilege & data) >> bitOrder) == 1);
            return val;
        }

        public void SetBit(int bitOrder, bool value)
        {
            int data = (int)Math.Pow(2D, bitOrder);

            if (value == true)
                Privilege = Privilege | data;
            else if (Privilege >= data)
                Privilege = Privilege - data;
        }

        public override string ToString()
        {
            return Convert.ToString(Privilege, 2);
        }
    }
}
