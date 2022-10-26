using AutoMapper;
using FW.Web.Configurations.Mappings.CategoryProfiles;
using FW.Web.Configurations.Mappings.ChangesProductsProfiles;
using FW.Web.Configurations.Mappings.DishesProfiles;
using FW.Web.Configurations.Mappings.IngredientsProfiles;
using FW.Web.Configurations.Mappings.ProductsProfiles;
using FW.Web.Configurations.Mappings.RecipesProfiles;
using FW.Web.Configurations.Mappings.WarehousesProfiles;

namespace FW.Web.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(Configuration()));
        }
        private static MapperConfiguration Configuration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CategoryInfoQueryMappingsProfile>();
                cfg.AddProfile<CategoryCreateMappingsProfile>();
                cfg.AddProfile<CategoryUpdateMappingsProfile>();

                cfg.AddProfile<ChangesProductInfoQueryMappingsProfile>();

                cfg.AddProfile<DishInfoQueryMappingsProfile>();
                cfg.AddProfile<DishCreateMappingsProfile>();
                cfg.AddProfile<DishUpdateMappingsProfile>();

                cfg.AddProfile<IngredientInfoQueryMappingsProfile>();
                cfg.AddProfile<IngredientCreateMappingsProfile>();
                cfg.AddProfile<IngredientUpdateMappingsProfile>();

                cfg.AddProfile<ProductInfoQueryMappingsProfile>();
                cfg.AddProfile<ProductCreateMappingsProfile>();
                cfg.AddProfile<ProductUpdateMappingsProfile>();

                cfg.AddProfile<RecipeInfoQueryMappingsProfile>();
                cfg.AddProfile<RecipeCreateMappingsProfile>();
                cfg.AddProfile<RecipeUpdateMappingsProfile>();

                cfg.AddProfile<WarehouseInfoQueryMappingsProfile>();
                cfg.AddProfile<WarehouseCreateMappingsProfile>();
                cfg.AddProfile<WarehouseUpdateMappingsProfile>();
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}
