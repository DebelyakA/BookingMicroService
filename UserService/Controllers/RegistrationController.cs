using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.MicrosoftExtensions;
using System.Linq;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly BookingContext _context;
        private readonly JwtProvider _jwtProvider;
        public RegistrationController(BookingContext context, JwtProvider jwtProvider)
        {
            _context = context;
            _jwtProvider = jwtProvider;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserEntity newUser, [FromQuery] Guid? id = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(await _context.User.AnyAsync(u => u.Email == newUser.Email || u.PhoneNumber == newUser.PhoneNumber))
            {
                return BadRequest("Пользователь с такой почтой или номером телефона уже зарегистрирован");
            }
            newUser.Id = Guid.NewGuid();
            newUser.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(newUser.PasswordHash);
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok("Пользователь зарегистрирован");

        }
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var User = await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == email);
            if (User == null)
            {
                return NotFound("Пользователь не найден");
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, User.PasswordHash))
            {
                return BadRequest("Неверный пароль");
            }
            var token = _jwtProvider.GenerateToken(User);
            HttpContext.Response.Cookies.Append("testcookie", token);
            return Ok(token);
        }
    }
}
