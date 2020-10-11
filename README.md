# ASP.NET Core Template

A ready-to-use template for ASP.NET Core with repositories, services, models mapping, DI and StyleCop warnings fixed.

## Build status

[![Build Status](https://nikolayit.visualstudio.com/AspNetCoreTemplate/_apis/build/status/NikolayIT.ASP.NET-Core-Template?branchName=master)](https://nikolayit.visualstudio.com/AspNetCoreTemplate/_build/latest?definitionId=15&branchName=master)

## Authors

- [Nikolay Kostov](https://github.com/NikolayIT)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)

## Package Installation

You can install this template using [NuGet](https://www.nuget.org/packages/AspNetCoreTemplate):

```powershell
dotnet new --install AspNetCoreTemplate
```

```powershell
dotnet new aspnet-core-template -n YourProjectName
``` 

## Overview

### Common

**AspNetCoreTemplate.Common** will contains common things for the project solution. For example: 
- [GlobalConstants.cs](https://github.com/NikolayIT/ASP.NET-Core-Template/blob/master/src/AspNetCoreTemplate.Common/GlobalConstants.cs). 

### Data
This solution folder contains three subfolders
- AspNetCoreTemplate.Data.Common
- AspNetCoreTemplate.Data.Models
- AspNetCoreTemplate.Data

##### AspNetCoreTemplate.Data.Common
[AspNetCoreTemplate.Data.Common.Models](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Data/AspNetCoreTemplate.Data.Common/Models) provides us abstract generics classes and interfaces, which holds information about our entities for example when the object is Created, Modified, Deleted or IsDeleted. It contains a property for the primary key as well.

[AspNetCoreTemplate.Data.Common.Repositories](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Data/AspNetCoreTemplate.Data.Common/Repositories) provides two interfaces IDeletableEntityRepository and IRepository, which are part of our **repository pattern**.

##### AspNetCoreTemplate.Data.Models
[AspNetCoreTemplate.Data.Models](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Data/AspNetCoreTemplate.Data.Models) contains ApplicationUser and ApplicationRole classes, which inherits IdentityRole and IdentityUsers.

##### AspNetCoreTemplate.Data
[AspNetCoreTemplate.Data](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Data/AspNetCoreTemplate.Data) contains our DbContext, Migrations, Configuraitons. It holds our Seeding and Repository functionality.

### Services
This solution folder contains four subfolders
- AspNetCoreTemplate.Services.Data
- AspNetCoreTemplate.Services.Mapping
- AspNetCoreTemplate.Services.Messaging
- AspNetCoreTemplate.Services

##### AspNetCoreTemplate.Services.Data
[AspNetCoreTemplate.Services.Data](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Services/AspNetCoreTemplate.Services.Data) wil contains our service layer. 

##### AspNetCoreTemplate.Services.Mapping
[AspNetCoreTemplate.Services.Mapping](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Services/AspNetCoreTemplate.Services.Mapping) provides simplified functionlity  for auto mapping. For example:

```csharp
using Blog.Data.Models;
using Blog.Services.Mapping;

public class TagViewModel : IMapFrom<Tag>
{
    public int Id { get; set; }

    public string Name { get; set; }
}
```

Or if you have something specific:

```csharp
using System;

using AutoMapper;
using Blog.Data.Models;
using Blog.Services.Mapping;

public class IndexPostViewModel : IMapFrom<Post>, IHaveCustomMappings
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string ImageUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Post, IndexPostViewModel>()
            .ForMember(
                source => source.Author,
                destination => destination.MapFrom(member => member.ApplicationUser.UserName));
    }
}

```

##### AspNetCoreTemplate.Services.Messaging

[AspNetCoreTemplate.Services.Messaging](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Services/AspNetCoreTemplate.Services.Messaging) a ready to use integration with [SendGrid](https://sendgrid.com/)

##### AspNetCoreTemplate.Services

[AspNetCoreTemplate.Services.Data](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Services/AspNetCoreTemplate.Services) 

### Services
This solution folder contains three subfolders
- AspNetCoreTemplate.Services.Data.Tests
- AspNetCoreTemplate.Web.Tests
- Sandbox

##### AspNetCoreTemplate.Services.Data.Tests

[AspNetCoreTemplate.Services.Data.Tests](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Tests/AspNetCoreTemplate.Services.Data.Tests) will hold unit tests for our service layer. It's already have installer XUnit.

##### AspNetCoreTemplate.Web.Tests

[AspNetCoreTemplate.Web.Tests](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Tests/AspNetCoreTemplate.Web.Tests) setted up Selenuim tests.

##### Sandbox

[Sandbox](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Tests/Sandbox)

### Web
This solution folder contains three subfolders
- AspNetCoreTemplate.Web.Infrastructure
- AspNetCoreTemplate.Web.ViewModels
- AspNetCoreTemplate.Web

##### AspNetCoreTemplate.Web.Infrastructure

[AspNetCoreTemplate.Web.Infrastructure](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Web/AspNetCoreTemplate.Web.Infrastructure) will contains functionality like Middlewares and Filters.

##### AspNetCoreTemplate.Web.ViewModels

[AspNetCoreTemplate.Web.ViewModels](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Web/AspNetCoreTemplate.Web.ViewModels) will contains objects, which will be mapped from our entities and used in the front-end.

##### AspNetCoreTemplate.Web

[AspNetCoreTemplate.Web](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master/src/Web/AspNetCoreTemplate.Web)

## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/NikolayIT/ASP.NET-Core-Template/issues).

## Contributors

- [Stoyan Shopov](https://github.com/StoyanShopov)

## Example Projects
- https://github.com/NikolayIT/PressCenters.com
- https://github.com/NikolayIT/nikolay.it

## License

This project is licensed with the [MIT license](LICENSE).
