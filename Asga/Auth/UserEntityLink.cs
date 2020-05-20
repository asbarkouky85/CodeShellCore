﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace Asga.Auth{    [Table("UserEntityLinks", Schema = "Auth")]    public partial class UserEntityLink : AsgaModelBase, IAsgaModel    {        public long Id { get; set; }        [StringLength(60)]        public string EntityName { get; set; }        public long EntityId { get; set; }        public long UserId { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        public long? CreatedBy { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? UpdatedBy { get; set; }        [ForeignKey("UserId")]        [InverseProperty("UserEntityLinks")]		[System.Runtime.Serialization.IgnoreDataMember]
        public User User { get; set; }    }}