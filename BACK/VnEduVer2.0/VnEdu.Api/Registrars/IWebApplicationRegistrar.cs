namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of interface IWebApplicationRegistrar
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public interface IWebApplicationRegistrar : IRegistrar
    {
        /// <summary>
        /// RegisterPipelineConponents
        /// </summary>
        /// <param name="app">App</param>
        /// CreatedBy: MinhVN(20/12/2022)
        public void RegisterPipelineConponents(WebApplication app);
    }
}
