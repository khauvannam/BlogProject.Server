using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route(("/[controller]"))]
[ApiController]
[Authorize(Policy = "Admin")]
public class TagController : ControllerBase { }
