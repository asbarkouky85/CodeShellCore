﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace Asga.Public{    [Table("AboutSections", Schema = "Pub")]    public partial class AboutSection : AsgaModelBase, IAsgaModel    {        public long Id { get; set; }        public string Text { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        public long? CreatedBy { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? UpdatedBy { get; set; }    }}