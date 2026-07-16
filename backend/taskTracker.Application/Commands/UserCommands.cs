using taskTracker.Application.Common.Interfaces;
using taskTracker.Application.DTO;
using taskTracker.Domain.Entities;

namespace taskTracker.Application.Commands;

public class UserCommands : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public UserCommands(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator
  )
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task RegisterAsync(RegisterRequest request)
  {
    if (!await _userRepository.IsEmailUniqueAsync(request.Email))
    {
      throw new Exception("Email is already in use.");
    }

    var hashedPassword = _passwordHasher.HashPassword(request.Password);

    var user = User.Create(
      request.Email,
      request.UserName,
      hashedPassword
    );

    await _userRepository.AddUserAsync(user);
  }

  public async Task<AuthResponse> LoginAsync(LoginRequest request)
  {
    var user = await _userRepository.GetUserByEmailAsync(request.Email);

    if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
    {
      throw new Exception("Invalid email or password.");
    }

    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthResponse(
      user.Id,
      user.UserName,
      user.Email,
      token
    );
  }
}