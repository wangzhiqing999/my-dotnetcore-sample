using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace A0010_TestWebApiV9.Transformers
{

    /// <summary>
    /// Bearer 身份验证的示例转换器.
    /// </summary>
    /// <param name="authenticationSchemeProvider"></param>
    public sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
    {
        private readonly string _authenticationSchemeName = "Bearer";

        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
            if (authenticationSchemes.Any(authScheme => authScheme.Name == _authenticationSchemeName))
            {
                // Add the security scheme at the document level
                var requirements = new Dictionary<string, OpenApiSecurityScheme>
                {
                    [_authenticationSchemeName] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = _authenticationSchemeName.ToLower(), // "bearer" refers to the header name here
                        In = ParameterLocation.Header,
                        BearerFormat = "Json Web Token"
                    }
                };
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = requirements;

                // Apply it as a requirement for all operations
                foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
                {
                    operation.Value.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = _authenticationSchemeName,
                                Type = ReferenceType.SecurityScheme
                            }
                        }] = Array.Empty<string>()
                    });
                }
            }
        }
    }
}
