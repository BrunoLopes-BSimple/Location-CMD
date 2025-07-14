using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests;

public class RepositoryTestBase
{
    protected readonly Mock<IMapper> _mapper;
    protected readonly LocationContext context;

    protected RepositoryTestBase()
    {
        _mapper = new Mock<IMapper>();
        var options = new DbContextOptionsBuilder<LocationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        context = new LocationContext(options);
    }
}
