namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of WebApplicationRegistrar
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class WebApplicationRegistrar : IWebApplicationRegistrar
    {
        /// <summary>
        /// RegisterPipelineConponents
        /// </summary>
        /// <param name="app">WebApplication</param>
        public void RegisterPipelineConponents(WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "VnEdu V1");
                config.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.MapControllers();
        }
    }
}
