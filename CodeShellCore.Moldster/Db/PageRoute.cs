﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace CodeShellCore.Moldster{    public partial class PageRoute : MoldsterModelBase, IMoldsterModel    {        public long Id { get; set; }        public long PageId { get; set; }        public long? ListUrl { get; set; }        public long? AddUrl { get; set; }        public long? EditUrl { get; set; }        public long? DetailsUrl { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        public long? CreatedBy { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? UpdatedBy { get; set; }        [ForeignKey("PageId")]        [InverseProperty("PageRoutes")]		[System.Runtime.Serialization.IgnoreDataMember]
        public Page Page { get; set; }    }}