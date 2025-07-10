using Domain.OData;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Web.Tests;

public class GetTests
{
    public GetTests()
    {
        Initialize();
    }

    #region Fields
    private IServiceProvider serviceProvider;
    private IHttpClientFactory clientFactory;
    #endregion Fields

    private void Initialize()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddHttpClient();
        serviceProvider = services.BuildServiceProvider();

        clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantSearchAndFilterNoResult(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$search=One&$filter=Name eq 'Two'", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Empty(collection);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantSearchAndFilterExpand(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$search=One&$filter=CreatedDate gt 2012-11-11T00:00:00.00Z&$expand=Buildings", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantSearchExpand(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$search=One&$expand=Buildings", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantSearchNoExpand(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$search=One", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.False(collection.First()?.Buildings?.Any() == true);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsFilterEqAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$expand=Buildings&$filter=Name eq 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsFilterNeAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$expand=Buildings&$filter=Name ne 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(3, collection.First().Buildings.Count);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantFilterEqNoExpand(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$filter=Name eq 'One'", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.False(collection.First()?.Buildings?.Any() == true);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantFilterGtDateNoExpand(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$filter=CreatedDate gt 2012-11-11T00:00:00.00Z", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.Null(collection.First().Buildings);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantFilterLtDateNoExpand(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$filter=CreatedDate lt 2012-11-11T12:00:00.00Z", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Empty(collection);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsNoFilterAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$expand=Buildings&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.Equal(3, collection.First().Buildings.Count);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantNoExpandNoFilterAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.False(collection.First()?.Buildings?.Any() == true);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantNoExpandFilterEqAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$filter=Name eq 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.False(collection.First()?.Buildings?.Any() == true);
            Assert.Equal("One", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsSelectNameAndBuilderExpandBuilderExpandCityFilterNeAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$select=Name&$expand=Buildings($select=Name,Builder;$expand=Builder($select=Name,City;$expand=City))&$filter=Name ne 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(3, collection.First().Buildings.Count);
            Assert.NotNull(collection.First().Buildings.First().Builder);
            Assert.NotNull(collection.First().Buildings.First().Builder.City);
            Assert.NotEqual(default, collection.First().Buildings.First().Builder.City.Name);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsExpandBuilderExpandCityFilterNeAndOrderBy(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$expand=Buildings($expand=Builder($expand=City))&$filter=Name ne 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(3, collection.First().Buildings.Count);
            Assert.NotNull(collection.First().Buildings.First().Builder);
            Assert.NotNull(collection.First().Buildings.First().Builder.City);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantFilterEqAndOrderBy(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder,Tenant&$filter=name eq 'One L1'", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Single(collection);
            Assert.Equal("Sam", collection.First().Builder.Name);
            Assert.Equal("One", collection.First().Tenant.Name);
            Assert.Equal("One L1", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantFilterOnNestedPropertyAndOrderBy(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder,Tenant&$filter=Builder/Name eq 'Sam'&$orderby=Name asc", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.Equal("Sam", collection.First().Builder.Name);
            Assert.Equal("One", collection.First().Tenant.Name);
            Assert.Equal("One L1", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityFilterOnPropertyAndOrderBy(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder($expand=City),Tenant&$filter=Name ne 'One L2'&$orderby=Name desc", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(4, collection.Count);
            Assert.NotNull(collection.First().Builder.City);
            Assert.Equal("Two L3", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityFilterOnNestedNestedPropertyWithCount(string port)
    {
        string query = "/corebuilding?$top=5&$expand=Builder($expand=City),Tenant&$filter=Builder/City/Name eq 'Leeds'&$count=true";
        Test
        (
            await Get<CoreBuilding>
            (
                query,
                port
            )
        );

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.Equal("Leeds", collection.First().Builder.City.Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityOrderByName(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder($expand=City),Tenant&$orderby=Name desc", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(5, collection.Count);
            Assert.Equal("Leeds", collection.First().Builder.City.Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityOrderByNameThenByIdentity(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder($expand=City),Tenant&$orderby=Name desc,Identity", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(5, collection.Count);
            Assert.Equal("Leeds", collection.First().Builder.City.Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityOrderByBuilderName(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder($expand=City),Tenant&$orderby=Builder/Name", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(5, collection.Count);
            Assert.Equal("London", collection.First().Builder.City.Name);
            Assert.Equal("Two L1", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityOrderByBuilderNameSkip3Take1WithCount(string port)
    {
        string query = "/corebuilding?$skip=4&$top=1&$expand=Builder($expand=City),Tenant&$orderby=Name desc,Identity&$count=true";
        Test
        (
            await Get<CoreBuilding>
            (
                query,
                port
            )
        );

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Single(collection);
            Assert.Equal("London", collection.First().Builder.City.Name);
            Assert.Equal("One L1", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderTenantExpandCityOrderByBuilderNameSkip3Take1NoCount(string port)
    {
        string query = "/corebuilding?$skip=4&$top=1&$expand=Builder($expand=City),Tenant&$orderby=Name desc,Identity";
        Test
        (
            await Get<CoreBuilding>
            (
                query,
                port
            )
        );

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Single(collection);
            Assert.Equal("London", collection.First().Builder.City.Name);
            Assert.Equal("One L1", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingSelectName_WithoutOrder_WithoutTop(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$select=Name", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(5, collection.Count);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantOrderByCountOfReference(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$expand=Buildings&$orderby=Buildings/$count desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.NotNull(collection.First().Buildings);
            Assert.Equal("Two", collection.First().Name);
            Assert.Equal(3, collection.First().Buildings.Count);
            Assert.Equal(2, collection.Last().Buildings.Count);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantOrderByFilteredCount(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$expand=Buildings&$orderby=Buildings/$count($filter=Name eq 'One L1') desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.NotNull(collection.First().Buildings);
            Assert.Equal("One", collection.First().Name);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.Equal(3, collection.Last().Buildings.Count);
        }
    }

    [Theory]
    [InlineData("16324")]
    //EF 6 seems to have a problem with circular reference Building/Tenant/Buildings
    //[InlineData("16325")]
    public async ValueTask CoreBuildingOrderByCountOfChildReferenceOfReference(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$expand=Tenant($expand=Buildings)&$orderby=Tenant/Buildings/$count desc", port));
        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(5, collection.Count);
            Assert.NotNull(collection.First().Tenant.Buildings);
            Assert.Equal(3, collection.First().Tenant.Buildings.Count);
            Assert.Equal(2, collection.Last().Tenant.Buildings.Count);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask CoreBuildingOrderByPropertyOfChildReferenceOfReference(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$expand=Builder($expand=City)&$orderby=Builder/City/Name desc", port));
        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Equal(5, collection.Count);
            Assert.NotNull(collection.First().Builder.City);
            Assert.Equal("London", collection.First().Builder.City.Name);
            Assert.Equal("Leeds", collection.Last().Builder.City.Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantSelectNameExpandBuildings(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$select=Name&$expand=Buildings&$orderby=Name", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Equal(2, collection.Count);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.Equal("One", collection.First().Name);
            Assert.Equal(default, collection.First().Identity);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsFilterEqAndOrderBy_FirstBuildingHasValues(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$select=Buildings&$expand=Buildings&$filter=Name eq 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.NotNull(collection.First().Buildings.First().Name);
            Assert.NotEqual(default, collection.First().Buildings.First().Identity);
            Assert.Equal(default, collection.First().Identity);
            Assert.Null(collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingSelectNameExpandBuilder_BuilderNameShouldBeSam(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$select=Name&$expand=Builder($select=Name)&$filter=name eq 'One L1'", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Single(collection);
            Assert.Equal("Sam", collection.First().Builder.Name);
            Assert.Equal(default, collection.First().Builder.Id);
            Assert.Null(collection.First().Builder.City);
            Assert.Null(collection.First().Tenant);
            Assert.Equal("One L1", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderSelectNamefilterEqAndOrderBy(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder($select=Name)&$filter=name eq 'One L1'", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Single(collection);
            Assert.Equal("Sam", collection.First().Builder.Name);
            Assert.Equal(default, collection.First().Builder.Id);
            Assert.Null(collection.First().Builder.City);
            Assert.Equal("One L1", collection.First().Name);
            Assert.Null(collection.First().Tenant);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask BuildingExpandBuilderSelectNameExpandCityFilterEqAndOrderBy_CityShouldBeExpanded_BuilderNameShouldBeSam_BuilderIdShouldBeZero(string port)
    {
        Test(await Get<CoreBuilding>("/corebuilding?$top=5&$expand=Builder($select=Name;$expand=City)&$filter=name eq 'One L1'", port));

        void Test(ICollection<CoreBuilding> collection)
        {
            Assert.Single(collection);
            Assert.Equal("Sam", collection.First().Builder.Name);
            Assert.Equal(default, collection.First().Builder.Id);
            Assert.Equal("London", collection.First().Builder.City.Name);
            Assert.Equal(1, collection.First().Builder.City.Id);
            Assert.Equal("One L1", collection.First().Name);
            Assert.Null(collection.First().Tenant);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsSelectNameAndBuilderExpandBuilderExpandCityFilterNeAndOrderBy_filterAndSortChildCollection(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$select=Name&$expand=Buildings($filter=Name ne 'Two L1';$orderby=Name;$select=Name,Builder;$expand=Builder($select=Name,City;$expand=City))&$filter=Name ne 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(2, collection.First().Buildings.Count);
            Assert.Equal("Two L2", collection.First().Buildings.First().Name);
            Assert.NotNull(collection.First().Buildings.First().Builder);
            Assert.NotNull(collection.First().Buildings.First().Builder.City);
            Assert.NotEqual(default, collection.First().Buildings.First().Builder.City.Name);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    [Theory]
    [InlineData("16324")]
    public async ValueTask OpsTenantExpandBuildingsExpandBuilderExpandCityFilterNeAndOrderBy_filterAndSortChildCollection(string port)
    {
        Test(await Get<OpsTenant>("/opstenant?$top=5&$expand=Buildings($filter=Name ne '';$orderby=Name desc;$expand=Builder($expand=City))&$filter=Name ne 'One'&$orderby=Name desc", port));

        void Test(ICollection<OpsTenant> collection)
        {
            Assert.Single(collection);
            Assert.Equal(3, collection.First().Buildings.Count);
            Assert.Equal("Two L3", collection.First().Buildings.First().Name);
            Assert.NotNull(collection.First().Buildings.First().Builder);
            Assert.NotNull(collection.First().Buildings.First().Builder.City);
            Assert.Equal("Two", collection.First().Name);
        }
    }

    private async Task<ICollection<TModel>> Get<TModel>(string query, string port)
    {
        HttpResponseMessage result = await clientFactory.CreateClient().GetAsync
        (
            $"http://localhost:{port}{query}"
        );

        result.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<OData<TModel>>(await result.Content.ReadAsStringAsync()).Value;
    }
}

public class OData<T>
{
    public List<T> Value { get; set; }
}