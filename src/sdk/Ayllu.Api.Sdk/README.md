# AYLLU API SDK

Sdk gerado automaticamente com dotnet kiota a partir da especificação openapi da API.

## Modos de uso 
 
    Criar serviços para consumir sdk. Os serviços devem injetar um objeto do tipo ``` IRequestAdapter ``` e uma string com a url base da requisição. Os metodos a serem implementados no serviço variam dentre o consumo dos endpoints disponíveis na api.

    Service de exemplo: 

    ```csharp
    
    public class IdentityService : IIdentityService
    {
        private readonly IRequestAdapter _requestAdapter;
        private readonly string _baseUrl;

        public IdentityService(string baseUrl)
        {
            _baseUrl = baseUrl;
            var authProvider = new AnonymousAuthenticationProvider(); 
            _requestAdapter = new HttpClientRequestAdapter(authProvider)
            {
                BaseUrl = _baseUrl
            };
        }

        public async Task<AccessTokenResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var builder = new LoginRequestBuilder(new Dictionary<string, object>
            {
                { "baseurl", _baseUrl }
            }, _requestAdapter);

            return await builder.PostAsync(loginRequest, cancellationToken: cancellationToken);
        }

        public async Task RegisterAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var registerRequest = new RegisterRequest
            {
                Email = email,
                Password = password
            }
            var builder = new RegisterRequestBuilder(new Dictionary<string, object>
            {
                { "baseurl", _baseUrl }
            }, _requestAdapter);

            return await builder.PostAsync(registerRequest, cancellationToken: cancellationToken);
        }
 
    }


    ```

    