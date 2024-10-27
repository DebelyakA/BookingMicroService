using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace UserService
{
    public class UserEntity
    {
        [Column("user_id")]
        public Guid Id { get; set; }
        [Column("user_secondName")]
        public required string SecondName { get; set; }
        [Column("user_name")]
        public required string Name { get; set; }
        [Column("user_lastName")]
        public required string LastName { get; set; }
        [EmailAddress]
        [Column("user_email")]
        public required string Email { get; set; }
        [MinLength(8, ErrorMessage = "Пароль должен быть не менее 8 символов"), MaxLength(50, ErrorMessage = "Пароль не должен превышать 50 символов")]
        [Column("user_hashPassword")]
        public required string PasswordHash { get; set; }
        [RegularExpression(@"^(79|89)[0-9]{9}$", ErrorMessage = "Некорректный формат номера телефона")]
        [Column("user_phoneNumber")]
        public required string PhoneNumber { get; set; }

    }
}
