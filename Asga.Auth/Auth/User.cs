using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asga.Auth
{
    [Table("Users", Schema = "Auth")]
    public partial class User : AuthModelBase, IAuthModel
    {
        public User()
        {
            UserEntityLinks = new HashSet<UserEntityLink>();
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(50)]
        public string LogonName { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public long? PersonId { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50)]
        public string Mobile { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(300)]
        public string Photo { get; set; }
        public bool? Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }
        public long? TenantId { get; set; }
        public long? AppId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("AppId")]
        [InverseProperty("Users")]
		[System.Runtime.Serialization.IgnoreDataMember]
        public App App { get; set; }
        [ForeignKey("TenantId")]
        [InverseProperty("Users")]
		[System.Runtime.Serialization.IgnoreDataMember]
        public Tenant Tenant { get; set; }
        [InverseProperty("User")]
        public ICollection<UserEntityLink> UserEntityLinks { get; set; }
        [InverseProperty("User")]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
