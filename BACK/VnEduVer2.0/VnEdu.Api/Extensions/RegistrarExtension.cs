using VnEdu.Api.Registrars;

namespace VnEdu.Api.Extensions
{
    /// <summary>
    /// Information of RegistrarExtension
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public static class RegistrarExtension
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <param name="scanningType">Type</param>
        /// CreatedBy: MinhVN(20/12/2022)
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationBuilderRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }

        /// <summary>
        /// RegisterPipelineComponents
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="scanningType">Type</param>
        /// CreatedBy: MinhVN(20/12/2022)
        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterPipelineConponents(app);
            }
        }

        /// <summary>
        /// GetRegistrars
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="scanningType">Type</param>
        /// <returns>List T</returns>
        /// CreatedBy: MinhVN(20/12/2022)
        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistrar
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
