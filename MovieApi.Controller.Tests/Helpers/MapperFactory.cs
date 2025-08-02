using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieData.Data.Configurations;

namespace MovieApi.Controller.Tests.Helpers;

public class MapperFactory
{
	public static IMapper Create()
	{
		var configurationExpression = new MapperConfigurationExpression();
		configurationExpression.AddProfile<MapperProfile>();

		var config = new MapperConfiguration(configurationExpression, new LoggerFactory());

		return config.CreateMapper();
	}
}
