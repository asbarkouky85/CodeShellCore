using CodeShellCore.Data.Recursion;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Resources;
using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace CodeShellCore.Moldster.Domains{    public partial class Domain : MoldsterModelBase, IMoldsterModel, IRecursiveModel    {        public Domain()        {            PageCategories = new HashSet<PageCategory>();            Pages = new HashSet<Page>();            Resources = new HashSet<Resource>();        }        public long Id { get; set; }        [StringLength(50)]        public string Name { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? CreatedBy { get; set; }        public long? UpdatedBy { get; set; }        public long? ParentId { get; set; }        public string Chain { get; set; }        public string NameChain { get; set; }        [InverseProperty("Domain")]        public ICollection<PageCategory> PageCategories { get; set; }        [InverseProperty("Domain")]        public ICollection<Page> Pages { get; set; }        [InverseProperty("Domain")]        public ICollection<Resource> Resources { get; set; }        [NotMapped]
        public bool HasContents { get; set; }
        [NotMapped]
        public int ContentCount { get; set; }
        [NotMapped]
        public IEnumerable<IRecursiveModel> Children { get; set; }    }}