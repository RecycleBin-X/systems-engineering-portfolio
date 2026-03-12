public class RegisterDto
{
    public string UserId { get; set; }
    public string Email { get; set; }
    // password must including at least two of the following three: letters, digits, special characters
    public string UserPass { get; set; }
    public string Captcha { get; set; }

    public RegisterDto(string userId, string email, string userPass, string captcha)
    {
        UserId = userId;
        Email = email;
        UserPass = userPass;
        Captcha = captcha;
    }
}

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserEconomyService _userEconomyService;
    private readonly IValidationHelper _validationHelper;
    private readonly IHttpRequestService _httpRequestService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IPasswordResetService _passwordResetService;
    private readonly IEncryptionUtility _encryptionUtility;

    private readonly IMapper _mapper;

    public UserController(
        IUserService userService,
        IUserEconomyService userEconomyService,
        IValidationHelper validationHelper,
        IHttpRequestService httpRequestService,
        IEmailSenderService emailSenderService,
        IMapper mapper,
        IPasswordResetService passwordResetService,
        IEncryptionUtility encryptionUtility
    )
    {
        _userService = userService;
        _userEconomyService = userEconomyService;
        _validationHelper = validationHelper;
        _httpRequestService = httpRequestService;
        _emailSenderService = emailSenderService;
        _mapper = mapper;
        _passwordResetService = passwordResetService;
        _encryptionUtility = encryptionUtility;
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
    {
        registerDto.Email = ValidationUtility.NormalizeEmail(registerDto.Email);
        if (string.IsNullOrEmpty(registerDto.Email))
            return BadRequest(ServiceResult<string>.Error(Constants.ErrorInvalidUserForm));

        if (!_validationHelper.RegisterFormValidation(registerDto))
            return BadRequest(ServiceResult<string>.Error(Constants.ErrorInvalidUserForm));

        // ValidateCaptchaAsync;
        var userIp = Request.Headers["X-Real-IP"].FirstOrDefault();
        if (string.IsNullOrEmpty(userIp))
        {
            userIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        }

        var captchaResult = _httpRequestService.ValidateCaptchaAsync(registerDto.Captcha, userIp!).Result;

        if (!captchaResult)
            return BadRequest(ServiceResult<string>.Error(Constants.ErrorCaptchaFailed));

        var result = await _userService.RegisterUserAsync(registerDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}