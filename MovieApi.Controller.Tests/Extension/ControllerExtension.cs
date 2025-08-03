using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controller.Tests.Extension;

public static class ControllerExtension
{
	public static void SetupDefaultHttpContext(this ControllerBase controller)
	{
		var httpContext = new DefaultHttpContext();

		var controllerContext = new ControllerContext
		{
			HttpContext = httpContext
		};

		controller.ControllerContext = controllerContext;
	}
}
