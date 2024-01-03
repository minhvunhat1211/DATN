namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of interface IWebApplicationBuilderRegistrar
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public interface IWebApplicationBuilderRegistrar : IRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">Builder</param>
        /// CreatedBy: MinhVN(20/12/2022)
        public void RegisterServices(WebApplicationBuilder builder);
    }
}
