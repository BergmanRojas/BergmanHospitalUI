using Microsoft.JSInterop;

namespace BergmanHospitalUI.Services;

public class TokenProvider
{
    private readonly IJSRuntime _js;
    private const string TokenKey = "authToken";

    public string? Token { get; private set; }

    public TokenProvider(IJSRuntime js)
    {
        _js = js;
    }

    public void SetToken(string token)
    {
        Token = token;
        _ = _js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    public void ClearToken()
    {
        Token = null;
        _ = _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
    }

    public async Task LoadTokenAsync()
    {
        Token = await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
    }
}
