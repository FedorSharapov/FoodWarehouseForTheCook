using AutoMapper;
using FW.BusinessLogic.Services.Mappings.CategoryProfiles;
using FW.BusinessLogic.Services.Mappings.ChangesProductsProfiles;
using FW.BusinessLogic.Services.Mappings.DishesProfiles;
using FW.BusinessLogic.Services.Mappings.IngredientsProfiles;
using FW.BusinessLogic.Services.Mappings.ProductsProfiles;
using FW.BusinessLogic.Services.Mappings.RecipesProfiles;
using FW.BusinessLogic.Services.Mappings.WarehousesProfiles;

namespace FW.Management.Configurations
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
                cfg.AddProfile<CategoryCreateMappingsProfile>();
                cfg.AddProfile<CategoryUpdateMappingsProfile>();
                cfg.AddProfile<CategoryInfoResponseMappingsProfile>();

                cfg.AddProfile<ChangesProductInfoResponseMappingsProfile>();

                cfg.AddProfile<DishCreateMappingsProfile>();
                cfg.AddProfile<DishUpdateMappingsProfile>();
                cfg.AddProfile<DishInfoResponseMappingsProfile>();

                cfg.AddProfile<IngredientCreateMappingsProfile>();
                cfg.AddProfile<IngredientUpdateMappingsProfile>();
                cfg.AddProfile<IngredientInfoResponseMappingsProfile>();

                cfg.AddProfile<ProductCreateMappingsProfile>();
                cfg.AddProfile<ProductUpdateMappingsProfile>();
                cfg.AddProfile<ProductInfoResponseMappingsProfile>();

                cfg.AddProfile<RecipeCreateMappingsProfile>();
                cfg.AddProfile<RecipeUpdateMappingsProfile>();
                cfg.AddProfile<RecipeInfoResponseMappingsProfile>();

                cfg.AddProfile<WarehouseCreateMappingsProfile>();
                cfg.AddProfile<WarehouseUpdateMappingsProfile>();
                cfg.AddProfile<WarehouseInfoResponseMappingsProfile>();
            });

            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}
