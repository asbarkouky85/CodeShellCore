﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace Asga.Mobile{    [Table("UserDevices", Schema = "Mob")]    public partial class UserDevice : AsgaMobileModelBase, IAsgaMobileModel    {        public long Id { get; set; }        public int Type { get; set; }        [StringLength(200)]        public string TokenId { get; set; }        public long UserId { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        public long? CreatedBy { get; set; }        [StringLength(200)]        public string DeviceId { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? UpdatedBy { get; set; }
        public string PreferredLanguage { get; set; }
    }}